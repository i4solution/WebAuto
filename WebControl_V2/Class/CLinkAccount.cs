using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebControl_V2.Class
{
    [Serializable]
    public class CLinkAccount
    {
        string accountType = "";
        string username = "";
        string password = "";
        int countJob = 0;
        int countFB = 0;
        int countUpFB = 0;
        bool enableJob = false;
        public CLinkAccount(string type, string user, string pass, int count, int countFace, bool enable)
        {
            accountType = type;
            username = user;
            password = pass;
            countJob = count;
            countFB = countFace;
            enableJob = enable;            
        }
        public string User
        {
            get { return username; }
            
        }
        public string Password
        {
            get { return password; }
        }
        public string Type
        {
            get { return accountType; }
        }
        public int JobCount
        {
            get { return countJob; }
            set { countJob = value; }
        }
        public int JobCountFB
        {
            get { return countFB; }
            set { countFB = value; }
        }
        public int JobCountUpFB
        {
            get { return countUpFB; }
            set { countUpFB = value; }
        }
        public bool EnableJob
        {
            get { return enableJob; }
            set { enableJob = value; }
        }        
    }
}
