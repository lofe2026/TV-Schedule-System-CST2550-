using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TVSchedulingSystem.Services;
using TVSchedulingSystem.Models;

namespace TVSchedulingSystem.Forms
{
    public partial class ClientForm : Form
    {
        private ScheduleManager _manager;
        private System.Windows.Forms.Timer clockTimer;

        public ClientForm()
        {
            InitializeComponent();

            _manager = new ScheduleManager();
            _manager.LoadFromDatabase();

            this.Load += ClientForm_Load;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;

            StartClock();
        }

        // -------------------------------------
        // CLOCK
        // -------------------------------------
        private void StartClock()
        {
            clockTimer = new System.Windows.Forms.Timer();
            clockTimer.Interval = 1000;
            clockTimer.Tick += ClockTimer_Tick;
            clockTimer.Start();

            UpdateClock();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            UpdateClock();
        }

        private void UpdateClock()
        {
            lblClock.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        // -------------------------------------
        // LOAD FORM
        // -------------------------------------
        private void ClientForm_Load(object sender, EventArgs e)
        {
            cmbChannel.Items.Clear();
            cmbChannel.Items.Add(1);
            cmbChannel.Items.Add(2);
            cmbChannel.Items.Add(3);

            cmbChannel.SelectedIndex = 0;

            LoadSchedules();
        }

        private void cmbChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSchedules();
        }

        // -------------------------------------
        // LOAD SCHEDULES (USER FRIENDLY)
        // -------------------------------------
        private void LoadSchedules()
        {
            if (cmbChannel.SelectedItem == null)
                return;

            int channelId = Convert.ToInt32(cmbChannel.SelectedItem);

            Schedule[] schedules = _manager.GetSchedulesByChannel(channelId)
                                           .OrderBy(s => s.StartTime)
                                           .ToArray();

            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // USER FRIENDLY COLUMNS
            dataGridView1.Columns.Add("ProgramName", "Program Name");
            dataGridView1.Columns.Add("ChannelName", "Channel");
            dataGridView1.Columns.Add("StartDisplay", "Starts At");
            dataGridView1.Columns.Add("EndDisplay", "Ends At");
            dataGridView1.Columns.Add("DurationDisplay", "Duration");

            // IMAGE COLUMN
            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            imgCol.Name = "Preview";
            imgCol.HeaderText = "Preview";
            imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
            dataGridView1.Columns.Add(imgCol);

            // hidden image path
            dataGridView1.Columns.Add("ImagePath", "ImagePath");
            dataGridView1.Columns["ImagePath"].Visible = false;

            for (int i = 0; i < schedules.Length; i++)
            {
                Schedule s = schedules[i];

                string programName = string.IsNullOrWhiteSpace(s.ProgramID)
                    ? "Unknown Program"
                    : s.ProgramID;

                string channelName = "Channel " + s.ChannelID;

                string startDisplay = s.StartTime.ToString("dd/MM/yyyy HH:mm");
                string endDisplay = s.EndTime.ToString("dd/MM/yyyy HH:mm");

                TimeSpan duration = s.EndTime - s.StartTime;
                string durationDisplay = ((int)duration.TotalMinutes) + " mins";

                Image img = LoadImageSafe(s.ImagePath);

                dataGridView1.Rows.Add(
                    programName,
                    channelName,
                    startDisplay,
                    endDisplay,
                    durationDisplay,
                    img,
                    s.ImagePath
                );
            }

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows[0].Selected = true;
            }
        }

        // -------------------------------------
        // IMAGE PREVIEW
        // -------------------------------------
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
                return;

            try
            {
                string imagePath = dataGridView1.CurrentRow.Cells["ImagePath"].Value?.ToString();

                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    picturePreview.Image = LoadImageSafe(imagePath);
                }
                else
                {
                    picturePreview.Image = null;
                }
            }
            catch
            {
                picturePreview.Image = null;
            }
        }

        private Image LoadImageSafe(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
                return null;

            try
            {
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (var temp = Image.FromStream(stream))
                {
                    return new Bitmap(temp);
                }
            }
            catch
            {
                return null;
            }
        }

        // -------------------------------------
        // CLEANUP
        // -------------------------------------
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (clockTimer != null)
            {
                clockTimer.Stop();
                clockTimer.Dispose();
            }

            base.OnFormClosing(e);
        }

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Close();
        }
    }
}