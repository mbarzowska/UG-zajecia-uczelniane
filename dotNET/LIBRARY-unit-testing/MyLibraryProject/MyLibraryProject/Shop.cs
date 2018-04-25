using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLibraryProject {
    public class Shop {
        public List<IUser> UsersList = new List<IUser>();
        public List<IUser> LoggedUsersList = new List<IUser>();
        public List<IGame> GamesList = new List<IGame>();

        public IUser FindUser(string username) {
            if (String.IsNullOrEmpty(username)) {
                throw new ArgumentNullException("Empty string");
            }
            return UsersList.Find(u => u.Username == username);
        }

        public IUser FindLoggedUser(string username) {
            if (String.IsNullOrEmpty(username)) {
                throw new ArgumentNullException("Empty string");
            }
            return LoggedUsersList.Find(u => u.Username == username);
        }

        public bool UserExists(string username) {
            if (FindUser(username) == null || String.IsNullOrEmpty(username)) {
                return false;
            }
            return true;
        }

        public bool UserIsLoggedIn(string username) {
            if (FindLoggedUser(username) == null || String.IsNullOrEmpty(username)) {
                return false;
            }
            return true;
        }

        public IGame FindGame(string gameName) {
            if (String.IsNullOrEmpty(gameName)) {
                throw new ArgumentNullException("Empty string");
            }
            return GamesList.Find(g => g.Name == gameName);
        }
        
        public bool GameExists(string gameName) {
            if (FindGame(gameName) == null || String.IsNullOrEmpty(gameName)) {
                return false;
            }
            return true;
        }

        public bool Register(string username, decimal accountBalance) {
            if (UserExists(username)) {
                return false;
            }

            User newUser = new User(username, accountBalance);
            UsersList.Add(newUser);
            return true;
        }

        public bool LogIn(string username) {
            if (String.IsNullOrEmpty(username)) {
                throw new ArgumentNullException("Empty string");
            }

            if (UserExists(username)) {
                LoggedUsersList.Add(FindUser(username));
                return true;
            } else {
                throw new InvalidOperationException("User: " + username + " doesn't exist");
            }
        }

        public bool IsLoggedIn(string username) {
            if (String.IsNullOrEmpty(username)) {
                throw new ArgumentNullException("Empty string");
            }

            if (UserIsLoggedIn(username)) {
                return true;
            } else {
                throw new InvalidOperationException("User: " + username + " is not logged in");
            }
        }

        public void ShopBuyGame(string gameName, decimal price) {
            if (String.IsNullOrEmpty(gameName)) {
                throw new ArgumentNullException("Empty string");
            }

            if (GameExists(gameName)) {
                throw new InvalidOperationException("We already own the game " + gameName);
            }
            
            Game newGame = new Game(gameName, price);
            GamesList.Add(newGame);
        }

        public void UserBuyGame(string username, string gameName) {
            IUser user = FindUser(username);
            IGame game = FindGame(gameName);

            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(gameName)) {
                throw new ArgumentNullException("Empty string");
            }

            if (UserIsLoggedIn(username)) {
                if (user.UsersGamesList.Find(u => u.Name == gameName) != null) {
                    throw new InvalidOperationException("User " + username + " already owns the game " + gameName);
                }

                if (user.AccountBalance >= game.Price) {
                    user.UsersGamesList.Add(game);
                    user.AccountBalance -= game.Price;
                    GamesList.Remove(game);
                } else {
                    throw new InvalidOperationException("User " + username + " doesn't have enough money to buy " + gameName);
                }
            } else {
                throw new InvalidOperationException("User " + username + " is not logged in");
            }
        }
    }
}
