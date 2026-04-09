using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TVSchedulingSystem.Forms;
using TVSchedulingSystem.Models;
using TVSchedulingSystem.Repositories;
using TVSchedulingSystem.Services;

namespace TVSchedulingSystem
{
    public partial class MainForm : Form
    {
        private readonly ScheduleManager _manager;
        private readonly AIService _aiService;
        private readonly ProgramRepository _programRepository;
        private readonly List<ChatTurn> _conversation = new List<ChatTurn>();

        private List<ProgramItem> _programItems = new List<ProgramItem>();

        private Button btnBack;
        private Button btnSendAi;
        private Button btnSelectPreview;
        private Button btnAddProgram;

        private string _selectedPreviewPath = string.Empty;

        private System.Windows.Forms.Timer timerClock = new System.Windows.Forms.Timer();

        private DataGridView dataGridView1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel2;
        private ComboBox cmbProgram;
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
        private Label label6;

        private Panel panelPreview;
        private Panel panelAi;

        private PictureBox picturePreview;
        private RichTextBox txtAiChat;
        private TextBox txtChatInput;
        private Label lblProgramTitle;
        private Label lblClock;
        private Button btnRemove;

        public MainForm()
        {
            InitializeComponent();

            _manager = new ScheduleManager();
            _aiService = new AIService();
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

            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            cmbChannel.SelectedIndexChanged += cmbChannel_SelectedIndexChanged;
            cmbProgram.SelectedIndexChanged += cmbProgram_SelectedIndexChanged;

            btnAdd.Click += btnAdd_Click;
            btnSuggest.Click += btnSuggest_Click;
            btnRemove.Click += btnRemove_Click;
            btnSendAi.Click += btnSendAi_Click;
            btnBack.Click += btnBack_Click;
            btnSelectPreview.Click += btnSelectPreview_Click;
            btnAddProgram.Click += btnAddProgram_Click;
            txtChatInput.KeyDown += txtChatInput_KeyDown;
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
            label6 = new Label();
            label4 = new Label();
            label5 = new Label();
            cmbProgram = new ComboBox();
            txtProgramId = new TextBox();
            dtpStartTime = new DateTimePicker();
            numDuration = new NumericUpDown();
            panelPreview = new Panel();
            picturePreview = new PictureBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            btnAdd = new Button();
            btnSuggest = new Button();
            btnRemove = new Button();
            btnSelectPreview = new Button();
            btnAddProgram = new Button();
            lblProgramTitle = new Label();
            lblClock = new Label();
            panelAi = new Panel();
            txtAiChat = new RichTextBox();
            txtChatInput = new TextBox();
            btnSendAi = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numDuration).BeginInit();
            panelPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picturePreview).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            panelAi.SuspendLayout();
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
            tableLayoutPanel1.SetRowSpan(dataGridView1, 2);
            dataGridView1.RowTemplate.Height = 70;
            dataGridView1.Size = new Size(954, 458);
            dataGridView1.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.WhiteSmoke;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(btnBack, 1, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Controls.Add(panelPreview, 1, 1);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 0, 2);
            tableLayoutPanel1.Controls.Add(lblProgramTitle, 1, 2);
            tableLayoutPanel1.Controls.Add(dataGridView1, 0, 3);
            tableLayoutPanel1.Controls.Add(lblClock, 1, 3);
            tableLayoutPanel1.Controls.Add(panelAi, 1, 4);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 180F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1413, 764);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.RoyalBlue;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Tempus Sans ITC", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(954, 60);
            label1.TabIndex = 0;
            label1.Text = "TV Program Scheduling System";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnBack
            // 
            btnBack.Dock = DockStyle.Fill;
            btnBack.Location = new Point(963, 3);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(447, 54);
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
            tableLayoutPanel2.Controls.Add(label6, 0, 2);
            tableLayoutPanel2.Controls.Add(label4, 0, 3);
            tableLayoutPanel2.Controls.Add(label5, 0, 4);
            tableLayoutPanel2.Controls.Add(cmbProgram, 1, 1);
            tableLayoutPanel2.Controls.Add(txtProgramId, 1, 2);
            tableLayoutPanel2.Controls.Add(dtpStartTime, 1, 3);
            tableLayoutPanel2.Controls.Add(numDuration, 1, 4);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 63);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 5;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.Size = new Size(954, 174);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // cmbChannel
            // 
            cmbChannel.Dock = DockStyle.Fill;
            cmbChannel.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbChannel.FormattingEnabled = true;
            cmbChannel.Location = new Point(241, 3);
            cmbChannel.Name = "cmbChannel";
            cmbChannel.Size = new Size(710, 28);
            cmbChannel.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Location = new Point(3, 0);
            label2.Name = "label2";
            label2.Size = new Size(232, 34);
            label2.TabIndex = 0;
            label2.Text = "Channel";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Fill;
            label3.Location = new Point(3, 34);
            label3.Name = "label3";
            label3.Size = new Size(232, 34);
            label3.TabIndex = 1;
            label3.Text = "Program";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Dock = DockStyle.Fill;
            label6.Location = new Point(3, 68);
            label6.Name = "label6";
            label6.Size = new Size(232, 34);
            label6.TabIndex = 2;
            label6.Text = "Program ID";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = DockStyle.Fill;
            label4.Location = new Point(3, 102);
            label4.Name = "label4";
            label4.Size = new Size(232, 34);
            label4.TabIndex = 3;
            label4.Text = "Start Time";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Dock = DockStyle.Fill;
            label5.Location = new Point(3, 136);
            label5.Name = "label5";
            label5.Size = new Size(232, 38);
            label5.TabIndex = 4;
            label5.Text = "Duration";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cmbProgram
            // 
            cmbProgram.Dock = DockStyle.Fill;
            cmbProgram.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProgram.FormattingEnabled = true;
            cmbProgram.Location = new Point(241, 37);
            cmbProgram.Name = "cmbProgram";
            cmbProgram.Size = new Size(710, 28);
            cmbProgram.TabIndex = 1;
            // 
            // txtProgramId
            // 
            txtProgramId.Dock = DockStyle.Fill;
            txtProgramId.Location = new Point(241, 71);
            txtProgramId.Name = "txtProgramId";
            txtProgramId.Size = new Size(710, 27);
            txtProgramId.TabIndex = 2;
            // 
            // dtpStartTime
            // 
            dtpStartTime.Dock = DockStyle.Fill;
            dtpStartTime.Location = new Point(241, 105);
            dtpStartTime.Name = "dtpStartTime";
            dtpStartTime.Size = new Size(710, 27);
            dtpStartTime.TabIndex = 3;
            // 
            // numDuration
            // 
            numDuration.Dock = DockStyle.Fill;
            numDuration.Location = new Point(241, 139);
            numDuration.Name = "numDuration";
            numDuration.Size = new Size(710, 27);
            numDuration.TabIndex = 4;
            // 
            // panelPreview
            // 
            panelPreview.BackColor = Color.Black;
            panelPreview.Controls.Add(picturePreview);
            panelPreview.Dock = DockStyle.Fill;
            panelPreview.Location = new Point(963, 63);
            panelPreview.Name = "panelPreview";
            panelPreview.Padding = new Padding(8);
            panelPreview.Size = new Size(447, 174);
            panelPreview.TabIndex = 4;
            // 
            // picturePreview
            // 
            picturePreview.BackColor = Color.Black;
            picturePreview.Dock = DockStyle.Fill;
            picturePreview.Location = new Point(8, 8);
            picturePreview.Name = "picturePreview";
            picturePreview.Size = new Size(431, 158);
            picturePreview.SizeMode = PictureBoxSizeMode.Zoom;
            picturePreview.TabIndex = 0;
            picturePreview.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(btnAdd);
            flowLayoutPanel1.Controls.Add(btnSuggest);
            flowLayoutPanel1.Controls.Add(btnRemove);
            flowLayoutPanel1.Controls.Add(btnSelectPreview);
            flowLayoutPanel1.Controls.Add(btnAddProgram);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(3, 243);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(954, 54);
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
            btnSuggest.TabIndex = 1;
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
            btnRemove.TabIndex = 2;
            btnRemove.Text = "Remove";
            btnRemove.UseVisualStyleBackColor = false;
            // 
            // btnSelectPreview
            // 
            btnSelectPreview.BackColor = Color.RoyalBlue;
            btnSelectPreview.FlatStyle = FlatStyle.Flat;
            btnSelectPreview.ForeColor = Color.White;
            btnSelectPreview.Location = new Point(405, 3);
            btnSelectPreview.Name = "btnSelectPreview";
            btnSelectPreview.Size = new Size(128, 35);
            btnSelectPreview.TabIndex = 3;
            btnSelectPreview.Text = "Select Preview";
            btnSelectPreview.UseVisualStyleBackColor = false;
            // 
            // btnAddProgram
            // 
            btnAddProgram.BackColor = Color.MediumSeaGreen;
            btnAddProgram.FlatStyle = FlatStyle.Flat;
            btnAddProgram.ForeColor = Color.White;
            btnAddProgram.Location = new Point(539, 3);
            btnAddProgram.Name = "btnAddProgram";
            btnAddProgram.Size = new Size(128, 35);
            btnAddProgram.TabIndex = 4;
            btnAddProgram.Text = "Add Program";
            btnAddProgram.UseVisualStyleBackColor = false;
            // 
            // lblProgramTitle
            // 
            lblProgramTitle.BackColor = Color.Black;
            lblProgramTitle.Dock = DockStyle.Fill;
            lblProgramTitle.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblProgramTitle.ForeColor = Color.White;
            lblProgramTitle.Location = new Point(963, 240);
            lblProgramTitle.Name = "lblProgramTitle";
            lblProgramTitle.Size = new Size(447, 60);
            lblProgramTitle.TabIndex = 5;
            lblProgramTitle.Text = "Program Preview";
            lblProgramTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblClock
            // 
            lblClock.BackColor = Color.White;
            lblClock.Dock = DockStyle.Fill;
            lblClock.Font = new Font("Consolas", 28.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblClock.ForeColor = Color.Orange;
            lblClock.Location = new Point(963, 300);
            lblClock.Name = "lblClock";
            lblClock.Size = new Size(447, 90);
            lblClock.TabIndex = 6;
            lblClock.Text = "00:00:00";
            lblClock.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelAi
            // 
            panelAi.BackColor = Color.Black;
            panelAi.Controls.Add(txtAiChat);
            panelAi.Controls.Add(txtChatInput);
            panelAi.Controls.Add(btnSendAi);
            panelAi.Dock = DockStyle.Fill;
            panelAi.Location = new Point(963, 393);
            panelAi.Name = "panelAi";
            panelAi.Padding = new Padding(8);
            panelAi.Size = new Size(447, 368);
            panelAi.TabIndex = 7;
            // 
            // txtAiChat
            // 
            txtAiChat.BackColor = Color.Black;
            txtAiChat.BorderStyle = BorderStyle.None;
            txtAiChat.Dock = DockStyle.Top;
            txtAiChat.ForeColor = Color.White;
            txtAiChat.Location = new Point(8, 8);
            txtAiChat.Name = "txtAiChat";
            txtAiChat.ReadOnly = true;
            txtAiChat.Size = new Size(431, 300);
            txtAiChat.TabIndex = 0;
            txtAiChat.Text = "";
            // 
            // txtChatInput
            // 
            txtChatInput.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtChatInput.Location = new Point(8, 325);
            txtChatInput.Name = "txtChatInput";
            txtChatInput.Size = new Size(330, 27);
            txtChatInput.TabIndex = 1;
            // 
            // btnSendAi
            // 
            btnSendAi.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSendAi.BackColor = Color.MediumPurple;
            btnSendAi.FlatStyle = FlatStyle.Flat;
            btnSendAi.ForeColor = Color.White;
            btnSendAi.Location = new Point(344, 323);
            btnSendAi.Name = "btnSendAi";
            btnSendAi.Size = new Size(95, 31);
            btnSendAi.TabIndex = 2;
            btnSendAi.Text = "Send";
            btnSendAi.UseVisualStyleBackColor = false;
            // 
            // MainForm
            // 
            ClientSize = new Size(1413, 764);
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
            panelPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picturePreview).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            panelAi.ResumeLayout(false);
            panelAi.PerformLayout();
            ResumeLayout(false);
        }

        private void MainForm_Load(object sender, EventArgs e)
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

                AppendChat("AI", "Hello. I am your scheduling assistant. Ask me about conflicts, channels, or better time slots.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading MainForm: " + ex.Message);
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
            AppendChat("AI", "Suggested next available slot on channel " + channelId + ": " + candidate.ToString("dd/MM/yyyy HH:mm"));
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

        private void btnSelectPreview_Click(object sender, EventArgs e)
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

        private async void btnSendAi_Click(object sender, EventArgs e)
        {
            await SendCurrentMessageAsync();
        }

        private async void txtChatInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await SendCurrentMessageAsync();
            }
        }

        private async System.Threading.Tasks.Task SendCurrentMessageAsync()
        {
            string userMessage = txtChatInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(userMessage))
                return;

            AppendChat("You", userMessage);
            txtChatInput.Clear();

            try
            {
                btnSendAi.Enabled = false;
                AppendChat("AI", "Thinking...");

                int selectedChannel = cmbChannel.SelectedItem != null
                    ? Convert.ToInt32(cmbChannel.SelectedItem)
                    : 1;

                DateTime selectedStart = NormalizeToMinute(dtpStartTime.Value);
                int selectedDuration = (int)numDuration.Value;

                string aiReply = await _aiService.SendChatAsync(
                    _conversation,
                    userMessage,
                    _manager.GetAllSchedules(),
                    selectedChannel,
                    selectedStart,
                    selectedDuration
                );

                RemoveLastThinkingMessage();

                _conversation.Add(new ChatTurn { Role = "user", Content = userMessage });
                _conversation.Add(new ChatTurn { Role = "assistant", Content = aiReply });

                AppendChat("AI", aiReply);
            }
            catch (Exception ex)
            {
                RemoveLastThinkingMessage();
                AppendChat("AI", "Error: " + ex.Message);
            }
            finally
            {
                btnSendAi.Enabled = true;
            }
        }

        private void RemoveLastThinkingMessage()
        {
            string target = "AI: Thinking..." + Environment.NewLine + Environment.NewLine;

            if (txtAiChat.Text.EndsWith(target))
            {
                txtAiChat.Text = txtAiChat.Text.Substring(0, txtAiChat.Text.Length - target.Length);
            }
            else
            {
                txtAiChat.Text = txtAiChat.Text.Replace(target, string.Empty);
            }

            txtAiChat.SelectionStart = txtAiChat.TextLength;
            txtAiChat.ScrollToCaret();
        }

        private void AppendChat(string speaker, string message)
        {
            txtAiChat.AppendText($"{speaker}: {message}{Environment.NewLine}{Environment.NewLine}");
            txtAiChat.SelectionStart = txtAiChat.TextLength;
            txtAiChat.ScrollToCaret();
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
                Form prompt = new Form()
                {
                    Width = 420,
                    Height = 170,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = caption,
                    StartPosition = FormStartPosition.CenterScreen,
                    MinimizeBox = false,
                    MaximizeBox = false
                };

                Label textLabel = new Label()
                {
                    Left = 20,
                    Top = 20,
                    Width = 360,
                    Text = text
                };

                TextBox textBox = new TextBox()
                {
                    Left = 20,
                    Top = 50,
                    Width = 360
                };

                Button confirmation = new Button()
                {
                    Text = "OK",
                    Left = 220,
                    Width = 75,
                    Top = 85,
                    DialogResult = DialogResult.OK
                };

                Button cancel = new Button()
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