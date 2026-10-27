using System;
using System.Windows.Forms;
using Restaurant_Ordering_and_Management_System.Interfaces;

namespace Restaurant_Ordering_and_Management_System.Forms
{
    public partial class FormSignUp : Form
    {
        private readonly IAccountService _accountService;

        public string CreatedUsername { get; private set; }

        public FormSignUp(IAccountService accountService)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            InitializeComponent();
            textBox4.UseSystemPasswordChar = true;
            textBox3.UseSystemPasswordChar = true;
            btnSignUp.Click += BtnSignUp_Click;
            linkLabel1.LinkClicked += LinkLabel1_LinkClicked;
            cbxPrivacyPolicy.CheckedChanged += CbxShowPassword_CheckedChanged;
            Text = "Create Administrator Account";
        }

        private void BtnSignUp_Click(object sender, EventArgs e)
        {
            if (!string.Equals(textBox4.Text, textBox3.Text, StringComparison.Ordinal))
            {
                MessageBox.Show("The passwords do not match.", "Create Account",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Clear();
                textBox3.Focus();
                return;
            }

            try
            {
                _accountService.RegisterFirstAdmin(textBox1.Text, textBox2.Text, textBox4.Text);
                CreatedUsername = textBox1.Text.Trim();
                MessageBox.Show("Administrator account created. You can now sign in.",
                    "Account Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not create the account: " + ex.Message,
                    "Create Account Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }

        private void CbxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            textBox4.UseSystemPasswordChar = !cbxPrivacyPolicy.Checked;
            textBox3.UseSystemPasswordChar = !cbxPrivacyPolicy.Checked;
        }
    }
}
