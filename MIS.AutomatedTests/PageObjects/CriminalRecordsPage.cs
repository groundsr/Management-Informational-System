using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.AutomatedTests.PageObjects
{
    class CriminalRecordsPage
    {
        private IWebDriver webDriver;

        public CriminalRecordsPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
            PageFactory.InitElements(webDriver, this);
           
        }
        [FindsBy(How = How.XPath, Using = "/html/body/div[1]/main/div[2]")]
        IWebElement recordsContainer;
        private int recordsNumber;

        public void GoToPage()
        {
            webDriver.Navigate().GoToUrl("https://localhost:44300/CriminalRecord");
        }

        public void DeleteLastRecord()
        {
            recordsNumber = recordsContainer.FindElements(By.ClassName("record")).Count();
            if (recordsNumber > 0)
            {
                /// html / body / div[1] / main / div[2] / div[1] / div / form / button
                IWebElement removeButton = webDriver.FindElement(By.XPath("/html/body/div[1]/main/div[2]/div[" + recordsNumber + "]/div/form/button"));
                ((IJavaScriptExecutor)webDriver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
                removeButton.Click();
            }
        }

        public bool RecordDeleted()
        {
            if (recordsNumber != 0)
                return recordsNumber == recordsContainer.FindElements(By.ClassName("record")).Count() + 1;
            return true;
        }
    }
}
