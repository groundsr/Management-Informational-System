using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.AutomatedTests.PageObjects
{
    class PoliceSectionPage
    {
        private IWebDriver webDriver;

        public PoliceSectionPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
            PageFactory.InitElements(webDriver, this);
            navbarButton.Click();
            //execute before request ends?

        }
        [FindsBy(How = How.XPath, Using = "/html/body/div[2]/div/div/div[2]")]
        private IWebElement requestsContainer;
        [FindsBy(How = How.XPath, Using = "/html/body/header/nav/div/div/ul[1]/li[3]/a")]
        private IWebElement navbarButton;
        private int requests;

        public void GoToPage()
        {
            webDriver.Navigate().GoToUrl("https://localhost:44300/PoliceSection");
        }
        public void OpenRequestsModal()
        {
            navbarButton.Click();
        }
        public void DeclineTheLastRequest()
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable
                (By.XPath("/html/body/div[2]/div/div/div[2]")));
            requests = requestsContainer.FindElements(By.ClassName("request")).Count();
            string xpath = "/html/body/div[2]/div/div/div[2]/div/div[2]/a[1]";
            if (requests > 1)
            {
                xpath = "/html/body/div[2]/div/div/div[2]/div[" + requests + "]/div[2]/a[1]";
            }
            if (requests > 0)
            {
                var declineButton = webDriver.FindElement(By.XPath(xpath));
                declineButton.Click();
            }
        }
        public bool LastRequestDeclined()
        {
            if (requests > 0)
            {
                var currentRequests = requestsContainer.FindElements(By.ClassName("request")).Count() + 1;
                return requests == currentRequests;
            }
            return true;
        }
    }
}
