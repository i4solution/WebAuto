using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebControl_V2.Class;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using bqImplementWeb;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace WebControl_V2
{
    public partial class frmMain : Form, IUpdateInterface
    {
        ArrayList jobLoad = new ArrayList();
        bqService test;
        CGoLike goLike;
        int timeCountDown = 0;
        string currentProfile = "";
        public frmMain()
        {            
            goLike = new CGoLike();
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Text = CGlobal.ver;

            btnLikeArticle.BackColor = Color.YellowGreen;
            btnLikeFanPage.BackColor = Color.YellowGreen;
            btnFollow.BackColor = Color.YellowGreen;

            currentProfile = Properties.Settings.Default.DefaultProfile;
            if (currentProfile != "")
            {
                if (File.Exists(currentProfile) == false)
                {
                    MessageBox.Show(this, "Không tồn tại mở file khi khởi động Web Auto.\\nHãy cài đặt lại.", "Web Auto - Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    using (System.IO.StreamReader fileIO = new System.IO.StreamReader(currentProfile))
                    {
                        string data = fileIO.ReadLine();
                        if (CGlobal.user == null)
                            CGlobal.user = new CUserAccount("", "");

                        CGlobal.user.DefineObject(data);
                        CGlobal.user = CGlobal.user.Decode;
                        if (CGlobal.user != null)
                        {
                            txtUserName.Text = CGlobal.user.User;
                            txtPassword.Text = CGlobal.user.Password;
                            txtGolikeDelay1.Text = CGlobal.user.InstricGoLikeDelay1.ToString();
                            txtFBDelay1.Text = CGlobal.user.InstricFBDelay1.ToString();
                            gridUser.Rows.Clear();
                            foreach (CLinkAccount ac in CGlobal.user.linkAccount.Values)
                            {
                                gridUser.Rows.Add(new object[] { ac.User, ac.Password, ac.JobCount.ToString(), ac.EnableJob });
                            }
                        }
                        //btnDefaultProfile.Enabled = true;
                        this.Text = CGlobal.ver + " - " + currentProfile.Split('\\')[currentProfile.Split('\\').Length - 1];
                    }
                }
            }

            //gridUser.Rows.Add(new object[] { "826418106", "abcd1234@", "1" });
            //gridUser.Rows.Add(new object[] { "833821537", "abcd1234@", "1" , true});
            gridUser.CellFormatting += new DataGridViewCellFormattingEventHandler(gridUser_CellFormatting);
            gridUser.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(gridUser_EditingControlShowing);
        }
        public void LoadJobs(System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> Job)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    gridJob.Rows.Clear();
                    for (int i = 0; i < Job.Count; i++)
                        gridJob.Rows.Add(new object[] { "", Job[i].Text, "Wait" });
                });
                return;
            }
            
        }
        public void UpdateJob(int id, string JobID, string status)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    if (id < gridJob.Rows.Count)
                    {
                        gridJob.Rows[id].Cells[0].Value = JobID;
                        gridJob.Rows[id].Cells[2].Value = status;
                    }
                    if (status.Equals("Thanh Cong"))
                    {
                        gridJobReport.Rows.Add(new object[] { JobID, DateTime.Now.ToString("dd/MM/yyyy H:mm:ss"), status });
                    }
                });
            }
        }
        public void UpdateProgress(string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    lblProgress.Text = text;
                    if (text.Contains(":)"))
                    {
                        gridJob.Rows.Clear();
                        lblProgress.Text = "...";
                    }
                });
            }
        }
        public void UpdateAccountJob(string id, string settingJobCount, string jobCount)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    foreach (DataGridViewRow r in gridUser.Rows)
                    {
                        if (r.Cells[0].Value.Equals(id))
                            r.Cells[2].Value = jobCount + "/" + settingJobCount;
                    }
                });
            }
        }
        public void UpdateAccount(string id, string value)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    if (id == "So du")
                    {
                        lblMoney.Text = value;
                    }
                    else if (id == "Cho duyet")
                    {
                        lblCheck.Text = value;
                    }
                    else if (id == "Can lam lai")
                    {
                        lblRedo.Text = value;
                    }
                    else if (id == "Timer")
                    {
                        lblCountDown.Text = value;
                    }
                    else if (id == "facebook")
                    {
                        lblFaceAccount.Text = "Tai khoan Face: " + value;
                    }
                });
            }
        }
        void gridUser_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (gridUser.Columns[e.ColumnIndex].Index == 1)
            {
                gridUser.Rows[e.RowIndex].Tag = e.Value;
                e.Value = new String('\u25CF', e.Value.ToString().Length);
            }
        }

        void gridUser_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (gridUser.CurrentCell.ColumnIndex == 1)//select target column
            {
                TextBox textBox = e.Control as TextBox;
                if (textBox != null)
                {
                    textBox.UseSystemPasswordChar = true;
                }
            }
            else
            {
                TextBox textBox = e.Control as TextBox;
                if (textBox != null)
                {
                    textBox.UseSystemPasswordChar = false;
                }
            }
        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (test != null)
                test.Quit();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (btnRun.Text == "Làm việc")
            {
                CGlobal._pauseJob = false;
                btnRun.Text = "Tạm ngưng";                
            }
            else if (btnRun.Text == "Tạm ngưng")
            {
                CGlobal._pauseJob = true;
                btnRun.Text = "Làm tiếp";                
                UpdateProgress("Đợi điểm tạm ngưng");
                return;
            }
            else if (btnRun.Text == "Làm tiếp")
            {
                CGlobal._pauseJob = false;
                btnRun.Text = "Tạm ngưng";
                UpdateProgress("");
                return;
            }
            if (txtUserName.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show(this, "Hãy kiểm tra lại Tên đăng nhập hoăc Mật khẩu.", "Web Auto - Sai dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (CGlobal.user == null)
                CGlobal.user = new CUserAccount(txtUserName.Text, txtPassword.Text);
            try
            {
                CGlobal.user.GoLikeDelay1 = Int32.Parse(txtGolikeDelay1.Text);
                CGlobal.user.FBDelay1 = Int32.Parse(txtFBDelay1.Text);
            }
            catch (Exception ii)
            {
                MessageBox.Show(this, "Hãy kiểm tra lại thời gian.", "Web Auto - Sai dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            
            foreach (DataGridViewRow r in gridUser.Rows)
            {
                if (r.Cells[0].Value != null && r.Cells[1].Value != null && r.Cells[2].Value != null)
                {
                    if (r.Cells[0].Value.ToString() == "-1" || r.Cells[2].Value.ToString() == "0")
                    {
                        MessageBox.Show(this, "Hay kiem tra lai tai khoan FB", "Web Auto - Sai du lieu", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    if ((bool)(r.Cells[3].Value) == true)
                    {
                        CGlobal.user.ReadJobCount("facebook", r.Cells[0].Value.ToString());
                        if (r.Cells[2].Value.ToString().Contains("/"))
                        {
                            r.Cells[2].Value = CGlobal.user.ReadJobCount("facebook", r.Cells[0].Value.ToString());
                        }
                        if (CGlobal.user.addLinkAccount("facebook", r.Cells[0].Value.ToString(), r.Cells[1].Value.ToString(), Int32.Parse(r.Cells[2].Value.ToString()), (bool)(r.Cells[3].Value)) == false)
                        {
                            CGlobal.user.ResetJobCount("facebook", r.Cells[0].Value.ToString(), Int32.Parse(r.Cells[2].Value.ToString()));
                            CGlobal.user.ResetJobEnable("facebook", r.Cells[0].Value.ToString(), true);
                        }
                    }
                    else
                    {                        
                        if (CGlobal.user.addLinkAccount("facebook", r.Cells[0].Value.ToString(), r.Cells[1].Value.ToString(), Int32.Parse(r.Cells[2].Value.ToString()), (bool)(r.Cells[3].Value)) == false)
                        {
                            CGlobal.user.ResetJobEnable("facebook", r.Cells[0].Value.ToString(), false);
                        }
                    }
                }
            }

            Action<object> action = (object obj) =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    //btnRun.Enabled = false;
                    btnLoad.Enabled = false;
                    btnSave.Enabled = false;
                    btnLikeArticle.Enabled = false;
                    btnLikeFanPage.Enabled = false;
                    btnFollow.Enabled = false;
                    timeCountDown = 0;
                    timer1.Start();
                });
                
                foreach (string id in CGlobal.user.linkAccount.Keys)
                {
                    if (CGlobal.user.linkAccount[id].EnableJob == false)
                        continue;
                    while (CGlobal._pauseJob)
                    {
                        UpdateProgress("Tạm ngưng ..");
                        System.Threading.Thread.Sleep(550);
                        UpdateProgress("Tạm ngưng .....");
                    }
                    test = new bqChromeService();

                    test.Initialize();

                    test.Start();

                    //test.GotoURL("https://app.golike.net");

                    goLike.LinkAccount = CGlobal.user.linkAccount[id];
                    goLike.UpdateGUI = this;
                    goLike.DoJob(test);

                    test.Quit();
                }
                this.Invoke((MethodInvoker)delegate
                {
                    //btnRun.Enabled = true;
                    btnRun.Text = "Làm việc";
                    btnLoad.Enabled = true;
                    btnSave.Enabled = true;
                    btnLikeArticle.Enabled = true;
                    btnLikeFanPage.Enabled = true;
                    btnFollow.Enabled = true;
                    timeCountDown = 0;
                    lblCountDown.Text = "0";
                    timer1.Stop();
                });
            };
            Task t1 = new Task(action, "jobQB#1");
            t1.Start();            
        }

        private void btnLikeArticle_Click(object sender, EventArgs e)
        {
            if (btnLikeArticle.BackColor != Color.YellowGreen)
            {
                btnLikeArticle.BackColor = Color.YellowGreen;
                goLike.LikeArticel = true;
            }
            else
            {
                goLike.LikeArticel = false;
                btnLikeArticle.BackColor = SystemColors.ButtonFace;
            }
        }

        private void btnLikeFanPage_Click(object sender, EventArgs e)
        {
            if (btnLikeFanPage.BackColor != Color.YellowGreen)
            {
                btnLikeFanPage.BackColor = Color.YellowGreen;
                goLike.LikePage = true;
            }
            else
            {
                goLike.LikePage = false;
                btnLikeFanPage.BackColor = SystemColors.ButtonFace;
            }
        }

        private void btnFollow_Click(object sender, EventArgs e)
        {
            if (btnFollow.BackColor != Color.YellowGreen)
            {
                btnFollow.BackColor = Color.YellowGreen;
                goLike.FollowPage = true;
            }
            else
            {
                goLike.FollowPage = false;
                btnFollow.BackColor = SystemColors.ButtonFace;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            gridUser.Rows.Add(new object[] { "-1", "asyn", "0" });
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int idx = gridUser.CurrentRow.Index;
            gridUser.Rows.RemoveAt(idx);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CUserAccount user = new CUserAccount(txtUserName.Text, txtPassword.Text);
            foreach (DataGridViewRow r in gridUser.Rows)
            {
                if (r.Cells[0].Value != null && r.Cells[1].Value != null && r.Cells[2].Value != null)
                {
                    if (r.Cells[0].Value.ToString() == "-1" || r.Cells[2].Value.ToString() == "0")
                    {
                        MessageBox.Show(this, "Hay kiem tra lai tai khoan FB", "Web Auto - Sai du lieu", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    user.addLinkAccount("facebook", r.Cells[0].Value.ToString(), r.Cells[1].Value.ToString(), Int32.Parse(r.Cells[2].Value.ToString()), (bool)(r.Cells[3].Value));
                }
            }
            try
            {
                user.GoLikeDelay1 = Int32.Parse(txtGolikeDelay1.Text);
                user.FBDelay1 = Int32.Parse(txtFBDelay1.Text);
            }
            catch (Exception ii)
            {
                MessageBox.Show(this, "Hay kiem tra lai thoi gian.", "Web Auto - Sai du lieu", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            
            string file = "";
            using (SaveFileDialog openFileDialog = new SaveFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.CurrentDirectory + "\\profile";
                openFileDialog.Filter = "Web Auto (*.wbq)|*.wbq|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    file = openFileDialog.FileName;
                }
            }
            if (file != "")
            {
                using (System.IO.StreamWriter fileIO = new System.IO.StreamWriter(file))
                {
                    fileIO.Write(user.EndCode());
                }
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string file = "";
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.CurrentDirectory + "\\profile";
                openFileDialog.Filter = "Web Auto (*.wbq)|*.wbq|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    file = openFileDialog.FileName;                   
                }
            }
            if (file != "")
            {
                using (System.IO.StreamReader fileIO = new System.IO.StreamReader(file))
                {
                    string data = fileIO.ReadLine();
                    if (CGlobal.user == null)
                        CGlobal.user = new CUserAccount("", "");

                    CGlobal.user.DefineObject(data);
                    CGlobal.user = CGlobal.user.Decode;
                    if (CGlobal.user != null)
                    {
                        txtUserName.Text = CGlobal.user.User;
                        txtPassword.Text = CGlobal.user.Password;
                        txtGolikeDelay1.Text = CGlobal.user.InstricGoLikeDelay1.ToString();
                        txtFBDelay1.Text = CGlobal.user.InstricFBDelay1.ToString();
                        gridUser.Rows.Clear();
                        foreach (CLinkAccount ac in CGlobal.user.linkAccount.Values)
                        {
                            gridUser.Rows.Add(new object[] { ac.User, ac.Password, ac.JobCount.ToString(), ac.EnableJob});
                        }
                    }
                    currentProfile = file;
                    btnDefaultProfile.Enabled = true;
                    this.Text = CGlobal.ver + " - " + file.Split('\\')[file.Split('\\').Length - 1];
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Int32.TryParse(lblCountDown.Text, out timeCountDown))
            {
                timeCountDown -= 500;
                if (timeCountDown >= 0)
                {
                    lblCountDown.Text = timeCountDown.ToString();
                }
                else
                    lblCountDown.Text = "0";
            }
                     
        }

        private void btnDefaultProfile_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Bạn muốn cài đặt mở file khi khởi động Web Auto.", "Web Auto - Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                Properties.Settings.Default.DefaultProfile = currentProfile;
                Properties.Settings.Default.Save();
                MessageBox.Show(this, "Cài đặt mở file khi khởi động Web Auto thành công.", "Web Auto - Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
