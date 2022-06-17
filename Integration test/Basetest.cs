using www.responsivefight.tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;


namespace www.responsivefight.tests.Integration_test
{
   public class Basetest 
    {
        public static WebDriver webDriver { get; set; }

        public static string appURL = AssemblySetUp.TestContext.Properties["app_url"].ToString();
        public static string app_user_name = AssemblySetUp.TestContext.Properties["user_name"].ToString();
        public static string app_pwd = AssemblySetUp.TestContext.Properties["app_pwd"].ToString();
        public static string webBrowser = AssemblySetUp.TestContext.Properties["webBrowser"].ToString();
        public static string existing_user_name = AssemblySetUp.TestContext.Properties["existing_user_name"].ToString();
        public static string existing_pwd = AssemblySetUp.TestContext.Properties["existing_pwd"].ToString(); //expected_score
        public static string expected_score = AssemblySetUp.TestContext.Properties["expected_score"].ToString(); //expected_score

    }
    
}
