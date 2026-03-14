using TVSchedulingSystem.Services;
using TVSchedulingSystem.Models;
using System.IO;

namespace TVSchedulingSystem
{
    public partial class MainForm : Form
    {
        private readonly ScheduleManager _manager;
        private Button btnSelectImage;
        System.Windows.Forms.Timer timerClock = new System.Windows.Forms.Timer();
        public MainForm()
        {
            InitializeComponent();


            _manager = new ScheduleManager();

            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;

            timerClock.Interval = 1000;
            timerClock.Tick += TimerClock_Tick;
            timerClock.Start();

            cmbChannel.Items.Add(1);
            cmbChannel.Items.Add(2);
            cmbChannel.Items.Add(3);
            cmbChannel.SelectedIndex = 0;

            numDuration.Minimum = 1;
            numDuration.Maximum = 600;

            dtpStartTime.Format = DateTimePickerFormat.Custom;
            dtpStartTime.CustomFormat = "dd/MM/yyyy HH:mm";

        }

        string selectedImagePath = "";

        private void TimerClock_Tick(object? sender, EventArgs e)
        {
            lblClock.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void RefreshGrid(int channelId)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = _manager.GetSchedulesByChannel(channelId);
        }

        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            cmbChannel = new ComboBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            txtProgramId = new TextBox();
            dtpStartTime = new DateTimePicker();
            numDuration = new NumericUpDown();
            flowLayoutPanel1 = new FlowLayoutPanel();
            btnAdd = new Button();
            btnSuggest = new Button();
            btnRemove = new Button();
            btnSelectImage = new Button();
            label1 = new Label();
            panel1 = new Panel();
            picturePreview = new PictureBox();
            lblProgramTitle = new Label();
            lblClock = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numDuration).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picturePreview).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 303);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(684, 221);
            dataGridView1.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.White;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 354F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 0, 2);
            tableLayoutPanel1.Controls.Add(dataGridView1, 0, 3);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(panel1, 1, 1);
            tableLayoutPanel1.Controls.Add(lblProgramTitle, 1, 2);
            tableLayoutPanel1.Controls.Add(lblClock, 1, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.ForeColor = SystemColors.ActiveCaptionText;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 180F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1044, 527);
            tableLayoutPanel1.TabIndex = 0;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
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
            tableLayoutPanel2.ForeColor = Color.Black;
            tableLayoutPanel2.Location = new Point(3, 63);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 4;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.Size = new Size(684, 174);
            tableLayoutPanel2.TabIndex = 1;
            tableLayoutPanel2.Paint += tableLayoutPanel2_Paint;
            // 
            // cmbChannel
            // 
            cmbChannel.Dock = DockStyle.Fill;
            cmbChannel.FormattingEnabled = true;
            cmbChannel.Location = new Point(174, 3);
            cmbChannel.Name = "cmbChannel";
            cmbChannel.Size = new Size(507, 28);
            cmbChannel.TabIndex = 9;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Location = new Point(3, 0);
            label2.Name = "label2";
            label2.Size = new Size(165, 43);
            label2.TabIndex = 0;
            label2.Text = "Channel";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Fill;
            label3.Location = new Point(3, 43);
            label3.Name = "label3";
            label3.Size = new Size(165, 43);
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
            label4.Size = new Size(165, 43);
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
            label5.Size = new Size(165, 45);
            label5.TabIndex = 3;
            label5.Text = "Duration";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtProgramId
            // 
            txtProgramId.Dock = DockStyle.Fill;
            txtProgramId.Location = new Point(174, 46);
            txtProgramId.Name = "txtProgramId";
            txtProgramId.Size = new Size(507, 27);
            txtProgramId.TabIndex = 5;
            // 
            // dtpStartTime
            // 
            dtpStartTime.Dock = DockStyle.Fill;
            dtpStartTime.Location = new Point(174, 89);
            dtpStartTime.Name = "dtpStartTime";
            dtpStartTime.Size = new Size(507, 27);
            dtpStartTime.TabIndex = 6;
            // 
            // numDuration
            // 
            numDuration.Dock = DockStyle.Fill;
            numDuration.Location = new Point(174, 132);
            numDuration.Name = "numDuration";
            numDuration.Size = new Size(507, 27);
            numDuration.TabIndex = 7;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(btnAdd);
            flowLayoutPanel1.Controls.Add(btnSuggest);
            flowLayoutPanel1.Controls.Add(btnRemove);
            flowLayoutPanel1.Controls.Add(btnSelectImage);
            flowLayoutPanel1.Dock = DockStyle.Top;
            flowLayoutPanel1.Location = new Point(3, 243);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(684, 50);
            flowLayoutPanel1.TabIndex = 2;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.RoyalBlue;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(3, 3);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(150, 35);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "Add Schedule";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnSuggest
            // 
            btnSuggest.BackColor = Color.RoyalBlue;
            btnSuggest.FlatStyle = FlatStyle.Flat;
            btnSuggest.ForeColor = Color.White;
            btnSuggest.Location = new Point(159, 3);
            btnSuggest.Name = "btnSuggest";
            btnSuggest.Size = new Size(150, 35);
            btnSuggest.TabIndex = 2;
            btnSuggest.Text = "Suggest Slot";
            btnSuggest.UseVisualStyleBackColor = false;
            btnSuggest.Click += btnSuggest_Click;
            // 
            // btnRemove
            // 
            btnRemove.BackColor = Color.RoyalBlue;
            btnRemove.Cursor = Cursors.Hand;
            btnRemove.Dock = DockStyle.Fill;
            btnRemove.FlatStyle = FlatStyle.Flat;
            btnRemove.ForeColor = Color.White;
            btnRemove.Location = new Point(315, 3);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(150, 35);
            btnRemove.TabIndex = 3;
            btnRemove.Text = "Remove";
            btnRemove.UseVisualStyleBackColor = false;
            btnRemove.Click += btnRemove_Click;
            // 
            // btnSelectImage
            // 
            btnSelectImage.BackColor = Color.RoyalBlue;
            btnSelectImage.Cursor = Cursors.Hand;
            btnSelectImage.Dock = DockStyle.Fill;
            btnSelectImage.FlatStyle = FlatStyle.Flat;
            btnSelectImage.ForeColor = Color.White;
            btnSelectImage.Location = new Point(471, 3);
            btnSelectImage.Name = "btnSelectImage";
            btnSelectImage.Size = new Size(150, 35);
            btnSelectImage.TabIndex = 4;
            btnSelectImage.Text = "Select Preview";
            btnSelectImage.UseVisualStyleBackColor = false;
            btnSelectImage.Click += btnSelectImage_Click;
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
            label1.Click += label1_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel1.BackColor = Color.Black;
            panel1.Controls.Add(picturePreview);
            panel1.Location = new Point(693, 63);
            panel1.Name = "panel1";
            panel1.Size = new Size(348, 174);
            panel1.TabIndex = 4;
            // 
            // picturePreview
            // 
            picturePreview.Dock = DockStyle.Fill;
            picturePreview.Location = new Point(0, 0);
            picturePreview.Name = "picturePreview";
            picturePreview.Size = new Size(348, 174);
            picturePreview.SizeMode = PictureBoxSizeMode.StretchImage;
            picturePreview.TabIndex = 0;
            picturePreview.TabStop = false;
            // 
            // lblProgramTitle
            // 
            lblProgramTitle.AutoSize = true;
            lblProgramTitle.BackColor = Color.Black;
            lblProgramTitle.Dock = DockStyle.Top;
            lblProgramTitle.Font = new Font("Segoe UI", 12F);
            lblProgramTitle.ForeColor = Color.White;
            lblProgramTitle.Location = new Point(693, 240);
            lblProgramTitle.Name = "lblProgramTitle";
            lblProgramTitle.Size = new Size(348, 28);
            lblProgramTitle.TabIndex = 5;
            lblProgramTitle.Text = "Program Preview";
            lblProgramTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblClock
            // 
            lblClock.AutoSize = true;
            lblClock.Dock = DockStyle.Top;
            lblClock.Font = new Font("Consolas", 28.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblClock.ForeColor = Color.Orange;
            lblClock.Location = new Point(693, 300);
            lblClock.Name = "lblClock";
            lblClock.Size = new Size(348, 55);
            lblClock.TabIndex = 6;
            lblClock.Text = "00:00:00";
            lblClock.TextAlign = ContentAlignment.MiddleCenter;
            lblClock.Click += lblClock_Click;
            // 
            // MainForm
            // 
            ClientSize = new Size(1044, 527);
            Controls.Add(tableLayoutPanel1);
            Name = "MainForm";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numDuration).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picturePreview).EndInit();
            ResumeLayout(false);

        }

        private void MainForm_Load(object? sender, EventArgs e)
        {
            if (cmbChannel.SelectedItem != null)
            {
                int channelId = Convert.ToInt32(cmbChannel.SelectedItem);
                RefreshGrid(channelId);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbChannel.SelectedItem != null)
            {
                int channelId = Convert.ToInt32(cmbChannel.SelectedItem);
                RefreshGrid(channelId);
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

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

                int channelId = Convert.ToInt32(
                    dataGridView1.CurrentRow.Cells["ChannelID"].Value);

                DateTime startTime = Convert.ToDateTime(
                    dataGridView1.CurrentRow.Cells["StartTime"].Value);

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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtProgramId.Text))
                {
                    MessageBox.Show("Program ID is required.");
                    return;
                }

                // Prevent NullReferenceException when no channel is selected
                if (cmbChannel.SelectedItem == null)
                {
                    MessageBox.Show("Please select a channel.");
                    return;
                }

                if (string.IsNullOrEmpty(selectedImagePath))
                {
                    MessageBox.Show("Please select a preview image first.");
                    return;
                }

                int channelId = Convert.ToInt32(cmbChannel.SelectedItem);
                string programId = txtProgramId.Text;
                int duration = (int)numDuration.Value;
                DateTime start = dtpStartTime.Value;

                bool result = _manager.AddSchedule(
                    new Random().Next(1, 10000),
                    channelId,
                    programId,
                    start,
                    duration,
                    selectedImagePath);

                if (result)
                {
                    MessageBox.Show("Schedule added successfully.");

                    // Update preview
                    if (!string.IsNullOrEmpty(selectedImagePath))
                    {
                        picturePreview.Image = Image.FromFile(selectedImagePath);
                    }

                    lblProgramTitle.Text = "Program ID: " + programId;

                    RefreshGrid(channelId);
                }

                else
                {
                    MessageBox.Show("Conflict detected.");
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

            var schedules = _manager
                .GetSchedulesByChannel(channelId)
                .OrderBy(s => s.StartTime)
                .ToList();

            // If no schedules exist → suggest now
            if (schedules.Count == 0)
            {
                DateTime now = DateTime.Now;

                now = new DateTime(
                    now.Year,
                    now.Month,
                    now.Day,
                    now.Hour,
                    now.Minute,
                    0);

                dtpStartTime.Value = now;

                MessageBox.Show("No schedules found. Suggesting current time.");
                return;
            }

            // Check gaps between schedules
            for (int i = 0; i < schedules.Count - 1; i++)
            {
                DateTime currentEnd = schedules[i].EndTime;
                DateTime nextStart = schedules[i + 1].StartTime;

                double gapMinutes = (nextStart - currentEnd).TotalMinutes;

                if (gapMinutes >= requiredDuration)
                {
                    dtpStartTime.Value = currentEnd;
                    MessageBox.Show("Found available gap.");
                    return;
                }
            }

            // If no gap found → suggest after last schedule
            DateTime lastEnd = schedules.Last().EndTime;
            dtpStartTime.Value = lastEnd;

            MessageBox.Show("No gaps found. Suggesting next available time.");
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
        private Label lblProgramTitle;
        private Label lblClock;
        private Button btnRemove;

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
                return;

            try
            {
                string programName = dataGridView1.CurrentRow.Cells["ProgramID"].Value?.ToString();
                string imagePath = dataGridView1.CurrentRow.Cells["ImagePath"].Value?.ToString();

                lblProgramTitle.Text = programName;

                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    picturePreview.Image = new Bitmap(imagePath);
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblClock_Click(object sender, EventArgs e)
        {

        }

        private void ShowProgramPreview(string programName)
        {
            lblProgramTitle.Text = programName;

            // Optional preview image
            picturePreview.Image = Image.FromFile("preview.jpg");
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Image Files|*.jpg;*.png;*.jpeg";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                selectedImagePath = dialog.FileName;

                picturePreview.Image = Image.FromFile(selectedImagePath);
            }
        }
    }
}
