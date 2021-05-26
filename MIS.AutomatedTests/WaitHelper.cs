using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.AutomatedTests
{
    public class WaitHelper
    {
        private WebDriverWait wait;
        private IWebDriver webDriver;

        public WaitHelper()
        {
            webDriver = new ChromeDriver();
            

        }
        public void WaitElementXPath(string xPath)
        {
            wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(3));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable
                (By.XPath(xPath)));
        }
    }
}
