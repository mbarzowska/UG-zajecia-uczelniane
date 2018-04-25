package Projekt1;

import java.io.*;
import java.nio.charset.Charset;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.text.ParseException;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.*;

public class BoardCafe {
    public List<User> UsersList = new ArrayList<User>();
    public List<User> LoggedUsersList = new ArrayList<User>();
    public List<Game> GamesList = new ArrayList<Game>();

    public void LoadDatabase(String databasePath) throws FileNotFoundException {
        Scanner scanner = new Scanner(new File(databasePath));
        while(scanner.hasNextLine()) {
            try {
                String[] gameData = scanner.nextLine().split(";");

                String name = gameData[0];
                double price = Double.parseDouble(gameData[1]);
                // godziny dostepnosci   gameData[2]
                // niedostepne dni   gameData[3]
                // niedostepne godziny  gameData[4]
                // opis gameData[5]

                String[] tmpOpenHours = gameData[2].split("-");
                int availableHoursStart = Integer.parseInt(tmpOpenHours[0]);
                int availableHoursEnd = Integer.parseInt(tmpOpenHours[1]);

                String[] tmpClosedDays = gameData[3].split(" ");
                boolean days[] = new boolean[7];
                for (int i = 0; i < tmpClosedDays.length; i++) {
                    if (Integer.parseInt(tmpClosedDays[i]) == 1) {
                        days[i] = true;
                    } else {
                        days[i] = false;
                    }
                }

                String[] tmpUnavailableHours = gameData[4].split("-");
                int unavailableHoursStart = Integer.parseInt(tmpUnavailableHours[0]);
                int unavailableHoursEnd = Integer.parseInt(tmpUnavailableHours[1]);

                String description = gameData[5];
                GamesList.add(new Game(name, price, availableHoursStart, availableHoursEnd, days, unavailableHoursStart, unavailableHoursEnd, description));
            } catch(Exception e) {
                continue;
            }
        }
        scanner.close();
    }

    private static User findUserByUsername(List<User> usersList, String username) {
        return usersList.stream().filter(user -> username.equals(user.getUsername())).findFirst().orElse(null);
    }

    public User FindUser(String username) {
        if (username == null || username.isEmpty()) {
            throw new IllegalArgumentException("Username is an empty string!");
        }
        return findUserByUsername(UsersList, username);
    }

    public User FindLoggedUser(String username) {
        if (username == null || username.isEmpty()) {
            throw new IllegalArgumentException("Username is an empty string!");
        }
        return findUserByUsername(LoggedUsersList, username);
    }

    public boolean UserExists(String username) {
        if (username == null || username.isEmpty() || FindUser(username) == null ) {
            return false;
        }
        return true;
    }

    public boolean UserIsLoggedIn(User user) {
        if (user == null || FindLoggedUser(user.getUsername()) == null) {
            return false;
        }
        return true;
    }

    private static Game findGameByName(List<Game> gamesList, String gameName) {
        return gamesList.stream().filter(game -> gameName.equals(game.getName())).findFirst().orElse(null);
    }

    private static BookedGame findUsersGameByName(List<BookedGame> gamesList, String gameName) {
        return gamesList.stream().filter(item -> gameName.equals(item.getGame().getName())).findFirst().orElse(null);
    }

    public Game FindGame(String gameName) {
        if (gameName == null || gameName.isEmpty()) {
            throw new IllegalArgumentException("Game name is an empty string!");
        }
        return findGameByName(GamesList, gameName);
    }

    public boolean GameExists(String gameName) {
        if (gameName == null || gameName.isEmpty() || FindGame(gameName) == null) {
            return false;
        }
        return true;
    }

    public boolean Register(String username, String password) {
        if (username == null || username.isEmpty()
            || password == null || password.isEmpty()) {
            throw new IllegalArgumentException("Username or password is an empty string!");
        }
        if (UserExists(username)) {
            throw new IllegalArgumentException("Username " + username + " is already taken!");
        }

        User newUser = new User(username, password);
        UsersList.add(newUser);
        return true;
    }

    public boolean LogIn(String username, String password) {
        if (username == null || username.isEmpty()
            || password == null || password.isEmpty()) {
            throw new IllegalArgumentException("Username or password is an empty string!");
        }

        if (UserExists(username)) {
            User user = FindUser(username);
            if (user.getPassword().equals(password)) {
                LoggedUsersList.add(user);
                System.out.println("User " + username + " logged in successfully!");
                return true;
            } else {
                throw new IllegalArgumentException("Incorrect password!");
            }
        } else {
            throw new UnsupportedOperationException("User: " + username + " doesn't exist");
        }
    }

    public boolean LogOut(User user) {
        if (!UserExists(user.getUsername())) {
            throw new IllegalArgumentException("User does not exist!");
        }

        User userToLogout = FindLoggedUser(user.getUsername());
        if (userToLogout == null) {
            throw new UnsupportedOperationException("User is currently not logged in, can't perform logout!");
        } else {
            LoggedUsersList.remove(userToLogout);
            System.out.println("User " + user.getUsername() + " logged out successfully!");
            return true;
        }
    }

    public boolean UserBookGame(User user, String gameName, String sDate, String sTime) throws ParseException, IOException {
        if (gameName == null || gameName.isEmpty()) {
            throw new IllegalArgumentException("Wrong input!\nGame name is an empty string!");
        }

        Game game = FindGame(gameName);
        if (game == null) {
            throw new UnsupportedOperationException("This game does not exist in our database!");
        }

        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("dd/MM/yyyy");
        LocalDate date = LocalDate.parse(sDate, formatter);

        if (transformDaysToWords(game.getDays()).contains(date.getDayOfWeek().toString())) {
            throw new UnsupportedOperationException("You can't book a game for a date it's not available to book, sorry!");
        }

        int time;

        try {
            time = Integer.parseInt(sTime);
        } catch (Exception ex) {
            System.out.println("Invalid data! Please make sure given time is a number!");
            return false;
        }

        if (time < game.getAvailableHoursStart() || time >= game.getAvailableHoursEnd()) {
            throw new UnsupportedOperationException("You can't book a game for a time period when we're closed, sorry!");
        }

        if (time >= game.getAvailableHoursStart() && time < game.getAvailableHoursEnd()) {
            if (time >= game.getUnavailableHoursStart() && time < game.getUnavailableHoursEnd()) {
                throw new UnsupportedOperationException("You can't book a game for a time period when it's not available to book, sorry!");
            }
        }

        if (UserIsLoggedIn(user)) {
            User loggedUser = FindLoggedUser(user.getUsername());

            if (findUsersGameByName(loggedUser.UsersBookedGamesList, gameName) != null) {
                throw new UnsupportedOperationException("User " + user.getUsername() + "already booked the game " + gameName);
            }

            for (User allUsers : UsersList) {
                for (BookedGame item : allUsers.UsersBookedGamesList) {
                    if (item.getGame().getName().equals(gameName) && item.getDate().equals(date) && item.getTime() == time) {
                        throw new UnsupportedOperationException("User " + allUsers.getUsername() + " already booked the game " + gameName + " on " + item.getDate().toString() + ", at " + time);
                    }
                }
            }

            if (loggedUser.getAccountBalance() >= game.getPrice()) {
                Random rand = new Random();
                int  randomValue = rand.nextInt(50000) + 1;
                String bookingID = randomValue + "/"
                        + loggedUser.getUsername() + "/"
                        + game.getName() + "/"
                        + date + "/"
                        + time;
                BookedGame bookedGame = new BookedGame(game, date, time, bookingID);

                loggedUser.UsersBookedGamesList.add(bookedGame);
                loggedUser.setAccountBalance(loggedUser.getAccountBalance() - game.getPrice());

                List<String> lines = Arrays.asList("CONFIRMATION\n Successfully booked a game: ", game.getName(), " for: ", loggedUser.getUsername(), " on: ", date.toString(), " time: ", String.valueOf(time), " ID: ", String.valueOf(randomValue));
                Path file = Paths.get("src/main/resources/confirmations/","bookingNo" + randomValue +".txt");
                Files.write(file, lines, Charset.forName("UTF-8"));

                System.out.println(lines.toString().replaceAll(", ", "").replaceAll("\\[", "").replaceAll("]", ""));
            } else {
                throw new UnsupportedOperationException("User " + user.getUsername() + " doesn't have enough money to book " + gameName);
            }
        }
        return true;
    }

    private static String transformDaysToWords(boolean[] table) {
        String[] daysInWords = {"MONDAY", "TUESDAY", "WEDNESDAY", "THURSDAY", "FRIDAY", "SATURDAY", "SUNDAY"};
        StringBuilder wordedDaysBuilder = new StringBuilder();
        for (int i = 0; i < table.length; i++) {
            if (table[i] == true) {
                wordedDaysBuilder.append(daysInWords[i] + "; ");
            } else {
                continue;
            }
        }
        String wordedDays = wordedDaysBuilder.toString();
        return wordedDays;
    }

    public void ShowGamesList() {
        for (Game item : this.GamesList) {
            System.out.println("Name: " + item.getName()
                    + " | price: " + item.getPrice()
                    + " | hours: from " + item.getAvailableHoursStart() + " 'till " + item.getAvailableHoursEnd()
                    + " | unavailable: " + transformDaysToWords(item.getDays())
                    + "| unavailable hours: from " + item.getUnavailableHoursStart() + " 'till " + item.getUnavailableHoursEnd());
        }
    }
}