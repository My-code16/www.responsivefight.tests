using www.responsivefight.tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace www.responsivefight.tests
{
    [TestClass]
    public class AssemblySetUp
    {
        public static WebdriverUtil WebDriver = new WebdriverUtil();
        public static string DeploymentDirectory;
        public static string ChromeDriverPath;
        public static TestContext TestContext;

        [AssemblyInitialize]
        public static void Setup(TestContext testContext)
        {
            DeploymentDirectory = testContext.DeploymentDirectory;
            TestContext = testContext;
        }


        [AssemblyCleanup]
        public static void AssemblyCleanUp()
        {
            GC.Collect();
            if(WebDriver != null)
                WebDriver.QuitAndCloseWebDriver();
        }
    }
}
