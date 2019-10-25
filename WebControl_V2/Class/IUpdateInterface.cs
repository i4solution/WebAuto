using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;

namespace WebControl_V2.Class
{
    public interface IUpdateInterface
    {
        void LoadJobs(System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> Job);
        void UpdateJob(int id, string JobID, string status);
        void UpdateProgress(string text);
        void UpdateAccount(string id, string value);

        void UpdateAccountJob(string id, string settingJobCount, string jobCount);
    }
}
