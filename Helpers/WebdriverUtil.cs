using www.responsivefight.tests.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using System;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;

namespace www.responsivefight.tests.Helpers
{
    public class WebdriverUtil
    {
        public static WebDriver Driver;

        public WebDriver driver { get { return Driver; } }

        public static WebDriver InitializeBrowser(TestContext context)
        {
            var browser = context.Properties["webBrowser"];
            //In case of an error please make sure that u are assigned a *.runsettings config file
            Enum.TryParse(browser.ToString(), out Browsers browserEn);
            var webDriver = new WebdriverUtil();
            webDriver.LaunchBrowser(browserEn);
            Trace.WriteLine(nameof(InitializeBrowser)+" " + browserEn + " is successful launched");
            return webDriver.driver;
        }

        private static WebDriver CreateDriver(Browsers browser)
        {
            switch (browser)
            {
                case Browsers.Firefox:

                    if (Driver == null)
                    {
                        new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                        Driver = new FirefoxDriver();

                    }
                    break;
                case Browsers.InternetExplorer:
                    if (Driver == null)
                    {
                        new WebDriverManager.DriverManager().SetUpDriver(new InternetExplorerConfig());
                        Driver = new InternetExplorerDriver();
                    }
                    break;

                case Browsers.Chrome:
                    if (Driver == null)
                    {

                        new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                        Driver = new ChromeDriver();
                    }
                    break;

                case Browsers.Safari:
                    if (Driver == null)
                    {
                        Driver = new SafariDriver();
                    }
                    break;
                case Browsers.MSEdge:
                    if (Driver == null)
                    {
                        new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                        Driver = new EdgeDriver();
                    }
                    break;
                default:
                    goto case Browsers.Chrome;
            }
            return Driver;
        }

        public void LaunchBrowser(Browsers browser)
        {
            try
            {
                switch (browser.ToString().ToUpper())
                {
                    case "FIREFOX":
                        Driver = CreateDriver(Browsers.Firefox);
                        break;
                    case "INTERNETEXPLORER":
                        Driver = CreateDriver(Browsers.InternetExplorer);
                        break;
                    case "CHROME":
                        Driver = CreateDriver(Browsers.Chrome);
                        break;
                    case "SAFARI":
                        Driver = CreateDriver(Browsers.Safari);
                        break;
                    case "MSEDGE":
                        Driver = CreateDriver(Browsers.MSEdge);
                        break;
                    default:
                        goto case "CHROME";
                }
                Driver.Manage().Window.Maximize();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public void QuitAndCloseWebDriver()
        {
            Driver?.Close();
            Driver?.Quit();
        }

        public static void SendKeys(String locator_type, String find_element_value, String enter_value, WebDriver webDriver)
        {
            try
            {
                if (locator_type.Equals("id"))
                {
                    webDriver.FindElement(By.Id(find_element_value)).SendKeys(enter_value);

                }
                else if (locator_type.Equals("xpath"))
                {
                    webDriver.FindElement(By.XPath(find_element_value)).SendKeys(enter_value);

                }
                else if (locator_type.Equals("cssselector"))
                {
                    webDriver.FindElement(By.CssSelector(find_element_value)).SendKeys(enter_value);

                }
                else if (locator_type.Equals("classname"))
                {
                    webDriver.FindElement(By.ClassName(find_element_value)).SendKeys(enter_value);

                }
                else if (locator_type.Equals("name"))
                {
                    webDriver.FindElement(By.Name(find_element_value)).SendKeys(enter_value);

                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Unable to enter the values");
                throw new Exception(ex.InnerException.ToString());
            }

        }

        public static void ClickButton(String locator_type, String find_element_value, WebDriver webDriver)
        {
            try
            {
                if (locator_type.Equals("id"))
                {
                    webDriver.FindElement(By.Id(find_element_value)).Click();

                }
                else if (locator_type.Equals("xpath"))
                {
                    webDriver.FindElement(By.XPath(find_element_value)).Click();

                }
                else if (locator_type.Equals("cssselector"))
                {
                    webDriver.FindElement(By.CssSelector(find_element_value)).Click();

                }
                else if (locator_type.Equals("classname"))
                {
                    webDriver.FindElement(By.ClassName(find_element_value)).Click();

                }
                else if (locator_type.Equals("name"))
                {
                    webDriver.FindElement(By.Name(find_element_value)).Click();

                }
                else if (locator_type.Equals("linktext"))
                {
                    webDriver.FindElement(By.LinkText(find_element_value)).Click();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to click the element");
                throw new Exception(ex.InnerException.ToString());
            }
        }

       
    }
}
