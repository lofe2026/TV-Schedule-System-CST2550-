using System;
using System.Drawing;
using System.IO;
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

            this.Load += ClientForm_Load;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;

            StartClock();
        }

        // -------------------------------------
        // START CLOCK
        // -------------------------------------
        private void StartClock()
        {
            clockTimer = new System.Windows.Forms.Timer();
            clockTimer.Interval = 1000; // 1 second
            clockTimer.Tick += ClockTimer_Tick;
            clockTimer.Start();

            UpdateClock(); // show time immediately
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
        }

        private void cmbChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSchedules();
        }

        // -------------------------------------
        // LOAD SCHEDULES INTO GRID (WITH IMAGE)
        // -------------------------------------
        private void LoadSchedules()
        {
            if (cmbChannel.SelectedItem == null)
                return;

            int channelId = Convert.ToInt32(cmbChannel.SelectedItem);

            Schedule[] schedules = _manager.GetSchedulesByChannel(channelId);

            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.Columns.Add("ScheduleID", "ID");
            dataGridView1.Columns.Add("ChannelID", "Channel");
            dataGridView1.Columns.Add("ProgramID", "Program");
            dataGridView1.Columns.Add("StartTime", "Start");
            dataGridView1.Columns.Add("EndTime", "End");

            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            imgCol.Name = "Preview";
            imgCol.HeaderText = "Preview";
            imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
            dataGridView1.Columns.Add(imgCol);

            dataGridView1.Columns.Add("ImagePath", "ImagePath");
            dataGridView1.Columns["ImagePath"].Visible = false;

            for (int i = 0; i < schedules.Length; i++)
            {
                Image img = null;
                string path = schedules[i].ImagePath;

                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    try
                    {
                        using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                        using (var temp = Image.FromStream(stream))
                        {
                            img = new Bitmap(temp);
                        }
                    }
                    catch
                    {
                        img = null;
                    }
                }

                dataGridView1.Rows.Add(
                    schedules[i].ScheduleID,
                    schedules[i].ChannelID,
                    schedules[i].ProgramID,
                    schedules[i].StartTime,
                    schedules[i].EndTime,
                    img,
                    schedules[i].ImagePath
                );
            }

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows[0].Selected = true;
            }
        }

        // -------------------------------------
        // SHOW IMAGE PREVIEW (RIGHT SIDE)
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
                    using (var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    using (var temp = Image.FromStream(stream))
                    {
                        picturePreview.Image = new Bitmap(temp);
                    }
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void lblClock_Click(object sender, EventArgs e)
        {
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (clockTimer != null)
            {
                clockTimer.Stop();
                clockTimer.Dispose();
            }

            base.OnFormClosing(e);
        }
    }
}