package Project2.Repositories.ListDependent;

import Project2.Models.User;
import Project2.Repositories.Interfaces.UserInfoHandling;

import java.util.ArrayList;
import java.util.List;

public class UserRepository implements UserInfoHandling {

    private List<User> usersList = new ArrayList<User>();

    @Override
    public User getUser(String username) {
        return usersList.stream().filter(x -> x.getUsername().equals(username)).findFirst().orElse(null);
    }

    @Override
    public void addUser(User user) {
        usersList.add(user);
    }

    @Override
    public boolean userExists(String username) {
        return usersList.stream().anyMatch(x -> x.getUsername().equals(username));
    }
}
