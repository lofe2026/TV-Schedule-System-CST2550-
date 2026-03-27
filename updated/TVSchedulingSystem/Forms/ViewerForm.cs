using System;
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

            if (elapsed.TotalMinutes >= 30)
            {
                sessionTimer.Stop();
                MessageBox.Show("Viewer session expired after 30 minutes.");

                LoginForm loginForm = new LoginForm();
                loginForm.Show();

                this.Close();
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

            dataGridView1.Columns.Add("ScheduleID", "Schedule ID");
            dataGridView1.Columns.Add("ChannelID", "Channel");
            dataGridView1.Columns.Add("ProgramID", "Program");
            dataGridView1.Columns.Add("StartTime", "Start Time");
            dataGridView1.Columns.Add("EndTime", "End Time");

            for (int channelId = 1; channelId <= 3; channelId++)
            {
                Schedule[] schedules = _manager.GetSchedulesByChannel(channelId);

                for (int i = 0; i < schedules.Length; i++)
                {
                    dataGridView1.Rows.Add(
                        schedules[i].ScheduleID,
                        schedules[i].ChannelID,
                        schedules[i].ProgramID,
                        schedules[i].StartTime,
                        schedules[i].EndTime
                    );
                }
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
    }
}