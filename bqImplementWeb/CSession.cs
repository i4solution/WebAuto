using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Runtime.InteropServices;

namespace bqImplementWeb
{
    public class CSession
    {
        internal struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }
        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO Dummy);
        [DllImport("Kernel32.dll")]
        private static extern uint GetLastError();

        public static uint GetIdleTime()
        {
            LASTINPUTINFO LastUserAction = new LASTINPUTINFO();
            LastUserAction.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(LastUserAction);
            GetLastInputInfo(ref LastUserAction);
            return ((uint)Environment.TickCount - LastUserAction.dwTime);
        }

        public static long GetTickCount()
        {
            return Environment.TickCount;
        }

        public static long GetLastInputTime()
        {
            LASTINPUTINFO LastUserAction = new LASTINPUTINFO();
            LastUserAction.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(LastUserAction);
            if (!GetLastInputInfo(ref LastUserAction))
            {
                throw new Exception(GetLastError().ToString());
            }

            return LastUserAction.dwTime;
        }


        public event EventHandler sessionTrigger;
        public event EventHandler sessionTimeOut;
        string _user = "";
        List<string> _action;
        string _visited;
        int periodTimer;
        int Expired;
        Timer _tick;
        bool _enableTimeout;
        public CSession()
        {
            _enableTimeout = true;
            _user = "";
            _visited = "";
            periodTimer = 1;
            _action = new List<string>();
            _tick = new System.Timers.Timer();
            _tick.Elapsed += new ElapsedEventHandler(Idle_PreProcessInput);
            //_tick.Tick += new EventHandler(SessionExpired);
            //_tick.Interval = periodTimer * 1000 * 10000;
            _tick.Interval = periodTimer * 1000;
            _tick.Enabled = true;
        }
        void SessionExpired(object sender, EventArgs e)
        {
            if (_user == null)
                return;
            else if (_user == "")
                return;
            _tick.Stop();
            DestroySession();
            if (sessionTimeOut != null)
                sessionTimeOut(null, null);
        }
        void Idle_PreProcessInput(object sender, ElapsedEventArgs e)
        {
            if (_user != "")
            {
                if (_enableTimeout)
                {
                    if (Expired <= 0)
                    {
                        SessionExpired(this, null);
                    }
                }
                Expired--;
            }            
        }
        public bool CreateSession(string user, int exp)
        {
            if (user == null)
                return false;
            else if (user == "")
                return false;
            _user = user;
            if (exp != 0)
            {
                Expired = exp;
                _tick.Enabled = true;
                _tick.Start();
            }
            if (sessionTrigger != null)
                sessionTrigger(null, null);
            return true;
        }
        public bool IsPermission(string Name)
        {
            if (_user == null)
                return false;
            return true;
        }
        public bool EnableTimeout
        {
            set
            {
                _enableTimeout = value;
            }
        }
        public bool DestroySession()
        {
            _user = "";
            if (sessionTrigger != null)
                sessionTrigger(null, null);
            return true;
        }
        public string Visited
        {
            set { _visited = value; }
            get { return _visited; }
        }
        public bool Verify
        {
            get
            {
                if (_user == null)
                    return false;
                return true;
            }
        }
        public string Username
        {
            get
            {
                if (_user == null)
                    return "";
                return _user;
            }
        }
        
    }
}
