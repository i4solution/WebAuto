using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;

namespace bqImplementWeb
{
    public class bqChromeService : bqService
    {
        public override void Initialize()
        {         
            if (drivePath == "")
                service = ChromeDriverService.CreateDefaultService(Environment.CurrentDirectory);
            else
                service = ChromeDriverService.CreateDefaultService(drivePath);
            service.HideCommandPromptWindow = true;

        }
        public override void Start(bool visible)
        {
            string user_agent = @"Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.50 Safari/537.36";
            service.Start();
            DriverOptions option = new ChromeOptions();
            ((ChromeOptions)option).AddArguments("chrome.switches", "--disable-extensions");
            ((ChromeOptions)option).AddArguments("--disable-notifications");
            //((ChromeOptions)option).AddArguments("--disable-gpu");
            ((ChromeOptions)option).AddArguments("window-size=1024x600");
            ((ChromeOptions)option).AddArguments(@"user-agent={" + user_agent + "}");
            if (visible == true)
                ((ChromeOptions)option).AddArguments("--headless");  
            ((ChromeOptions)option).LeaveBrowserRunning = true;
            ((ChromeOptions)option).PageLoadStrategy = PageLoadStrategy.Normal;
            driver = new RemoteWebDriver(service.ServiceUrl, option);            
            driver.Manage().Window.Minimize();
        }
    }
}
