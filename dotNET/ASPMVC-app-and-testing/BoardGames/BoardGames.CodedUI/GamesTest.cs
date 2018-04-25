using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;


namespace BoardGames.CodedUI {

    [CodedUITest]
    public class GamesTest {
        public GamesTest() {
        }

        [TestMethod]
        public void OpenBrowser() {
            this.UIMap.OpenIELocalhost();
            this.UIMap.AssertOnLocalhostMain();
        }

        [TestMethod]
        public void LoginAsAdmin() {
            this.UIMap.OpenIELocalhost();
            this.UIMap.LoginAsAdmin();
            this.UIMap.AssertIsLoggedAsAdmin();

            this.UIMap.GoToGamesIndex();
            this.UIMap.AssertOnGamesIndex();

            this.UIMap.AddGame();
            this.UIMap.AssertGameWasAddedSoTableContainsIt();
            
            this.UIMap.EditWithInvalidData();
            this.UIMap.AssertGameWasNotEdited_AndGotError();

            this.UIMap.EditWithValidData();
            this.UIMap.AssertGameWasEdited_AndRedirectedToGamesPage();

            this.UIMap.ShowDetails();
            this.UIMap.AssertGameDetailsWereShown();

            this.UIMap.GoBackToGameIndex();
            this.UIMap.AssertOnGamesIndex();

            this.UIMap.DeleteAGame();
            this.UIMap.Logout();
            this.UIMap.AssertIsLoggedOut_NavbarContainsLoginOption();
        }


        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ////Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        ////Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        #endregion

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext {
            get {
                return testContextInstance;
            }
            set {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;

        public UIMap UIMap {
            get {
                if (this.map == null) {
                    this.map = new UIMap();
                }

                return this.map;
            }
        }

        private UIMap map;
    }
}
