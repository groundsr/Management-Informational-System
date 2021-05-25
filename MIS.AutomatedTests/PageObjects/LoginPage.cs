using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;

namespace MIS.AutomatedTests.PageObjects
{
    public class LoginPage
    {
        private IWebDriver webDriver;

        public LoginPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }
        [FindsBy(How = How.XPath , Using = "/html/body/div[1]/main/div/div/section/form/div[2]/input")]
        IWebElement usernameField;
        [FindsBy(How = How.XPath , Using = "/html/body/div[1]/main/div/div/section/form/div[3]/input")]
        IWebElement passwordField;
        [FindsBy(How = How.XPath , Using = "/html/body/div[1]/main/div/div/section/form/div[5]/button")]
        IWebElement loginButton;
        internal void Login(string username, string password)
        {
            usernameField.Clear();
            usernameField.SendKeys(username);
            passwordField.Clear();
            passwordField.SendKeys(password);
            loginButton.Click();
        }
    }
}