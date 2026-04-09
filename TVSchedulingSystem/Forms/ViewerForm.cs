using System;
using System.Linq;
using System.Windows.Forms;
using TVSchedulingSystem.Services;
using TVSchedulingSystem.Models;

namespace TVSchedulingSystem.Forms
{
    public partial class ViewerForm : Form
    {
        private readonly ScheduleManager _manager;
        private System.Windows.Forms.Timer sessionTimer;
        private DateTime sessionStart;

        public ViewerForm()
        {
            InitializeComponent();

            _manager = new ScheduleManager();
            _manager.LoadFromDatabase();

            StartViewerSession();
            LoadSchedules();
        }

        private void StartViewerSession()
        {
            sessionStart = DateTime.Now;

            sessionTimer = new System.Windows.Forms.Timer();
            sessionTimer.Interval = 1000;
            sessionTimer.Tick += SessionTimer_Tick;
            sessionTimer.Start();
        }

        private void SessionTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - sessionStart;

            // Change back to 30 if you want the real requirement
            if (elapsed.TotalMinutes >= 0.3)
            {
                sessionTimer.Stop();
                MessageBox.Show("Viewer session expired after 30 minutes.");

                LoginForm loginForm = new LoginForm();
                loginForm.Show();

                Close();
            }
        }

        private void LoadSchedules()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.Columns.Add("ProgramName", "Program Name");
            dataGridView1.Columns.Add("ChannelName", "Channel");
            dataGridView1.Columns.Add("StartDisplay", "Starts At");
            dataGridView1.Columns.Add("EndDisplay", "Ends At");
            dataGridView1.Columns.Add("DurationDisplay", "Duration");

            for (int channelId = 1; channelId <= 3; channelId++)
            {
                Schedule[] schedules = _manager.GetSchedulesByChannel(channelId)
                                              .OrderBy(s => s.StartTime)
                                              .ToArray();

                for (int i = 0; i < schedules.Length; i++)
                {
                    Schedule schedule = schedules[i];

                    string programName = string.IsNullOrWhiteSpace(schedule.ProgramID)
                        ? "Unknown Program"
                        : schedule.ProgramID;

                    string channelName = "Channel " + schedule.ChannelID;

                    string startDisplay = schedule.StartTime.ToString("dd/MM/yyyy HH:mm");
                    string endDisplay = schedule.EndTime.ToString("dd/MM/yyyy HH:mm");

                    TimeSpan duration = schedule.EndTime - schedule.StartTime;
                    string durationDisplay = ((int)duration.TotalMinutes) + " mins";

                    dataGridView1.Rows.Add(
                        programName,
                        channelName,
                        startDisplay,
                        endDisplay,
                        durationDisplay
                    );
                }
            }

            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns["ProgramName"].FillWeight = 150;
                dataGridView1.Columns["ChannelName"].FillWeight = 70;
                dataGridView1.Columns["StartDisplay"].FillWeight = 110;
                dataGridView1.Columns["EndDisplay"].FillWeight = 110;
                dataGridView1.Columns["DurationDisplay"].FillWeight = 70;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (sessionTimer != null)
            {
                sessionTimer.Stop();
                sessionTimer.Dispose();
            }

            base.OnFormClosing(e);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();

            Close();
        }
    }
}