using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyLibraryProject.Fakes;
using System;
using System.Collections.Generic;

namespace MyLibraryProject.Tests {
    [TestClass]
    public class ShopTest {
        Shop myShop;

        [TestInitialize]
        public void SetUp() {
            myShop = new Shop();
        }

        [TestMethod]
        public void Shop_FindUser_IfUserExists_ShouldReturnUser() {
            StubIUser user = new StubIUser() {
                UsernameGet = () => { return "Mika"; },
                AccountBalanceGet = () => { return 100m; }
            };

            myShop.Register(user.UsernameGet(), user.AccountBalanceGet());
            var result = myShop.FindUser(user.UsernameGet());

            Assert.AreEqual(result.Username, user.UsernameGet());
        }

        [TestMethod]
        public void Shop_FindUser_IfUserDoesntExist_ShouldReturnNull() {
            StubIUser user = new StubIUser() {
                UsernameGet = () => { return "Mika"; }
            };

            var result = myShop.FindUser(user.UsernameGet());

            Assert.AreEqual(result, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Shop_FindUser_IfNotGivenValue_ShouldThrowException() {
            StubIUser user = new StubIUser() {
                UsernameGet = () => { return null; }
            };

            myShop.FindUser(user.UsernameGet());

            Assert.Fail();
        }




        [TestMethod]
        public void Shop_FindLoggedUser_IfUserIsLoggedIn_ShouldReturnUser() {
            StubIUser user = new StubIUser() {
                UsernameGet = () => { return "Mika"; },
                AccountBalanceGet = () => { return 100m; }
            };

            myShop.Register(user.UsernameGet(), user.AccountBalanceGet());
            var registeredUser = myShop.FindUser(user.UsernameGet());

            myShop.LogIn(registeredUser.Username);
            var loggedUser = myShop.FindLoggedUser(registeredUser.Username);

            Assert.AreEqual(loggedUser.Username, user.UsernameGet());
        }

        [TestMethod]
        public void Shop_FindLoggedUser_IfUserIsNotLoggedIn_ShouldReturnNull() {
            StubIUser user = new StubIUser() {
                UsernameGet = () => { return "Mika"; }
            };

            var loggedUser = myShop.FindLoggedUser(user.UsernameGet());

            Assert.AreEqual(loggedUser, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Shop_FindLoggedUser_IfNotGivenValue_ShouldThrowException() {
            StubIUser user = new StubIUser() {
                UsernameGet = () => { return null; }
            };

            myShop.FindLoggedUser(user.UsernameGet());

            Assert.Fail();
        }




        [TestMethod]
        public void Shop_UserExists_IfUserExists_ShouldReturnTrue() {
            StubIUser user = new StubIUser() {
                UsernameGet = () => { return "Mika"; },
                AccountBalanceGet = () => { return 100m; }
            };

            myShop.Register(user.UsernameGet(), user.AccountBalanceGet());
            var result = myShop.UserExists(user.UsernameGet());

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Shop_UserExists_IfUserDoesntExist_ShouldReturnFalse() {
            StubIUser user = new StubIUser() {
                UsernameGet = () => { return "Mika"; }
            };

            var result = myShop.UserExists(user.UsernameGet());

            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Shop_UserExists_IfNotGivenValue_ShouldThrowException() {
            StubIUser user = new StubIUser() {
                UsernameGet = () => { return null; }
            };

            myShop.UserExists(user.UsernameGet());

            Assert.Fail();
        }




        [TestMethod]
        public void Shop_FindGame_IfGameExists_ShouldReturnGame() {
            StubIGame game = new StubIGame() { 
                NameGet = () => { return "Monopoly"; } //, PriceGet = () => { return 100m; }
            };

            myShop.GamesList.Add(game);
            var result = myShop.FindGame(game.NameGet());

            Assert.AreEqual(result.Name, game.NameGet());
        }

        [TestMethod]
        public void Shop_FindGame_IfGameDoesntExist_ShouldReturnNull() {
            StubIGame game = new StubIGame() {
                NameGet = () => { return "Monopoly"; },
            };

            var result = myShop.FindGame(game.NameGet());

            Assert.AreEqual(result, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Shop_FindGame_IfNotGivenValue_ShouldThrowException() {
            StubIGame game = new StubIGame() {
                NameGet = () => { return null; },
            };

            myShop.FindGame(game.NameGet());

            Assert.Fail();
        }




        [TestMethod]
        public void Shop_GameExists_IfGameExists_ShouldReturnTrue() {
            StubIGame game = new StubIGame() {
                NameGet = () => { return "Monopoly"; },
            };

            myShop.GamesList.Add(game);
            var result = myShop.GameExists(game.NameGet());

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Shop_GameExists_IfGameDoesntExist_ShouldReturnFalse() {
            StubIGame game = new StubIGame() {
                NameGet = () => { return "Monopoly"; },
            };

            var result = myShop.GameExists(game.NameGet());

            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Shop_GameExists_IfNotGivenValue() {
            StubIGame game = new StubIGame() {
                NameGet = () => { return null; },
            };

            myShop.GameExists(game.NameGet());

            Assert.Fail();
        }




        [TestMethod]
        public void Shop_Register_IfNewUser_ShouldReturnTrue() {
            StubIUser user = new StubIUser() {
                UsernameGet = () => { return "Mika"; },     // zwroci mika jesli bedzie zapytany o geta
                AccountBalanceGet = () => { return 100m; }
            };

            myShop.Register(user.UsernameGet(), user.AccountBalanceGet());

            Assert.AreEqual(1, myShop.UsersList.Count);
        }

        [TestMethod]
        public void Shop_Register_IfUsernameTaken_ShouldReturnFalse() {
            StubIUser user = new StubIUser() {
                UsernameGet = () => { return "Mika"; },
                AccountBalanceGet = () => { return 100m; }
            };

            myShop.Register(user.UsernameGet(), user.AccountBalanceGet());
            var result = myShop.Register(user.UsernameGet(), user.AccountBalanceGet());

            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Shop_Register_IfNotGivenValue_ShouldThrowException() {
            StubIUser user = new StubIUser() {
                UsernameGet = () => { return null; },
                AccountBalanceGet = () => { return 100m; }
            };

            myShop.Register(user.UsernameGet(), user.AccountBalanceGet());

            Assert.Fail();
        }




        // login if user exists is verified in IsLoggedIn when user takes action

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Shop_LogIn_IfUserDoesntExist_ShouldThrowException() {
            StubIUser user = new StubIUser() {
                UsernameGet = () => { return "Mika"; } // , AccountBalanceGet = () => { return 100m; }
            };

            var result = myShop.LogIn(user.UsernameGet());

            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Shop_LogIn_IfNotGivenValue_ShouldThrowException() {
            StubIUser user = new StubIUser() {
                UsernameGet = () => { return null; } // , AccountBalanceGet = () => { return 100m; }
            };

            myShop.UsersList.Add(user);
            var result = myShop.LogIn(user.UsernameGet());

            Assert.Fail();
        }




        [TestMethod]
        public void Shop_ShopBuyGame_IfShopDoesntOwnTheGame_ShouldAddGame() {
            StubIGame game = new StubIGame() {
                NameGet = () => { return "Monopoly"; },
                PriceGet = () => { return 100m; }
            };

            myShop.ShopBuyGame(game.NameGet(), game.PriceGet());

            Assert.AreEqual(1, myShop.GamesList.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Shop_ShopBuyGame_IfShopOwnsTheGame_ShouldThrowException() {
            StubIGame game = new StubIGame() {
                NameGet = () => { return "Monopoly"; },
                PriceGet = () => { return 100m; }
            };

            myShop.ShopBuyGame(game.NameGet(), game.PriceGet());
            myShop.ShopBuyGame(game.NameGet(), game.PriceGet());

            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Shop_ShopBuyGame_IfNotGivenValue_ShouldThrowException() {
            StubIGame game = new StubIGame() {
                NameGet = () => { return null; },
                PriceGet = () => { return 100m; }
            };

            myShop.ShopBuyGame(game.NameGet(), game.PriceGet());

            Assert.Fail();
        }




        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Shop_UserBuyGame_IfUserIsNotLoggedIn_ShouldThrowException() {
            StubIUser user = new StubIUser() {
                UsernameGet = () => { return "Mika"; },
                AccountBalanceGet = () => { return 100m; }
            };
            StubIGame game = new StubIGame() {
                NameGet = () => { return "Monopoly"; },
                PriceGet = () => { return 10m; }
            };

            myShop.Register(user.UsernameGet(), user.AccountBalanceGet());
            var registeredUser = myShop.FindUser(user.UsernameGet());

            myShop.ShopBuyGame(game.NameGet(), game.PriceGet());
            myShop.UserBuyGame(registeredUser.Username, game.NameGet());

            Assert.Fail();
        }

        [TestMethod]
        public void Shop_UserBuyGame_IfUserDoesntOwnTheGame_ShouldAddGameToUsersAccount() {
            StubIUser user = new StubIUser() {
                UsernameGet = () => { return "Mika"; },
                AccountBalanceGet = () => { return 100m; }
            };
            StubIGame game = new StubIGame() {
                NameGet = () => { return "Monopoly"; },
                PriceGet = () => { return 10m; }
            };

            myShop.Register(user.UsernameGet(), user.AccountBalanceGet());
            var registeredUser = myShop.FindUser(user.UsernameGet());

            myShop.LogIn(registeredUser.Username);
            var loggedUser = myShop.FindLoggedUser(registeredUser.Username);

            myShop.ShopBuyGame(game.NameGet(), game.PriceGet());
            myShop.UserBuyGame(loggedUser.Username, game.NameGet());

            Assert.AreEqual(1, loggedUser.UsersGamesList.Count);
        }

        [TestMethod]
        public void Shop_UserBuyGame_IfUserDoesntOwnTheGames_ShouldAddMultipleGamesToUsersAccount() {
            StubIUser user = new StubIUser() {
                UsernameGet = () => { return "Mika"; },
                AccountBalanceGet = () => { return 100m; }
            };

            StubIGame game = new StubIGame() {
                NameGet = () => { return "Monopoly"; },
                PriceGet = () => { return 10m; }
            };

            StubIGame game2 = new StubIGame() {
                NameGet = () => { return "Monopoliy"; },
                PriceGet = () => { return 10m; }
            };

            myShop.Register(user.UsernameGet(), user.AccountBalanceGet());
            var registeredUser = myShop.FindUser(user.UsernameGet());

            myShop.LogIn(registeredUser.Username);
            var loggedUser = myShop.FindLoggedUser(registeredUser.Username);

            myShop.ShopBuyGame(game.NameGet(), game.PriceGet());
            myShop.UserBuyGame(loggedUser.Username, game.NameGet());
            myShop.ShopBuyGame(game2.NameGet(), game2.PriceGet());
            myShop.UserBuyGame(loggedUser.Username, game2.NameGet());

            Assert.AreEqual(2, loggedUser.UsersGamesList.Count);
        }

        [TestMethod]
        public void Shop_UserBuyGame_IfUserDoesntOwnTheGame_ShouldRemovePriceValueFromUsersAccount() {
            StubIUser user = new StubIUser() {
                UsernameGet = () => { return "Mika"; },
                AccountBalanceGet = () => { return 100m; }
            };
            StubIGame game = new StubIGame() {
                NameGet = () => { return "Monopoly"; },
                PriceGet = () => { return 10m; }
            };

            myShop.Register(user.UsernameGet(), user.AccountBalanceGet());
            var registeredUser = myShop.FindUser(user.UsernameGet());

            myShop.LogIn(registeredUser.Username);
            var loggedUser = myShop.FindLoggedUser(registeredUser.Username);

            myShop.ShopBuyGame(game.NameGet(), game.PriceGet());
            myShop.UserBuyGame(loggedUser.Username, game.NameGet());

            Assert.AreEqual(90m, loggedUser.AccountBalance);
        }

        [TestMethod]
        public void Shop_UserBuyGame_IfUserDoesntOwnTheGame_ShouldRemoveGameFromShopAccount() {
            StubIUser user = new StubIUser() {
                UsernameGet = () => { return "Mika"; },
                AccountBalanceGet = () => { return 100m; }
            };
            StubIGame game = new StubIGame() {
                NameGet = () => { return "Monopoly"; },
                PriceGet = () => { return 100m; }
            };

            myShop.Register(user.UsernameGet(), user.AccountBalanceGet());
            var registeredUser = myShop.FindUser(user.UsernameGet());

            myShop.LogIn(registeredUser.Username);
            var loggedUser = myShop.FindLoggedUser(registeredUser.Username);

            myShop.ShopBuyGame(game.NameGet(), game.PriceGet());
            myShop.UserBuyGame(loggedUser.Username, game.NameGet());

            Assert.AreEqual(0, myShop.GamesList.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Shop_UserBuyGame_IfUserOwnsTheGame_ShouldThrowException() {
            StubIUser user = new StubIUser() {
                UsernameGet = () => { return "Mika"; },
                AccountBalanceGet = () => { return 100m; }
            };

            StubIGame game = new StubIGame() {
                NameGet = () => { return "Monopoly"; },
                PriceGet = () => { return 10m; }
            };

            myShop.Register(user.UsernameGet(), user.AccountBalanceGet());
            var registeredUser = myShop.FindUser(user.UsernameGet());

            myShop.LogIn(registeredUser.Username);
            var loggedUser = myShop.FindLoggedUser(registeredUser.Username);

            myShop.ShopBuyGame(game.NameGet(), game.PriceGet());
            myShop.UserBuyGame(loggedUser.Username, game.NameGet());
            myShop.ShopBuyGame(game.NameGet(), game.PriceGet());
            myShop.UserBuyGame(loggedUser.Username, game.NameGet());

            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Shop_UserBuyGame_IfUserDoesnthaveMoney_ShouldThrowException() {
            StubIUser user = new StubIUser() {
                UsernameGet = () => { return "Mika"; },
                AccountBalanceGet = () => { return 100m; }
            };

            StubIGame game = new StubIGame() {
                NameGet = () => { return "Monopoly"; },
                PriceGet = () => { return 1000m; }
            };

            myShop.Register(user.UsernameGet(), user.AccountBalanceGet());
            var registeredUser = myShop.FindUser(user.UsernameGet());

            myShop.LogIn(registeredUser.Username);
            var loggedUser = myShop.FindLoggedUser(registeredUser.Username);

            myShop.ShopBuyGame(game.NameGet(), game.PriceGet());
            myShop.UserBuyGame(loggedUser.Username, game.NameGet());

            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Shop_UserBuyGame_IfNotGivenValue_ShouldThrowException() {
            StubIUser user = new StubIUser() {
                UsernameGet = () => { return ""; } // , AccountBalanceGet = () => { return 100m; }
            };
            StubIGame game = new StubIGame() {
                NameGet = () => { return null; } // , PriceGet = () => { return 100m; }
            };

            myShop.UserBuyGame(user.UsernameGet(), game.NameGet());

            Assert.Fail();
        }




        [TestMethod]
        public void Shop_AddingSeveralGames_ShouldShowTheyreUnique() {
            StubIGame game1 = new StubIGame() {
                NameGet = () => { return "Monopoly"; },
                PriceGet = () => { return 100m; }
            };
            StubIGame game2 = new StubIGame() {
                NameGet = () => { return "Taboo"; },
                PriceGet = () => { return 100m; }
            };
            StubIGame game3 = new StubIGame() {
                NameGet = () => { return "5 sekund"; },
                PriceGet = () => { return 100m; }
            };

            myShop.ShopBuyGame(game1.NameGet(), game1.PriceGet());
            myShop.ShopBuyGame(game2.NameGet(), game2.PriceGet());
            myShop.ShopBuyGame(game3.NameGet(), game3.PriceGet());

            CollectionAssert.AllItemsAreUnique(myShop.GamesList);
        }

        [TestMethod]
        public void Shop_AddingSeveralGames_ShouldShowTheyreNotNull() {
            StubIGame game1 = new StubIGame() {
                NameGet = () => { return "Monopoly"; },
                PriceGet = () => { return 100m; }
            };
            StubIGame game2 = new StubIGame() {
                NameGet = () => { return "Taboo"; },
                PriceGet = () => { return 100m; }
            };
            StubIGame game3 = new StubIGame() {
                NameGet = () => { return "5 sekund"; },
                PriceGet = () => { return 100m; }
            };

            myShop.ShopBuyGame(game1.NameGet(), game1.PriceGet());
            myShop.ShopBuyGame(game2.NameGet(), game2.PriceGet());
            myShop.ShopBuyGame(game3.NameGet(), game3.PriceGet());

            CollectionAssert.AllItemsAreNotNull(myShop.GamesList);
        }
    }
}
