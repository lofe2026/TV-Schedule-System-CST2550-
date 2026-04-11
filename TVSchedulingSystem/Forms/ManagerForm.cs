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
    public partial class ManagerForm : Form
    {
        private readonly ScheduleManager _manager;
        private readonly ProgramRepository _programRepository;

        private List<ProgramItem> _programItems = new List<ProgramItem>();
        private string _selectedPreviewPath = string.Empty;

        private readonly System.Windows.Forms.Timer timerClock = new System.Windows.Forms.Timer();

        public ManagerForm()
        {
            InitializeComponent();

            _manager = new ScheduleManager();
            _programRepository = new ProgramRepository();

            timerClock.Interval = 1000;
            timerClock.Tick += TimerClock_Tick;
            timerClock.Start();

            cmbChannel.Items.Clear();
            cmbChannel.Items.Add(1);
            cmbChannel.Items.Add(2);
            cmbChannel.Items.Add(3);
            cmbChannel.SelectedIndex = 0;

            numDuration.Minimum = 1;
            numDuration.Maximum = 600;
            numDuration.Value = 30;

            dtpStartTime.Format = DateTimePickerFormat.Custom;
            dtpStartTime.CustomFormat = "dd/MM/yyyy HH:mm";

            txtProgramId.ReadOnly = true;
            cmbChannel.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProgram.DropDownStyle = ComboBoxStyle.DropDownList;

            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            cmbChannel.SelectedIndexChanged += cmbChannel_SelectedIndexChanged;
            cmbProgram.SelectedIndexChanged += cmbProgram_SelectedIndexChanged;

            btnAdd.Click += btnAdd_Click;
            btnSuggest.Click += btnSuggest_Click;
            btnRemove.Click += btnRemove_Click;
            btnSelectImage.Click += btnSelectImage_Click;
            btnAddProgram.Click += btnAddProgram_Click;
            btnBack.Click += btnBack_Click;
        }

        private void ManagerForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadPrograms();
                _manager.LoadFromDatabase();

                if (cmbChannel.SelectedItem != null)
                {
                    int channelId = Convert.ToInt32(cmbChannel.SelectedItem);
                    RefreshGrid(channelId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading ManagerForm: " + ex.Message);
            }
        }

        private void LoadPrograms()
        {
            _programItems = _programRepository.GetPrograms();
            cmbProgram.Items.Clear();

            foreach (ProgramItem item in _programItems)
            {
                cmbProgram.Items.Add(item);
            }

            if (cmbProgram.Items.Count > 0)
            {
                cmbProgram.SelectedIndex = 0;
            }
            else
            {
                txtProgramId.Clear();
                ClearPreview();
            }
        }

        private void cmbProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProgram.SelectedItem is ProgramItem selectedProgram)
            {
                txtProgramId.Text = selectedProgram.ProgramCode;
                lblProgramTitle.Text = selectedProgram.ProgramName;
                SetPicturePreview(selectedProgram.ImagePath);
                _selectedPreviewPath = string.Empty;
            }
        }

        private void TimerClock_Tick(object sender, EventArgs e)
        {
            lblClock.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void cmbChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbChannel.SelectedItem != null)
            {
                int channelId = Convert.ToInt32(cmbChannel.SelectedItem);
                RefreshGrid(channelId);
            }
        }

        private void RefreshGrid(int channelId)
        {
            Schedule[] schedules = _manager.GetSchedulesByChannel(channelId)
                .OrderBy(s => s.StartTime)
                .ToArray();

            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowTemplate.Height = 70;

            dataGridView1.Columns.Add("ScheduleID", "Schedule ID");
            dataGridView1.Columns.Add("ChannelID", "Channel");
            dataGridView1.Columns.Add("ProgramID", "Program ID");
            dataGridView1.Columns.Add("StartTime", "Start Time");
            dataGridView1.Columns.Add("EndTime", "End Time");

            DataGridViewImageColumn previewColumn = new DataGridViewImageColumn
            {
                Name = "Preview",
                HeaderText = "Preview",
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                Width = 120
            };
            dataGridView1.Columns.Add(previewColumn);

            dataGridView1.Columns.Add("ImagePath", "ImagePath");
            dataGridView1.Columns["ImagePath"].Visible = false;

            foreach (Schedule schedule in schedules)
            {
                Image image = LoadImageSafe(schedule.ImagePath);

                dataGridView1.Rows.Add(
                    schedule.ScheduleID,
                    schedule.ChannelID,
                    schedule.ProgramID,
                    schedule.StartTime.ToString("dd/MM/yyyy HH:mm"),
                    schedule.EndTime.ToString("dd/MM/yyyy HH:mm"),
                    image,
                    schedule.ImagePath
                );
            }

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows[0].Selected = true;
                UpdatePreviewFromSelectedRow();
            }
            else
            {
                if (cmbProgram.SelectedItem is ProgramItem selectedProgram)
                {
                    lblProgramTitle.Text = selectedProgram.ProgramName;
                    SetPicturePreview(selectedProgram.ImagePath);
                }
                else
                {
                    ClearPreview();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbChannel.SelectedItem == null)
                {
                    MessageBox.Show("Please select a channel.");
                    return;
                }

                if (!(cmbProgram.SelectedItem is ProgramItem selectedProgram))
                {
                    MessageBox.Show("Please select a program.");
                    return;
                }

                int channelId = Convert.ToInt32(cmbChannel.SelectedItem);
                int duration = (int)numDuration.Value;
                DateTime requestedStart = NormalizeToMinute(dtpStartTime.Value);
                int scheduleId = GetNextScheduleId();

                string previewPathToUse = !string.IsNullOrWhiteSpace(_selectedPreviewPath)
                    ? _selectedPreviewPath
                    : selectedProgram.ImagePath;

                bool result = _manager.AddSchedule(
                    scheduleId,
                    channelId,
                    selectedProgram.ProgramCode,
                    requestedStart,
                    duration,
                    previewPathToUse
                );

                if (result)
                {
                    MessageBox.Show("Schedule added successfully.");
                    RefreshGrid(channelId);
                    numDuration.Value = 30;
                    _selectedPreviewPath = string.Empty;
                }
                else
                {
                    MessageBox.Show("The schedule could not be added. It may conflict with an existing schedule.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnSuggest_Click(object sender, EventArgs e)
        {
            if (cmbChannel.SelectedItem == null)
            {
                MessageBox.Show("Please select a channel.");
                return;
            }

            int channelId = Convert.ToInt32(cmbChannel.SelectedItem);
            int requiredDuration = (int)numDuration.Value;
            DateTime requestedStart = NormalizeToMinute(dtpStartTime.Value);

            Schedule[] schedules = _manager.GetSchedulesByChannel(channelId)
                .OrderBy(s => s.StartTime)
                .ToArray();

            DateTime candidate = requestedStart;

            foreach (Schedule schedule in schedules)
            {
                if (candidate.AddMinutes(requiredDuration) <= schedule.StartTime)
                    break;

                if (candidate < schedule.EndTime)
                    candidate = schedule.EndTime;
            }

            dtpStartTime.Value = candidate;
            MessageBox.Show("Suggested next available slot on channel " + channelId + ": " + candidate.ToString("dd/MM/yyyy HH:mm"));
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Please select a schedule to remove.");
                    return;
                }

                int channelId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ChannelID"].Value);
                DateTime startTime = DateTime.Parse(dataGridView1.CurrentRow.Cells["StartTime"].Value.ToString());

                bool result = _manager.RemoveSchedule(channelId, startTime);

                if (result)
                {
                    MessageBox.Show("Schedule removed successfully.");
                    RefreshGrid(channelId);
                }
                else
                {
                    MessageBox.Show("Schedule not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Select Preview Image";
                dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _selectedPreviewPath = dialog.FileName;
                    SetPicturePreview(_selectedPreviewPath);

                    if (cmbProgram.SelectedItem is ProgramItem selectedProgram)
                    {
                        lblProgramTitle.Text = selectedProgram.ProgramName;
                    }
                    else if (!string.IsNullOrWhiteSpace(txtProgramId.Text))
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

        private void btnAddProgram_Click(object sender, EventArgs e)
        {
            string programCode = Prompt.ShowDialog("Enter Program Code:", "Add Program");
            if (string.IsNullOrWhiteSpace(programCode))
            {
                MessageBox.Show("Program Code is required.");
                return;
            }

            string programName = Prompt.ShowDialog("Enter Program Name:", "Add Program");
            if (string.IsNullOrWhiteSpace(programName))
            {
                MessageBox.Show("Program Name is required.");
                return;
            }

            string imagePath = string.Empty;

            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Select Program Image";
                dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = dialog.FileName;
                }
            }

            try
            {
                bool result = _programRepository.AddProgram(programCode.Trim(), programName.Trim(), imagePath);

                if (result)
                {
                    MessageBox.Show("Program added successfully.");
                    LoadPrograms();

                    ProgramItem addedProgram = _programItems.FirstOrDefault(p => p.ProgramCode == programCode.Trim());
                    if (addedProgram != null)
                    {
                        cmbProgram.SelectedItem = addedProgram;
                    }
                }
                else
                {
                    MessageBox.Show("A program with that code already exists.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding program: " + ex.Message);
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
                string programId = Convert.ToString(dataGridView1.CurrentRow.Cells["ProgramID"].Value);
                string imagePath = Convert.ToString(dataGridView1.CurrentRow.Cells["ImagePath"].Value);

                ProgramItem matchedProgram = _programItems.FirstOrDefault(p => p.ProgramCode == programId);

                if (matchedProgram != null)
                {
                    lblProgramTitle.Text = matchedProgram.ProgramName;
                }
                else
                {
                    lblProgramTitle.Text = string.IsNullOrWhiteSpace(programId) ? "Program Preview" : programId;
                }

                SetPicturePreview(imagePath);
            }
            catch
            {
                ClearPreview();
            }
        }

        private int GetNextScheduleId()
        {
            Schedule[] allSchedules = _manager.GetAllSchedules();
            int maxId = 0;

            foreach (Schedule schedule in allSchedules)
            {
                if (schedule.ScheduleID > maxId)
                    maxId = schedule.ScheduleID;
            }

            return maxId + 1;
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

            picturePreview.Image = LoadImageSafe(imagePath);
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

        private DateTime NormalizeToMinute(DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, 0);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (timerClock != null)
            {
                timerClock.Stop();
                timerClock.Dispose();
            }

            if (picturePreview.Image != null)
            {
                Image oldImage = picturePreview.Image;
                picturePreview.Image = null;
                oldImage.Dispose();
            }

            base.OnFormClosing(e);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            Close();
        }

        private static class Prompt
        {
            public static string ShowDialog(string text, string caption)
            {
                Form prompt = new Form
                {
                    Width = 420,
                    Height = 170,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = caption,
                    StartPosition = FormStartPosition.CenterScreen,
                    MinimizeBox = false,
                    MaximizeBox = false
                };

                Label textLabel = new Label
                {
                    Left = 20,
                    Top = 20,
                    Width = 360,
                    Text = text
                };

                TextBox textBox = new TextBox
                {
                    Left = 20,
                    Top = 50,
                    Width = 360
                };

                Button confirmation = new Button
                {
                    Text = "OK",
                    Left = 220,
                    Width = 75,
                    Top = 85,
                    DialogResult = DialogResult.OK
                };

                Button cancel = new Button
                {
                    Text = "Cancel",
                    Left = 305,
                    Width = 75,
                    Top = 85,
                    DialogResult = DialogResult.Cancel
                };

                prompt.Controls.Add(textLabel);
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(cancel);
                prompt.AcceptButton = confirmation;
                prompt.CancelButton = cancel;

                return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : string.Empty;
            }
        }
    }
}