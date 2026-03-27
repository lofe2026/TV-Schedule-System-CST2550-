using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TVSchedulingSystem.Models;
using TVSchedulingSystem.Services;

namespace TVSchedulingSystem.Forms
{
    public partial class ManagerForm : Form
    {
        private readonly ScheduleManager _manager;
        private System.Windows.Forms.Timer _clockTimer;
        private string _selectedImagePath = string.Empty;

        public ManagerForm()
        {
            InitializeComponent();

            _manager = new ScheduleManager();

            btnAdd.Click += btnAdd_Click;
            btnSuggest.Click += btnSuggest_Click;
            btnSelectImage.Click += btnSelectImage_Click;
            cmbChannel.SelectedIndexChanged += cmbChannel_SelectedIndexChanged;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
        }

        private void ManagerForm_Load(object sender, EventArgs e)
        {
            SetupForm();
            StartClock();
            LoadChannels();
            LoadSchedules();
        }

        // =========================
        // FORM SETUP
        // =========================
        private void SetupForm()
        {
            dtpStartTime.Format = DateTimePickerFormat.Custom;
            dtpStartTime.CustomFormat = "dd/MM/yyyy HH:mm";

            numDuration.Minimum = 1;
            numDuration.Maximum = 600;
            numDuration.Value = 30;

            cmbChannel.DropDownStyle = ComboBoxStyle.DropDownList;

            lblProgramTitle.Text = "Program Preview";
            lblClock.Text = "00:00:00";

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
        }

        // =========================
        // CLOCK
        // =========================
        private void StartClock()
        {
            _clockTimer = new System.Windows.Forms.Timer();
            _clockTimer.Interval = 1000;
            _clockTimer.Tick += ClockTimer_Tick;
            _clockTimer.Start();
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

        // =========================
        // CHANNELS
        // =========================
        private void LoadChannels()
        {
            cmbChannel.Items.Clear();

            cmbChannel.Items.Add(1);
            cmbChannel.Items.Add(2);
            cmbChannel.Items.Add(3);

            if (cmbChannel.Items.Count > 0)
            {
                cmbChannel.SelectedIndex = 0;
            }
        }

        private void cmbChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSchedules();
        }

        // =========================
        // GRID LOAD
        // =========================
        private void LoadSchedules()
        {
            if (cmbChannel.SelectedItem == null)
                return;

            int channelId = Convert.ToInt32(cmbChannel.SelectedItem);
            Schedule[] schedules = _manager.GetSchedulesByChannel(channelId);

            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            dataGridView1.Columns.Add("ScheduleID", "Schedule ID");
            dataGridView1.Columns.Add("ChannelID", "Channel");
            dataGridView1.Columns.Add("ProgramID", "Program ID");
            dataGridView1.Columns.Add("StartTime", "Start Time");
            dataGridView1.Columns.Add("EndTime", "End Time");

            DataGridViewImageColumn previewColumn = new DataGridViewImageColumn();
            previewColumn.Name = "Preview";
            previewColumn.HeaderText = "Preview";
            previewColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            dataGridView1.Columns.Add(previewColumn);

            dataGridView1.Columns.Add("ImagePath", "ImagePath");
            dataGridView1.Columns["ImagePath"].Visible = false;

            for (int i = 0; i < schedules.Length; i++)
            {
                Image image = LoadImageSafe(schedules[i].ImagePath);

                dataGridView1.Rows.Add(
                    schedules[i].ScheduleID,
                    schedules[i].ChannelID,
                    schedules[i].ProgramID,
                    schedules[i].StartTime.ToString("dd/MM/yyyy HH:mm"),
                    schedules[i].EndTime.ToString("dd/MM/yyyy HH:mm"),
                    image,
                    schedules[i].ImagePath
                );
            }

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows[0].Selected = true;
                UpdatePreviewFromSelectedRow();
            }
            else
            {
                ClearPreview();
            }
        }

        // =========================
        // ADD SCHEDULE
        // =========================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbChannel.SelectedItem == null)
            {
                MessageBox.Show("Please select a channel.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtProgramId.Text))
            {
                MessageBox.Show("Please enter a Program ID.");
                return;
            }

            int channelId = Convert.ToInt32(cmbChannel.SelectedItem);
            string programId = txtProgramId.Text.Trim();
            DateTime startTime = dtpStartTime.Value;
            int durationMinutes = Convert.ToInt32(numDuration.Value);

            int scheduleId = GetNextScheduleId();

            bool added;
            try
            {
                added = _manager.AddSchedule(
                    scheduleId,
                    channelId,
                    programId,
                    startTime,
                    durationMinutes,
                    _selectedImagePath
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return;
            }

            if (!added)
            {
                MessageBox.Show("Schedule could not be added. The slot may already exist or conflict with another schedule.");
                return;
            }

            MessageBox.Show("Schedule added successfully.");
            ClearInputsAfterAdd();
            LoadSchedules();
        }

        // =========================
        // SUGGEST SLOT
        // =========================
        private void btnSuggest_Click(object sender, EventArgs e)
        {
            if (cmbChannel.SelectedItem == null)
            {
                MessageBox.Show("Please select a channel first.");
                return;
            }

            int channelId = Convert.ToInt32(cmbChannel.SelectedItem);
            Schedule[] schedules = _manager.GetSchedulesByChannel(channelId);

            DateTime suggestedTime;

            if (schedules.Length == 0)
            {
                suggestedTime = new DateTime(
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    9,
                    0,
                    0
                );
            }
            else
            {
                suggestedTime = schedules[0].EndTime;

                for (int i = 1; i < schedules.Length; i++)
                {
                    if (schedules[i].EndTime > suggestedTime)
                    {
                        suggestedTime = schedules[i].EndTime;
                    }
                }
            }

            dtpStartTime.Value = suggestedTime;
            MessageBox.Show("Suggested next available slot: " + suggestedTime.ToString("dd/MM/yyyy HH:mm"));
        }

        // =========================
        // SELECT IMAGE
        // =========================
        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Select Preview Image";
                dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _selectedImagePath = dialog.FileName;

                    SetPicturePreview(_selectedImagePath);

                    if (!string.IsNullOrWhiteSpace(txtProgramId.Text))
                    {
                        lblProgramTitle.Text = txtProgramId.Text.Trim();
                    }
                    else
                    {
                        lblProgramTitle.Text = "Program Preview";
                    }
                }
            }
        }

        // =========================
        // GRID SELECTION PREVIEW
        // =========================
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            UpdatePreviewFromSelectedRow();
        }

        private void UpdatePreviewFromSelectedRow()
        {
            if (dataGridView1.CurrentRow == null)
                return;

            string programId = Convert.ToString(dataGridView1.CurrentRow.Cells["ProgramID"].Value);
            string imagePath = Convert.ToString(dataGridView1.CurrentRow.Cells["ImagePath"].Value);

            if (!string.IsNullOrWhiteSpace(programId))
            {
                lblProgramTitle.Text = programId;
            }
            else
            {
                lblProgramTitle.Text = "Program Preview";
            }

            SetPicturePreview(imagePath);
        }

        // =========================
        // HELPERS
        // =========================
        private int GetNextScheduleId()
        {
            int maxId = 0;

            for (int channelId = 1; channelId <= 3; channelId++)
            {
                Schedule[] schedules = _manager.GetSchedulesByChannel(channelId);

                for (int i = 0; i < schedules.Length; i++)
                {
                    if (schedules[i].ScheduleID > maxId)
                    {
                        maxId = schedules[i].ScheduleID;
                    }
                }
            }

            return maxId + 1;
        }

        private void ClearInputsAfterAdd()
        {
            txtProgramId.Clear();
            numDuration.Value = 30;
            _selectedImagePath = string.Empty;
            lblProgramTitle.Text = "Program Preview";

            if (picturePreview.Image != null)
            {
                Image oldImage = picturePreview.Image;
                picturePreview.Image = null;
                oldImage.Dispose();
            }
        }

        private void ClearPreview()
        {
            lblProgramTitle.Text = "Program Preview";

            if (picturePreview.Image != null)
            {
                Image oldImage = picturePreview.Image;
                picturePreview.Image = null;
                oldImage.Dispose();
            }
        }

        private void SetPicturePreview(string imagePath)
        {
            if (picturePreview.Image != null)
            {
                Image oldImage = picturePreview.Image;
                picturePreview.Image = null;
                oldImage.Dispose();
            }

            if (!string.IsNullOrWhiteSpace(imagePath) && File.Exists(imagePath))
            {
                picturePreview.Image = LoadImageSafe(imagePath);
            }
            else
            {
                picturePreview.Image = null;
            }
        }

        private Image LoadImageSafe(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath) || !File.Exists(imagePath))
                return null;

            try
            {
                using (FileStream stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                using (Image temp = Image.FromStream(stream))
                {
                    return new Bitmap(temp);
                }
            }
            catch
            {
                return null;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_clockTimer != null)
            {
                _clockTimer.Stop();
                _clockTimer.Dispose();
            }

            if (picturePreview.Image != null)
            {
                Image oldImage = picturePreview.Image;
                picturePreview.Image = null;
                oldImage.Dispose();
            }

            base.OnFormClosing(e);
        }
    }
}