using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace BoardGames.Selenium {

    [TestClass]
    public class LoginTest {

        private const string URL = "http://localhost:5000/Account/Login";
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
        public void Login_ValidData_DoesLogIn() {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            _driver.Navigate().GoToUrl(URL);

            _driver.FindElement(By.Id("login_emailBox")).SendKeys("admin@admins.com");
            _driver.FindElement(By.Id("login_passwdBox")).SendKeys("P@ssw0rd");
            _driver.FindElement(By.Id("login_submit")).SendKeys(Keys.Enter);
            IWebElement hello = _driver.FindElement(By.Id("navbar_helloForLoggedIn"));

            StringAssert.Contains(hello.Text, "admin");
        }

        [TestMethod]
        public void Login_InvalidData_DoesntLogInAndRedirectsToLoginPage() {
            var expected = URL;
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            _driver.Navigate().GoToUrl(URL);

            _driver.FindElement(By.Id("login_emailBox")).SendKeys("admin@admins.com");
            _driver.FindElement(By.Id("login_passwdBox")).SendKeys("invalid");
            _driver.FindElement(By.Id("login_submit")).SendKeys(Keys.Enter);
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(UrlToBe(expected));

            Assert.AreEqual(expected, _driver.Url);
        }



        [TestMethod]
        public void Logout_DoesLogOut() {
            var expected = "http://localhost:5000/";
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            _driver.Navigate().GoToUrl(URL);

            _driver.FindElement(By.Id("login_emailBox")).SendKeys("admin@admins.com");
            _driver.FindElement(By.Id("login_passwdBox")).SendKeys("P@ssw0rd");
            _driver.FindElement(By.Id("login_submit")).SendKeys(Keys.Enter);
            _driver.FindElement(By.Id("navbar_logoutLink")).SendKeys(Keys.Enter);
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(UrlToBe(expected));

            Assert.AreEqual(expected, _driver.Url);
        }



        [TestCleanup]
        public void Cleanup() {
            _driver.Quit();
        }



        public static Func<IWebDriver, bool> UrlToBe(string url) {
            return (driver) => driver.Url.ToLowerInvariant().Equals(url.ToLowerInvariant());
        }
    }
}