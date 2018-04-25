using System.Collections.Generic;

namespace MyLibraryProject {
    public interface IUser {
        string Username { get; }
        decimal AccountBalance { get; set; }
        List<IGame> UsersGamesList { get; }
        void AddMoney(decimal amount);
        string ShowAccountBalance();
    }

    public class User : IUser {
        public string Username { get; set; }
        public decimal AccountBalance { get; set; }
        public List<IGame> UsersGamesList { get; set; }

        public User() {
            UsersGamesList = new List<IGame>();
        }

        public User(string username, decimal accountBalance) : this () {
            Username = username;
            AccountBalance = accountBalance;
        }

        public void AddMoney(decimal amount) {
            AccountBalance += amount;
        }

        public string ShowAccountBalance() {
            return "Users " + Username + "account balance is: " + AccountBalance;
        }
    }
}
