using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TVSchedulingSystem.Forms;
using TVSchedulingSystem.Models;
using TVSchedulingSystem.Services;

namespace TVSchedulingSystem
{
    public partial class MainForm : Form
    {
        private readonly ScheduleManager _manager;

        private Button btnSelectImage;
        private Button btnBack;
        private Button btnAiRecommendChannel;
        private Button btnAiSuggestSlot;
        private Button btnApplyAiRecommendation;

        private System.Windows.Forms.Timer timerClock = new System.Windows.Forms.Timer();
        private string selectedImagePath = string.Empty;

        // AI Agent state
        private int _recommendedChannel = -1;
        private DateTime _recommendedStartTime = DateTime.MinValue;
        private int _recommendedDuration = 0;
        private bool _hasAiRecommendation = false;
        private string _recommendedReason = string.Empty;

        public MainForm()
        {
            InitializeComponent();

            _manager = new ScheduleManager();

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

            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            cmbChannel.SelectedIndexChanged += cmbChannel_SelectedIndexChanged;

            btnAdd.Click += btnAdd_Click;
            btnSuggest.Click += btnSuggest_Click;
            btnRemove.Click += btnRemove_Click;
            btnSelectImage.Click += btnSelectImage_Click;
            btnAiRecommendChannel.Click += btnAiRecommendChannel_Click;
            btnAiSuggestSlot.Click += btnAiSuggestSlot_Click;
            btnApplyAiRecommendation.Click += btnApplyAiRecommendation_Click;
            btnBack.Click += btnBack_Click;
        }

        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            tableLayoutPanel1 = new TableLayoutPanel();
            label1 = new Label();
            btnBack = new Button();
            tableLayoutPanel2 = new TableLayoutPanel();
            cmbChannel = new ComboBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            txtProgramId = new TextBox();
            dtpStartTime = new DateTimePicker();
            numDuration = new NumericUpDown();
            panel1 = new Panel();
            txtAiOutput = new TextBox();
            picturePreview = new PictureBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            btnAdd = new Button();
            btnSuggest = new Button();
            btnRemove = new Button();
            btnSelectImage = new Button();
            btnAiRecommendChannel = new Button();
            btnAiSuggestSlot = new Button();
            btnApplyAiRecommendation = new Button();
            lblProgramTitle = new Label();
            lblClock = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numDuration).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picturePreview).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 303);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 70;
            dataGridView1.Size = new Size(1053, 358);
            dataGridView1.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.White;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(btnBack, 1, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Controls.Add(panel1, 1, 1);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 0, 2);
            tableLayoutPanel1.Controls.Add(lblProgramTitle, 1, 2);
            tableLayoutPanel1.Controls.Add(dataGridView1, 0, 3);
            tableLayoutPanel1.Controls.Add(lblClock, 1, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 180F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1413, 664);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.RoyalBlue;
            label1.Dock = DockStyle.Left;
            label1.Font = new Font("Tempus Sans ITC", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(317, 60);
            label1.TabIndex = 0;
            label1.Text = "TV Program Scheduling System";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnBack
            // 
            btnBack.Dock = DockStyle.Fill;
            btnBack.Location = new Point(1062, 3);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(348, 54);
            btnBack.TabIndex = 8;
            btnBack.Text = "Back to Login";
            btnBack.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75F));
            tableLayoutPanel2.Controls.Add(cmbChannel, 1, 0);
            tableLayoutPanel2.Controls.Add(label2, 0, 0);
            tableLayoutPanel2.Controls.Add(label3, 0, 1);
            tableLayoutPanel2.Controls.Add(label4, 0, 2);
            tableLayoutPanel2.Controls.Add(label5, 0, 3);
            tableLayoutPanel2.Controls.Add(txtProgramId, 1, 1);
            tableLayoutPanel2.Controls.Add(dtpStartTime, 1, 2);
            tableLayoutPanel2.Controls.Add(numDuration, 1, 3);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 63);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 4;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.Size = new Size(1053, 174);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // cmbChannel
            // 
            cmbChannel.Dock = DockStyle.Fill;
            cmbChannel.FormattingEnabled = true;
            cmbChannel.Location = new Point(266, 3);
            cmbChannel.Name = "cmbChannel";
            cmbChannel.Size = new Size(784, 28);
            cmbChannel.TabIndex = 9;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Location = new Point(3, 0);
            label2.Name = "label2";
            label2.Size = new Size(257, 43);
            label2.TabIndex = 0;
            label2.Text = "Channel";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Fill;
            label3.Location = new Point(3, 43);
            label3.Name = "label3";
            label3.Size = new Size(257, 43);
            label3.TabIndex = 1;
            label3.Text = "Program ID";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = DockStyle.Fill;
            label4.Location = new Point(3, 86);
            label4.Name = "label4";
            label4.Size = new Size(257, 43);
            label4.TabIndex = 2;
            label4.Text = "Start Time";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Dock = DockStyle.Fill;
            label5.Location = new Point(3, 129);
            label5.Name = "label5";
            label5.Size = new Size(257, 45);
            label5.TabIndex = 3;
            label5.Text = "Duration";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtProgramId
            // 
            txtProgramId.Dock = DockStyle.Fill;
            txtProgramId.Location = new Point(266, 46);
            txtProgramId.Name = "txtProgramId";
            txtProgramId.Size = new Size(784, 27);
            txtProgramId.TabIndex = 5;
            // 
            // dtpStartTime
            // 
            dtpStartTime.Dock = DockStyle.Fill;
            dtpStartTime.Location = new Point(266, 89);
            dtpStartTime.Name = "dtpStartTime";
            dtpStartTime.Size = new Size(784, 27);
            dtpStartTime.TabIndex = 6;
            // 
            // numDuration
            // 
            numDuration.Dock = DockStyle.Fill;
            numDuration.Location = new Point(266, 132);
            numDuration.Name = "numDuration";
            numDuration.Size = new Size(784, 27);
            numDuration.TabIndex = 7;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Black;
            panel1.Controls.Add(txtAiOutput);
            panel1.Controls.Add(picturePreview);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(1062, 63);
            panel1.Name = "panel1";
            panel1.Size = new Size(348, 174);
            panel1.TabIndex = 4;
            // 
            // txtAiOutput
            // 
            txtAiOutput.BackColor = Color.Black;
            txtAiOutput.Dock = DockStyle.Fill;
            txtAiOutput.ForeColor = Color.White;
            txtAiOutput.Location = new Point(0, 95);
            txtAiOutput.Multiline = true;
            txtAiOutput.Name = "txtAiOutput";
            txtAiOutput.ReadOnly = true;
            txtAiOutput.ScrollBars = ScrollBars.Vertical;
            txtAiOutput.Size = new Size(348, 79);
            txtAiOutput.TabIndex = 1;
            // 
            // picturePreview
            // 
            picturePreview.BackColor = Color.Black;
            picturePreview.Dock = DockStyle.Top;
            picturePreview.Location = new Point(0, 0);
            picturePreview.Name = "picturePreview";
            picturePreview.Size = new Size(348, 95);
            picturePreview.SizeMode = PictureBoxSizeMode.Zoom;
            picturePreview.TabIndex = 0;
            picturePreview.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(btnAdd);
            flowLayoutPanel1.Controls.Add(btnSuggest);
            flowLayoutPanel1.Controls.Add(btnRemove);
            flowLayoutPanel1.Controls.Add(btnSelectImage);
            flowLayoutPanel1.Controls.Add(btnAiRecommendChannel);
            flowLayoutPanel1.Controls.Add(btnAiSuggestSlot);
            flowLayoutPanel1.Controls.Add(btnApplyAiRecommendation);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(3, 243);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(1053, 54);
            flowLayoutPanel1.TabIndex = 2;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.RoyalBlue;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(3, 3);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(128, 35);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "Add Schedule";
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnSuggest
            // 
            btnSuggest.BackColor = Color.RoyalBlue;
            btnSuggest.FlatStyle = FlatStyle.Flat;
            btnSuggest.ForeColor = Color.White;
            btnSuggest.Location = new Point(137, 3);
            btnSuggest.Name = "btnSuggest";
            btnSuggest.Size = new Size(128, 35);
            btnSuggest.TabIndex = 2;
            btnSuggest.Text = "Suggest Slot";
            btnSuggest.UseVisualStyleBackColor = false;
            // 
            // btnRemove
            // 
            btnRemove.BackColor = Color.RoyalBlue;
            btnRemove.FlatStyle = FlatStyle.Flat;
            btnRemove.ForeColor = Color.White;
            btnRemove.Location = new Point(271, 3);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(128, 35);
            btnRemove.TabIndex = 3;
            btnRemove.Text = "Remove";
            btnRemove.UseVisualStyleBackColor = false;
            // 
            // btnSelectImage
            // 
            btnSelectImage.BackColor = Color.RoyalBlue;
            btnSelectImage.FlatStyle = FlatStyle.Flat;
            btnSelectImage.ForeColor = Color.White;
            btnSelectImage.Location = new Point(405, 3);
            btnSelectImage.Name = "btnSelectImage";
            btnSelectImage.Size = new Size(128, 35);
            btnSelectImage.TabIndex = 4;
            btnSelectImage.Text = "Select Preview";
            btnSelectImage.UseVisualStyleBackColor = false;
            // 
            // btnAiRecommendChannel
            // 
            btnAiRecommendChannel.BackColor = Color.RoyalBlue;
            btnAiRecommendChannel.FlatStyle = FlatStyle.Flat;
            btnAiRecommendChannel.ForeColor = Color.White;
            btnAiRecommendChannel.Location = new Point(539, 3);
            btnAiRecommendChannel.Name = "btnAiRecommendChannel";
            btnAiRecommendChannel.Size = new Size(128, 35);
            btnAiRecommendChannel.TabIndex = 7;
            btnAiRecommendChannel.Text = "AI Recommend";
            btnAiRecommendChannel.UseVisualStyleBackColor = false;
            // 
            // btnAiSuggestSlot
            // 
            btnAiSuggestSlot.BackColor = Color.RoyalBlue;
            btnAiSuggestSlot.FlatStyle = FlatStyle.Flat;
            btnAiSuggestSlot.ForeColor = Color.White;
            btnAiSuggestSlot.Location = new Point(673, 3);
            btnAiSuggestSlot.Name = "btnAiSuggestSlot";
            btnAiSuggestSlot.Size = new Size(128, 35);
            btnAiSuggestSlot.TabIndex = 8;
            btnAiSuggestSlot.Text = "AI Suggest";
            btnAiSuggestSlot.UseVisualStyleBackColor = false;
            // 
            // btnApplyAiRecommendation
            // 
            btnApplyAiRecommendation.BackColor = Color.DarkGreen;
            btnApplyAiRecommendation.FlatStyle = FlatStyle.Flat;
            btnApplyAiRecommendation.ForeColor = Color.White;
            btnApplyAiRecommendation.Location = new Point(807, 3);
            btnApplyAiRecommendation.Name = "btnApplyAiRecommendation";
            btnApplyAiRecommendation.Size = new Size(185, 35);
            btnApplyAiRecommendation.TabIndex = 9;
            btnApplyAiRecommendation.Text = "Apply AI Recommendation";
            btnApplyAiRecommendation.UseVisualStyleBackColor = false;
            // 
            // lblProgramTitle
            // 
            lblProgramTitle.BackColor = Color.Black;
            lblProgramTitle.Dock = DockStyle.Fill;
            lblProgramTitle.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblProgramTitle.ForeColor = Color.White;
            lblProgramTitle.Location = new Point(1062, 240);
            lblProgramTitle.Name = "lblProgramTitle";
            lblProgramTitle.Size = new Size(348, 60);
            lblProgramTitle.TabIndex = 5;
            lblProgramTitle.Text = "Program Preview";
            lblProgramTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblClock
            // 
            lblClock.BackColor = Color.White;
            lblClock.Dock = DockStyle.Top;
            lblClock.Font = new Font("Consolas", 28.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblClock.ForeColor = Color.Orange;
            lblClock.Location = new Point(1062, 300);
            lblClock.Name = "lblClock";
            lblClock.Size = new Size(348, 70);
            lblClock.TabIndex = 6;
            lblClock.Text = "00:00:00";
            lblClock.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            ClientSize = new Size(1413, 664);
            Controls.Add(tableLayoutPanel1);
            Name = "MainForm";
            Text = "TV Program Scheduling System";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numDuration).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picturePreview).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void MainForm_Load(object? sender, EventArgs e)
        {
            _manager.LoadFromDatabase();

            if (cmbChannel.SelectedItem != null)
            {
                int channelId = Convert.ToInt32(cmbChannel.SelectedItem);
                RefreshGrid(channelId);
            }

            txtAiOutput.Text = "AI Agent ready." + Environment.NewLine +
                               "It can detect conflicts, recommend a better slot, recommend a better channel, and apply its recommendation.";
            UpdateClock();
        }

        private void TimerClock_Tick(object? sender, EventArgs e)
        {
            UpdateClock();
        }

        private void UpdateClock()
        {
            lblClock.Text = DateTime.Now.ToString("HH:mm:ss");
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

            DataGridViewImageColumn previewColumn = new DataGridViewImageColumn();
            previewColumn.Name = "Preview";
            previewColumn.HeaderText = "Preview";
            previewColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            previewColumn.Width = 120;
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

        private void cmbChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbChannel.SelectedItem != null)
            {
                int channelId = Convert.ToInt32(cmbChannel.SelectedItem);
                RefreshGrid(channelId);
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

                if (string.IsNullOrWhiteSpace(txtProgramId.Text))
                {
                    MessageBox.Show("Program ID is required.");
                    return;
                }

                int channelId = Convert.ToInt32(cmbChannel.SelectedItem);
                string programId = txtProgramId.Text.Trim();
                int duration = (int)numDuration.Value;
                DateTime requestedStart = NormalizeToMinute(dtpStartTime.Value);
                DateTime requestedEnd = requestedStart.AddMinutes(duration);

                // AI agent checks conflict before adding
                if (HasConflict(channelId, requestedStart, requestedEnd))
                {
                    RunAiAgentAnalysis(channelId, requestedStart, duration, true);
                    MessageBox.Show("Conflict detected. The AI agent has prepared a recommendation.");
                    return;
                }

                int scheduleId = GetNextScheduleId();

                bool result = _manager.AddSchedule(
                    scheduleId,
                    channelId,
                    programId,
                    requestedStart,
                    duration,
                    selectedImagePath
                );

                if (result)
                {
                    MessageBox.Show("Schedule added successfully.");
                    txtAiOutput.Text =
                        "AI Agent:" + Environment.NewLine +
                        "Schedule added successfully." + Environment.NewLine +
                        "Channel: " + channelId + Environment.NewLine +
                        "Start Time: " + requestedStart.ToString("dd/MM/yyyy HH:mm") + Environment.NewLine +
                        "Duration: " + duration + " minutes";

                    RefreshGrid(channelId);

                    txtProgramId.Clear();
                    numDuration.Value = 30;
                    selectedImagePath = string.Empty;
                    ClearAiRecommendation();
                }
                else
                {
                    RunAiAgentAnalysis(channelId, requestedStart, duration, true);
                    MessageBox.Show("The schedule could not be added. The AI agent has prepared a recommendation.");
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

            DateTime suggestedTime = FindBestSlotFrom(channelId, requiredDuration, requestedStart);
            dtpStartTime.Value = suggestedTime;

            txtAiOutput.Text =
                "AI Agent Suggestion:" + Environment.NewLine +
                "Channel: " + channelId + Environment.NewLine +
                "Suggested Start Time: " + suggestedTime.ToString("dd/MM/yyyy HH:mm") + Environment.NewLine +
                "Reason: earliest non-conflicting slot on the current channel.";

            MessageBox.Show("Suggested next available slot: " + suggestedTime.ToString("dd/MM/yyyy HH:mm"));
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
                DateTime startTime = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["StartTime"].Value);

                bool result = _manager.RemoveSchedule(channelId, startTime);

                if (result)
                {
                    MessageBox.Show("Schedule removed successfully.");
                    txtAiOutput.Text = "AI Agent: Schedule removed successfully.";
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
                    selectedImagePath = dialog.FileName;
                    SetPicturePreview(selectedImagePath);

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

        private void btnAiSuggestSlot_Click(object sender, EventArgs e)
        {
            if (cmbChannel.SelectedItem == null)
            {
                txtAiOutput.Text = "AI Agent: Please select a channel first.";
                return;
            }

            int channelId = Convert.ToInt32(cmbChannel.SelectedItem);
            int duration = Convert.ToInt32(numDuration.Value);
            DateTime requestedStart = NormalizeToMinute(dtpStartTime.Value);

            DateTime bestSlot = FindBestSlotFrom(channelId, duration, requestedStart);
            dtpStartTime.Value = bestSlot;

            _recommendedChannel = channelId;
            _recommendedStartTime = bestSlot;
            _recommendedDuration = duration;
            _recommendedReason = "earliest non-conflicting slot on the selected channel";
            _hasAiRecommendation = true;

            txtAiOutput.Text =
                "AI Agent Analysis:" + Environment.NewLine +
                "Requested Channel: " + channelId + Environment.NewLine +
                "Requested Start: " + requestedStart.ToString("dd/MM/yyyy HH:mm") + Environment.NewLine +
                "Duration: " + duration + " minutes" + Environment.NewLine + Environment.NewLine +
                "Recommendation:" + Environment.NewLine +
                "Stay on Channel " + channelId + " at " + bestSlot.ToString("dd/MM/yyyy HH:mm") + Environment.NewLine +
                "Reason: " + _recommendedReason + ".";
        }

        private void btnAiRecommendChannel_Click(object sender, EventArgs e)
        {
            int duration = Convert.ToInt32(numDuration.Value);
            DateTime requestedStart = NormalizeToMinute(dtpStartTime.Value);

            Recommendation recommendation = FindBestChannelAndSlot(duration, requestedStart);

            _recommendedChannel = recommendation.ChannelId;
            _recommendedStartTime = recommendation.StartTime;
            _recommendedDuration = duration;
            _recommendedReason = recommendation.Reason;
            _hasAiRecommendation = true;

            cmbChannel.SelectedItem = recommendation.ChannelId;
            dtpStartTime.Value = recommendation.StartTime;

            txtAiOutput.Text =
                "AI Agent Analysis:" + Environment.NewLine +
                "Requested Start: " + requestedStart.ToString("dd/MM/yyyy HH:mm") + Environment.NewLine +
                "Duration: " + duration + " minutes" + Environment.NewLine + Environment.NewLine +
                "Recommendation:" + Environment.NewLine +
                "Best Channel: " + recommendation.ChannelId + Environment.NewLine +
                "Best Time: " + recommendation.StartTime.ToString("dd/MM/yyyy HH:mm") + Environment.NewLine +
                "Reason: " + recommendation.Reason + ".";
        }

        private void btnApplyAiRecommendation_Click(object sender, EventArgs e)
        {
            if (!_hasAiRecommendation)
            {
                txtAiOutput.Text =
                    "AI Agent:" + Environment.NewLine +
                    "There is no recommendation to apply yet.";
                return;
            }

            cmbChannel.SelectedItem = _recommendedChannel;
            dtpStartTime.Value = _recommendedStartTime;
            numDuration.Value = Math.Max(numDuration.Minimum, Math.Min(numDuration.Maximum, _recommendedDuration));

            txtAiOutput.Text =
                "AI Agent:" + Environment.NewLine +
                "Recommendation applied successfully." + Environment.NewLine +
                "Channel: " + _recommendedChannel + Environment.NewLine +
                "Start Time: " + _recommendedStartTime.ToString("dd/MM/yyyy HH:mm") + Environment.NewLine +
                "Duration: " + _recommendedDuration + " minutes" + Environment.NewLine +
                "Reason: " + _recommendedReason + Environment.NewLine +
                "You may now click Add Schedule to confirm.";
        }

        // =========================
        // AI AGENT LOGIC
        // =========================

        private void RunAiAgentAnalysis(int requestedChannel, DateTime requestedStart, int durationMinutes, bool conflictDetected)
        {
            Recommendation sameChannelRecommendation = new Recommendation
            {
                ChannelId = requestedChannel,
                StartTime = FindBestSlotFrom(requestedChannel, durationMinutes, requestedStart),
                Reason = "earliest non-conflicting slot on the same channel"
            };

            Recommendation bestOverallRecommendation = FindBestChannelAndSlot(durationMinutes, requestedStart);

            DateTime requestedEnd = requestedStart.AddMinutes(durationMinutes);

            _recommendedChannel = bestOverallRecommendation.ChannelId;
            _recommendedStartTime = bestOverallRecommendation.StartTime;
            _recommendedDuration = durationMinutes;
            _recommendedReason = bestOverallRecommendation.Reason;
            _hasAiRecommendation = true;

            if (!conflictDetected)
            {
                txtAiOutput.Text =
                    "AI Agent Analysis:" + Environment.NewLine +
                    "Requested Channel: " + requestedChannel + Environment.NewLine +
                    "Requested Start: " + requestedStart.ToString("dd/MM/yyyy HH:mm") + Environment.NewLine +
                    "Duration: " + durationMinutes + " minutes" + Environment.NewLine + Environment.NewLine +
                    "Result:" + Environment.NewLine +
                    "No conflict detected." + Environment.NewLine + Environment.NewLine +
                    "Recommendation:" + Environment.NewLine +
                    "Current selection is valid." + Environment.NewLine +
                    "Reason: the requested slot fits without overlapping any existing programme.";
                return;
            }

            txtAiOutput.Text =
                "AI Agent Analysis:" + Environment.NewLine +
                "Requested Channel: " + requestedChannel + Environment.NewLine +
                "Requested Start: " + requestedStart.ToString("dd/MM/yyyy HH:mm") + Environment.NewLine +
                "Requested End: " + requestedEnd.ToString("dd/MM/yyyy HH:mm") + Environment.NewLine +
                "Duration: " + durationMinutes + " minutes" + Environment.NewLine + Environment.NewLine +
                "Result:" + Environment.NewLine +
                "Conflict detected with an existing schedule." + Environment.NewLine + Environment.NewLine +
                "Same-Channel Alternative:" + Environment.NewLine +
                "Channel " + sameChannelRecommendation.ChannelId + " at " +
                sameChannelRecommendation.StartTime.ToString("dd/MM/yyyy HH:mm") + Environment.NewLine + Environment.NewLine +
                "Best Overall Recommendation:" + Environment.NewLine +
                "Move to Channel " + bestOverallRecommendation.ChannelId + " at " +
                bestOverallRecommendation.StartTime.ToString("dd/MM/yyyy HH:mm") + Environment.NewLine +
                "Reason: " + bestOverallRecommendation.Reason + "." + Environment.NewLine + Environment.NewLine +
                "Click 'Apply AI Recommendation' to use this suggestion.";
        }

        private bool HasConflict(int channelId, DateTime newStart, DateTime newEnd)
        {
            Schedule[] schedules = _manager.GetSchedulesByChannel(channelId);

            for (int i = 0; i < schedules.Length; i++)
            {
                DateTime existingStart = NormalizeToMinute(schedules[i].StartTime);
                DateTime existingEnd = NormalizeToMinute(schedules[i].EndTime);

                if (newStart < existingEnd && newEnd > existingStart)
                {
                    return true;
                }
            }

            return false;
        }

        private DateTime FindBestSlotFrom(int channelId, int durationMinutes, DateTime preferredStart)
        {
            Schedule[] schedules = _manager.GetSchedulesByChannel(channelId)
                                           .OrderBy(s => s.StartTime)
                                           .ToArray();

            DateTime candidate = NormalizeToMinute(preferredStart);

            if (schedules.Length == 0)
                return candidate;

            for (int i = 0; i < schedules.Length; i++)
            {
                Schedule current = schedules[i];
                DateTime existingStart = NormalizeToMinute(current.StartTime);
                DateTime existingEnd = NormalizeToMinute(current.EndTime);

                if (candidate.AddMinutes(durationMinutes) <= existingStart)
                {
                    return candidate;
                }

                if (candidate < existingEnd)
                {
                    candidate = existingEnd;
                }
            }

            return candidate;
        }

        private Recommendation FindBestChannelAndSlot(int durationMinutes, DateTime preferredStart)
        {
            Recommendation best = new Recommendation
            {
                ChannelId = 1,
                StartTime = FindBestSlotFrom(1, durationMinutes, preferredStart),
                Reason = ""
            };

            long bestDelayMinutes = Math.Abs((long)(best.StartTime - preferredStart).TotalMinutes);
            best.Reason = "earliest non-conflicting slot with minimal delay from the requested start time";

            for (int channel = 2; channel <= 3; channel++)
            {
                DateTime candidateTime = FindBestSlotFrom(channel, durationMinutes, preferredStart);
                long candidateDelayMinutes = Math.Abs((long)(candidateTime - preferredStart).TotalMinutes);

                if (candidateTime < best.StartTime)
                {
                    best.ChannelId = channel;
                    best.StartTime = candidateTime;
                    bestDelayMinutes = candidateDelayMinutes;
                    best.Reason = "earliest non-conflicting slot across all channels";
                }
                else if (candidateTime == best.StartTime && candidateDelayMinutes < bestDelayMinutes)
                {
                    best.ChannelId = channel;
                    best.StartTime = candidateTime;
                    bestDelayMinutes = candidateDelayMinutes;
                    best.Reason = "same earliest slot but with smaller scheduling delay";
                }
            }

            if (best.StartTime == preferredStart)
            {
                best.Reason = "requested slot is already valid and does not conflict";
            }

            return best;
        }

        private DateTime NormalizeToMinute(DateTime value)
        {
            return new DateTime(
                value.Year,
                value.Month,
                value.Day,
                value.Hour,
                value.Minute,
                0
            );
        }

        private void ClearAiRecommendation()
        {
            _recommendedChannel = -1;
            _recommendedStartTime = DateTime.MinValue;
            _recommendedDuration = 0;
            _recommendedReason = string.Empty;
            _hasAiRecommendation = false;
        }

        // =========================
        // EXISTING UI/HELPERS
        // =========================

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
                string programName = Convert.ToString(dataGridView1.CurrentRow.Cells["ProgramID"].Value);
                string imagePath = Convert.ToString(dataGridView1.CurrentRow.Cells["ImagePath"].Value);

                lblProgramTitle.Text = string.IsNullOrWhiteSpace(programName) ? "Program Preview" : programName;
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

            for (int i = 0; i < allSchedules.Length; i++)
            {
                if (allSchedules[i].ScheduleID > maxId)
                    maxId = allSchedules[i].ScheduleID;
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

            if (!string.IsNullOrWhiteSpace(imagePath) && File.Exists(imagePath))
                picturePreview.Image = LoadImageSafe(imagePath);
            else
                picturePreview.Image = null;
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

        private class Recommendation
        {
            public int ChannelId { get; set; }
            public DateTime StartTime { get; set; }
            public string Reason { get; set; }
        }

        private DataGridView dataGridView1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel2;
        private TextBox txtProgramId;
        private DateTimePicker dtpStartTime;
        private NumericUpDown numDuration;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button btnSuggest;
        private Button btnAdd;
        private ComboBox cmbChannel;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Panel panel1;
        private PictureBox picturePreview;
        private TextBox txtAiOutput;
        private Label lblProgramTitle;
        private Label lblClock;
        private Button btnRemove;
    }
}