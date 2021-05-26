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
    public class HomePage
    {
        private IWebDriver webDriver;

        public HomePage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
            PageFactory.InitElements(webDriver, this);
        }
        [FindsBy(How = How.LinkText, Using = "Login")]
        private IWebElement loginButton;
        public LoginPage GoToLoginPage()
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(3));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists
                (By.LinkText("Login")));
            loginButton.Click();
            
            return new LoginPage(webDriver);
        }
        public void GoToPage()
        {
            webDriver.Navigate().GoToUrl("https://localhost:44300/");
        }
    }
}
