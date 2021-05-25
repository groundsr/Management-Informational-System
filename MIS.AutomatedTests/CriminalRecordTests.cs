using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIS.AutomatedTests.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MIS.AutomatedTests
{
    [TestClass]
    public class CriminalRecordTests
    {
        private IWebDriver webDriver;

        [TestInitialize]
        public void InitTests()
        {
            webDriver = new ChromeDriver();
        }
        [TestMethod]
        public void DeleteCriminalRecord_DeletesTheCriminalRecordWithTheGivenName()
        {
            string name = "";
            HomePage homePage = new HomePage(webDriver);
            homePage.GoToPage();
            LoginPage loginPage = homePage.GoToLoginPage();
            loginPage.Login("toma.mihnea9923@gmail.com", "P@ssw0rd");

        }

        [TestCleanup]
        public void Cleanup()
        {
            webDriver.Close();
        }
    }
}
