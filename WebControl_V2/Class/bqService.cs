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
    public abstract class Service
    {
        protected IWebDriver driver;
        protected DriverService service;
        protected DriverOptions option;
        protected string drivePath;
        public Service()
        {
            drivePath = "";
        }
        public void SetDriverPath(string p)
        {
            drivePath = p;
        }
        public abstract void Initialize();

        public abstract void Start();

        public void Quit()
        {
            driver.Quit();
        }
        public void GotoURL(string url)
        {
            driver.Navigate().GoToUrl(url);
        }
        public bool TryFindElement(By by, out IWebElement element)
        {
            try
            {
                element = driver.FindElement(by);
            }
            catch (NoSuchElementException ex)
            {
                element = null;
                return false;
            }
            return true;
        }
        //Accepting an Alert
        public void AlertAccept()
        {
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
        }

        //Closing an Alert
        public void AlertClose()
        {
            IAlert alert = driver.SwitchTo().Alert();
            alert.Dismiss();
        }
    }
}
