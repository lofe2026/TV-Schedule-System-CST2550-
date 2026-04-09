using System;
using System.Collections.Generic;
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
            _apiKey = "AIzaSyCbBC7aC5_9RloNoja1IdE8pZK-0aew5zU";

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
                return "You can ask me to check conflicts, suggest the next available slot, recommend another channel, or explain the selected schedule.";
            }

            string scheduleContext = BuildScheduleContext(
                schedules,
                selectedChannel,
                selectedStart,
                selectedDuration
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
                                "Do not invent schedule data. " +
                                "Be clear, practical, and concise."
                        }
                    }
                },
                contents = contents,
                generationConfig = new
                {
                    temperature = 0.4,
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

            return ExtractAssistantText(responseJson);
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

        private string ExtractAssistantText(string responseJson)
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
            int selectedDuration)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("CURRENT USER SELECTION");
            sb.AppendLine($"Selected channel: {selectedChannel}");
            sb.AppendLine($"Requested start: {selectedStart:dd/MM/yyyy HH:mm}");
            sb.AppendLine($"Requested duration: {selectedDuration} minutes");
            sb.AppendLine();

            sb.AppendLine("EXISTING SCHEDULES");
            if (schedules == null || schedules.Length == 0)
            {
                sb.AppendLine("No schedules available.");
            }
            else
            {
                foreach (Schedule s in schedules)
                {
                    sb.AppendLine(
                        $"ScheduleID={s.ScheduleID}, Channel={s.ChannelID}, Program={s.ProgramID}, " +
                        $"Start={s.StartTime:dd/MM/yyyy HH:mm}, End={s.EndTime:dd/MM/yyyy HH:mm}"
                    );
                }
            }

            sb.AppendLine();
            sb.AppendLine("RULES");
            sb.AppendLine("- Explain schedule conflicts simply.");
            sb.AppendLine("- Suggest a better time or channel when possible.");
            sb.AppendLine("- If no conflict exists, say the slot looks valid.");
            sb.AppendLine("- Keep the answer short and useful.");
            sb.AppendLine("- If the message is just a greeting, respond naturally.");

            return sb.ToString();
        }
    }

    public class ChatTurn
    {
        public string Role { get; set; } = "";
        public string Content { get; set; } = "";
    }
}