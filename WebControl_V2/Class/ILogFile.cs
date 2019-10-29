using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebControl_V2.Class
{
    public abstract class ILogFile
    {
        protected string filename;
        protected bool fileExist;
        public ILogFile()
        {
            fileExist = false;
            filename = "";
        }
        public abstract void AddLog(int id, string usr, string log);
        public abstract void GenerateFileName();
        public abstract bool IsOpen { get; }
        public abstract void FileClose();
        /// <summary>
        /// Checking exits filename.
        /// Return true: Exist false: Not exist
        /// </summary>
        /// <returns></returns>
        public abstract bool GenerateFileObject();

        /// <summary>
        /// Checking exits para name file.
        /// Return true: Exist false: Not exist
        /// </summary>
        /// <returns></returns>
        public abstract bool GenerateFileObject(string name);


        public bool FileExist
        {
            get { return fileExist; }
        }
        public string Filename
        {
            set { filename = value; }
            get { return filename; }
        }
    }
}
