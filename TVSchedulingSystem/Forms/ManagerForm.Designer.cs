namespace TVSchedulingSystem.Forms
{
    partial class ManagerForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

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
            btnSelectImage = new Button();
            btnAddProgram = new Button();
            lblProgramTitle = new Label();
            lblClock = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numDuration).BeginInit();
            panelPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picturePreview).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = System.Drawing.Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new System.Drawing.Point(3, 303);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            tableLayoutPanel1.SetRowSpan(dataGridView1, 2);
            dataGridView1.RowTemplate.Height = 70;
            dataGridView1.Size = new System.Drawing.Size(954, 458);
            dataGridView1.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = System.Drawing.Color.WhiteSmoke;
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
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 180F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new System.Drawing.Size(1413, 764);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = System.Drawing.Color.RoyalBlue;
            label1.Dock = DockStyle.Fill;
            label1.Font = new System.Drawing.Font("Tempus Sans ITC", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label1.ForeColor = System.Drawing.Color.White;
            label1.Location = new System.Drawing.Point(3, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(954, 60);
            label1.TabIndex = 0;
            label1.Text = "TV Program Scheduling System - Manager";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnBack
            // 
            btnBack.Dock = DockStyle.Fill;
            btnBack.Location = new System.Drawing.Point(963, 3);
            btnBack.Name = "btnBack";
            btnBack.Size = new System.Drawing.Size(447, 54);
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
            tableLayoutPanel2.Location = new System.Drawing.Point(3, 63);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 5;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.Size = new System.Drawing.Size(954, 174);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // cmbChannel
            // 
            cmbChannel.Dock = DockStyle.Fill;
            cmbChannel.FormattingEnabled = true;
            cmbChannel.Location = new System.Drawing.Point(241, 3);
            cmbChannel.Name = "cmbChannel";
            cmbChannel.Size = new System.Drawing.Size(710, 28);
            cmbChannel.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Location = new System.Drawing.Point(3, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(232, 34);
            label2.TabIndex = 0;
            label2.Text = "Channel";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Fill;
            label3.Location = new System.Drawing.Point(3, 34);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(232, 34);
            label3.TabIndex = 1;
            label3.Text = "Program";
            label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Dock = DockStyle.Fill;
            label6.Location = new System.Drawing.Point(3, 68);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(232, 34);
            label6.TabIndex = 2;
            label6.Text = "Program ID";
            label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = DockStyle.Fill;
            label4.Location = new System.Drawing.Point(3, 102);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(232, 34);
            label4.TabIndex = 3;
            label4.Text = "Start Time";
            label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Dock = DockStyle.Fill;
            label5.Location = new System.Drawing.Point(3, 136);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(232, 38);
            label5.TabIndex = 4;
            label5.Text = "Duration";
            label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbProgram
            // 
            cmbProgram.Dock = DockStyle.Fill;
            cmbProgram.FormattingEnabled = true;
            cmbProgram.Location = new System.Drawing.Point(241, 37);
            cmbProgram.Name = "cmbProgram";
            cmbProgram.Size = new System.Drawing.Size(710, 28);
            cmbProgram.TabIndex = 1;
            // 
            // txtProgramId
            // 
            txtProgramId.Dock = DockStyle.Fill;
            txtProgramId.Location = new System.Drawing.Point(241, 71);
            txtProgramId.Name = "txtProgramId";
            txtProgramId.Size = new System.Drawing.Size(710, 27);
            txtProgramId.TabIndex = 2;
            // 
            // dtpStartTime
            // 
            dtpStartTime.Dock = DockStyle.Fill;
            dtpStartTime.Location = new System.Drawing.Point(241, 105);
            dtpStartTime.Name = "dtpStartTime";
            dtpStartTime.Size = new System.Drawing.Size(710, 27);
            dtpStartTime.TabIndex = 3;
            // 
            // numDuration
            // 
            numDuration.Dock = DockStyle.Fill;
            numDuration.Location = new System.Drawing.Point(241, 139);
            numDuration.Name = "numDuration";
            numDuration.Size = new System.Drawing.Size(710, 27);
            numDuration.TabIndex = 4;
            // 
            // panelPreview
            // 
            panelPreview.BackColor = System.Drawing.Color.Black;
            panelPreview.Controls.Add(picturePreview);
            panelPreview.Dock = DockStyle.Fill;
            panelPreview.Location = new System.Drawing.Point(963, 63);
            panelPreview.Name = "panelPreview";
            panelPreview.Padding = new Padding(8);
            panelPreview.Size = new System.Drawing.Size(447, 174);
            panelPreview.TabIndex = 4;
            // 
            // picturePreview
            // 
            picturePreview.BackColor = System.Drawing.Color.Black;
            picturePreview.Dock = DockStyle.Fill;
            picturePreview.Location = new System.Drawing.Point(8, 8);
            picturePreview.Name = "picturePreview";
            picturePreview.Size = new System.Drawing.Size(431, 158);
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
            flowLayoutPanel1.Controls.Add(btnAddProgram);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new System.Drawing.Point(3, 243);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new System.Drawing.Size(954, 54);
            flowLayoutPanel1.TabIndex = 2;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = System.Drawing.Color.RoyalBlue;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.ForeColor = System.Drawing.Color.White;
            btnAdd.Location = new System.Drawing.Point(3, 3);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new System.Drawing.Size(128, 35);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "Add Schedule";
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnSuggest
            // 
            btnSuggest.BackColor = System.Drawing.Color.RoyalBlue;
            btnSuggest.FlatStyle = FlatStyle.Flat;
            btnSuggest.ForeColor = System.Drawing.Color.White;
            btnSuggest.Location = new System.Drawing.Point(137, 3);
            btnSuggest.Name = "btnSuggest";
            btnSuggest.Size = new System.Drawing.Size(128, 35);
            btnSuggest.TabIndex = 1;
            btnSuggest.Text = "Suggest Slot";
            btnSuggest.UseVisualStyleBackColor = false;
            // 
            // btnRemove
            // 
            btnRemove.BackColor = System.Drawing.Color.RoyalBlue;
            btnRemove.FlatStyle = FlatStyle.Flat;
            btnRemove.ForeColor = System.Drawing.Color.White;
            btnRemove.Location = new System.Drawing.Point(271, 3);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new System.Drawing.Size(128, 35);
            btnRemove.TabIndex = 2;
            btnRemove.Text = "Remove";
            btnRemove.UseVisualStyleBackColor = false;
            // 
            // btnSelectImage
            // 
            btnSelectImage.BackColor = System.Drawing.Color.RoyalBlue;
            btnSelectImage.FlatStyle = FlatStyle.Flat;
            btnSelectImage.ForeColor = System.Drawing.Color.White;
            btnSelectImage.Location = new System.Drawing.Point(405, 3);
            btnSelectImage.Name = "btnSelectImage";
            btnSelectImage.Size = new System.Drawing.Size(128, 35);
            btnSelectImage.TabIndex = 3;
            btnSelectImage.Text = "Select Preview";
            btnSelectImage.UseVisualStyleBackColor = false;
            // 
            // btnAddProgram
            // 
            btnAddProgram.BackColor = System.Drawing.Color.MediumSeaGreen;
            btnAddProgram.FlatStyle = FlatStyle.Flat;
            btnAddProgram.ForeColor = System.Drawing.Color.White;
            btnAddProgram.Location = new System.Drawing.Point(539, 3);
            btnAddProgram.Name = "btnAddProgram";
            btnAddProgram.Size = new System.Drawing.Size(128, 35);
            btnAddProgram.TabIndex = 4;
            btnAddProgram.Text = "Add Program";
            btnAddProgram.UseVisualStyleBackColor = false;
            // 
            // lblProgramTitle
            // 
            lblProgramTitle.BackColor = System.Drawing.Color.Black;
            lblProgramTitle.Dock = DockStyle.Fill;
            lblProgramTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            lblProgramTitle.ForeColor = System.Drawing.Color.White;
            lblProgramTitle.Location = new System.Drawing.Point(963, 240);
            lblProgramTitle.Name = "lblProgramTitle";
            lblProgramTitle.Size = new System.Drawing.Size(447, 60);
            lblProgramTitle.TabIndex = 5;
            lblProgramTitle.Text = "Program Preview";
            lblProgramTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblClock
            // 
            lblClock.BackColor = System.Drawing.Color.White;
            lblClock.Dock = DockStyle.Fill;
            lblClock.Font = new System.Drawing.Font("Consolas", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lblClock.ForeColor = System.Drawing.Color.Orange;
            lblClock.Location = new System.Drawing.Point(963, 300);
            lblClock.Name = "lblClock";
            lblClock.Size = new System.Drawing.Size(447, 90);
            lblClock.TabIndex = 6;
            lblClock.Text = "00:00:00";
            lblClock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ManagerForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1413, 764);
            Controls.Add(tableLayoutPanel1);
            Name = "ManagerForm";
            Text = "TV Program Scheduling System - Manager";
            Load += ManagerForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numDuration).EndInit();
            panelPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picturePreview).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Button btnBack;
        private TableLayoutPanel tableLayoutPanel2;
        private ComboBox cmbChannel;
        private Label label2;
        private Label label3;
        private Label label6;
        private Label label4;
        private Label label5;
        private ComboBox cmbProgram;
        private TextBox txtProgramId;
        private DateTimePicker dtpStartTime;
        private NumericUpDown numDuration;
        private Panel panelPreview;
        private PictureBox picturePreview;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button btnAdd;
        private Button btnSuggest;
        private Button btnRemove;
        private Button btnSelectImage;
        private Button btnAddProgram;
        private Label lblProgramTitle;
        private Label lblClock;
    }
}