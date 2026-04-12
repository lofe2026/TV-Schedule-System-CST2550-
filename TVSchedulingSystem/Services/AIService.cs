using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TVSchedulingSystem.Models;

namespace TVSchedulingSystem.Services
{
    public class AIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        private const string Model = "gemini-2.5-flash";
        private static readonly string Endpoint =
            $"https://generativelanguage.googleapis.com/v1beta/models/{Model}:generateContent";

        public AIService()
        {
            // Replace with your NEW Gemini API key
            _apiKey = "AIzaSyDYku8jGt9Ja3ZmWceNsXGKA0dlJYfTEYM";

            if (string.IsNullOrWhiteSpace(_apiKey))
            {
                throw new InvalidOperationException("Gemini API key is missing.");
            }

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("x-goog-api-key", _apiKey);
        }

        public async Task<string> SendChatAsync(
            List<ChatTurn> conversation,
            string userMessage,
            Schedule[] schedules,
            int selectedChannel,
            DateTime selectedStart,
            int selectedDuration)
        {
            string cleanedMessage = (userMessage ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(cleanedMessage))
            {
                return "Please type a message.";
            }

            if (IsGreeting(cleanedMessage))
            {
                return "Hello! How can I help you with schedule conflicts, better time slots, or channel suggestions?";
            }

            if (IsHelpRequest(cleanedMessage))
            {
                return "You can ask me to check conflicts, suggest the next available future slot, recommend another channel, or explain the selected schedule.";
            }

            DateTime now = GetNextValidStartTime();

            DateTime effectiveSelectedStart = NormalizeToMinute(selectedStart);
            if (effectiveSelectedStart < now)
            {
                effectiveSelectedStart = now;
            }

            Schedule[] safeSchedules = (schedules ?? Array.Empty<Schedule>())
                .OrderBy(s => s.ChannelID)
                .ThenBy(s => s.StartTime)
                .ToArray();

            string scheduleContext = BuildScheduleContext(
                safeSchedules,
                selectedChannel,
                effectiveSelectedStart,
                selectedDuration,
                now
            );

            var contents = new List<object>();

            if (conversation != null)
            {
                foreach (ChatTurn turn in conversation)
                {
                    string role = NormalizeRole(turn.Role);

                    contents.Add(new
                    {
                        role,
                        parts = new[]
                        {
                            new { text = turn.Content ?? string.Empty }
                        }
                    });
                }
            }

            contents.Add(new
            {
                role = "user",
                parts = new[]
                {
                    new
                    {
                        text =
                            "Use the schedule context below only when relevant to the user's message." +
                            Environment.NewLine + Environment.NewLine +
                            scheduleContext +
                            Environment.NewLine +
                            "USER MESSAGE:" + Environment.NewLine +
                            cleanedMessage
                    }
                }
            });

            var requestBody = new
            {
                system_instruction = new
                {
                    parts = new[]
                    {
                        new
                        {
                            text =
                                "You are an AI assistant inside a TV Program Scheduling System desktop application. " +
                                "If the user sends a greeting, respond naturally and briefly. " +
                                "If the user asks about schedules, conflicts, time slots, or channels, use the provided schedule context. " +
                                "Never suggest a time earlier than the CURRENT TIME in the context. " +
                                "If the user's selected time is already in the past, treat the current time as the earliest valid time. " +
                                "When suggesting time slots, only suggest future valid slots that do not overlap existing schedules on the same channel. " +
                                "Ignore schedules that already ended before the CURRENT TIME when deciding future suggestions. " +
                                "Do not invent schedule data. " +
                                "Be clear, practical, short, and useful. " +
                                "When possible, answer with an exact date and time in dd/MM/yyyy HH:mm format."
                        }
                    }
                },
                contents = contents,
                generationConfig = new
                {
                    temperature = 0.2,
                    maxOutputTokens = 500
                }
            };

            string json = JsonSerializer.Serialize(requestBody);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");

            using HttpResponseMessage response = await _httpClient.PostAsync(Endpoint, content);
            string responseJson = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw CreateGeminiException(responseJson, response.StatusCode);
            }

            return ExtractAssistantText(responseJson, selectedChannel, effectiveSelectedStart, selectedDuration, now, safeSchedules);
        }

        private string NormalizeRole(string role)
        {
            string value = (role ?? string.Empty).Trim().ToLower();

            if (value == "assistant" || value == "model" || value == "ai")
                return "model";

            return "user";
        }

        private bool IsGreeting(string message)
        {
            string lower = message.Trim().ToLower();

            return lower == "hi" ||
                   lower == "hello" ||
                   lower == "hey" ||
                   lower == "good morning" ||
                   lower == "good afternoon" ||
                   lower == "good evening";
        }

        private bool IsHelpRequest(string message)
        {
            string lower = message.Trim().ToLower();

            return lower == "help" ||
                   lower == "what can you do" ||
                   lower == "what do you do" ||
                   lower == "how can you help";
        }

        private Exception CreateGeminiException(string responseJson, System.Net.HttpStatusCode statusCode)
        {
            try
            {
                using JsonDocument doc = JsonDocument.Parse(responseJson);

                if (doc.RootElement.TryGetProperty("error", out JsonElement error))
                {
                    string message = error.TryGetProperty("message", out JsonElement msg)
                        ? msg.GetString() ?? "Unknown Gemini API error."
                        : "Unknown Gemini API error.";

                    if (statusCode == System.Net.HttpStatusCode.Unauthorized ||
                        statusCode == System.Net.HttpStatusCode.Forbidden)
                    {
                        return new InvalidOperationException(
                            "Gemini authentication failed. Check your API key. Details: " + message
                        );
                    }

                    if ((int)statusCode == 429)
                    {
                        return new InvalidOperationException(
                            "Gemini free-tier quota or rate limit reached. Details: " + message
                        );
                    }

                    return new InvalidOperationException("Gemini API error: " + message);
                }
            }
            catch
            {
            }

            return new InvalidOperationException("Gemini API error: " + responseJson);
        }

        private string ExtractAssistantText(
            string responseJson,
            int selectedChannel,
            DateTime effectiveSelectedStart,
            int selectedDuration,
            DateTime now,
            Schedule[] schedules)
        {
            using JsonDocument doc = JsonDocument.Parse(responseJson);

            if (doc.RootElement.TryGetProperty("candidates", out JsonElement candidates) &&
                candidates.ValueKind == JsonValueKind.Array &&
                candidates.GetArrayLength() > 0)
            {
                JsonElement firstCandidate = candidates[0];

                if (firstCandidate.TryGetProperty("content", out JsonElement content) &&
                    content.TryGetProperty("parts", out JsonElement parts) &&
                    parts.ValueKind == JsonValueKind.Array)
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (JsonElement part in parts.EnumerateArray())
                    {
                        if (part.TryGetProperty("text", out JsonElement textElement))
                        {
                            string text = textElement.GetString();
                            if (!string.IsNullOrWhiteSpace(text))
                            {
                                sb.Append(text);
                            }
                        }
                    }

                    string result = sb.ToString().Trim();

                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        DateTime safeSuggestion = FindNextAvailableSlot(
                            schedules,
                            selectedChannel,
                            effectiveSelectedStart,
                            selectedDuration,
                            now
                        );

                        string safeSuggestionText = safeSuggestion.ToString("dd/MM/yyyy HH:mm");

                        if (LooksLikeTimeSuggestionRequest(result))
                        {
                            return EnsureFutureSuggestion(result, safeSuggestionText, safeSuggestion, now, selectedChannel);
                        }

                        return result;
                    }
                }
            }

            return "No assistant response was returned.";
        }

        private string BuildScheduleContext(
            Schedule[] schedules,
            int selectedChannel,
            DateTime selectedStart,
            int selectedDuration,
            DateTime currentTime)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("CURRENT TIME");
            sb.AppendLine(currentTime.ToString("dd/MM/yyyy HH:mm"));
            sb.AppendLine();

            sb.AppendLine("CURRENT USER SELECTION");
            sb.AppendLine($"Selected channel: {selectedChannel}");
            sb.AppendLine($"Requested start: {selectedStart:dd/MM/yyyy HH:mm}");
            sb.AppendLine($"Requested duration: {selectedDuration} minutes");
            sb.AppendLine();

            sb.AppendLine("UPCOMING AND EXISTING SCHEDULES");
            if (schedules == null || schedules.Length == 0)
            {
                sb.AppendLine("No schedules available.");
            }
            else
            {
                foreach (Schedule s in schedules)
                {
                    string status = s.EndTime <= currentTime ? "PAST" : "ACTIVE_OR_FUTURE";

                    sb.AppendLine(
                        $"ScheduleID={s.ScheduleID}, Channel={s.ChannelID}, Program={s.ProgramID}, " +
                        $"Start={s.StartTime:dd/MM/yyyy HH:mm}, End={s.EndTime:dd/MM/yyyy HH:mm}, Status={status}"
                    );
                }
            }

            sb.AppendLine();
            sb.AppendLine("STRICT RULES");
            sb.AppendLine("- Never suggest any time earlier than CURRENT TIME.");
            sb.AppendLine("- If Requested start is earlier than CURRENT TIME, use CURRENT TIME as the earliest valid start.");
            sb.AppendLine("- For time suggestions, ignore schedules whose End time is earlier than or equal to CURRENT TIME.");
            sb.AppendLine("- Explain conflicts simply.");
            sb.AppendLine("- Suggest a better future time or another channel when possible.");
            sb.AppendLine("- If no conflict exists, say the slot looks valid.");
            sb.AppendLine("- Keep the answer short and useful.");
            sb.AppendLine("- If the message is just a greeting, respond naturally.");
            sb.AppendLine("- Use exact date and time in dd/MM/yyyy HH:mm format.");

            return sb.ToString();
        }

        private bool LooksLikeTimeSuggestionRequest(string text)
        {
            string lower = (text ?? string.Empty).ToLower();

            return lower.Contains("slot") ||
                   lower.Contains("schedule") ||
                   lower.Contains("time") ||
                   lower.Contains("available") ||
                   lower.Contains("next") ||
                   lower.Contains("suggest");
        }

        private string EnsureFutureSuggestion(string aiText, string safeSuggestionText, DateTime safeSuggestion, DateTime now, int channelId)
        {
            if (safeSuggestion < now)
            {
                safeSuggestion = now;
                safeSuggestionText = now.ToString("dd/MM/yyyy HH:mm");
            }

            string lower = (aiText ?? string.Empty).ToLower();

            if (lower.Contains("next available") ||
                lower.Contains("suggested") ||
                lower.Contains("better time") ||
                lower.Contains("available slot") ||
                lower.Contains("next slot"))
            {
                return "Suggested next available slot on channel " + channelId + ": " + safeSuggestionText;
            }

            return aiText;
        }

        private DateTime FindNextAvailableSlot(
            Schedule[] schedules,
            int channelId,
            DateTime requestedStart,
            int durationMinutes,
            DateTime now)
        {
            DateTime candidate = requestedStart < now ? now : requestedStart;
            candidate = NormalizeToMinute(candidate);

            Schedule[] channelSchedules = (schedules ?? Array.Empty<Schedule>())
                .Where(s => s.ChannelID == channelId && s.EndTime > now)
                .OrderBy(s => s.StartTime)
                .ToArray();

            foreach (Schedule schedule in channelSchedules)
            {
                if (candidate.AddMinutes(durationMinutes) <= schedule.StartTime)
                {
                    return candidate;
                }

                if (candidate < schedule.EndTime)
                {
                    candidate = NormalizeToMinute(schedule.EndTime);
                }
            }

            return candidate;
        }

        private DateTime NormalizeToMinute(DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, 0);
        }

        private DateTime GetNextValidStartTime()
        {
            DateTime now = DateTime.Now;

            if (now.Second > 0 || now.Millisecond > 0)
            {
                now = now.AddMinutes(1);
            }

            return new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
        }
    }

    public class ChatTurn
    {
        public string Role { get; set; } = "";
        public string Content { get; set; } = "";
    }
}