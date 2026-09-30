using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_Ordering_and_Management_System.Forms
{
    public partial class FormLogIn : Form
    {
        public FormLogIn()
        {
            InitializeComponent();

            string fullText = "Don't have an account? Sign Up";
            string linkText = "Sign Up";

            lblSignUp.Text = fullText;

            int startIndex = fullText.IndexOf(linkText);
            if (startIndex >= 0)
            {
                lblSignUp.LinkArea = new LinkArea(startIndex, linkText.Length);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "admin" && txtPassword.Text == "password")
            {
                MessageBox.Show("Login Success", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                Form1 mainForm = new Form1();
                mainForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtUsername.Focus();
            }
        }

        private void pnlBody_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblSignUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormSignUp signUpForm = new FormSignUp();
            signUpForm.Show();

            this.Hide();

            signUpForm.FormClosed += (s, args) => this.Show();
        }
    }
}
 