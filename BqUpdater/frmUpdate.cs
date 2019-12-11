using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using bqInet;
using System.IO;

namespace BqUpdater
{
    public partial class frmUpdate : Form
    {
        string _ver = "";
        public frmUpdate(string version)
        {
            InitializeComponent();
            string[] tmp = version.Split('$');
            if (tmp.Length == 2)
            {
                _ver = tmp[0];
                string[] news = tmp[1].Split(new string[] { "\\n" }, StringSplitOptions.None);
                if (news.Length > 0)
                    lblUpdateVer.Text = news[0];
                for (int i = 1; i < news.Length; i++)
                    txtDetail.AppendText(news[i] + "\n");
            }
            lblCurrentVer.Text = _ver;
            if (_ver == "")
                btnUpdate.Text = "Tải về";
            else
                btnUpdate.Text = "Cập nhật";
        }
        public void SetCurrentVer(string ver)
        {
            _ver = ver;
            lblCurrentVer.Text = _ver;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("WebControl_V2.exe", "no_update");
            System.Threading.Thread.Sleep(1000);
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (bqInet.CbqDownloader.DownloadFile(@"http://vuhoang8x.000webhostapp.com/Auto_Web/WebControl_V2.bqt", Environment.CurrentDirectory, WebClientDownloadProgressChanged, WebClientDownloadCompleted) == true)
            {             
                //System.Threading.Thread.Sleep(1000);
                //System.Diagnostics.Process.Start("WebControl_V2.exe", "no_update");
                //System.Threading.Thread.Sleep(1000);
                //this.Close();
            }
            else
            {
                lblInfo.Text = "THÂT BẠI";
                lblInfo.Visible = true;
            }
            //bqInet.CbqInet.RequestDownloadFile();
        }
        private void WebClientDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                lblInfo.Visible = true;
                lblInfo.Text = e.ProgressPercentage.ToString() + " %";
            });
        }

        private void WebClientDownloadCompleted(object sender, AsyncCompletedEventArgs args)
        {
            if (args.Cancelled)
            {
                lblInfo.Text = "THÂT BẠI";
                lblInfo.Visible = true;
            }
            else
            {
                lblInfo.Text = "THÀNH CÔNG";
                lblInfo.Visible = true;
                if (File.Exists(Environment.CurrentDirectory + "\\WebControl_V2.exe"))
                {
                    File.Delete(Environment.CurrentDirectory + "\\WebControl_V2.exe");
                }
                File.Move(Environment.CurrentDirectory + "\\WebControl_V2.bqt", Environment.CurrentDirectory + "\\WebControl_V2.exe");

                System.Threading.Thread.Sleep(1000);
                System.Diagnostics.Process.Start("WebControl_V2.exe", "no_update");
                System.Threading.Thread.Sleep(1000);
                this.Close();
            }
        }
    }
}
