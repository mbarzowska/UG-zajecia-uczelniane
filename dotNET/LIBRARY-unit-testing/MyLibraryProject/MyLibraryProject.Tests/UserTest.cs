using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyLibraryProject;

namespace MyLibraryProject.Tests {

    [TestClass]
    public class UserTest {
        User myUser;
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void SetUp() {
            myUser = new User();
        }

        [TestMethod]
        public void User_AddMoney_IfEmptyAccount_ShouldHaveThatMoney() {
            myUser.AddMoney(100m);

            Assert.AreEqual(100m, myUser.AccountBalance);
        }

        [TestMethod]
        public void User_AddMoney_IfAccountWithValue_ShouldHaveTheRightAmountOfMoney() {
            myUser.AccountBalance = 200m;

            myUser.AddMoney(100m);

            Assert.AreEqual(300m, myUser.AccountBalance);
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
            "|DataDirectory|\\BuyingGames.csv", "BuyingGames#csv",
            DataAccessMethod.Sequential), DeploymentItem("BuyingGames.csv")]
        public void User_AddMoney_FromData_ReturnsCorrectValues() {
            decimal originalMoneyAmount = Convert.ToDecimal(TestContext.DataRow["originalMoneyAmount"]);
            decimal amountOfPayment = Convert.ToDecimal(TestContext.DataRow["amountOfPayment"]);
            decimal expectedTotal = Convert.ToDecimal(TestContext.DataRow["expectedTotal"]);
            myUser.AccountBalance = originalMoneyAmount;

            myUser.AddMoney(amountOfPayment);

            Assert.AreEqual(expectedTotal, myUser.AccountBalance);
        }




        [TestMethod]
        public void User_ShowAccountBalance__ShouldReturnInfo_P1() {
            myUser.Username = "Mika";
            myUser.AccountBalance = 200m;

            string result = myUser.ShowAccountBalance();

            StringAssert.Contains(result, myUser.Username);
        }

        [TestMethod]
        public void User_ShowAccountBalance__ShouldReturnInfo_P2() {
            myUser.Username = "Mika";
            myUser.AccountBalance = 200m;

            string result = myUser.ShowAccountBalance();

            StringAssert.Contains(result, myUser.AccountBalance.ToString());
        }
    }
}
