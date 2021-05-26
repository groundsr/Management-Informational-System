using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIS.AutomatedTests.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.AutomatedTests
{
    [TestClass]
    public class MeetingRequestTests
    {
        private IWebDriver webDriver;

        [TestInitialize]
        public void InitTests()
        {
            webDriver = new ChromeDriver();
        }
        [TestMethod]
        public void DeclineMeetingRequest_DeletesTheLastRequestIfAny()
        {
            HomePage homePage = new HomePage(webDriver);
            homePage.GoToPage();
            LoginPage loginPage = homePage.GoToLoginPage();
            loginPage.Login("admin@admin.com", "P@ssw0rd");
            PoliceSectionPage policeSectionPage = new PoliceSectionPage(webDriver);
            policeSectionPage.GoToPage();
            policeSectionPage.OpenRequestsModal();
            policeSectionPage.DeclineTheLastRequest();

            Assert.IsTrue(policeSectionPage.LastRequestDeclined());
        }
        [TestCleanup]
        public void Cleanup()
        {
            webDriver.Close();
        }

    }
}
