using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using bqImplementWeb;

namespace WebControl_V2.Class
{
    public class CGoLike : IJobAcquist
    {
        bool _likeArticel;
        bool _follow;
        bool _likePage;
        bool _exit;
        IWebDriver driver;
        CLinkAccount linkAccount;
        IUpdateInterface bqInterface;
        public CGoLike()
        {
            _likeArticel = true;
            _likePage = true;
            _follow = true;
            _exit = false;
        }
        public bool LikeArticel
        {
            set { _likeArticel = value; }
            get {return _likeArticel;}
        }
        public bool LikePage
        {
            set { _likePage = value; }
            get { return _likePage; }
        }
        public bool FollowPage
        {
            set { _follow = value; }
            get { return _follow; }
        }
        public CLinkAccount LinkAccount
        {
            set { linkAccount = value; }
            get { return linkAccount; }
        }
        public IUpdateInterface UpdateGUI
        {
            set { bqInterface = value; }
        }
        public void Exit()
        {
            _exit = true;
        }
        public void DoJob(bqService service)
        {
            driver = service.Driver();
            int countJob = linkAccount.JobCount;
            int jobFinish = 0;
            string jobIDText = "";
            string faceName = "";

            service.GotoURL("http://facebook.com");

            TimeSpan t = new TimeSpan(TimeSpan.TicksPerSecond * 120);
            WebDriverWait w = new WebDriverWait(driver, t);
            w.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

            //service.Driver().Manage().Window.Minimize();

            //Login facebook
            //class username text: input.inputtext.login_form_input_box
            //class pass : input.inputtext.login_form_input_box
            //id Login : input.u_0_3 (Đăng nhập)
            IWebElement loginButtonFB = null;
            if (service.TryFindElement(By.Id("loginbutton"), out loginButtonFB))
            {
                System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> faceLogin = driver.FindElements(By.CssSelector("input.inputtext.login_form_input_box"));
                faceLogin[0].SendKeys(linkAccount.User);
                bqInterface.UpdateAccount("Timer", "1000");
                System.Threading.Thread.Sleep(1000);
                
                faceLogin[1].SendKeys(linkAccount.Password);
                bqInterface.UpdateAccount("Timer", "2000");
                System.Threading.Thread.Sleep(2000);
                

                while (CGlobal._pauseJob)
                {
                    bqInterface.UpdateProgress("Tạm ngưng ..");
                    System.Threading.Thread.Sleep(270);
                    bqInterface.UpdateProgress("Tạm ngưng .....");
                    System.Threading.Thread.Sleep(270);
                }

                loginButtonFB.Click();
                bqInterface.UpdateAccount("Timer", "5000");
                System.Threading.Thread.Sleep(5000);
                
                //try
                //{
                //    IAlert a = driver.SwitchTo().Alert();
                //    driver.SwitchTo().Alert().Accept();
                //}
                //catch (Exception ii)
                //{ }
                //To get facebook name : a._5afe
                if (service.TryFindElement(By.CssSelector("a._5afe"), out loginButtonFB))
                {
                    faceName = loginButtonFB.GetAttribute("title");
                    bqInterface.UpdateAccount("facebook", faceName);
                }
            }
            else if (service.TryFindElement(By.CssSelector("button._42ft._4jy0._6lth._4jy6._4jy1.selected._51sy"), out loginButtonFB))
            {
                System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> faceLogin = driver.FindElements(By.CssSelector("input.inputtext._55r1._6luy"));
                faceLogin[0].SendKeys(linkAccount.User);
                bqInterface.UpdateAccount("Timer", "1000");
                System.Threading.Thread.Sleep(1000);
                
                faceLogin[1].SendKeys(linkAccount.Password);
                bqInterface.UpdateAccount("Timer", "2000");
                System.Threading.Thread.Sleep(2000);
                

                while (CGlobal._pauseJob)
                {
                    bqInterface.UpdateProgress("Tạm ngưng ..");
                    System.Threading.Thread.Sleep(270);
                    bqInterface.UpdateProgress("Tạm ngưng .....");
                    System.Threading.Thread.Sleep(270);
                }

                loginButtonFB.Click();
                bqInterface.UpdateAccount("Timer", "5000");
                System.Threading.Thread.Sleep(5000);
                
                //try
                //{
                //    IAlert a = driver.SwitchTo().Alert();
                //    driver.SwitchTo().Alert().Accept();
                //}
                //catch (Exception ii)
                //{ }
                //To get facebook name : a._5afe
                if (service.TryFindElement(By.CssSelector("a._5afe"), out loginButtonFB))
                {
                    faceName = loginButtonFB.GetAttribute("title");
                    bqInterface.UpdateAccount("facebook", faceName);
                }
            }

            service.GotoURL("https://app.golike.net");
            //service.Driver().Manage().Window.Minimize();

            bqInterface.UpdateAccount("Timer", CGlobal.user.GoLikeDelay1.ToString());
            System.Threading.Thread.Sleep(CGlobal.user.GoLikeDelay1);
            

            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> ab = driver.FindElements(By.CssSelector("input.form-control"));
            if (ab.Count > 1)
            {
                ab[0].SendKeys(CGlobal.user.User);

                bqInterface.UpdateAccount("Timer", CGlobal.user.GoLikeDelay1.ToString());
                System.Threading.Thread.Sleep(CGlobal.user.GoLikeDelay1);
                

                ab[1].SendKeys(CGlobal.user.Password);

                bqInterface.UpdateAccount("Timer", CGlobal.user.GoLikeDelay1.ToString());
                System.Threading.Thread.Sleep(CGlobal.user.GoLikeDelay1);
                

                IWebElement loginButton = driver.FindElement(By.CssSelector("button.btn.bg-gradient-1.py-2.border-0.text-light.btn-block"));
                if (loginButton != null)
                {
                    bqInterface.UpdateAccount("Timer", "1500");
                    System.Threading.Thread.Sleep(1500);
                    
                    bqInterface.UpdateProgress("Dang nhap");
                    loginButton.Click();

                    //for (var i = 0; i < 10; i++)
                    //{
                    //    if (driver.PageSource.Length > 500)
                    //        break;
                    //    System.Threading.Thread.Sleep(500);
                    //}

                    bqInterface.UpdateAccount("Timer", CGlobal.user.GoLikeDelay1.ToString());
                    System.Threading.Thread.Sleep(CGlobal.user.GoLikeDelay1);
                    

                }
            }
            else
            {
                bqInterface.UpdateAccount("Timer", CGlobal.user.GoLikeDelay1.ToString());
                System.Threading.Thread.Sleep(CGlobal.user.GoLikeDelay1);
                
            }

            while (CGlobal._pauseJob)
            {
                bqInterface.UpdateProgress("Tạm ngưng ..");
                System.Threading.Thread.Sleep(270);
                bqInterface.UpdateProgress("Tạm ngưng .....");
                System.Threading.Thread.Sleep(270);
            }

            bqInterface.UpdateAccount("Timer", "2500");
            System.Threading.Thread.Sleep(2500);
            

            IWebElement redo = null;
            if (service.TryFindElement(By.CssSelector("h6.font-semi-bold.current_coin"), out redo))
            {
                bqInterface.UpdateAccount("So du", redo.Text);
            }
            if (service.TryFindElement(By.CssSelector("h6.font-semi-bold.pending_coin"), out redo))
            {
                bqInterface.UpdateAccount("Cho duyet", redo.Text);
            }
            //Check "Can lam lai"           
            if (CGlobal.user.EnableRedoJob && service.TryFindElement(By.CssSelector("h6.font-semi-bold.hold_coin"), out redo))
            {//Confirm OK to exit
                bqInterface.UpdateAccount("Can lam lai", redo.Text);
                if (redo.Text.Equals("0 đ") == false)
                {
                    redo.Click();
                    bqInterface.UpdateAccount("Timer", "2500");
                    System.Threading.Thread.Sleep(2500);
                    

                    while (CGlobal._pauseJob)
                    {
                        bqInterface.UpdateProgress("Tạm ngưng ..");
                        System.Threading.Thread.Sleep(270);
                        bqInterface.UpdateProgress("Tạm ngưng .....");
                        System.Threading.Thread.Sleep(270);
                    }

                    //Check Facebook or Instagram
                    //p.font-bold.font-16.my-0 red
                    //ab[0] : Facebook
                    //ab[1] : Instagram
                    ab = driver.FindElements(By.CssSelector("p.font-bold.font-16.my-0.red"));
                    if (ab[0].Text.Equals("0 đ") == false)
                    {//For Facebook
                        bqInterface.UpdateProgress("Can lam lai");
                        ab[0].Click();

                        bqInterface.UpdateAccount("Timer", CGlobal.user.GoLikeDelay1.ToString());
                        System.Threading.Thread.Sleep(CGlobal.user.GoLikeDelay1);
                        

                        //Select Facebook account
                        //div.col-7.pr-3
                        IWebElement selectAccount = null;
                        if (service.TryFindElement(By.CssSelector("div.col-7.pr-3"), out selectAccount))
                        {
                            selectAccount.Click();
                            bqInterface.UpdateAccount("Timer", "1000");
                            System.Threading.Thread.Sleep(1000);
                            

                            while (CGlobal._pauseJob)
                            {
                                bqInterface.UpdateProgress("Tạm ngưng ..");
                                System.Threading.Thread.Sleep(270);
                                bqInterface.UpdateProgress("Tạm ngưng .....");
                                System.Threading.Thread.Sleep(270);
                            }

                            //Step by step load data from account
                            //span.bg-red.px-1.font-14.mr-2
                            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> lstAccount = driver.FindElements(By.CssSelector("span.bg-red.px-1.font-14.mr-2"));
                            if (lstAccount.Count > 0)
                            {
                                try
                                {
                                    for (int h = 0; h < lstAccount.Count; h++)
                                    {
                                        lstAccount[0].Click();
                                        bqInterface.UpdateAccount("Timer", CGlobal.user.GoLikeDelay1.ToString());
                                        System.Threading.Thread.Sleep(CGlobal.user.GoLikeDelay1);

                                        //List of Job need redo
                                        ab = driver.FindElements(By.CssSelector("button.btn.btn-block.bg-button-1.px-0.complete-rework"));
                                        if (ab != null)
                                        {
                                            for (int j = 0; j < ab.Count; j++)
                                            {
                                                bqInterface.UpdateProgress("Hoan thanh cong viec can lam... " + (j + 1).ToString());
                                                ab[j].Click();
                                                bqInterface.UpdateAccount("Timer", CGlobal.user.GoLikeDelay1.ToString());
                                                System.Threading.Thread.Sleep(CGlobal.user.GoLikeDelay1);


                                                while (CGlobal._pauseJob)
                                                {
                                                    bqInterface.UpdateProgress("Tạm ngưng ..");
                                                    System.Threading.Thread.Sleep(270);
                                                    bqInterface.UpdateProgress("Tạm ngưng .....");
                                                    System.Threading.Thread.Sleep(270);
                                                }

                                                IWebElement confirmRedo = null;
                                                if (service.TryFindElement(By.CssSelector("button.swal2-confirm.swal2-styled"), out confirmRedo))
                                                {
                                                    confirmRedo.Click();
                                                    bqInterface.UpdateAccount("Timer", "2500");
                                                    System.Threading.Thread.Sleep(2500);

                                                }
                                            }
                                        }
                                    }
                                }
                                catch (Exception e1)
                                {

                                }
                            }
                        }                        
                    }
                }
            }
            //End
            _exit = false;
            while (--countJob >= 0 || _exit)
            {

                while (CGlobal._pauseJob)
                {
                    bqInterface.UpdateProgress("Tạm ngưng ..");
                    System.Threading.Thread.Sleep(270);
                    bqInterface.UpdateProgress("Tạm ngưng .....");
                    System.Threading.Thread.Sleep(270);
                }

                if (jobFinish >= linkAccount.JobCount || _exit)
                    break;

                //font-20 d-block mb-1 icon-wallet
                ab = driver.FindElements(By.CssSelector("i.font-20.d-block.mb-1.icon-wallet"));
                if (ab != null)
                {

                    //((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
                    //driver.SwitchTo().Window(driver.WindowHandles.Last());
                    bqInterface.UpdateProgress("Kiem tien");
                    ab[0].Click();

                    //for (var i = 0; i < 10; i++)
                    //{
                    //    if (driver.PageSource.Length > 100)
                    //        break;
                    //    System.Threading.Thread.Sleep(500);
                    //}

                    bqInterface.UpdateAccount("Timer", CGlobal.user.GoLikeDelay1.ToString());
                    System.Threading.Thread.Sleep(CGlobal.user.GoLikeDelay1);
                    
                    //btn btn-xs btn-outline-light
                    //Click Facebook channel
                    ab = driver.FindElements(By.CssSelector("div.btn.btn-xs.btn-outline-light"));
                    ab[1].Click();
                    bqInterface.UpdateAccount("Timer", CGlobal.user.GoLikeDelay1.ToString());
                    System.Threading.Thread.Sleep(CGlobal.user.GoLikeDelay1);
                    

                    while (CGlobal._pauseJob)
                    {
                        bqInterface.UpdateProgress("Tạm ngưng ..");
                        System.Threading.Thread.Sleep(270);
                        bqInterface.UpdateProgress("Tạm ngưng .....");
                        System.Threading.Thread.Sleep(270);
                    }

                    //Choose FB account 
                    //div.col-8.pl-3.pr-0
                    //Select Facebook account
                    //div.col-7.pr-3
                    bqInterface.UpdateProgress("Chon facebook account.");
                    IWebElement selectAccount = null;
                    if (service.TryFindElement(By.CssSelector("div.col-7.pr-3"), out selectAccount))
                    {
                        selectAccount.Click();
                        bqInterface.UpdateAccount("Timer", "3000");
                        System.Threading.Thread.Sleep(3000);
                        
                        //Step by step load data from account
                        //span.bg-red.px-1.font-14.mr-2
                        System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> filterAccount = driver.FindElements(By.CssSelector("div.col-8.pl-3.pr-0"));
                        bool match = false;
                        if (filterAccount.Count > 0)
                        {
                            for (int h = 0; h < filterAccount.Count; h++)
                            {
                                string name = filterAccount[h].FindElement(By.TagName("span")).Text;
                                if (name.Equals(faceName))
                                {
                                    match = true;
                                    filterAccount[h].Click();
                                    bqInterface.UpdateAccount("Timer", "1500");
                                    System.Threading.Thread.Sleep(1500);
                                    
                                    break;
                                }
                                
                            }
                            if (match == false)
                            {
                                bqInterface.UpdateProgress("Kiem tra lai tai khoan facebook trong GoLike");
                                break;
                            }
                        }
                    } 
                    
                   
                    bqInterface.UpdateProgress("Loc cong viec");

                    bqInterface.UpdateAccount("Timer", CGlobal.user.GoLikeDelay1.ToString());
                    System.Threading.Thread.Sleep(CGlobal.user.GoLikeDelay1);
                    

                    while (CGlobal._pauseJob)
                    {
                        bqInterface.UpdateProgress("Tạm ngưng ..");
                        System.Threading.Thread.Sleep(270);
                        bqInterface.UpdateProgress("Tạm ngưng .....");
                        System.Threading.Thread.Sleep(270);
                    }

                    //filter job
                    //Show List Job
                    //Class div.col.mr-2.px-2.text-right
                    IWebElement job = null;
                    if (service.TryFindElement(By.CssSelector("div.col.mr-2.px-2.text-right"), out job))
                    {//Confirm OK to exit
                        job.Click();
                        bqInterface.UpdateAccount("Timer", "3000");
                        System.Threading.Thread.Sleep(3000);
                    }
                    //Choose Job
                    //Class i.material-icons.mr-2.float-right.b200
                    //Index: 0: Like bai | 1 : Share bai | 2 : Theo doi | 3 : Like Fanpage | 4: Binh luan | 5 : Danh gia
                    System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> filterJob = driver.FindElements(By.CssSelector("i.material-icons.mr-2.float-right.b200"));
                    if (LikeArticel == false)
                    {
                        filterJob[0].Click();
                        bqInterface.UpdateAccount("Timer", "3000");
                        System.Threading.Thread.Sleep(3000);
                        
                        job.Click();
                    }
                    if (filterJob.Count == 0)
                    {
                        bqInterface.UpdateProgress("Khong tim thay facebook trong GoLike ?");
                        break;
                    }
                    filterJob[1].Click();
                    bqInterface.UpdateAccount("Timer", "3000");
                    System.Threading.Thread.Sleep(3000);
                    
                    job.Click();

                    if (FollowPage == false)
                    {
                        filterJob[2].Click();
                        bqInterface.UpdateAccount("Timer", "3000");
                        System.Threading.Thread.Sleep(3000);
                        
                        job.Click();
                    }

                    if (LikePage == false)
                    {
                        filterJob[3].Click();
                        bqInterface.UpdateAccount("Timer", "3000");
                        System.Threading.Thread.Sleep(3000);
                        
                        job.Click();
                    }

                    filterJob[4].Click();
                    bqInterface.UpdateAccount("Timer", "3000");
                    System.Threading.Thread.Sleep(3000);
                    
                    job.Click();

                    filterJob[5].Click();
                    bqInterface.UpdateAccount("Timer", "3000");
                    System.Threading.Thread.Sleep(3000);
                    

                    driver.Navigate().Back();

                    bqInterface.UpdateAccount("Timer", "20000");
                    System.Threading.Thread.Sleep(20000);
                    

                    driver.Navigate().Forward();

                    //List of Job
                    while (true)
                    {
                        ab = driver.FindElements(By.CssSelector("div.card.mb-2"));
                        if (ab.Count > 0)
                            break;
                    }
                    

                    if (ab.Count == 0)
                    {
                        //Refresh Job after choose option
                        //class i.material-icons.float-right.mt-1 mr-2.bg-gradient-1
                        if (service.TryFindElement(By.CssSelector("i.material-icons.float-right.mt-1.mr-2.bg-gradient-1"), out job))
                        {//Confirm OK to exit
                            job.Click();
                            bqInterface.UpdateAccount("Timer", "30000");
                            System.Threading.Thread.Sleep(30000);
                            
                            //driver.Navigate().Refresh();
                            ab = driver.FindElements(By.CssSelector("div.card.mb-2"));
                        }
                    }

                    while (CGlobal._pauseJob)
                    {
                        bqInterface.UpdateProgress("Tạm ngưng ..");
                        System.Threading.Thread.Sleep(270);
                        bqInterface.UpdateProgress("Tạm ngưng .....");
                        System.Threading.Thread.Sleep(270);
                    }

                    bqInterface.UpdateProgress("Bat dau ...");

                    CEventLog.Log.WriteEntry(linkAccount.User, "Bat dau ... [Job count on GoLike: " + ab.Count + "]");

                    bqInterface.UpdateAccount("Timer", CGlobal.user.GoLikeDelay1.ToString());
                    System.Threading.Thread.Sleep(CGlobal.user.GoLikeDelay1);
                    

                    bqInterface.LoadJobs(ab);

                    for (int i = 0; i < ab.Count; )
                    {
                        while (CGlobal._pauseJob)
                        {
                            bqInterface.UpdateProgress("Tạm ngưng ..");
                            System.Threading.Thread.Sleep(270);
                            bqInterface.UpdateProgress("Tạm ngưng .....");
                            System.Threading.Thread.Sleep(270);
                        }

                        if (jobFinish >= linkAccount.JobCount || _exit)
                            break;
                        try
                        {
                            ab[i].Click();
                        }
                        catch (Exception e)
                        {
                            CEventLog.Log.WriteEntry(linkAccount.User, "Point#1 : Cannot click on job");
                            bqInterface.UpdateProgress("Lay lai cong viec .....");

                            ab = driver.FindElements(By.CssSelector("div.card.mb-2"));

                            bqInterface.UpdateAccount("Timer", CGlobal.user.GoLikeDelay1.ToString());
                            System.Threading.Thread.Sleep(CGlobal.user.GoLikeDelay1);
                        }

                        bqInterface.UpdateAccount("Timer", "1000");
                        System.Threading.Thread.Sleep(1000);
                        
                        //font-18 font-bold b200 block-text
                        IWebElement info = null;//driver.FindElement(By.CssSelector("span.font-18.font-bold.b200.block-text"));
                        if (service.TryFindElement(By.CssSelector("span.font-18.font-bold.b200.block-text"), out info) == false)
                        {//GoLike display Message Box "Job Da Du So Luong"
                            if (service.TryFindElement(By.CssSelector("button.swal2-confirm.swal2-styled"), out info))
                            {//Confirm OK to exit
                                info.Click();
                                bqInterface.UpdateAccount("Timer", CGlobal.user.GoLikeDelay1.ToString());
                                System.Threading.Thread.Sleep(CGlobal.user.GoLikeDelay1);
                                
                            }
                            //i++;
                            //continue;
                            if (service.TryFindElement(By.CssSelector("i.material-icons.float-right.mt-1.mr-2.bg-gradient-1"), out job))
                            {//Confirm OK to exit
                                job.Click();
                                bqInterface.UpdateAccount("Timer", "30000");
                                System.Threading.Thread.Sleep(30000);

                                //driver.Navigate().Refresh();
                                ab = driver.FindElements(By.CssSelector("div.card.mb-2"));
                            }
                            i = 0;
                            continue;
                        }

                        string value = info.Text; //TĂNG LIKE CHO FANPAGE
                        Console.WriteLine("COMAPRE ==>> " + value);

                        CEventLog.Log.WriteEntry(linkAccount.User, "Point#2 Job name: ");

                        IWebElement jobID = null;
                        //Load Job ID   h6.font-id
                        if (service.TryFindElement(By.CssSelector("h6.font-id"), out jobID) == true)
                        {
                            jobIDText = jobID.Text; //TĂNG LIKE CHO FANPAGE
                            bqInterface.UpdateJob(i, jobIDText, "Dang lam");
                        }

                        //row align-items-center
                        System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> openby = driver.FindElements(By.CssSelector("a.row.align-items-center"));
                        System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> finish = driver.FindElements(By.CssSelector("div.card.card-job-detail"));
                        openby[1].Click();

                        //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                        bqInterface.UpdateAccount("Timer", CGlobal.user.GoLikeDelay1.ToString());
                        System.Threading.Thread.Sleep(CGlobal.user.GoLikeDelay1);
                        

                        //Open facebook on another tab.
                        //Like :Button likeButton _4jy0 _4jy4 _517h _51sy _42ft
                        driver.SwitchTo().Window(driver.WindowHandles.Last());

                        bqInterface.UpdateProgress("Dang nhap facebook");

                        //Login facebook
                        //class username text: input.inputtext.login_form_input_box
                        //class pass : input.inputtext.login_form_input_box
                        //id Login : input.u_0_3 (Đăng nhập)
                        IWebElement loginButton = null;
                        if (service.TryFindElement(By.Id("loginbutton"), out loginButton))
                        {
                            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> faceLogin = driver.FindElements(By.CssSelector("input.inputtext.login_form_input_box"));
                            faceLogin[0].SendKeys(linkAccount.User);
                            bqInterface.UpdateAccount("Timer", "1000");
                            System.Threading.Thread.Sleep(1000);
                            
                            faceLogin[1].SendKeys(linkAccount.Password);
                            bqInterface.UpdateAccount("Timer", "2000");
                            System.Threading.Thread.Sleep(2000);
                            
                            loginButton.Click();
                            bqInterface.UpdateAccount("Timer", "3000");
                            System.Threading.Thread.Sleep(3000);
                            
                            driver.Navigate().Back();
                            bqInterface.UpdateAccount("Timer", "5000");
                            System.Threading.Thread.Sleep(5000);
                            

                            //try
                            //{
                            //    IAlert a = driver.SwitchTo().Alert();
                            //    driver.SwitchTo().Alert().Accept();
                            //}
                            //catch (Exception ii)
                            //{ }
                        }
                        //End

                        //Waiting alert facebook
                        bqInterface.UpdateAccount("Timer", CGlobal.user.FBDelay1.ToString());
                        System.Threading.Thread.Sleep(CGlobal.user.FBDelay1);
                        
                        while (CGlobal._pauseJob)
                        {
                            bqInterface.UpdateProgress("Tạm ngưng ..");
                            System.Threading.Thread.Sleep(270);
                            bqInterface.UpdateProgress("Tạm ngưng .....");
                            System.Threading.Thread.Sleep(270);
                        }
                        //try
                        //{
                        //    IAlert a = driver.SwitchTo().Alert();
                        //    driver.SwitchTo().Alert().Accept();
                        //}
                        //catch (Exception ii)
                        //{ }
                        //End 

                        bqInterface.UpdateAccount("Timer", CGlobal.user.FBDelay1.ToString());
                        System.Threading.Thread.Sleep(CGlobal.user.FBDelay1);
                        
                        //Check Facebook "Bat dau tro chuyen" window.
                        //If window present then CLOSE it
                        try
                        {
                            if (service.TryFindElement(By.CssSelector("div._66n5"), out loginButton))
                            {
                                System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> faceChat = driver.FindElements(By.CssSelector("div._66n5"));
                                if (faceChat.Count >= 4)
                                    faceChat[3].Click();
                                bqInterface.UpdateAccount("Timer", CGlobal.user.FBDelay1.ToString());
                                System.Threading.Thread.Sleep(CGlobal.user.FBDelay1);
                            }
                        }
                        catch (Exception ie)
                        {

                        }
                        //End

                        bool faceOK = true;
                        if (value.Contains("THEO"))
                        {//TANG LUOT THEO DOI 
                            CEventLog.Log.WriteEntry(linkAccount.User, "Point#3 Follow: ");
                            bqInterface.UpdateProgress("Cong viec : Theo doi FanPage");

                            IWebElement follow = null; //driver.FindElement(By.CssSelector("Button.likeButton._4jy0._4jy4._517h._51sy._42ft"));
                            if (service.TryFindElement(By.CssSelector("a._42ft._4jy0._63_s._4jy4._4jy2.selected._51sy.mrs"), out follow))
                            {
                                //Waiting alert facebook
                                bqInterface.UpdateAccount("Timer", CGlobal.user.FBDelay1.ToString());
                                System.Threading.Thread.Sleep(CGlobal.user.FBDelay1);

                                CEventLog.Log.WriteEntry(linkAccount.User, "Point#3.1 Follow: ");

                                while (CGlobal._pauseJob)
                                {
                                    bqInterface.UpdateProgress("Tạm ngưng ..");
                                    System.Threading.Thread.Sleep(270);
                                    bqInterface.UpdateProgress("Tạm ngưng .....");
                                    System.Threading.Thread.Sleep(270);
                                }

                                //try
                                //{
                                //    IAlert a = driver.SwitchTo().Alert();
                                //    driver.SwitchTo().Alert().Accept();
                                //}
                                //catch (Exception ii)
                                //{ }
                                //End 
                                follow.Click();

                                bqInterface.UpdateAccount("Timer", CGlobal.user.FBDelay1.ToString());
                                System.Threading.Thread.Sleep(CGlobal.user.FBDelay1);

                            }
                            else
                                faceOK = false;
                        }
                        else if (value.Contains("LIKE CHO FANPAGE"))
                        {//TĂNG LIKE CHO FANPAGE
                            bqInterface.UpdateProgress("Cong viec : Like Page");
                            CEventLog.Log.WriteEntry(linkAccount.User, "Point#4 LIKE CHO FANPAGE: ");

                            IWebElement like = null; //driver.FindElement(By.CssSelector("Button.likeButton._4jy0._4jy4._517h._51sy._42ft"));
                            if (service.TryFindElement(By.CssSelector("Button.likeButton._4jy0._4jy4._517h._51sy._42ft"), out like))
                            {
                                //Waiting alert facebook
                                bqInterface.UpdateAccount("Timer", "3000");
                                System.Threading.Thread.Sleep(3000);
                               

                                while (CGlobal._pauseJob)
                                {
                                    bqInterface.UpdateProgress("Tạm ngưng ..");
                                    System.Threading.Thread.Sleep(270);
                                    bqInterface.UpdateProgress("Tạm ngưng .....");
                                    System.Threading.Thread.Sleep(270);
                                }

                                //try
                                //{
                                //    IAlert a = driver.SwitchTo().Alert();
                                //    driver.SwitchTo().Alert().Accept();
                                //}
                                //catch (Exception ii)
                                //{ }
                                //End 
                                like.Click();

                                bqInterface.UpdateAccount("Timer", CGlobal.user.FBDelay1.ToString());
                                System.Threading.Thread.Sleep(CGlobal.user.FBDelay1);

                            }
                            else
                                faceOK = false;
                        }
                        else if (value.Contains("TĂNG LIKE CHO BÀI VIẾT") || value.Contains("TĂNG LIKE CHO ALBUM"))
                        {//Thich Bai viet : Class
                            //Like Icon hidden on bai viet : i._6rk2.img.sp_60uDIWt_Org.sx_32f45f 
                            //Like Icon on Nhan Xet column : i._6rk2.img.sp_60uDIWt_Org.sx_6ec908 
                            //Like Icon on Mix bai viet :    a._6a-y._3l2t._18vj ==> index 2
                            //Like DIV div._8c74 ==> index 32 for Like Icon
                            bqInterface.UpdateProgress("Cong viec : Like bai viet");

                            CEventLog.Log.WriteEntry(linkAccount.User, "Point#5 TANG LIKE: ");

                            IWebElement like = null;
                            if (service.TryFindElement(By.CssSelector("i._6rk2.img.sp_60uDIWt_Org.sx_6ec908"), out like))
                            {
                                //Waiting alert facebook
                                bqInterface.UpdateAccount("Timer", "3000");
                                System.Threading.Thread.Sleep(3000);


                                while (CGlobal._pauseJob)
                                {
                                    bqInterface.UpdateProgress("Tạm ngưng ..");
                                    System.Threading.Thread.Sleep(270);
                                    bqInterface.UpdateProgress("Tạm ngưng .....");
                                    System.Threading.Thread.Sleep(270);
                                }

                                //try
                                //{
                                //    IAlert a = driver.SwitchTo().Alert();
                                //    driver.SwitchTo().Alert().Accept();
                                //}
                                //catch (Exception ii)
                                //{ }
                                //End 
                                bool ok = true;
                                try
                                {
                                    System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> likeArticel = driver.FindElements(By.CssSelector("i._6rk2.img.sp_60uDIWt_Org.sx_6ec908"));
                                    likeArticel[1].Click();
                                }
                                catch (Exception ii)
                                {
                                    ok = false;
                                }
                                if (ok == false)
                                {
                                    try
                                    {
                                        System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> likeArticel = driver.FindElements(By.CssSelector("a._6a-y._3l2t._18vj"));
                                        likeArticel[2].Click();
                                        ok = true;
                                    }
                                    catch (Exception j)
                                    {
                                        ok = false;
                                    }
                                    if (ok == false)
                                    {
                                        try
                                        {
                                            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> likeArticel = driver.FindElements(By.CssSelector("a._6a-y._3l2t._18vj"));
                                            likeArticel[0].Click();
                                            ok = true;
                                        }
                                        catch (Exception j)
                                        {
                                            ok = false;
                                        }
                                    }
                                    if (ok == false)
                                    {
                                        try
                                        {
                                            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> likeArticel = driver.FindElements(By.CssSelector("a._6a-y._3l2t._18vj"));
                                            likeArticel[1].Click();
                                            ok = true;
                                        }
                                        catch (Exception j)
                                        {
                                            faceOK = false;
                                        }
                                    }
                                }

                                bqInterface.UpdateAccount("Timer", CGlobal.user.FBDelay1.ToString());
                                System.Threading.Thread.Sleep(CGlobal.user.FBDelay1);

                            }
                            else if (service.TryFindElement(By.CssSelector("a._6a-y._3l2t._18vj"), out like))
                            {
                                //Waiting alert facebook
                                bqInterface.UpdateAccount("Timer", "3000");
                                System.Threading.Thread.Sleep(3000);


                                while (CGlobal._pauseJob)
                                {
                                    bqInterface.UpdateProgress("Tạm ngưng ..");
                                    System.Threading.Thread.Sleep(270);
                                    bqInterface.UpdateProgress("Tạm ngưng .....");
                                    System.Threading.Thread.Sleep(270);
                                }

                                bool ok = true;
                                try
                                {
                                    System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> likeArticel = driver.FindElements(By.CssSelector("i._6rk2.img.sp_60uDIWt_Org.sx_6ec908"));
                                    likeArticel[1].Click();
                                }
                                catch (Exception ii)
                                {
                                    ok = false;
                                }
                                if (ok == false)
                                {
                                    try
                                    {
                                        System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> likeArticel = driver.FindElements(By.CssSelector("a._6a-y._3l2t._18vj"));
                                        likeArticel[2].Click();
                                        ok = true;
                                    }
                                    catch (Exception j)
                                    {
                                        ok = false;
                                    }
                                    if (ok == false)
                                    {
                                        try
                                        {
                                            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> likeArticel = driver.FindElements(By.CssSelector("a._6a-y._3l2t._18vj"));
                                            likeArticel[0].Click();
                                            ok = true;
                                        }
                                        catch (Exception j)
                                        {
                                            ok = false;
                                        }
                                    }
                                    if (ok == false)
                                    {
                                        try
                                        {
                                            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> likeArticel = driver.FindElements(By.CssSelector("a._6a-y._3l2t._18vj"));
                                            likeArticel[1].Click();
                                            ok = true;
                                        }
                                        catch (Exception j)
                                        {
                                            faceOK = false;
                                        }
                                    }
                                }

                                bqInterface.UpdateAccount("Timer", CGlobal.user.FBDelay1.ToString());
                                System.Threading.Thread.Sleep(CGlobal.user.FBDelay1);

                            }
                            else if (service.TryFindElement(By.CssSelector("div._khz._4sz1._4rw5._3wv2"), out like))
                            {
                                //Waiting alert facebook
                                bqInterface.UpdateAccount("Timer", "3000");
                                System.Threading.Thread.Sleep(3000);


                                while (CGlobal._pauseJob)
                                {
                                    bqInterface.UpdateProgress("Tạm ngưng ..");
                                    System.Threading.Thread.Sleep(270);
                                    bqInterface.UpdateProgress("Tạm ngưng .....");
                                    System.Threading.Thread.Sleep(270);
                                }

                                //try
                                //{
                                //    IAlert a = driver.SwitchTo().Alert();
                                //    driver.SwitchTo().Alert().Accept();
                                //}
                                //catch (Exception ii)
                                //{ }
                                //End 
                                bool ok = true;
                                try
                                {
                                    System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> likeArticel = driver.FindElements(By.CssSelector("div._khz._4sz1._4rw5._3wv2"));
                                    likeArticel[1].Click();
                                }
                                catch (Exception ii)
                                {
                                    ok = false;
                                }

                                bqInterface.UpdateAccount("Timer", CGlobal.user.FBDelay1.ToString());
                                System.Threading.Thread.Sleep(CGlobal.user.FBDelay1);
                            }
                            else
                                faceOK = false;
                        }
                        else if (value.Contains("TĂNG LOVE CHO BÀI VIẾT"))
                        {//
                            //Like Icon hidden on bai viet : i._6rk2.img.sp_60uDIWt_Org.sx_32f45f 
                            //Like Icon on Nhan Xet column : i._6rk2.img.sp_60uDIWt_Org.sx_6ec908 
                            //Like Icon on Mix bai viet :    a._6a-y._3l2t._18vj
                            bqInterface.UpdateProgress("Cong viec : Love bai viet");

                            CEventLog.Log.WriteEntry(linkAccount.User, "Point#6 TANG LOVE: " );
                            try
                            {
                                IWebElement like = null;
                                if (service.TryFindElement(By.CssSelector("i._6rk2.img.sp_60uDIWt_Org.sx_6ec908"), out like))
                                {
                                    //Waiting alert facebook
                                    bqInterface.UpdateAccount("Timer", "3000");
                                    System.Threading.Thread.Sleep(3000);

                                    System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> likeArticel = driver.FindElements(By.CssSelector("a._6a-y._3l2t._18vj"));
                                    OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                                    action.MoveToElement(likeArticel[0]).Perform();

                                    CEventLog.Log.WriteEntry(linkAccount.User, "Point#6 TANG LOVE: Mouse move OK");

                                    bqInterface.UpdateAccount("Timer", "3000");
                                    System.Threading.Thread.Sleep(3000);

                                    while (CGlobal._pauseJob)
                                    {
                                        bqInterface.UpdateProgress("Tạm ngưng ..");
                                        System.Threading.Thread.Sleep(270);
                                        bqInterface.UpdateProgress("Tạm ngưng .....");
                                        System.Threading.Thread.Sleep(270);
                                    }

                                    //try
                                    //{
                                    //    IAlert a = driver.SwitchTo().Alert();
                                    //    driver.SwitchTo().Alert().Accept();
                                    //}
                                    //catch (Exception ii)
                                    //{ }
                                    //End 
                                    likeArticel = driver.FindElements(By.CssSelector("span._iuw"));
                                    likeArticel[2].Click();

                                    bqInterface.UpdateAccount("Timer", CGlobal.user.FBDelay1.ToString());
                                    System.Threading.Thread.Sleep(CGlobal.user.FBDelay1);

                                }
                            }
                            catch (Exception ex)
                            {
                                faceOK = false;
                            }
                        }
                        else
                            faceOK = false;

                        if (faceOK)
                        {
                            bqInterface.UpdateProgress("Cong viec : Hoan thanh");

                            CEventLog.Log.WriteEntry(linkAccount.User, "Point#7 FACEBOOK OK: ");

                            //Change back to first tab
                            driver.SwitchTo().Window(driver.WindowHandles.First());
                            //service.Driver().Manage().Window.Minimize();

                            bqInterface.UpdateAccount("Timer", CGlobal.user.GoLikeDelay1.ToString());
                            System.Threading.Thread.Sleep(CGlobal.user.GoLikeDelay1);

                            finish[2].Click();

                            bqInterface.UpdateAccount("Timer", CGlobal.user.GoLikeDelay1.ToString());
                            System.Threading.Thread.Sleep(CGlobal.user.GoLikeDelay1);

                            while (CGlobal._pauseJob)
                            {
                                bqInterface.UpdateProgress("Tạm ngưng ..");
                                System.Threading.Thread.Sleep(270);
                                bqInterface.UpdateProgress("Tạm ngưng .....");
                                System.Threading.Thread.Sleep(270);
                            }

                            bqInterface.UpdateAccount("Timer", CGlobal.user.FBDelay1.ToString());
                            System.Threading.Thread.Sleep(CGlobal.user.FBDelay1);

                            //Read mesage after click on Hoan Thanh Cong Viec
                            //div.swal2-content
                            IWebElement confirm = null;
                            string txt = "";
                            bool isArticelFull = false;
                            if (service.TryFindElement(By.CssSelector("div.swal2-content"), out confirm))
                            {
                                txt = confirm.FindElement(By.Id("swal2-content")).Text;
                                if (txt.Contains(":"))
                                    txt = txt.Split(':')[1];
                                else
                                {
                                    isArticelFull = true;
                                    CEventLog.Log.WriteEntry(linkAccount.User, "Point#8 GOLIKE NOT ACCEPT: ");
                                }

                            }
                            
                            if (service.TryFindElement(By.CssSelector("button.swal2-confirm.swal2-styled"), out confirm))
                            {
                                confirm.Click();
                                bqInterface.UpdateAccount("Timer", CGlobal.user.GoLikeDelay1.ToString());
                                System.Threading.Thread.Sleep(CGlobal.user.GoLikeDelay1);
                                
                            }
                            if (isArticelFull == true)
                            {
                                bqInterface.UpdateAccount("Timer", CGlobal.user.GoLikeDelay1.ToString());
                                System.Threading.Thread.Sleep(CGlobal.user.GoLikeDelay1);

                                //driver.Navigate().Back();

                                //bqInterface.UpdateAccount("Timer", CGlobal.user.GoLikeDelay1.ToString());
                                //System.Threading.Thread.Sleep(CGlobal.user.GoLikeDelay1);

                                //driver.Navigate().Refresh();

                                IWebElement error = null;
                                if (service.TryFindElement(By.CssSelector("div.col-6.pl-0.pr-2"), out error))
                                {//found div "Bo qua"
                                    bqInterface.UpdateAccount("Timer", CGlobal.user.GoLikeDelay1.ToString());
                                    System.Threading.Thread.Sleep(CGlobal.user.GoLikeDelay1);

                                    //found button "Bo qua"
                                    //button.btn.btn-block.px-0.bg-button-1
                                    if (service.TryFindElement(By.CssSelector("button.btn.btn-block.px-0.bg-button-1"), out error))
                                    {
                                        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                                        js.ExecuteScript("arguments[0].scrollIntoView();", error);

                                        bqInterface.UpdateAccount("Timer", CGlobal.user.GoLikeDelay1.ToString());
                                        System.Threading.Thread.Sleep(CGlobal.user.GoLikeDelay1);

                                        error.Click();
                                    }
                                    bqInterface.UpdateAccount("Timer", CGlobal.user.GoLikeDelay1.ToString());
                                    System.Threading.Thread.Sleep(CGlobal.user.GoLikeDelay1);

                                    if (service.TryFindElement(By.CssSelector("button.swal2-confirm.swal2-styled"), out confirm))
                                    {
                                        confirm.Click();
                                        bqInterface.UpdateAccount("Timer", CGlobal.user.GoLikeDelay1.ToString());
                                        System.Threading.Thread.Sleep(CGlobal.user.GoLikeDelay1);

                                    }
                                }
                                if (service.TryFindElement(By.CssSelector("a.row.align-items-center"), out error))
                                {//GoLike cannot back to Job list page
                                    driver.Navigate().Back();

                                    bqInterface.UpdateAccount("Timer", CGlobal.user.GoLikeDelay1.ToString());
                                    System.Threading.Thread.Sleep(CGlobal.user.GoLikeDelay1);

                                }
                                while (CGlobal._pauseJob)
                                {
                                    bqInterface.UpdateProgress("Tạm ngưng ..");
                                    System.Threading.Thread.Sleep(270);
                                    bqInterface.UpdateProgress("Tạm ngưng .....");
                                    System.Threading.Thread.Sleep(270);
                                }

                                bqInterface.UpdateAccount("Timer", CGlobal.user.GoLikeDelay1.ToString());
                                System.Threading.Thread.Sleep(CGlobal.user.GoLikeDelay1);

                                bqInterface.UpdateJob(i, jobIDText, "That Bai");
                                //bqInterface.UpdateAccountJob(linkAccount.User, linkAccount.JobCount.ToString(), jobFinish.ToString());
                            }
                            else
                            {
                                if (Int32.TryParse(txt, out jobFinish) == false)
                                    jobFinish++;
                                bqInterface.UpdateJob(i, jobIDText, "Thanh Cong");
                                bqInterface.UpdateAccountJob(linkAccount.User, linkAccount.JobCount.ToString(), jobFinish.ToString());
                                CEventLog.Log.WriteEntry(linkAccount.User, "Point#8 GOLIKE ACCEPT: ");
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine("Facebook content NOT valid ");

                            CEventLog.Log.WriteEntry(linkAccount.User, "Point#9 FACEBOOK INVALID: ");

                            //jobFinish++;

                            bqInterface.UpdateJob(i, jobIDText, "That bai");

                            driver.SwitchTo().Window(driver.WindowHandles.First());
                            //service.Driver().Manage().Window.Minimize();

                            bqInterface.UpdateAccount("Timer", CGlobal.user.GoLikeDelay1.ToString());
                            System.Threading.Thread.Sleep(CGlobal.user.GoLikeDelay1);


                            IWebElement error = null;
                            if (service.TryFindElement(By.CssSelector("div.col-6.pl-0.pr-2"), out error))
                            {//found div "Bo qua"
                                bqInterface.UpdateAccount("Timer", CGlobal.user.GoLikeDelay1.ToString());
                                System.Threading.Thread.Sleep(CGlobal.user.GoLikeDelay1);                                

                                //found button "Bo qua"
                                //button.btn.btn-block.px-0.bg-button-1
                                if (service.TryFindElement(By.CssSelector("button.btn.btn-block.px-0.bg-button-1"), out error))
                                {
                                    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                                    js.ExecuteScript("arguments[0].scrollIntoView();", error);

                                    bqInterface.UpdateAccount("Timer", CGlobal.user.GoLikeDelay1.ToString());
                                    System.Threading.Thread.Sleep(CGlobal.user.GoLikeDelay1);

                                    error.Click();
                                }
                                bqInterface.UpdateAccount("Timer", CGlobal.user.GoLikeDelay1.ToString());
                                System.Threading.Thread.Sleep(CGlobal.user.GoLikeDelay1);
                                
                                IWebElement confirm = null;
                                if (service.TryFindElement(By.CssSelector("button.swal2-confirm.swal2-styled"), out confirm))
                                {
                                    confirm.Click();
                                    bqInterface.UpdateAccount("Timer", CGlobal.user.GoLikeDelay1.ToString());
                                    System.Threading.Thread.Sleep(CGlobal.user.GoLikeDelay1);
                                    
                                }
                            }
                            if (service.TryFindElement(By.CssSelector("a.row.align-items-center"), out error))
                            {
                                driver.Navigate().Back();

                                bqInterface.UpdateAccount("Timer", CGlobal.user.GoLikeDelay1.ToString());
                                System.Threading.Thread.Sleep(CGlobal.user.GoLikeDelay1);
                               
                            }
                            while (CGlobal._pauseJob)
                            {
                                bqInterface.UpdateProgress("Tạm ngưng ..");
                                System.Threading.Thread.Sleep(270);
                                bqInterface.UpdateProgress("Tạm ngưng .....");
                                System.Threading.Thread.Sleep(270);
                            }
                        }

                        driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                        driver.SwitchTo().Window(driver.WindowHandles.First());
                        //service.Driver().Manage().Window.Minimize();
                       

                        bqInterface.UpdateAccount("Timer", CGlobal.user.FBDelay1.ToString());
                        System.Threading.Thread.Sleep(CGlobal.user.FBDelay1);

                        driver.Navigate().Refresh();

                        bqInterface.UpdateProgress("...");

                        while (CGlobal._pauseJob)
                        {
                            bqInterface.UpdateProgress("Tạm ngưng ..");
                            System.Threading.Thread.Sleep(270);
                            bqInterface.UpdateProgress("Tạm ngưng .....");
                            System.Threading.Thread.Sleep(270);
                        }
                        
                        if (service.TryFindElement(By.CssSelector("i.material-icons.float-right.mt-1.mr-2.bg-gradient-1"), out job) == false)
                        {
                            driver.Navigate().Back();

                            bqInterface.UpdateAccount("Timer", CGlobal.user.FBDelay1.ToString());
                            System.Threading.Thread.Sleep(CGlobal.user.FBDelay1);
                        }

                        IWebElement check = null;
                        if (service.TryFindElement(By.CssSelector("div.card.mb-2"), out check))
                        {
                            ab = driver.FindElements(By.CssSelector("div.card.mb-2"));
                            i = 0;
                            if (ab.Count == 0)
                            {
                                //Refresh Job after choose option
                                //class i.material-icons.float-right.mt-1 mr-2.bg-gradient-1
                                if (service.TryFindElement(By.CssSelector("i.material-icons.float-right.mt-1.mr-2.bg-gradient-1"), out job))
                                {//Confirm OK to exit
                                    job.Click();
                                    bqInterface.UpdateAccount("Timer", "20000");
                                    System.Threading.Thread.Sleep(20000);
                                    

                                    ab = driver.FindElements(By.CssSelector("div.card.mb-2"));
                                    if (ab.Count == 0)
                                        break;                                    
                                }
                            }
                            bqInterface.LoadJobs(ab);
                        }
                        else
                        {
                            //Refresh Job after choose option
                            //class i.material-icons.float-right.mt-1 mr-2.bg-gradient-1
                            if (service.TryFindElement(By.CssSelector("i.material-icons.float-right.mt-1.mr-2.bg-gradient-1"), out job))
                            {//Confirm OK to exit
                                job.Click();
                                bqInterface.UpdateAccount("Timer", "20000");
                                System.Threading.Thread.Sleep(20000);
                               

                                ab = driver.FindElements(By.CssSelector("div.card.mb-2"));
                                if (ab.Count == 0)
                                    break;
                                bqInterface.LoadJobs(ab);
                            }
                        }

                    }
                    bqInterface.UpdateProgress("Xong viec :)");
                    Console.WriteLine("COMPLETE ==>> ");
                }
            }
        }
    }
}
