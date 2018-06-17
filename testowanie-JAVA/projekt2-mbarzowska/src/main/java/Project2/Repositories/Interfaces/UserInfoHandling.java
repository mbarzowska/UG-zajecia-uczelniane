package Project2.Repositories.Interfaces;

import Project2.Models.User;

public interface UserInfoHandling {
    User getUser(String username);
    void addUser(User user);
    boolean userExists(String username);
}
