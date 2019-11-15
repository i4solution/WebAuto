using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using bqImplementWeb;
using WebControl_V2.Class;

namespace WebControl_V2
{
    public partial class frmRegister : Form, IRegister
    {
        public frmRegister()
        {
            InitializeComponent();
        }
        public event EventHandler<LoginArgs> OnLogin;
        public event EventHandler<LoginArgs> OnRegister;
        public void Show()
        {
            lblInfo.Visible = false;
            lblEmailError.Visible = lblPassError.Visible = lblPhoneError.Visible = lblUserError.Visible = false;
            this.ShowDialog();
        }
        public void UpdateStatus(string status)
        {
            if (status == "OK")
            {
                lblInfo.Text = "THÀNH CÔNG";
                lblInfo.ForeColor = Color.LawnGreen;
                lblInfo.Visible = true;
                CGlobal._session.CreateSession(txtUserName.Text, 5);
            }
            else if (status == "FAIL")
            {
                lblInfo.Text = "THẤT BẠI";
                lblInfo.ForeColor = Color.Red;
                lblInfo.Visible = true;
            }
        }
        private void chkRegister_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRegister.Checked)
            {
                lblEmail.Visible = lblPhone.Visible = true;
                txtEmail.Visible = txtPhone.Visible = true;
                btnRegister.Visible = true;
                btnLogin.Visible = false;
            }
            else
            {
                lblEmail.Visible = lblPhone.Visible = false;
                txtEmail.Visible = txtPhone.Visible = false;
                btnRegister.Visible = false;
                btnLogin.Visible = true;
            }
        }
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        bool IsPhoneNumber(string number)
        {
            return true;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                lblUserError.Visible = true;
            }
            if (txtPassword.Text == "")
            {
                lblPassError.Visible = true;
            }
            LoginArgs arg = new LoginArgs();
            arg.username = txtUserName.Text;
            arg.pass = txtPassword.Text;
            OnLogin(sender, arg);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                lblUserError.Visible = true;
            }
            if (txtPassword.Text == "")
            {
                lblPassError.Visible = true;
            }
            if (IsValidEmail(txtEmail.Text) == false)
            {
                lblEmailError.Visible = true;
            }
            if (IsPhoneNumber(txtPhone.Text) == false)
            {
                lblPhoneError.Visible = true;
            }
            LoginArgs arg = new LoginArgs();
            arg.username = txtUserName.Text;
            arg.pass = txtPassword.Text;
            arg.email = txtEmail.Text;
            arg.phone = txtPhone.Text;
            OnRegister(sender, arg);
        }

        private void txtUserName_Enter(object sender, EventArgs e)
        {
            lblUserError.Visible = false;
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            lblPassError.Visible = false;
        }

        private void txtEmail_Enter(object sender, EventArgs e)
        {
            lblEmailError.Visible = false;
        }

        private void txtPhone_Enter(object sender, EventArgs e)
        {
            lblPhoneError.Visible = false;
        }
    }
}
