﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;

namespace WebControl_V2.Class
{
    public class ChromeService : Service
    {
        public override void Initialize()
        {
            if (drivePath == "")
                service = ChromeDriverService.CreateDefaultService(Environment.CurrentDirectory);
            else
                service = ChromeDriverService.CreateDefaultService(drivePath);
            service.HideCommandPromptWindow = true;
            
        }
        public override void Start()
        {
            service.Start();
            DriverOptions option = new ChromeOptions();
            ((ChromeOptions)option).AddArguments("chrome.switches", "--disable-extensions");
            ((ChromeOptions)option).AddArguments("--disable-notifications");
            ((ChromeOptions)option).LeaveBrowserRunning = true;
            ((ChromeOptions)option).PageLoadStrategy = PageLoadStrategy.Normal;
            driver = new RemoteWebDriver(service.ServiceUrl, option);
            //driver.Manage().Window.Minimize();
        }
    }
}
