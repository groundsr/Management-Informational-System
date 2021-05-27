using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.AutomatedTests.PageObjects
{
    public class PolicemanPage
    {
        private IWebDriver webDriver;

        private IWebElement deleteButton;
        [FindsBy(How=How.XPath,Using = "/html/body/div[1]/main/table/tbody")]

        private IWebElement policemanContainer;

        private int policemen;


        public void DeleteLastPoliceman()
        {
            policemen = policemanContainer.FindElements(By.TagName("tr")).Count();
            if(policemen >0)
            {
                string XPath = "/html/body/div[1]/main/table/tbody/tr/td[4]/form/button";
                if(policemen > 1)
                {
                    ((IJavaScriptExecutor)webDriver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
                    XPath = "/html/body/div[1]/main/table/tbody/tr[" + policemen + "]/td[4]/form/button";
                }
                deleteButton = webDriver.FindElement(By.XPath(XPath));
                
                deleteButton.Click();
            }
        }

        public bool LastPolicemanDeleted()
        {
            int currentPolicemen = policemanContainer.FindElements(By.TagName("tr")).Count();
            if (policemen> 0)
            {
                return policemen == currentPolicemen + 1;
            }
            return true;
        }

        public void GoToPage()
        {
            webDriver.Navigate().GoToUrl("https://localhost:44300/Policeman");
        }

        



        public PolicemanPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
            PageFactory.InitElements(webDriver, this);
        }
    }
}
