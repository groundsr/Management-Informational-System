using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Threading.Tasks;

namespace MIS.AutomatedTests.PageObjects
{
    public class LoginPage
    {
        private IWebDriver webDriver;

        public LoginPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
            PageFactory.InitElements(webDriver, this);
        }
        [FindsBy(How = How.Id , Using = "Input_Email")]
        IWebElement usernameField;
        [FindsBy(How = How.XPath , Using = "/html/body/div[1]/main/div/div/section/form/div[3]/input")]
        IWebElement passwordField;
        [FindsBy(How = How.XPath , Using = "/html/body/div[1]/main/div/div/section/form/div[5]/button")]
        IWebElement loginButton;
        public void Login(string username, string password)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable
                (By.XPath("/html/body/div[1]/main/div/div/section/form/div[5]/button")));
            usernameField.Clear();
            usernameField.SendKeys(username);
            passwordField.Clear();
            passwordField.SendKeys(password);
            loginButton.Click();
        }
    }
}