namespace TVSchedulingSystem.Forms
{
    partial class ViewerForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dataGridView1;
        private Label lblClock;
        private Label lblProgramTitle;
        private PictureBox picturePreview;

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
            btnBack = new Button();
            lblClock = new Label();
            lblProgramTitle = new Label();
            picturePreview = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picturePreview).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeight = 29;
            // keep layout simple: dock to left so preview on right is visible
            dataGridView1.Dock = DockStyle.Left;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(740, 446);
            dataGridView1.TabIndex = 0;
            // 
            // btnBack
            // 
            btnBack.Dock = DockStyle.Bottom;
            btnBack.Location = new Point(0, 446);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(1000, 54);
            btnBack.TabIndex = 8;
            btnBack.Text = "Back to Login";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // lblClock
            // 
            lblClock.AutoSize = true;
            lblClock.Location = new Point(12, 9);
            lblClock.Name = "lblClock";
            lblClock.Size = new Size(70, 13);
            lblClock.TabIndex = 0;
            lblClock.Text = "00:00:00";
            // 
            // lblProgramTitle
            // 
            lblProgramTitle.AutoSize = false;
            lblProgramTitle.Location = new Point(760, 12);
            lblProgramTitle.Name = "lblProgramTitle";
            lblProgramTitle.Size = new Size(220, 24);
            lblProgramTitle.TabIndex = 9;
            lblProgramTitle.Text = "Program Preview";
            lblProgramTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picturePreview
            // 
            picturePreview.Location = new Point(760, 42);
            picturePreview.Name = "picturePreview";
            picturePreview.Size = new Size(220, 180);
            picturePreview.SizeMode = PictureBoxSizeMode.Zoom;
            picturePreview.TabIndex = 10;
            picturePreview.TabStop = false;
            // 
            // ViewerForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 500);
            Controls.Add(lblProgramTitle);
            Controls.Add(picturePreview);
            Controls.Add(lblClock);
            Controls.Add(btnBack);
            Controls.Add(dataGridView1);
            Name = "ViewerForm";    
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Viewer - Schedule Table";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)picturePreview).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnBack;
    }
}