using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace WebControl_V2.Class
{
    public class CLogReport : ILogFile
    {
        StreamWriter sw;
        static CLogReport _log;
        CLogReport()
            : base()
        {
        }
        public static CLogReport Instance()
        {
            if (_log == null)
                _log = new CLogReport();
            return _log;
        }
        public override bool IsOpen
        {
            get { return (sw == null) ? false : true; }
        }
        public override void FileClose()
        {
            if (IsOpen)
                sw.Close();
        }
        public override void AddLog(int id, string usr, string log)
        {
            GenerateFileName();
            GenerateFileObject();
            if (!System.IO.Directory.Exists(CGlobal._logPath))
                return;
            if (sw == null)
                sw = File.AppendText(CGlobal._logPath + "\\" + filename);
            if (log.Length < 66)
            {
                int l = log.Length;
                for (int i = 0; i < (66 - l); i++)
                    log += " ";
            }
            string _id = id.ToString();
            if (_id.ToString().Length < 5)
            {
                int l = _id.Length;
                for (int i = 0; i < (5 - l); i++)
                    _id += " ";
            }
            string date = DateTime.Now.ToShortDateString() + "," + DateTime.Now.ToLongTimeString();
            if (date.Length < 19)
            {
                int l = date.Length;
                for (int i = 0; i < (19 - l); i++)
                    date += " ";
            }
            if (usr.Length < 10)
            {
                int l = usr.Length;
                for (int i = 0; i < (10 - l); i++)
                    usr += " ";
            }
            if (!fileExist)
            {
                sw.WriteLine("WEB AUTO JOB");
                sw.WriteLine("_________________________________________________________________________________________________________");
                sw.WriteLine("|      DATE         | ID  |   USER   |                         EVENTS                                   |");
                sw.WriteLine("|-------------------|-----|----------|------------------------------------------------------------------|");
                            /*|     19 char       |5char| 10 char  |                       66 char                                    | */
                sw.WriteLine("|" + date + "|" + _id + "|" + usr + "|" + log + "|");
            }
            else
            {
                sw.WriteLine("|" + date + "|" + _id + "|" + usr + "|" + log + "|");
            }
            sw.WriteLine("|-------------------|-----|----------|------------------------------------------------------------------|");
            sw.Close();
            sw = null;
        }
        public override void GenerateFileName()
        {
            System.Globalization.CultureInfo cult_info = System.Globalization.CultureInfo.CreateSpecificCulture("no");
            System.Globalization.Calendar cal = cult_info.Calendar;
            int weekCount = cal.GetWeekOfYear(DateTime.Now, cult_info.DateTimeFormat.CalendarWeekRule, cult_info.DateTimeFormat.FirstDayOfWeek);
            int monthCount = cal.GetMonth(DateTime.Now);
            //filename = "log." + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + ".txt";
            filename = "log." + weekCount.ToString() + DateTime.Now.Year.ToString() + ".txt";
        }
        /// <summary>
        /// Checking exits filename.
        /// Return true: Exist false: Not exist
        /// </summary>
        /// <returns></returns>
        public override bool GenerateFileObject()
        {
            if (File.Exists(CGlobal._logPath + "\\" + filename))
            {
                fileExist = true;
            }
            else
            {
                fileExist = false;
            }
            return fileExist;
        }
        /// <summary>
        /// Checking exits para name file.
        /// Return true: Exist false: Not exist
        /// </summary>
        /// <returns></returns>
        public override bool GenerateFileObject(string name)
        {
            if (File.Exists(CGlobal._logPath + "\\" + name))
            {
                fileExist = true;
            }
            else
            {
                fileExist = false;
            }
            return fileExist;
        }
    }
}
