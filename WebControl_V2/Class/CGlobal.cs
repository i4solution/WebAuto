using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using bqImplementWeb;


namespace WebControl_V2.Class
{
    public class CGlobal
    {
        public static string ver = "WEB AUTO Ver 1.4.5.6";
        public static string verOnlineUpdate = "1456";
        public static List<bqService> _service;
        public static CUserAccount user;
        public static bool _pauseJob;
        public static string _logPath = "";
        public static CBqRegisterPresenter _registerPresenter;
        public static CSession _session;
    }
}
