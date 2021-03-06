﻿using System;
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
        IJobAcquist goLike;
        int timeCountDown = 0;
        Task t1 = null;
        string currentProfile = "";
        string _msg = "";
        bool startDoJob = false;
        public frmMain(string msg)
        {
            goLike = new CGoLike();
            CGlobal._registerPresenter = new CBqRegisterPresenter();
            CGlobal._session = new CSession();
            CGlobal._session.sessionTimeOut += new EventHandler(OnSessionTimeOut);
            CGlobal._session.sessionTrigger += new EventHandler(OnSessionTrigger);
            CEventLog.Log.EnableLog = true;

            InitializeComponent();
            if (msg != "")
            {
                _msg = msg;
                this.Text = "Không có INTERNET";
            }
        }
        private void OnSessionTrigger(object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                if (CGlobal._session.Username != "")
                    //this.Text = CGlobal.ver;
                    lblRegister.Visible = false;
                else
                    //this.Text = CGlobal.ver + " - Hãy đăng ký để ủng hộ sản phẩm";
                    lblRegister.Visible = true;
            });
            

        }
        private void OnSessionTimeOut(object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                if (CGlobal._session.Username != "")
                    //this.Text = CGlobal.ver;
                    lblRegister.Visible = false;
                else
                    //this.Text = CGlobal.ver + " - Hãy đăng ký để ủng hộ sản phẩm";
                    lblRegister.Visible = true;
            });

        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            if (_msg == "")
                this.Text = CGlobal.ver;
            this.Invoke((MethodInvoker)delegate
            {
                if (CGlobal._session.Username != "")
                    //this.Text = CGlobal.ver;
                    lblRegister.Visible = false;
                else
                    //this.Text = CGlobal.ver + " - Hãy đăng ký để ủng hộ sản phẩm";
                    lblRegister.Visible = true;
            });

            btnLikeArticle.BackColor = Color.YellowGreen;
            btnLikeFanPage.BackColor = Color.YellowGreen;
            btnFollow.BackColor = Color.YellowGreen;

            gridUser.Columns["colFaceCount"].Visible = chkLinkCondition.Checked;
            gridUser.Columns["colMaxLink"].Visible = chkLinkCondition.Checked;

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
                            txtFaultLimit.Text = CGlobal.user.LimitFault.ToString();
                            chkEnableRedo.Checked = CGlobal.user.EnableRedoJob;
                            chkJobLessMoney.Checked = CGlobal.user.JobLessMoney;
                            chkLinkCondition.Checked = CGlobal.user.CheckLinkCondition;
                            gridUser.Rows.Clear();
                            foreach (CLinkAccount ac in CGlobal.user.linkAccount.Values)
                            {
                                string t = "";
                                if (ac.Type == "facebook")
                                    t = "Facebook";
                                else
                                    t = ac.Type;
                                gridUser.Rows.Add(new object[] { t, ac.User, ac.Password, ac.EnableJob, ac.JobCount.ToString(), ac.JobCountMax.ToString(), ac.JobCountFB.ToString(), ac.JobCountFBMax.ToString(), ac.AccountStatus });
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

            CGlobal._session.CreateSession("bq", 1800);
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
                    if (status.Equals("Thanh Cong") || status.Equals("That bai"))
                    {
                        if (status.Equals("Thanh Cong"))
                        {
                            int job = 0;
                            Int32.TryParse(lblJobSuccess.Text, out job);
                            lblJobSuccess.Text = (job + 1).ToString();
                        }
                        else if (status.Equals("That bai"))
                        {
                            int job = 0;
                            Int32.TryParse(lblJobFault.Text, out job);
                            lblJobFault.Text = (job + 1).ToString();
                        }
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
                    if (text == "GoLike Bao Tri !!!")
                    {
                        startDoJob = false;
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
                        if (r.Cells["colUser"].Value.Equals(id))
                            r.Cells["colJobCount"].Value = jobCount + "/" + settingJobCount;
                    }
                });
            }
        }
        public void UpdateAccountJobFB(string id, string settingJobCount, string jobCount)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    foreach (DataGridViewRow r in gridUser.Rows)
                    {
                        if (r.Cells["colUser"].Value.Equals(id))
                            r.Cells["colFaceCount"].Value = jobCount + "/" + settingJobCount;
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
                    else if (id.ToLower() == "facebook")
                    {
                        lblFaceAccount.Text = "Tai khoan Facebook: " + value;
                    }
                    else if (id.ToLower() == "instagram")
                    {
                        lblFaceAccount.Text = "Tai khoan Instagram: " + value;
                    }
                    else
                    {
                        string[] v = value.Split('#');
                        foreach (DataGridViewRow r in gridUser.Rows)
                        {
                            if (v[0] == "Status")
                            {
                                if (r.Cells["colUser"].Value.ToString() == id &&
                                    r.Cells["colAccount"].Value.ToString() == v[1])
                                {
                                    r.Cells["colAccountStatus"].Value = v[2];
                                    //r.Cells["colEnable"].Value = false;
                                }
                            } 
                            else if (v[0] == "Uncheck")
                            {
                                if (r.Cells["colUser"].Value.ToString() == id &&
                                    r.Cells["colAccount"].Value.ToString() == v[1])
                                {
                                    r.Cells["colAccountStatus"].Value = v[2];
                                    r.Cells["colEnable"].Value = false;
                                }
                            }
                        }
                    }
                });
            }
        }
        void gridUser_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (gridUser.Columns[e.ColumnIndex].Name == "colPass")
            {
                gridUser.Rows[e.RowIndex].Tag = e.Value;
                if (btnShowPassword.Tag.ToString() == "Hide")
                    e.Value = new String('\u25CF', e.Value.ToString().Length);
            }
        }

        void gridUser_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (gridUser.CurrentCell.OwningColumn.Name == "colPass")//select target column
            {
                TextBox textBox = e.Control as TextBox;
                if (textBox != null)
                {
                    if (btnShowPassword.Tag.ToString() == "Hide")
                        textBox.UseSystemPasswordChar = true;
                    else
                    {
                        textBox.UseSystemPasswordChar = false;
                    }
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
                if (t1 != null)
                {
                    if (t1.Status == TaskStatus.Running)
                        return;
                }
            }
            if (txtUserName.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show(this, "Hãy kiểm tra lại Tên đăng nhập hoăc Mật khẩu.", "Web Auto - Sai dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (CGlobal.user == null)
            {
                CGlobal.user = new CUserAccount(txtUserName.Text, txtPassword.Text);
                CGlobal.user.EnableRedoJob = chkEnableRedo.Checked;
                CGlobal.user.JobLessMoney = chkJobLessMoney.Checked;
            }
            try
            {
                CGlobal.user.GoLikeDelay1 = Int32.Parse(txtGolikeDelay1.Text);
                CGlobal.user.FBDelay1 = Int32.Parse(txtFBDelay1.Text);
                CGlobal.user.LimitFault = Int32.Parse(txtFaultLimit.Text);
                CGlobal.user.EnableRedoJob = chkEnableRedo.Checked;
                CGlobal.user.JobLessMoney = chkJobLessMoney.Checked;
                CGlobal.user.CheckLinkCondition = chkLinkCondition.Checked;
            }
            catch (Exception ii)
            {
                MessageBox.Show(this, "Hãy kiểm tra lại giá trị nhập.", "Web Auto - Sai dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            
            foreach (DataGridViewRow r in gridUser.Rows)
            {
                if (r.Cells["colUser"].Value != null && r.Cells["colPass"].Value != null && r.Cells["colJobCount"].Value != null && r.Cells["colFaceCount"].Value != null)
                {
                    int job = 0;
                    if (r.Cells["colUser"].Value.ToString() == "-1" || r.Cells["colUser"].Value.ToString() == "" || r.Cells["colPass"].Value.ToString() == "" ||
                        Int32.TryParse(r.Cells["colJobCount"].Value.ToString(),out job) == false || Int32.TryParse(r.Cells["colFaceCount"].Value.ToString(), out job) == false ||
                        Int32.TryParse(r.Cells["colMaxGoLike"].Value.ToString(), out job) == false || Int32.TryParse(r.Cells["colMaxLink"].Value.ToString(), out job) == false)
                    {
                        MessageBox.Show(this, "Hay kiem tra lai tai khoan FB", "Web Auto - Sai du lieu", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    if ((bool)(r.Cells["colEnable"].Value) == true)
                    {
                        CGlobal.user.ReadJobCount(r.Cells["colAccount"].Value.ToString(), r.Cells["colUser"].Value.ToString());
                        CGlobal.user.ReadJobCountFB(r.Cells["colAccount"].Value.ToString(), r.Cells["colUser"].Value.ToString());
                        if (r.Cells["colJobCount"].Value.ToString().Contains("/"))
                        {
                            r.Cells["colJobCount"].Value = CGlobal.user.ReadJobCount("facebook", r.Cells["colUser"].Value.ToString());
                        }
                        if (r.Cells["colFaceCount"].Value.ToString().Contains("/"))
                        {
                            r.Cells["colFaceCount"].Value = CGlobal.user.ReadJobCountFB("facebook", r.Cells["colUser"].Value.ToString());
                        }
                        if (CGlobal.user.addLinkAccount(r.Cells["colAccount"].Value.ToString(), r.Cells["colUser"].Value.ToString(), r.Cells["colPass"].Value.ToString(), Int32.Parse(r.Cells["colJobCount"].Value.ToString()), Int32.Parse(r.Cells["colFaceCount"].Value.ToString()), (bool)(r.Cells["colEnable"].Value), Int32.Parse(r.Cells["colMaxGoLike"].Value.ToString()), Int32.Parse(r.Cells["colMaxLink"].Value.ToString())) == false)
                        {
                            CGlobal.user.ResetJobCount(r.Cells["colAccount"].Value.ToString(), r.Cells["colUser"].Value.ToString(), Int32.Parse(r.Cells["colJobCount"].Value.ToString()));
                            CGlobal.user.ResetJobCountFB(r.Cells["colAccount"].Value.ToString(), r.Cells["colUser"].Value.ToString(), Int32.Parse(r.Cells["colFaceCount"].Value.ToString()));
                            CGlobal.user.ResetJobEnable(r.Cells["colAccount"].Value.ToString(), r.Cells["colUser"].Value.ToString(), true);
                        }
                    }
                    else
                    {
                        if (CGlobal.user.addLinkAccount(r.Cells["colAccount"].Value.ToString(), r.Cells["colUser"].Value.ToString(), r.Cells["colPass"].Value.ToString(), Int32.Parse(r.Cells["colJobCount"].Value.ToString()), Int32.Parse(r.Cells["colFaceCount"].Value.ToString()), (bool)(r.Cells["colEnable"].Value), Int32.Parse(r.Cells["colMaxGoLike"].Value.ToString()), Int32.Parse(r.Cells["colMaxLink"].Value.ToString())) == false)
                        {
                            CGlobal.user.ResetJobEnable(r.Cells["colAccount"].Value.ToString(), r.Cells["colUser"].Value.ToString(), false);
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
                    btnNew.Enabled = false;
                    btnLikeArticle.Enabled = false;
                    btnLikeFanPage.Enabled = false;
                    chkEnableRedo.Enabled = false;
                    chkJobLessMoney.Enabled = false;
                    btnFollow.Enabled = false;
                    chkInvisibleBrowser.Enabled = false;
                    gridUser.Enabled = false;
                    timeCountDown = 0;
                    timer1.Start();
                    startDoJob = true;
                    lblProgress.Text = "Go ...";
                });
                test = new bqChromeService();

                test.Initialize();

                test.Start(chkInvisibleBrowser.Checked);
                while (true)
                
                {
                    bool finish = true;
                    foreach (string id in CGlobal.user.linkAccount.Keys)
                    {
                        if (CGlobal.user.LimitFault <= 0)
                        {
                            int limit = 0;
                            if (Int32.TryParse(txtFaultLimit.Text, out limit))
                            {
                                CGlobal.user.LimitFault = limit;
                            }
                            else
                            {
                                MessageBox.Show("Xem lại giá trị Chuyển TK sau số lần thất bại.");
                                finish = true;
                                break;
                            }
                        }
                        //if (CGlobal.user.linkAccount[id].EnableJob == false)
                        //    continue;
                        if (CGlobal.user.linkAccount[id].EnableJob == false && CGlobal.user.EnableRedoJob == false)
                            continue;
                        else if (CGlobal.user.linkAccount[id].EnableJob == false && CGlobal.user.EnableRedoJob == true)
                            startDoJob = false;
                        if (CGlobal.user.CheckLinkCondition)
                        {
                            if (CGlobal.user.linkAccount[id].JobCountUp >= CGlobal.user.linkAccount[id].JobCount ||
                                CGlobal.user.linkAccount[id].JobCountUpFB >= CGlobal.user.linkAccount[id].JobCountFB)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            if (CGlobal.user.linkAccount[id].JobCountUp >= CGlobal.user.linkAccount[id].JobCount /*||
                                CGlobal.user.linkAccount[id].JobCountUpFB >= CGlobal.user.linkAccount[id].JobCountFB*/)
                            {
                                continue;
                            }
                        }

                        while (CGlobal._pauseJob)
                        {
                            UpdateProgress("Tạm ngưng ..");
                            System.Threading.Thread.Sleep(550);
                            UpdateProgress("Tạm ngưng .....");
                        }
                        

                        //test.GotoURL("https://app.golike.net");
                        if (CGlobal.user.linkAccount[id].Type == "Facebook")
                        {
                            goLike = new CGoLike();
                        }
                        else if (CGlobal.user.linkAccount[id].Type == "Instagram")
                            goLike = new CGoLikeInst();

                        goLike.LinkAccount = CGlobal.user.linkAccount[id];
                        goLike.UpdateGUI = this;

                        goLike.DoJob(test);
                        
                        //goLike.DoJobTest(test);

                        if (CGlobal.user.CheckLinkCondition)
                        {
                            if (CGlobal.user.linkAccount[id].JobCountUpMax >= CGlobal.user.linkAccount[id].JobCountMax)
                            {
                                CGlobal.user.linkAccount[id].JobCountUpMax = 0;
                            }
                            if (CGlobal.user.linkAccount[id].JobCountUpFBMax >= CGlobal.user.linkAccount[id].JobCountFBMax)
                            {
                                CGlobal.user.linkAccount[id].JobCountUpFBMax = 0;
                            }
                            if (CGlobal.user.linkAccount[id].JobCountUp < CGlobal.user.linkAccount[id].JobCount ||
                                CGlobal.user.linkAccount[id].JobCountUpFB < CGlobal.user.linkAccount[id].JobCountFB)
                            {
                                finish = false;
                            }
                        }
                        else
                        {
                            if (CGlobal.user.linkAccount[id].JobCountUpMax >= CGlobal.user.linkAccount[id].JobCountMax)
                            {
                                CGlobal.user.linkAccount[id].JobCountUpMax = 0;
                            }
                            if (CGlobal.user.linkAccount[id].JobCountUp < CGlobal.user.linkAccount[id].JobCount /*||
                                CGlobal.user.linkAccount[id].JobCountUpFB < CGlobal.user.linkAccount[id].JobCountFB*/)
                            {
                                finish = false;
                            }
                        }

                        UpdateProgress("Waiting next account...");
                        UpdateAccount("Timer", "9000");
                        System.Threading.Thread.Sleep(9000);                       

                        
                        if (startDoJob == false)
                            break;
                    }
                    if (finish || startDoJob == false)
                        break;
                }

                test.Quit();

                this.Invoke((MethodInvoker)delegate
                {
                    //btnRun.Enabled = true;
                    btnRun.Text = "Làm việc";
                    btnLoad.Enabled = true;
                    btnSave.Enabled = true;
                    btnNew.Enabled = true;
                    btnLikeArticle.Enabled = true;
                    btnLikeFanPage.Enabled = true;
                    chkEnableRedo.Enabled = true;
                    chkJobLessMoney.Enabled = true;
                    btnFollow.Enabled = true;
                    chkInvisibleBrowser.Enabled = true;
                    gridUser.Enabled = true;
                    timeCountDown = 0;
                    lblCountDown.Text = "0";
                    timer1.Stop();
                    startDoJob = false;
                });
            };
            t1 = new Task(action, "jobQB#1");
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
            gridUser.Rows.Add(new object[] { "Facebook", "-1", "asyn", false, "0", "0", "0", "0", "" });
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
                if (r.Cells["colUser"].Value != null && r.Cells["colPass"].Value != null && r.Cells["colJobCount"].Value != null && r.Cells["colFaceCount"].Value != null)
                {
                    int job = 0;
                    if (r.Cells["colUser"].Value.ToString() == "-1" || r.Cells["colUser"].Value.ToString() == "" || r.Cells["colPass"].Value.ToString() == "" ||
                        Int32.TryParse(r.Cells["colJobCount"].Value.ToString(), out job) == false || Int32.TryParse(r.Cells["colFaceCount"].Value.ToString(), out job) == false)
                    {
                        MessageBox.Show(this, "Hay kiem tra lai tai khoan FB", "Web Auto - Sai du lieu", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    user.addLinkAccount(r.Cells["colAccount"].Value.ToString(), r.Cells["colUser"].Value.ToString(), r.Cells["colPass"].Value.ToString(), Int32.Parse(r.Cells["colJobCount"].Value.ToString()), Int32.Parse(r.Cells["colFaceCount"].Value.ToString()), (bool)(r.Cells["colEnable"].Value), Int32.Parse(r.Cells["colMaxGoLike"].Value.ToString()), Int32.Parse(r.Cells["colMaxLink"].Value.ToString()));
                }
            }
            try
            {
                user.GoLikeDelay1 = Int32.Parse(txtGolikeDelay1.Text);
                user.FBDelay1 = Int32.Parse(txtFBDelay1.Text);
                user.LimitFault = Int32.Parse(txtFaultLimit.Text);
                user.EnableRedoJob = chkEnableRedo.Checked;
                user.JobLessMoney = chkJobLessMoney.Checked;
                user.CheckLinkCondition = chkLinkCondition.Checked;
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
                        txtFaultLimit.Text = CGlobal.user.LimitFault.ToString();
                        chkEnableRedo.Checked = CGlobal.user.EnableRedoJob;
                        chkJobLessMoney.Checked = CGlobal.user.JobLessMoney;
                        chkLinkCondition.Checked = CGlobal.user.CheckLinkCondition;
                        gridUser.Rows.Clear();
                        foreach (CLinkAccount ac in CGlobal.user.linkAccount.Values)
                        {
                            string t = "";
                            if (ac.Type == "facebook")
                                t = "Facebook";
                            else
                                t = ac.Type;
                            gridUser.Rows.Add(new object[] { t, ac.User, ac.Password, ac.EnableJob, ac.JobCount.ToString(), ac.JobCountMax.ToString(), ac.JobCountFB.ToString(), ac.JobCountFBMax.ToString(), ac.AccountStatus });
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

        private void chkLinkCondition_CheckedChanged(object sender, EventArgs e)
        {
            gridUser.Columns["colFaceCount"].Visible = chkLinkCondition.Checked;
            gridUser.Columns["colMaxLink"].Visible = chkLinkCondition.Checked;
            if (CGlobal.user != null)
                CGlobal.user.CheckLinkCondition = chkLinkCondition.Checked;
        }

        private void btnRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CGlobal._registerPresenter.Show();
        }

        private void btnShowPassword_Click(object sender, EventArgs e)
        {
            if (btnShowPassword.Tag.ToString() == "Hide")
            {
                btnShowPassword.Tag = "Show";
                btnShowPassword.Text = "Ẩn mật khẩu";
                foreach (DataGridViewRow r in gridUser.Rows)
                {
                    gridUser.InvalidateCell(r.Cells["colPass"].ColumnIndex, r.Index);
                }
            }
            else
            {
                btnShowPassword.Tag = "Hide";
                btnShowPassword.Text = "Hiện mật khẩu";
                foreach (DataGridViewRow r in gridUser.Rows)
                {
                    gridUser.InvalidateCell(r.Cells["colPass"].ColumnIndex, r.Index);
                }
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowPassword.Checked)
            {
                btnShowPassword.Tag = "Show";
                btnShowPassword.Text = "Ẩn mật khẩu";
                chkShowPassword.Text = "* * *";
                foreach (DataGridViewRow r in gridUser.Rows)
                {
                    gridUser.InvalidateCell(r.Cells["colPass"].ColumnIndex, r.Index);
                }
            }
            else
            {
                btnShowPassword.Tag = "Hide";
                btnShowPassword.Text = "Hiện mật khẩu";
                chkShowPassword.Text = "a b c";
                foreach (DataGridViewRow r in gridUser.Rows)
                {
                    gridUser.InvalidateCell(r.Cells["colPass"].ColumnIndex, r.Index);
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtUserName.Text = txtPassword.Text = "";
            gridUser.Rows.Clear();
        }
    }
}
