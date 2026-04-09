using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TVSchedulingSystem.Models;
using TVSchedulingSystem.Repositories;
using TVSchedulingSystem.Services;

namespace TVSchedulingSystem.Forms
{
    public partial class ClientForm : Form
    {
        private readonly ScheduleManager _manager;
        private readonly ProgramRepository _programRepository;

        private List<ProgramItem> _programItems = new List<ProgramItem>();
        private System.Windows.Forms.Timer clockTimer;

        public ClientForm()
        {
            InitializeComponent();

            _manager = new ScheduleManager();
            _programRepository = new ProgramRepository();

            Load += ClientForm_Load;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            cmbChannel.SelectedIndexChanged += cmbChannel_SelectedIndexChanged;

            StartClock();
        }

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

        private void ClientForm_Load(object sender, EventArgs e)
        {
            try
            {
                _manager.LoadFromDatabase();
                _programItems = _programRepository.GetPrograms();

                cmbChannel.Items.Clear();
                cmbChannel.Items.Add(1);
                cmbChannel.Items.Add(2);
                cmbChannel.Items.Add(3);

                if (cmbChannel.Items.Count > 0)
                    cmbChannel.SelectedIndex = 0;

                LoadSchedules();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading ClientForm: " + ex.Message);
            }
        }

        private void cmbChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSchedules();
        }

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
            dataGridView1.RowTemplate.Height = 70;

            dataGridView1.Columns.Add("ProgramName", "Program Name");
            dataGridView1.Columns.Add("ChannelName", "Channel");
            dataGridView1.Columns.Add("StartDisplay", "Starts At");
            dataGridView1.Columns.Add("EndDisplay", "Ends At");
            dataGridView1.Columns.Add("DurationDisplay", "Duration");

            DataGridViewImageColumn imgCol = new DataGridViewImageColumn
            {
                Name = "Preview",
                HeaderText = "Preview",
                ImageLayout = DataGridViewImageCellLayout.Zoom
            };
            dataGridView1.Columns.Add(imgCol);

            dataGridView1.Columns.Add("ImagePath", "ImagePath");
            dataGridView1.Columns["ImagePath"].Visible = false;

            foreach (Schedule s in schedules)
            {
                ProgramItem matchedProgram = _programItems.FirstOrDefault(
                    p => string.Equals(
                        p.ProgramCode?.Trim(),
                        s.ProgramID?.Trim(),
                        StringComparison.OrdinalIgnoreCase
                    )
                );

                string programName = matchedProgram != null
                    ? matchedProgram.ProgramName
                    : (string.IsNullOrWhiteSpace(s.ProgramID) ? "Unknown Program" : s.ProgramID);

                string channelName = "Channel " + s.ChannelID;
                string startDisplay = s.StartTime.ToString("dd/MM/yyyy HH:mm");
                string endDisplay = s.EndTime.ToString("dd/MM/yyyy HH:mm");
                string durationDisplay = ((int)(s.EndTime - s.StartTime).TotalMinutes) + " mins";

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
                UpdatePreviewFromSelectedRow();
            }
            else
            {
                ClearPreview();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            UpdatePreviewFromSelectedRow();
        }

        private void UpdatePreviewFromSelectedRow()
        {
            if (dataGridView1.CurrentRow == null)
                return;

            try
            {
                string programName = Convert.ToString(dataGridView1.CurrentRow.Cells["ProgramName"].Value);
                string imagePath = Convert.ToString(dataGridView1.CurrentRow.Cells["ImagePath"].Value);

                lblProgramTitle.Text = string.IsNullOrWhiteSpace(programName)
                    ? "Program Preview"
                    : programName;

                picturePreview.Image = LoadImageSafe(imagePath);
            }
            catch
            {
                ClearPreview();
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

        private Image LoadImageSafe(string imagePath)
        {
            try
            {
                string resolvedPath = ResolveImagePath(imagePath);

                if (string.IsNullOrWhiteSpace(resolvedPath) || !File.Exists(resolvedPath))
                    return null;

                using (FileStream stream = new FileStream(resolvedPath, FileMode.Open, FileAccess.Read))
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

        private string ResolveImagePath(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath))
                return string.Empty;

            if (Path.IsPathRooted(imagePath))
                return imagePath;

            string startupPath = Application.StartupPath;
            string combinedPath = Path.Combine(startupPath, imagePath);
            if (File.Exists(combinedPath))
                return combinedPath;

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            combinedPath = Path.Combine(baseDirectory, imagePath);
            if (File.Exists(combinedPath))
                return combinedPath;

            string currentDirectory = Directory.GetCurrentDirectory();
            combinedPath = Path.Combine(currentDirectory, imagePath);
            if (File.Exists(combinedPath))
                return combinedPath;

            return string.Empty;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (clockTimer != null)
            {
                clockTimer.Stop();
                clockTimer.Dispose();
            }

            if (picturePreview.Image != null)
            {
                Image oldImage = picturePreview.Image;
                picturePreview.Image = null;
                oldImage.Dispose();
            }

            base.OnFormClosing(e);
        }

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            Close();
        }
    }
}