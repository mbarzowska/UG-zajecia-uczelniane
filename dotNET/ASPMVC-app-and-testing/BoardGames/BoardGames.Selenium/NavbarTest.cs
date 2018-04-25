using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace BoardGames.Selenium {

    [TestClass]
    public class NavbarTest {

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
        public void NavBar_Brand_LinkContainsString() {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            _driver.Navigate().GoToUrl(URL);

            IWebElement elem = _driver.FindElement(By.Id("navbar_brand"));

            StringAssert.Contains(elem.Text, "GameNow! - board games rental on-line index site");
        }

        [TestMethod]
        public void NavBar_BrandClick_LinkRedirectsToPage() {
            var expected = URL + "/";

            _driver.FindElement(By.Id("navbar_brand")).SendKeys(Keys.Enter);
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(UrlToBe(expected));

            Assert.AreEqual(expected, _driver.Url);
        }



        [TestMethod]
        public void NavBar_Contact_LinkContainsString() {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            _driver.Navigate().GoToUrl(URL);

            IWebElement elem = _driver.FindElement(By.Id("navbar_contactLink"));

            StringAssert.Contains("Contact", elem.Text);
        }

        [TestMethod]
        public void NavBar_ContactClick_LinkRedirectsToPage() {
            var expected = URL + "/Home/Contact";

            _driver.FindElement(By.Id("navbar_contactLink")).SendKeys(Keys.Enter);
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(UrlToBe(expected));

            Assert.AreEqual(expected, _driver.Url);
        }



        [TestMethod]
        public void NavBar_RegisterClick_LinkRedirectsToPage() {
            var expected = URL + "/Account/Register";

            _driver.FindElement(By.Id("navbar_registerLink")).SendKeys(Keys.Enter);
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(UrlToBe(expected));

            Assert.AreEqual(expected, _driver.Url);
        }



        [TestMethod]
        public void NavBar_LoginClick_LinkRedirectsToPage() {
            var expected = URL + "/Account/Login";

            _driver.FindElement(By.Id("navbar_loginLink")).SendKeys(Keys.Enter);
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
