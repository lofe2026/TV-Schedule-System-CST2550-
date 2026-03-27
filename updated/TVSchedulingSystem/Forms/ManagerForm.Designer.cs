namespace TVSchedulingSystem.Forms
{
    partial class ManagerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
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
            btnSelectImage = new Button();
            dataGridView1 = new DataGridView();
            label1 = new Label();
            panel1 = new Panel();
            picturePreview = new PictureBox();
            lblProgramTitle = new Label();
            lblClock = new Label();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numDuration).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picturePreview).BeginInit();
            SuspendLayout();
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
            tableLayoutPanel1.Size = new Size(1112, 603);
            tableLayoutPanel1.TabIndex = 1;
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
            tableLayoutPanel2.Size = new Size(752, 174);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // cmbChannel
            // 
            cmbChannel.Dock = DockStyle.Fill;
            cmbChannel.FormattingEnabled = true;
            cmbChannel.Location = new Point(191, 3);
            cmbChannel.Name = "cmbChannel";
            cmbChannel.Size = new Size(558, 28);
            cmbChannel.TabIndex = 9;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Location = new Point(3, 0);
            label2.Name = "label2";
            label2.Size = new Size(182, 43);
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
            label3.Size = new Size(182, 43);
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
            label4.Size = new Size(182, 43);
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
            label5.Size = new Size(182, 45);
            label5.TabIndex = 3;
            label5.Text = "Duration";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtProgramId
            // 
            txtProgramId.Dock = DockStyle.Fill;
            txtProgramId.Location = new Point(191, 46);
            txtProgramId.Name = "txtProgramId";
            txtProgramId.Size = new Size(558, 27);
            txtProgramId.TabIndex = 5;
            // 
            // dtpStartTime
            // 
            dtpStartTime.Dock = DockStyle.Fill;
            dtpStartTime.Location = new Point(191, 89);
            dtpStartTime.Name = "dtpStartTime";
            dtpStartTime.Size = new Size(558, 27);
            dtpStartTime.TabIndex = 6;
            // 
            // numDuration
            // 
            numDuration.Dock = DockStyle.Fill;
            numDuration.Location = new Point(191, 132);
            numDuration.Name = "numDuration";
            numDuration.Size = new Size(558, 27);
            numDuration.TabIndex = 7;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(btnAdd);
            flowLayoutPanel1.Controls.Add(btnSuggest);
            flowLayoutPanel1.Controls.Add(btnSelectImage);
            flowLayoutPanel1.Dock = DockStyle.Top;
            flowLayoutPanel1.Location = new Point(3, 243);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(752, 50);
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
            // 
            // btnSelectImage
            // 
            btnSelectImage.BackColor = Color.RoyalBlue;
            btnSelectImage.Cursor = Cursors.Hand;
            btnSelectImage.Dock = DockStyle.Fill;
            btnSelectImage.FlatStyle = FlatStyle.Flat;
            btnSelectImage.ForeColor = Color.White;
            btnSelectImage.Location = new Point(315, 3);
            btnSelectImage.Name = "btnSelectImage";
            btnSelectImage.Size = new Size(150, 35);
            btnSelectImage.TabIndex = 4;
            btnSelectImage.Text = "Select Preview";
            btnSelectImage.UseVisualStyleBackColor = false;
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
            dataGridView1.Size = new Size(752, 297);
            dataGridView1.TabIndex = 3;
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
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel1.BackColor = Color.Black;
            panel1.Controls.Add(picturePreview);
            panel1.Location = new Point(761, 63);
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
            lblProgramTitle.Location = new Point(761, 240);
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
            lblClock.Location = new Point(761, 300);
            lblClock.Name = "lblClock";
            lblClock.Size = new Size(348, 55);
            lblClock.TabIndex = 6;
            lblClock.Text = "00:00:00";
            lblClock.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ManagerForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1112, 603);
            Controls.Add(tableLayoutPanel1);
            Name = "ManagerForm";
            Text = "ManagerForm";
            Load += ManagerForm_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numDuration).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picturePreview).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label lblClock;
        private PictureBox picturePreview;
        private Panel panel1;
        private ComboBox cmbChannel;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox txtProgramId;
        private DateTimePicker dtpStartTime;
        private Label label1;
        private Button btnRemove;
        private Button btnSelectImage;
        private NumericUpDown numDuration;
        private Button btnAdd;
        private TableLayoutPanel tableLayoutPanel2;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button btnSuggest;
        private DataGridView dataGridView1;
        private Label lblProgramTitle;
        private TableLayoutPanel tableLayoutPanel1;
    }
}