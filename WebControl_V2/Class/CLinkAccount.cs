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
        bool enableJob = false;
        public CLinkAccount(string type, string user, string pass, int count, bool enable)
        {
            accountType = type;
            username = user;
            password = pass;
            countJob = count;
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
        public bool EnableJob
        {
            get { return enableJob; }
            set { enableJob = value; }
        }
    }
}
