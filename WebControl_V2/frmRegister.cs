﻿using System;
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
                CGlobal._session.CreateSession(txtUserName.Text, 4320);
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
            //Regex validator = new Regex("^[3-9][0-9]{9}$");

            //return validator.Match(number).Success;
            return true;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            bool acceptData = true;
            lblInfo.Visible = false;
            if (txtUserName.Text == "")
            {
                lblUserError.Visible = true;
                acceptData = false;
            }
            if (txtPassword.Text == "")
            {
                lblPassError.Visible = true;
                acceptData = false;
            }
            if (acceptData == false)
                return;
            LoginArgs arg = new LoginArgs();
            arg.username = txtUserName.Text;
            arg.pass = txtPassword.Text;
            OnLogin(sender, arg);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            bool acceptData = true;
            if (txtUserName.Text == "")
            {
                lblUserError.Visible = true;
                acceptData = false;
            }
            if (txtPassword.Text == "")
            {
                lblPassError.Visible = true;
                acceptData = false;
            }
            if (txtPhone.Text == "")
            {
                lblPhoneError.Visible = true;
                acceptData = false;
            }
            if (IsValidEmail(txtEmail.Text) == false)
            {
                lblEmailError.Visible = true;
                acceptData = false;
            }
            if (IsPhoneNumber(txtPhone.Text) == false)
            {
                lblPhoneError.Visible = true;
                acceptData = false;
            }
            if (acceptData == false)
                return;
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

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) == false && (Keys)(e.KeyChar) != Keys.Back)
                e.Handled = true;
        }
    }
}
