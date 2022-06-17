using www.responsivefight.tests.Enums;
using www.responsivefight.tests.Helpers;
using www.responsivefight.tests.Integration_test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace www.responsivefight.tests
{
    [TestClass]
    public class RegressionTests : Basetest ,IDisposable
    {
        
        static FormattingHelper formLog = new FormattingHelper();
        static string score;
        int actual_score;
        int is_login_successful = 1;
        [ClassInitialize]

        public static void Class_initialize(TestContext context)
        {
            // formLog.LogTestCycle(IntCycleEn.Started, Context.TestName);
            webDriver = WebdriverUtil.InitializeBrowser(context);
            if (webDriver == null) throw new Exception("Driver does not exist,  on tests");
            
        }

        #region Website availability check
        /// <summary>
        /// This test will check whether the website is available by 
        /// checking the page title
        /// </summary>

        [TestMethod]
        public void Verify_website_availablity()
        {
            formLog.LogTestCycle(IntCycleEn.Started, nameof(Verify_website_availablity));
            if (webDriver == null) webDriver = WebdriverUtil.InitializeBrowser(AssemblySetUp.TestContext);
            Assert.IsNotNull(webDriver);
            webDriver.Navigate().GoToUrl(appURL);
            var PageTitle = webDriver.Title;
            var url = webDriver.Url;

            Assert.IsNotNull(PageTitle);
            Assert.IsInstanceOfType(PageTitle, typeof(string));
            Assert.IsTrue(url.Contains(appURL));
            var expResTitle = "COVID-19 THE GAME";
            if (PageTitle == expResTitle) Debug.WriteLine("Web page is loaded correctly");
            else Debug.WriteLine("Web Page does not opening ");
            formLog.LogTestCycle(IntCycleEn.Finished, nameof(Verify_website_availablity));
        }
        #endregion

        #region Registration
        /// <summary>
        /// This test will create a new user login and ensures that the user is created sucessfully
        /// and switched to the login form
        /// </summary>
        [TestMethod]
        public void Verify_registration_1()
        {
            formLog.LogTestCycle(IntCycleEn.Started, nameof(Verify_registration_1));
            if (webDriver == null) webDriver = WebdriverUtil.InitializeBrowser(AssemblySetUp.TestContext);
            Assert.IsNotNull(webDriver);
            webDriver.Navigate().GoToUrl(appURL);

            /*Click on register button*/
            try
            {

                WebdriverUtil.ClickButton("xpath", "//a[@id='rego']", webDriver);
                Trace.WriteLine(nameof(Verify_registration_1) + " Clicked the register button");

            } catch(Exception)
            {
                Trace.WriteLine(nameof(Verify_registration_1) + " Unable to click the register button");
            }


            /*enter on user name*/
            try
            {
                WebdriverUtil.SendKeys("xpath", "//input[@id='uname']", app_user_name, webDriver);
                Trace.WriteLine(nameof(Verify_registration_1) + " Entered the user name -" + app_user_name);
            }
            catch (Exception)
            {
                Trace.WriteLine(nameof(Verify_registration_1) + " Unable to enter user name");
            }


            /*enter on password*/
            try
            {
                WebdriverUtil.SendKeys("xpath", "//input[@id='pwd']", app_pwd, webDriver);
                Trace.WriteLine(nameof(Verify_registration_1) + " Entered the password");
            }
            catch (Exception)
            {
                Trace.WriteLine(nameof(Verify_registration_1) + " Unable to enter password");
            }


            /*re enter password*/
            try
            {
                WebdriverUtil.SendKeys("xpath", "//input[@id='psw-repeat']", app_pwd, webDriver);
                Trace.WriteLine(nameof(Verify_registration_1) + " Re-entered the password");
            }
            catch (Exception)
            {
                Trace.WriteLine(nameof(Verify_registration_1) + " Unable to re-enter password");
            }


            /*Click on sign up button*/
            try
            {
                WebdriverUtil.ClickButton("xpath", "//button[@id='signupbtn']", webDriver);
                Trace.WriteLine(nameof(Verify_registration_1) + " Clicked on the sign up button");
            }
            catch (Exception)
            {
                Trace.WriteLine(nameof(Verify_registration_1) + " Unable to click on sign-up button");
            }
            
            Thread.Sleep(2000);
            try
            {
                var popup = webDriver.FindElement(By.XPath("//span[@id='popup']")).Displayed;
                if (popup)
                {
                    var popup_message = webDriver.FindElement(By.XPath("//span[@id='popup']")).GetAttribute("textContent");
                    Trace.WriteLine(nameof(Verify_registration_1) + " System throws message " + popup_message);
                }
                else
                {
                    var login_name = webDriver.FindElement(By.XPath("//input[@id='worrior_username']")).GetAttribute("value");
                    if (!(String.IsNullOrEmpty(login_name)))
                    {
                        Trace.WriteLine(nameof(Verify_registration_1) + " is successful for the user " + app_user_name);
                    }
                }
                
            } catch (NullReferenceException e)
            {
               
                    Trace.WriteLine(nameof(Verify_registration_1) + " problem is user registration " + e);
                
            }
                    
          
            
           formLog.LogTestCycle(IntCycleEn.Finished, nameof(Verify_registration_1));
        }
        #endregion

        #region Leader board check
        /// <summary>
        /// This test will login  the app and attend the test until 
        /// it reaches the desired score entered in the run settings by
        /// keep re -attending the test
        /// 
        ///if login is not successful code will exit and test will not be attended
        /// </summary>
        [TestMethod ]
        public void Verify_leader_board_2()
        {
            formLog.LogTestCycle(IntCycleEn.Started, nameof(Verify_leader_board_2));
            if (webDriver == null) webDriver = WebdriverUtil.InitializeBrowser(AssemblySetUp.TestContext);
            Assert.IsNotNull(webDriver);
            int is_test_attempted = 0;
            Attend_game(webDriver, is_test_attempted);
            if (is_login_successful == 0)
                goto End_of_test;
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            try {
                do
                {
                    var user_lists = webDriver.FindElements(By.XPath("//*[@id='showData']/table/tbody/tr"));
                    for (int i = 0; i < user_lists.Count; i++)
                    {
                        if (i == 0) continue;/*to avoid the header name "user name " and "score"*/
                        var matched_user = user_lists[i].FindElement(By.XPath("//*[@id='showData']/table/tbody/tr[" + (i + 1) + "]/td[1]")).GetAttribute("textContent");
                        if (matched_user.Equals(existing_user_name) || matched_user.Equals(app_user_name))
                        {
                            score = user_lists[i].FindElement(By.XPath("//*[@id='showData']/table/tbody/tr[" + (i + 1) + "]/td[2]")).GetAttribute("textContent");
                            actual_score = Int32.Parse(score);
                            break;
                        }                        
                    }
                    is_test_attempted = 1;
                    Attend_game(webDriver, is_test_attempted);
                    Trace.WriteLine(nameof(Verify_leader_board_2) + " User finished the test successfully");
                } while (actual_score <=Int32.Parse(expected_score));
                Trace.WriteLine(nameof(Verify_leader_board_2) + " User has reached the desired score-" + score);
            }
            catch(Exception e)
            {
                Trace.WriteLine(nameof(Verify_leader_board_2) + " User details is not found in the leader board portal");
                Trace.WriteLine(nameof(Verify_leader_board_2) + " Exception" +e);
            }
            End_of_test:
            formLog.LogTestCycle(IntCycleEn.Finished, nameof(Verify_leader_board_2));
        }


        public void Attend_game(WebDriver webDriver, int is_test_attempted)
        {
            
            if (is_test_attempted == 0)
            {
                webDriver.Navigate().GoToUrl(appURL); //*[@id="start"]
                /*Login to the app with existing user credentials*/
                try
                {
                    WebdriverUtil.ClickButton("xpath", "//a[@id='login']", webDriver);
                    Trace.WriteLine(nameof(Verify_leader_board_2) + " User clicked on login button");
                }
                catch(Exception e)
                {
                    Trace.WriteLine(nameof(Verify_leader_board_2) + " Unable to click on login button due to error " +e);
                }
               
                try
                {
                    WebdriverUtil.SendKeys("xpath", "//input[@id='worrior_username']", existing_user_name, webDriver);
                    Trace.WriteLine(nameof(Verify_leader_board_2) + " User entered on user name");
                }
                catch (Exception e)
                {
                    Trace.WriteLine(nameof(Verify_leader_board_2) + "Unable to enter user name due to error " + e);
                }
               

                try
                {
                    WebdriverUtil.SendKeys("xpath", "//input[@id='worrior_pwd']", existing_pwd, webDriver);
                    Trace.WriteLine(nameof(Verify_leader_board_2) + " User to enter the password");
                }
                catch (Exception e)
                {
                    Trace.WriteLine(nameof(Verify_leader_board_2) + "Unable to enter the password due to error " + e);
                }
                

                try
                {
                    WebdriverUtil.ClickButton("xpath", "//a[@id='warrior']", webDriver);
                    Trace.WriteLine(nameof(Verify_leader_board_2) + " User clicked on submit button");
                }
                catch (Exception e)
                {
                    Trace.WriteLine(nameof(Verify_leader_board_2) + " Unable to click on submitton button due to error " + e);
                }

                Thread.Sleep(2000);
                try
                {
                    var popup = webDriver.FindElement(By.XPath("//span[@id='login_popup']")).Displayed;
                    if (popup)
                    {
                        var popup_message = webDriver.FindElement(By.XPath("//span[@id='login_popup']")).GetAttribute("textContent");
                        Trace.WriteLine(nameof(Verify_registration_1) + " System throws message " + popup_message);
                        
                        is_login_successful = 0;
                    }
                    else
                    {
                        Trace.WriteLine(nameof(Verify_leader_board_2) + " user logged in successfully");
                    }

                }
                catch (NullReferenceException e)
                {
                    Trace.WriteLine(nameof(Verify_registration_1) + " problem in login " + e);
                }
                

                webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
                if (is_login_successful == 1)
                {
                    try
                    {
                        WebdriverUtil.ClickButton("xpath", "//a[@id='start']", webDriver);
                        Trace.WriteLine(nameof(Verify_leader_board_2) + " User clicked on start button");
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine(nameof(Verify_leader_board_2) + " Unable to click on start button due to error " + e);
                    }
                    /*Click on Enter at  your own risk button and start the test*/

                    try
                    {
                        WebdriverUtil.ClickButton("xpath", "//a[@id='news']", webDriver);
                        Trace.WriteLine(nameof(Verify_leader_board_2) + " User clicked on enter your link button");

                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine(nameof(Verify_leader_board_2) + " Unable to click on enter you link button due to error " + e);

                    }

                    webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
                    try
                    {
                        WebdriverUtil.ClickButton("xpath", "//button[@id='start']", webDriver);
                        Trace.WriteLine(nameof(Verify_leader_board_2) + "user started the test successfully");
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine(nameof(Verify_leader_board_2) + " Unable to click on start  button due to error " + e);
                    }


                    try
                    {
                        for (int i = 1; i <= 13; i++)
                        {
                            if ((i == 1) || (i == 5) || (i == 6) || (i == 7) || (i == 10) || (i == 11) || (i == 13))
                            {
                                Thread.Sleep(3000);
                                // webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                                WebdriverUtil.ClickButton("xpath", "//a[@id='answer_1']", webDriver);
                                Thread.Sleep(3000);
                                //webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                                WebdriverUtil.ClickButton("xpath", "//button[@id='continue']", webDriver);
                                //webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                                Thread.Sleep(3000);
                            }
                            else
                            {
                                Thread.Sleep(3000);
                                //  webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                                WebdriverUtil.ClickButton("xpath", "//a[@id='answer_2']", webDriver);
                                Thread.Sleep(3000);
                                //webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                                WebdriverUtil.ClickButton("xpath", "//button[@id='continue']", webDriver);
                                //webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                                Thread.Sleep(3000);
                            }

                        }
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine(nameof(Verify_leader_board_2) + "user unable to attend the test due to error " + e);
                    }
                }
                else
                {
                    Trace.WriteLine(nameof(Verify_leader_board_2) + " User is unable to login");
                }
            }
            else /*Alreday test attempted*/
            {

                /*Click on continue fighting*/
                WebdriverUtil.ClickButton("xpath", "//button[@id='leaderboard_link']", webDriver);
                /*Click on Enter at  your own risk button and start the test*/
                WebdriverUtil.ClickButton("xpath", "//a[@id='news']", webDriver);
                webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
                WebdriverUtil.ClickButton("xpath", "//button[@id='start']", webDriver);
                Trace.WriteLine(nameof(Verify_leader_board_2) + " User Re started the test successfully");


                try
                {
                    Thread.Sleep(3000);
                    // webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                    WebdriverUtil.ClickButton("xpath", "//a[@id='answer_1']", webDriver);
                    Thread.Sleep(3000);
                    //webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                    WebdriverUtil.ClickButton("xpath", "//button[@id='continue']", webDriver);
                    //webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20)
                    Thread.Sleep(3000);
                }
                catch (Exception e)
                {
                    Trace.WriteLine(nameof(Verify_leader_board_2) + "user unable to continue the test due to error " + e);
                }
            }          
            
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue = true;
            }
        }

        ~RegressionTests() { Dispose(disposing: false); }


        public void Dispose()
        {

            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
