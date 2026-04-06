namespace TVSchedulingSystem.Forms
{
    partial class ViewerForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dataGridView1;

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
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeight = 29;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1000, 500);
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
            // ViewerForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 500);
            Controls.Add(btnBack);
            Controls.Add(dataGridView1);
            Name = "ViewerForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Viewer - Schedule Table";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnBack;
    }
}