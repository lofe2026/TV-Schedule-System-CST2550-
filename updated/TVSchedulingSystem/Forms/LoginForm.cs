using System;
using System.Windows.Forms;
using TVSchedulingSystem.Services;

namespace TVSchedulingSystem.Forms
{
    public partial class LoginForm : Form
    {
        private readonly UserService _service;

        public LoginForm()
        {
            InitializeComponent();

            _service = new UserService();

            cmbRole.Items.Clear();
            cmbRole.Items.Add("Admin");
            cmbRole.Items.Add("Manager");
            cmbRole.Items.Add("Client");
            cmbRole.SelectedIndex = 0;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                cmbRole.SelectedItem == null)
            {
                MessageBox.Show("Please fill all login fields.");
                return;
            }

            string selectedRole = cmbRole.SelectedItem.ToString();

            var user = _service.Login(
                txtUsername.Text.Trim(),
                txtPassword.Text.Trim(),
                selectedRole
            );

            if (user == null)
            {
                MessageBox.Show("Invalid login details.");
                return;
            }

            MessageBox.Show("Login successful!");

            if (user.Role == "Admin")
            {
                MainForm adminForm = new MainForm();
                adminForm.Show();
            }
            else if (user.Role == "Manager")
            {
                ManagerForm managerForm = new ManagerForm();
                managerForm.Show();
            }
            else if (user.Role == "Client")
            {
                ClientForm clientForm = new ClientForm();
                clientForm.Show();
            }

            this.Hide();
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            if (cmbRole.SelectedItem == null)
            {
                MessageBox.Show("Please select a role.");
                return;
            }

            string selectedRole = cmbRole.SelectedItem.ToString();

            if (selectedRole != "Client")
            {
                MessageBox.Show("Only Client can sign up.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter username and password.");
                return;
            }

            bool success = _service.Register(
                txtUsername.Text.Trim(),
                txtPassword.Text.Trim(),
                selectedRole
            );

            if (!success)
            {
                MessageBox.Show("Registration failed. Username may already exist.");
                return;
            }

            MessageBox.Show("Client account created successfully!");
        }

        private void btnViewerAccess_Click(object sender, EventArgs e)
        {
            ViewerForm viewerForm = new ViewerForm();
            viewerForm.Show();
            this.Hide();
        }
    }
}