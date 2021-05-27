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
    public class PolicemanTests
    {
        private IWebDriver webDriver;

        [TestInitialize]
        public void InitTests()
        {
            webDriver = new ChromeDriver();
        }
        [TestMethod]
        public void DeletePoliceman_DeletesTheLastPoliceman()
        {
            HomePage homePage = new HomePage(webDriver);
            homePage.GoToPage();
            LoginPage loginPage = homePage.GoToLoginPage();

            loginPage.Login("admin@admin.com", "P@ssw0rd");
            var policemanPage = new PolicemanPage(webDriver);
            policemanPage.GoToPage();
            policemanPage.DeleteLastPoliceman();
            Assert.IsTrue(policemanPage.LastPolicemanDeleted());
        }

        public void AddPoliceSection()
        {
            HomePage homePage = new HomePage(webDriver);
            homePage.GoToPage();
            LoginPage loginPage = homePage.GoToLoginPage();

            loginPage.Login("admin@admin.com", "P@ssw0rd");
        }

        [TestCleanup]
        public void Cleanup()
        {
            webDriver.Close();
        }
    }
}
