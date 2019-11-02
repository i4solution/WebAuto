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
        /// <summary>
        /// Setting job limit for GoLike
        /// </summary>
        int countJob = 0;
        int countUpJob = 0;
        /// <summary>
        /// countFB : Setting access limit for Link account
        /// countUpFB : Count access to Link account website
        /// </summary>
        int countFB = 0;
        int countUpFB = 0;
        /// <summary>
        /// Setting Max job for GoLike if count job reach, will change next account. Then
        /// when finish round, will checking if job count not reach to countJob setting... 
        /// </summary>
        int countMaxJob = 0;
        int countMaxFB = 0;
        int countUpMaxFB = 0;
        int countUpMaxJob = 0;

        string accountStatus = "";
        bool enableJob = false;
        public CLinkAccount(string type, string user, string pass, int count, int countFace, bool enable, int countMax, int countFaceMax)
        {
            accountType = type;
            username = user;
            password = pass;
            countJob = count;
            countFB = countFace;
            enableJob = enable;
            countMaxFB = countFaceMax;
            countMaxJob = countMax;
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
        public string AccountStatus
        {
            set { accountStatus = value; }
            get { return accountStatus; }
        }
        public int JobCount
        {
            get { return countJob; }
            set { countJob = value; }
        }
        public int JobCountUp
        {
            get { return countUpJob; }
            set { countUpJob = value; }
        }
        public int JobCountMax
        {
            get { return countMaxJob; }
            set { countMaxJob = value; }
        }
        public int JobCountUpMax
        {
            get { return countUpMaxJob; }
            set { countUpMaxJob = value; }
        }
        public int JobCountFBMax
        {
            get { return countMaxFB; }
            set { countMaxFB = value; }
        }
        public int JobCountUpFBMax
        {
            get { return countUpMaxFB; }
            set { countUpMaxFB = value; }
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
