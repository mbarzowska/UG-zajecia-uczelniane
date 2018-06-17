package Project2.Repositories.Interfaces;

import Project2.Models.User;

public interface LoggedUserInfoHandling {
    User getUser(String username);
    void addUser(User user);
    void deleteUser(String username);
    boolean userExists(String username);

    void addMoneyToUsersAccount(String username, String sAmount);
}
