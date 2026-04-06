namespace TVSchedulingSystem.Forms
{
    partial class ClientForm
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
            picturePreview = new PictureBox();
            cmbChannel = new ComboBox();
            dataGridView1 = new DataGridView();
            tableLayoutPanel1 = new TableLayoutPanel();
            lblClock = new Label();
            label1 = new Label();
            btnBack = new Button();
            ((System.ComponentModel.ISupportInitialize)picturePreview).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // picturePreview
            // 
            picturePreview.Dock = DockStyle.Fill;
            picturePreview.Location = new Point(894, 3);
            picturePreview.Name = "picturePreview";
            picturePreview.Size = new Size(886, 363);
            picturePreview.SizeMode = PictureBoxSizeMode.CenterImage;
            picturePreview.TabIndex = 3;
            picturePreview.TabStop = false;
            // 
            // cmbChannel
            // 
            cmbChannel.Dock = DockStyle.Right;
            cmbChannel.FormattingEnabled = true;
            cmbChannel.Location = new Point(1632, 0);
            cmbChannel.Name = "cmbChannel";
            cmbChannel.Size = new Size(151, 28);
            cmbChannel.TabIndex = 4;
            cmbChannel.SelectedIndexChanged += cmbChannel_SelectedIndexChanged;
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = SystemColors.Control;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 372);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(885, 364);
            dataGridView1.TabIndex = 0;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(picturePreview, 1, 0);
            tableLayoutPanel1.Controls.Add(dataGridView1, 0, 1);
            tableLayoutPanel1.Controls.Add(lblClock, 1, 1);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(1783, 739);
            tableLayoutPanel1.TabIndex = 5;
            // 
            // lblClock
            // 
            lblClock.AutoSize = true;
            lblClock.Dock = DockStyle.Fill;
            lblClock.Font = new Font("Consolas", 28.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblClock.ForeColor = Color.Cyan;
            lblClock.Location = new Point(894, 369);
            lblClock.Name = "lblClock";
            lblClock.Size = new Size(886, 370);
            lblClock.TabIndex = 6;
            lblClock.Text = "00:00:00";
            lblClock.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Showcard Gothic", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(885, 369);
            label1.TabIndex = 7;
            label1.Text = "LOFE TV scheduling system";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnBack
            // 
            btnBack.Dock = DockStyle.Top;
            btnBack.Location = new Point(0, 0);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(1632, 29);
            btnBack.TabIndex = 6;
            btnBack.Text = "Back to Login";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click_1;
            // 
            // ClientForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1783, 739);
            Controls.Add(btnBack);
            Controls.Add(cmbChannel);
            Controls.Add(tableLayoutPanel1);
            Name = "ClientForm";
            Text = "ClientForm";
            ((System.ComponentModel.ISupportInitialize)picturePreview).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private PictureBox picturePreview;
        private ComboBox cmbChannel;
        private DataGridView dataGridView1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lblClock;
        private Label label1;
        private Button btnBack;
    }
}