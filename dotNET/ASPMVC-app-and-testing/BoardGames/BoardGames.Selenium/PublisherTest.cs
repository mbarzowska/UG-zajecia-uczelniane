using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;

//TODO Run by Selenium_Publisher laylist from native test explorer
namespace BoardGames.Selenium {

    [TestClass]
    public class PublisherTest {

        private const string URL = "http://localhost:5000";
        private readonly string IE_DRIVER_PATH = Directory.GetCurrentDirectory();
        private IWebDriver _driver;

        private readonly InternetExplorerOptions _options = new InternetExplorerOptions() {
            InitialBrowserUrl = URL,
            IntroduceInstabilityByIgnoringProtectedModeSettings = true
        };

        [TestInitialize]
        public void Initialize() {
            _driver = new InternetExplorerDriver(IE_DRIVER_PATH, _options);
        }



        [TestMethod]
        public void RunAll() {
            Create_ValidData_AddsPublisher();
            Create_InValidData_DoesntAcceptPublisherAndRedirectsToCreate();
            Edit_InValidData_DoesntEditPublisherAndredirectsToEdit();
            Edit_ValidData_RecordIsUpdated();
            Details_ValidData_RecordIsShown();
            Delete_BackToList_RecordIsNotDeleted();
            Delete_Confirm_RecordIsDeleted();
        }

        [TestMethod]
        private void Create_ValidData_AddsPublisher() {
            var expected = URL + "/Publishers";
            LoginAsAdmin();

            _driver.FindElement(By.Id("navbar_publishersLink")).SendKeys(Keys.Enter);
            var elemOrig = _driver.FindElements(By.XPath("//table[@class='table']//tr")).Count;
            _driver.FindElement(By.Id("publishers_create")).SendKeys(Keys.Enter);
            _driver.FindElement(By.Id("pcreate_CompanyNameBox")).SendKeys("Disney Games");
            _driver.FindElement(By.Id("pcreate_FoundingDateBox")).SendKeys("22-04-2018");
            _driver.FindElement(By.Id("pcreate_CountryOfOriginBox")).SendKeys("USA");
            _driver.FindElement(By.Id("pcreate_TelephoneBox")).SendKeys("555-444-333");
            _driver.FindElement(By.Id("pcreate_submit")).SendKeys(Keys.Enter);
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(UrlToBe(expected));
            var elemUpd = _driver.FindElements(By.XPath("//table[@class='table']//tr")).Count;
    
            MultiAssert.Aggregate (
                () => Assert.AreEqual(expected, _driver.Url),
                () => Assert.AreEqual(elemOrig + 1, elemUpd));
        }

        [TestMethod]
        private void Create_InValidData_DoesntAcceptPublisherAndRedirectsToCreate() {
            var expected = URL + "/Publishers/Create";
            LoginAsAdmin();

            _driver.FindElement(By.Id("navbar_publishersLink")).SendKeys(Keys.Enter);
            _driver.FindElement(By.Id("publishers_create")).SendKeys(Keys.Enter);
            _driver.FindElement(By.Id("pcreate_CompanyNameBox")).SendKeys("");
            _driver.FindElement(By.Id("pcreate_FoundingDateBox")).SendKeys("");
            _driver.FindElement(By.Id("pcreate_CountryOfOriginBox")).SendKeys("");
            _driver.FindElement(By.Id("pcreate_TelephoneBox")).SendKeys("");
            _driver.FindElement(By.Id("pcreate_submit")).SendKeys(Keys.Enter);
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(UrlToBe(expected));

            Assert.AreEqual(expected, _driver.Url);
        }



        [TestMethod]
        private void Edit_ValidData_RecordIsUpdated() {
            var expected = URL + "/Publishers";
            var name = "Disney Games";
            var newDate = "01-01-2018";
            LoginAsAdmin();

            _driver.FindElement(By.Id("navbar_publishersLink")).SendKeys(Keys.Enter);
            _driver.FindElement(By.XPath("//table/tbody/tr[td" +
                                         "[normalize-space(text())='" + name + "']]//" +
                                         "a[@id='publishers_edit']")).SendKeys(Keys.Enter);
            var date = _driver.FindElement(By.Id("pedit_FoundingDateBox"));
            date.Click();
            var currentDate = date.GetAttribute("value");
            for (int i = 0; i < currentDate.Length; i++) {
                date.SendKeys(Keys.Backspace);
            }
            date.SendKeys(newDate);
            _driver.FindElement(By.Id("pedit_submit")).SendKeys(Keys.Enter);
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(UrlToBe(expected));

            Assert.AreEqual(expected, _driver.Url);
        }

        [TestMethod]
        private void Edit_InValidData_DoesntEditPublisherAndredirectsToEdit() {
            var expected = URL + "/Publishers/Edit";
            var name = "Disney Games";
            LoginAsAdmin();

            _driver.FindElement(By.Id("navbar_publishersLink")).SendKeys(Keys.Enter);
            _driver.FindElement(By.XPath("//table/tbody/tr[td" +
                                         "[normalize-space(text())='" + name + "']]//" +
                                         "a[@id='publishers_edit']")).SendKeys(Keys.Enter);
            var date = _driver.FindElement(By.Id("pedit_FoundingDateBox"));
            date.Click();
            var currentDate = date.GetAttribute("value");
            for (int i = 0; i < currentDate.Length; i++) {
                date.SendKeys(Keys.Backspace);
            }
            _driver.FindElement(By.Id("pedit_submit")).SendKeys(Keys.Enter);

            StringAssert.Contains(_driver.Url, expected);
        }



        [TestMethod]
        private void Details_ValidData_RecordIsShown() {
            var expected = URL + "/Publishers/Details";
            var name = "Disney Games";
            LoginAsAdmin();

            _driver.FindElement(By.Id("navbar_publishersLink")).SendKeys(Keys.Enter);
            _driver.FindElement(By.XPath("//table/tbody/tr[td" +
                                         "[normalize-space(text())='" + name + "']]//" +
                                         "a[@id='publishers_details']")).SendKeys(Keys.Enter);
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(UrlToContain(expected));
            StringAssert.Contains(_driver.Url, expected);
        }



        [TestMethod]
        private void Delete_BackToList_RecordIsNotDeleted() {
            var expected = URL + "/Publishers";
            var name = "Disney Games";
            LoginAsAdmin();

            _driver.FindElement(By.Id("navbar_publishersLink")).SendKeys(Keys.Enter);
            var elemOrig = _driver.FindElements(By.XPath("//table[@class='table']//tr")).Count;
            _driver.FindElement(By.XPath("//table/tbody/tr[td" +
                                         "[normalize-space(text())='" + name + "']]//" +
                                         "a[@id='publishers_delete']")).SendKeys(Keys.Enter);
            _driver.FindElement(By.Id("pdelete_back")).SendKeys(Keys.Enter);
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(UrlToBe(expected));
            var elemUpd = _driver.FindElements(By.XPath("//table[@class='table']//tr")).Count;

            MultiAssert.Aggregate(
                () => Assert.AreEqual(expected, _driver.Url),
                () => Assert.AreEqual(elemOrig, elemUpd));
        }

        [TestMethod]
        private void Delete_Confirm_RecordIsDeleted() {
            var expected = URL + "/Publishers";
            var name = "Disney Games";
            LoginAsAdmin();

            _driver.FindElement(By.Id("navbar_publishersLink")).SendKeys(Keys.Enter);
            var elemOrig = _driver.FindElements(By.XPath("//table[@class='table']//tr")).Count;
            _driver.FindElement(By.XPath("//table/tbody/tr[td" +
                                         "[normalize-space(text())='" + name + "']]//" +
                                         "a[@id='publishers_delete']")).SendKeys(Keys.Enter);
            _driver.FindElement(By.Id("pdelete_submit")).SendKeys(Keys.Enter);
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(UrlToBe(expected));
            var elemUpd = _driver.FindElements(By.XPath("//table[@class='table']//tr")).Count;

            MultiAssert.Aggregate (
                () => Assert.AreEqual(expected, _driver.Url),
                () => Assert.AreEqual(elemOrig - 1, elemUpd));
        }



        [TestCleanup]
        public void Cleanup() {
            _driver.Quit();
        }



        public static Func<IWebDriver, bool> UrlToBe(string url) {
            return (driver) => driver.Url.ToLowerInvariant().Equals(url.ToLowerInvariant());
        }

        public static Func<IWebDriver, bool> UrlToContain(string url) {
            return (driver) => driver.Url.ToLowerInvariant().Contains(url.ToLowerInvariant());
        }

        private void LoginAsAdmin() {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            _driver.Navigate().GoToUrl(URL + "/Account/Login");

            IWebElement emailBox = _driver.FindElement(By.Id("login_emailBox"));
            emailBox.SendKeys("admin@admins.com");
            IWebElement passwdBox = _driver.FindElement(By.Id("login_passwdBox"));
            passwdBox.SendKeys("P@ssw0rd");
            IWebElement submit = _driver.FindElement(By.Id("login_submit"));
            submit.SendKeys(Keys.Enter);
        }
    }
}
