using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace WebControl_V2.Class
{
    [Serializable]
    public class CUserAccount
    {
        string username = "";
        string password = "";
        int lickGoLikeDelay1 = 4000;
        int lickFBDelay1 = 7000;
        bool enableRedo = true;
        public Dictionary<string, CLinkAccount> linkAccount;
        string _obj;
        public CUserAccount(string user, string pass)
        {
            username = user;
            password = pass;
            linkAccount = new Dictionary<string, CLinkAccount>();
            enableRedo = true;
        }
        public string User
        {
            get { return username; }

        }
        public void DefineObject(string data)
        {
            _obj = data;
        }
        public string Password
        {
            get { return password; }
        }
        public int GoLikeDelay1
        {
            set { lickGoLikeDelay1 = value; }
            get { return lickGoLikeDelay1 + ((new Random().Next(1, 10)) * 200); }

        }
        public int InstricGoLikeDelay1
        {
            get { return lickGoLikeDelay1; }
        }
        public int InstricFBDelay1
        {
            get { return lickFBDelay1; }
        }
        public int FBDelay1
        {
            set { lickFBDelay1 = value; }
            get { return lickFBDelay1 + ((new Random().Next(1, 10)) * 100); }
        }
        public bool EnableRedoJob
        {
            get { return enableRedo; }
            set { enableRedo = value; }
        }
        public bool addLinkAccount (string type, string user, string pass, int count, bool enable)
        {
            if (linkAccount.ContainsKey(type + "$" + user) == false)
            {
                linkAccount.Add(type + "$" + user, new CLinkAccount(type, user, pass, count, enable));
                return true;
            }
            return false;
        }
        public int ReadJobCount(string type, string user)
        {
            if (linkAccount.ContainsKey(type + "$" + user) == true)
            {
                return linkAccount[type + "$" + user].JobCount;              
            }
            return -1;
        }
        public bool ResetJobCount(string type, string user, int job)
        {
            if (linkAccount.ContainsKey(type + "$" + user) == true)
            {
                linkAccount[type + "$" + user].JobCount = job;
                return true;
            }
            return false;
        }
        public bool ResetJobEnable(string type, string user, bool job)
        {
            if (linkAccount.ContainsKey(type + "$" + user) == true)
            {
                linkAccount[type + "$" + user].EnableJob = job;
                return true;
            }
            return false;
        }
        public string EndCode()
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, this);
                return Convert.ToBase64String(ms.GetBuffer());
            }
        }
        public CUserAccount Decode
        {
            get
            {
                CUserAccount tmp = null;
                BinaryFormatter bf = new BinaryFormatter();
                try
                {
                    using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(_obj)))
                    {
                        tmp = (CUserAccount)bf.Deserialize(ms);
                    }
                }
                catch (Exception ex)
                {
                }
                return tmp;
            }
        }
    }
}
