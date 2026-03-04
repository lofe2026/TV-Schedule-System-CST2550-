using TVSchedulingSystem.Services;
using TVSchedulingSystem.Models;

namespace TVSchedulingSystem
{
    public partial class MainForm : Form
    {
        private readonly ScheduleManager _manager;
        public MainForm()
        {
            InitializeComponent();

            _manager = new ScheduleManager();

            cmbChannel.Items.Add(1);
            cmbChannel.Items.Add(2);
            cmbChannel.Items.Add(3);
            cmbChannel.SelectedIndex = 0;

            numDuration.Minimum = 1;
            numDuration.Maximum = 600;

            dtpStartTime.Format = DateTimePickerFormat.Custom;
            dtpStartTime.CustomFormat = "dd/MM/yyyy HH:mm";

        }

        private void RefreshGrid(int channelId)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = _manager.GetSchedulesByChannel(channelId);
        }

        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            label1 = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            cmbChannel = new ComboBox();
            txtProgramId = new TextBox();
            dtpStartTime = new DateTimePicker();
            numDuration = new NumericUpDown();
            flowLayoutPanel1 = new FlowLayoutPanel();
            btnAdd = new Button();
            btnSuggest = new Button();
            btnRemove = new Button();
            dataGridView1 = new DataGridView();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numDuration).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 0, 2);
            tableLayoutPanel1.Controls.Add(dataGridView1, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
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
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.RoyalBlue;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Tempus Sans ITC", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(1038, 60);
            label1.TabIndex = 0;
            label1.Text = "TV Program Scheduling System";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Click += label1_Click;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(label2, 0, 0);
            tableLayoutPanel2.Controls.Add(label3, 0, 1);
            tableLayoutPanel2.Controls.Add(label4, 0, 2);
            tableLayoutPanel2.Controls.Add(label5, 0, 3);
            tableLayoutPanel2.Controls.Add(cmbChannel, 1, 0);
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
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(1038, 174);
            tableLayoutPanel2.TabIndex = 1;
            tableLayoutPanel2.Paint += tableLayoutPanel2_Paint;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Location = new Point(3, 0);
            label2.Name = "label2";
            label2.Size = new Size(513, 43);
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
            label3.Size = new Size(513, 43);
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
            label4.Size = new Size(513, 43);
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
            label5.Size = new Size(513, 45);
            label5.TabIndex = 3;
            label5.Text = "Duration";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cmbChannel
            // 
            cmbChannel.Dock = DockStyle.Fill;
            cmbChannel.FormattingEnabled = true;
            cmbChannel.Location = new Point(522, 3);
            cmbChannel.Name = "cmbChannel";
            cmbChannel.Size = new Size(513, 28);
            cmbChannel.TabIndex = 4;
            cmbChannel.SelectedIndexChanged += cmbChannel_SelectedIndexChanged;
            // 
            // txtProgramId
            // 
            txtProgramId.Dock = DockStyle.Fill;
            txtProgramId.Location = new Point(522, 46);
            txtProgramId.Name = "txtProgramId";
            txtProgramId.Size = new Size(513, 27);
            txtProgramId.TabIndex = 5;
            // 
            // dtpStartTime
            // 
            dtpStartTime.Dock = DockStyle.Fill;
            dtpStartTime.Location = new Point(522, 89);
            dtpStartTime.Name = "dtpStartTime";
            dtpStartTime.Size = new Size(513, 27);
            dtpStartTime.TabIndex = 6;
            // 
            // numDuration
            // 
            numDuration.Dock = DockStyle.Fill;
            numDuration.Location = new Point(522, 132);
            numDuration.Name = "numDuration";
            numDuration.Size = new Size(513, 27);
            numDuration.TabIndex = 7;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(btnAdd);
            flowLayoutPanel1.Controls.Add(btnSuggest);
            flowLayoutPanel1.Controls.Add(btnRemove);
            flowLayoutPanel1.Dock = DockStyle.Top;
            flowLayoutPanel1.Location = new Point(3, 243);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(1038, 50);
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
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 303);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1038, 221);
            dataGridView1.TabIndex = 3;
            // 
            // MainForm
            // 
            ClientSize = new Size(1044, 527);
            Controls.Add(tableLayoutPanel1);
            Name = "MainForm";
            Load += MainForm_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numDuration).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
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

        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private ComboBox cmbChannel;
        private TextBox txtProgramId;
        private DateTimePicker dtpStartTime;
        private NumericUpDown numDuration;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button btnAdd;
        private Button btnRemove;
        private DataGridView dataGridView1;
        private Button btnSuggest;

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

                int channelId = Convert.ToInt32(cmbChannel.SelectedItem);
                int programId = int.Parse(txtProgramId.Text);
                int duration = (int)numDuration.Value;
                DateTime start = dtpStartTime.Value;

                bool result = _manager.AddSchedule(
                    new Random().Next(1, 10000),
                    channelId,
                    programId,
                    start,
                    duration);

                if (result)
                {
                    MessageBox.Show("Schedule added successfully.");
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
    }
}
