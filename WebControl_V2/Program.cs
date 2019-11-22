using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using bqInet;
using Newtonsoft.Json;

namespace WebControl_V2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (CbqInet.PingDns())
            {
                string data = "";
                bool update = true;
                //Checking update software on server
                if (args.Length > 0)
                {
                    if (args[0] == "no_update")
                    {
                        update = false;                        
                    }
                }
                else
                {
                    //Check version on server
                    data = CbqInet.RequestPOSTPhpCheckVersion();
                    if (data == "")
                    {
                        update = false;
                    }
                    else
                    {
                        string[] arrData = data.Split(new string[] { "\\n" }, StringSplitOptions.None);
                        if (arrData.Length == 0)
                        {
                            update = false;
                        }
                        else
                        {
                            string[] header = arrData[0].Split('"');
                            if (header[1] != Class.CGlobal.verOnlineUpdate)
                            {
                                update = true;
                            }
                            else
                            {
                                update = false;
                            }
                        }
                    }
                }
                if (update == false)
                {
                    frmMain main = new frmMain("");
                    Application.Run(main);
                }
                else
                {
                    System.Diagnostics.Process.Start("bqupdater.exe", Class.CGlobal.verOnlineUpdate + "$" + data);
                }
                //End
            }
            else
            {
                frmMain main = new frmMain("NO_INTERNET");
                Application.Run(main);
                
            }
            //frmMain main = new frmMain();
            //Application.Run(main);
        }
    }
}
