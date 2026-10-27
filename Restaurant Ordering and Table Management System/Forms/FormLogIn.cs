using System;
using System.Windows.Forms;
using Restaurant_Ordering_and_Management_System.Models;
using Restaurant_Ordering_and_Management_System.Service;

namespace Restaurant_Ordering_and_Management_System.Forms
{
    public partial class FormLogIn : Form
    {
        private readonly AppServices _services;

        public FormLogIn(AppServices services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
            InitializeComponent();

            string fullText = "Don't have an account? Sign Up";
            string linkText = "Sign Up";

            lblSignUp.Text = fullText;
            Load += FormLogIn_Load;

            int startIndex = fullText.IndexOf(linkText);
            if (startIndex >= 0)
            {
                lblSignUp.LinkArea = new LinkArea(startIndex, linkText.Length);
            }
        }

        private void FormLogIn_Load(object sender, EventArgs e)
        {
            RefreshSignupAvailability();
        }

        private void RefreshSignupAvailability()
        {
            try
            {
                lblSignUp.Visible = !_services.Accounts.HasAccounts();
                btnLogin.Enabled = true;
            }
            catch (Exception ex)
            {
                btnLogin.Enabled = false;
                lblSignUp.Visible = false;
                MessageBox.Show("Could not check account setup: " + ex.Message,
                    "Account Setup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                AuthenticatedUser account = _services.Accounts.Authenticate(txtUsername.Text, txtPassword.Text);
                if (account == null)
                {
                    MessageBox.Show("Invalid username or password.", "Sign In Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtUsername.Focus();
                    return;
                }

                Hide();
                using (Form1 mainForm = new Form1(_services, account))
                {
                    DialogResult result = mainForm.ShowDialog(this);
                    if (result == DialogResult.Retry)
                    {
                        txtPassword.Clear();
                        Show();
                        Activate();
                    }
                    else
                    {
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sign in could not be completed: " + ex.Message,
                    "Sign In Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pnlBody_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblSignUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (FormSignUp signUpForm = new FormSignUp(_services.Accounts))
            {
                if (signUpForm.ShowDialog(this) == DialogResult.OK)
                {
                    txtUsername.Text = signUpForm.CreatedUsername;
                    txtPassword.Clear();
                }
            }

            RefreshSignupAvailability();
        }
    }
}
 