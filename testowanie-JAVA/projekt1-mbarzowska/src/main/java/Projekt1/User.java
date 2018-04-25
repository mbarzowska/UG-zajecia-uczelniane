package Projekt1;

import java.util.ArrayList;

public class User {
    private String Username;
    private String Password;
    private double AccountBalance;
    public ArrayList<BookedGame> UsersBookedGamesList;

    public User(String username, String password) {
        Username = username;
        Password = password;
        UsersBookedGamesList = new ArrayList<BookedGame>();
    }

    public void AddMoney(String sAmount) {
        double amount;
        try {
            amount = Double.parseDouble(sAmount);
            AccountBalance += amount;
        } catch (Exception ex) {
            PrintFormatWarning();
        }
    }

    public void PrintFormatWarning() {
        System.out.println("Invalid data! Please make sure given amount is a double format number [00.0]!");
    }

    public void ShowAccountBalance() {
        System.out.println("Users " + Username + " account balance is: " + AccountBalance);
    }

    public void ShowBookedGamesList() {
        for (BookedGame item : this.UsersBookedGamesList) {
            System.out.println("ID: " + item.getBookingID()
                    + "; booked game name: " + item.getGame().getName()
                    + "; booked game description: " + item.getGame().getDescription()
                    + "; date: " + item.getDate()
                    + "; time: " + item.getTime());
        }
    }

    // GETTERS, SETTERS deleted due to not using
    public String getUsername() {
        return Username;
    }

    public String getPassword() {
        return Password;
    }

    public double getAccountBalance() {
        return AccountBalance;
    }

    public void setAccountBalance(double accountBalance) {
        this.AccountBalance = accountBalance;
    }
}
