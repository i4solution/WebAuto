using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace WebControl_V2.Class
{
    class CEventLog
    {
        static CEventLog log;
        TraceListener listener;
        bool _debug = false;
        private CEventLog()
        {
            Stream file = File.Open(Environment.CurrentDirectory + "\\logBug.txt", FileMode.Append);
            listener = new DelimitedListTraceListener(file);
            // Add listener.
            Debug.Listeners.Clear();
            Debug.Listeners.Add(listener);
        }
        public static CEventLog Log
        {
            get
            {
                if (log == null)
                    log = new CEventLog();
                return log;
            }
        }
        public bool EnableLog
        {
            set { _debug = value; }
        }
        public void WriteEntry(string header, string detail)
        {
            if (_debug)
            {
                Debug.WriteLine(header + ", " + DateTime.Now.ToString());
                // Indent and then unindent after writing.
                Debug.Indent();
                Debug.WriteLine(detail);
                Debug.Unindent();
                Debug.Flush();
            }
        }
    }
}
