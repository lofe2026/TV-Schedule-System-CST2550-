namespace TVSchedulingSystem.Forms
{
    partial class LoginForm
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
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            btnLogin = new Button();
            btnSignup = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            cmbRole = new ComboBox();
            btnViewerAccess = new Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // txtUsername
            // 
            txtUsername.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtUsername.Location = new Point(267, 3);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(266, 27);
            txtUsername.TabIndex = 0;
            // 
            // txtPassword
            // 
            txtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtPassword.Location = new Point(267, 75);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(266, 27);
            txtPassword.TabIndex = 1;
            // 
            // btnLogin
            // 
            btnLogin.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnLogin.Location = new Point(267, 228);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(266, 69);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnSignup
            // 
            btnSignup.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnSignup.Location = new Point(267, 303);
            btnSignup.Name = "btnSignup";
            btnSignup.Size = new Size(266, 69);
            btnSignup.TabIndex = 5;
            btnSignup.Text = "Signup";
            btnSignup.UseVisualStyleBackColor = true;
            btnSignup.Click += btnSignup_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            tableLayoutPanel1.Controls.Add(btnSignup, 1, 4);
            tableLayoutPanel1.Controls.Add(btnLogin, 1, 3);
            tableLayoutPanel1.Controls.Add(cmbRole, 1, 2);
            tableLayoutPanel1.Controls.Add(btnViewerAccess, 1, 5);
            tableLayoutPanel1.Controls.Add(txtPassword, 1, 1);
            tableLayoutPanel1.Controls.Add(txtUsername, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 19.25926F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20.74074F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
            tableLayoutPanel1.Size = new Size(800, 450);
            tableLayoutPanel1.TabIndex = 6;
            // 
            // cmbRole
            // 
            cmbRole.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.FormattingEnabled = true;
            cmbRole.Location = new Point(267, 153);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new Size(266, 28);
            cmbRole.TabIndex = 3;
            // 
            // btnViewerAccess
            // 
            btnViewerAccess.Dock = DockStyle.Fill;
            btnViewerAccess.Location = new Point(267, 378);
            btnViewerAccess.Name = "btnViewerAccess";
            btnViewerAccess.Size = new Size(266, 69);
            btnViewerAccess.TabIndex = 6;
            btnViewerAccess.Text = "Continue as Viewer";
            btnViewerAccess.UseVisualStyleBackColor = true;
            btnViewerAccess.Click += btnViewerAccess_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayoutPanel1);
            Name = "LoginForm";
            Text = "LoginForm";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnSignup;
        private TableLayoutPanel tableLayoutPanel1;
        private ComboBox cmbRole;
        private Button btnViewerAccess;
    }
}