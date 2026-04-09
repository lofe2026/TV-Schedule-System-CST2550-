using System.Drawing;
using System.Windows.Forms;

namespace TVSchedulingSystem.Forms
{
    partial class ClientForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private Button btnBack;
        private ComboBox cmbChannel;
        private Label lblChannel;
        private DataGridView dataGridView1;
        private Panel panelPreview;
        private PictureBox picturePreview;
        private Label lblProgramTitle;
        private Label lblClock;
        private TableLayoutPanel tableLayoutPanelMain;
        private TableLayoutPanel tableLayoutPanelTop;
        private TableLayoutPanel tableLayoutPanelContent;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            if (disposing && picturePreview != null && picturePreview.Image != null)
            {
                picturePreview.Image.Dispose();
                picturePreview.Image = null;
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblTitle = new Label();
            btnBack = new Button();
            cmbChannel = new ComboBox();
            lblChannel = new Label();
            dataGridView1 = new DataGridView();
            panelPreview = new Panel();
            picturePreview = new PictureBox();
            lblProgramTitle = new Label();
            lblClock = new Label();
            tableLayoutPanelMain = new TableLayoutPanel();
            tableLayoutPanelTop = new TableLayoutPanel();
            tableLayoutPanelContent = new TableLayoutPanel();

            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panelPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picturePreview).BeginInit();
            tableLayoutPanelMain.SuspendLayout();
            tableLayoutPanelTop.SuspendLayout();
            tableLayoutPanelContent.SuspendLayout();
            SuspendLayout();

            // lblTitle
            lblTitle.BackColor = Color.RoyalBlue;
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("Tempus Sans ITC", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(3, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(994, 60);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "LOFE TV SCHEDULING SYSTEM";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;

            // btnBack
            btnBack.Dock = DockStyle.Fill;
            btnBack.Location = new Point(1003, 3);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(314, 54);
            btnBack.TabIndex = 1;
            btnBack.Text = "Back to Login";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click_1;

            // lblChannel
            lblChannel.Dock = DockStyle.Fill;
            lblChannel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblChannel.Location = new Point(3, 0);
            lblChannel.Name = "lblChannel";
            lblChannel.Size = new Size(114, 45);
            lblChannel.TabIndex = 2;
            lblChannel.Text = "Channel";
            lblChannel.TextAlign = ContentAlignment.MiddleLeft;

            // cmbChannel
            cmbChannel.Dock = DockStyle.Left;
            cmbChannel.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbChannel.FormattingEnabled = true;
            cmbChannel.Location = new Point(123, 8);
            cmbChannel.Margin = new Padding(3, 8, 3, 3);
            cmbChannel.Name = "cmbChannel";
            cmbChannel.Size = new Size(180, 28);
            cmbChannel.TabIndex = 3;

            // dataGridView1
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 3);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 70;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(914, 564);
            dataGridView1.TabIndex = 4;

            // panelPreview
            panelPreview.BackColor = Color.Black;
            panelPreview.Controls.Add(picturePreview);
            panelPreview.Dock = DockStyle.Fill;
            panelPreview.Location = new Point(923, 3);
            panelPreview.Name = "panelPreview";
            panelPreview.Padding = new Padding(8);
            panelPreview.Size = new Size(394, 564);
            panelPreview.TabIndex = 5;

            // picturePreview
            picturePreview.BackColor = Color.Black;
            picturePreview.Dock = DockStyle.Fill;
            picturePreview.Location = new Point(8, 8);
            picturePreview.Name = "picturePreview";
            picturePreview.Size = new Size(378, 548);
            picturePreview.SizeMode = PictureBoxSizeMode.Zoom;
            picturePreview.TabIndex = 0;
            picturePreview.TabStop = false;

            // lblProgramTitle
            lblProgramTitle.BackColor = Color.Black;
            lblProgramTitle.Dock = DockStyle.Fill;
            lblProgramTitle.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblProgramTitle.ForeColor = Color.White;
            lblProgramTitle.Location = new Point(923, 570);
            lblProgramTitle.Name = "lblProgramTitle";
            lblProgramTitle.Size = new Size(394, 50);
            lblProgramTitle.TabIndex = 6;
            lblProgramTitle.Text = "Program Preview";
            lblProgramTitle.TextAlign = ContentAlignment.MiddleCenter;

            // lblClock
            lblClock.Dock = DockStyle.Fill;
            lblClock.Font = new Font("Consolas", 22F, FontStyle.Bold);
            lblClock.ForeColor = Color.Cyan;
            lblClock.Location = new Point(923, 620);
            lblClock.Name = "lblClock";
            lblClock.Size = new Size(394, 70);
            lblClock.TabIndex = 7;
            lblClock.Text = "00:00:00";
            lblClock.TextAlign = ContentAlignment.MiddleCenter;

            // tableLayoutPanelTop
            tableLayoutPanelTop.ColumnCount = 2;
            tableLayoutPanelTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75F));
            tableLayoutPanelTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelTop.Controls.Add(lblTitle, 0, 0);
            tableLayoutPanelTop.Controls.Add(btnBack, 1, 0);
            tableLayoutPanelTop.Dock = DockStyle.Fill;
            tableLayoutPanelTop.Location = new Point(0, 0);
            tableLayoutPanelTop.Margin = new Padding(0);
            tableLayoutPanelTop.Name = "tableLayoutPanelTop";
            tableLayoutPanelTop.RowCount = 1;
            tableLayoutPanelTop.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelTop.Size = new Size(1320, 60);
            tableLayoutPanelTop.TabIndex = 8;

            // tableLayoutPanelContent
            tableLayoutPanelContent.ColumnCount = 2;
            tableLayoutPanelContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tableLayoutPanelContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutPanelContent.Controls.Add(dataGridView1, 0, 1);
            tableLayoutPanelContent.Controls.Add(panelPreview, 1, 1);
            tableLayoutPanelContent.Controls.Add(lblProgramTitle, 1, 2);
            tableLayoutPanelContent.Controls.Add(lblClock, 1, 3);
            tableLayoutPanelContent.Controls.Add(lblChannel, 0, 0);
            tableLayoutPanelContent.Controls.Add(cmbChannel, 0, 0);
            tableLayoutPanelContent.Dock = DockStyle.Fill;
            tableLayoutPanelContent.Location = new Point(0, 60);
            tableLayoutPanelContent.Margin = new Padding(0);
            tableLayoutPanelContent.Name = "tableLayoutPanelContent";
            tableLayoutPanelContent.RowCount = 4;
            tableLayoutPanelContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanelContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanelContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            tableLayoutPanelContent.Size = new Size(1320, 690);
            tableLayoutPanelContent.TabIndex = 9;

            // tableLayoutPanelMain
            tableLayoutPanelMain.ColumnCount = 1;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Controls.Add(tableLayoutPanelTop, 0, 0);
            tableLayoutPanelMain.Controls.Add(tableLayoutPanelContent, 0, 1);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.Location = new Point(0, 0);
            tableLayoutPanelMain.Margin = new Padding(0);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 2;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Size = new Size(1320, 750);
            tableLayoutPanelMain.TabIndex = 10;

            // ClientForm
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1320, 750);
            Controls.Add(tableLayoutPanelMain);
            Name = "ClientForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ClientForm";

            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panelPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picturePreview).EndInit();
            tableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanelTop.ResumeLayout(false);
            tableLayoutPanelContent.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
    }
}