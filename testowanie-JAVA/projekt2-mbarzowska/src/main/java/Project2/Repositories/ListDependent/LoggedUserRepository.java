package Project2.Repositories.ListDependent;

import Project2.Models.User;
import Project2.Repositories.Interfaces.LoggedUserInfoHandling;

import java.util.ArrayList;
import java.util.List;

public class LoggedUserRepository implements LoggedUserInfoHandling {

    private List<User> loggedUsersList = new ArrayList<User>();

    @Override
    public User getUser(String username) {
        return loggedUsersList.stream().filter(x -> x.getUsername().equals(username)).findFirst().orElse(null);
    }

    @Override
    public void addUser(User user) {
        loggedUsersList.add(user);
    }

    @Override
    public void deleteUser(String username) {
        loggedUsersList.removeIf(x -> x.getUsername().equals(username));

    }

    @Override
    public boolean userExists(String username) {
        return loggedUsersList.stream().anyMatch(x -> x.getUsername().equals(username));
    }

    @Override
    public void addMoneyToUsersAccount(String username, String sAmount) {
        double amount;
        User user = loggedUsersList.stream().filter(x -> x.getUsername().equals(username)).findFirst().orElse(null);
        if(user == null)
            return;
        try {
            amount = Double.parseDouble(sAmount);
            double newBalance = user.getAccountBalance() + amount;
            user.setAccountBalance(newBalance);
        } catch (Exception ex) {
            PrintFormatWarning();
        }
    }

    private void PrintFormatWarning() {
        System.out.println("Invalid data! Please make sure given amount is a double format number [00.0]!");
    }
}
