package Project2.Controllers;

import Project2.Models.BookedGame;
import Project2.Models.Game;
import Project2.Models.Interfaces.Validation;
import Project2.Models.User;
import Project2.Repositories.Interfaces.BookedGameHandling;
import Project2.Repositories.Interfaces.GameInfoHandling;
import Project2.Repositories.Interfaces.LoggedUserInfoHandling;
import Project2.Repositories.Interfaces.UserInfoHandling;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.nio.charset.Charset;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.text.ParseException;
import java.time.LocalDate;
import java.util.Arrays;
import java.util.List;
import java.util.Random;
import java.util.Scanner;

public class BoardCafe {

    private GameInfoHandling _gameRepository;
    private BookedGameHandling _bookedGameRepository;
    private UserInfoHandling _userRepository;
    private LoggedUserInfoHandling _loggedUserRepository;
    private Validation _validator;

    public BoardCafe(GameInfoHandling gameRepository, BookedGameHandling bookedGameRepository, UserInfoHandling userRepository, LoggedUserInfoHandling loggedUserRepository, Validation validator) {
        _gameRepository = gameRepository;
        _bookedGameRepository = bookedGameRepository;
        _userRepository = userRepository;
        _loggedUserRepository = loggedUserRepository;
        _validator = validator;
    }

    public void loadDatabase(String databasePath) throws FileNotFoundException {
        Scanner scanner = new Scanner(new File(databasePath));
        while(scanner.hasNextLine()) {
            try {
                String[] gameData = scanner.nextLine().split(";");

                String name = gameData[0];
                double price = Double.parseDouble(gameData[1]);
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
                if (_gameRepository.gameExists(name) == false) {
                    _gameRepository.addGame(new Game(name, price, availableHoursStart, availableHoursEnd, days, unavailableHoursStart, unavailableHoursEnd, description));
                }
            } catch(Exception e) {
                continue;
            }
        }
        scanner.close();
    }

    public boolean register(String username, String password) {
        if (_userRepository.userExists(username)) {
            throw new IllegalArgumentException("Username " + username + " is already taken!");
        }

        if (!_validator.validateUser(username, password)) {
            throw new IllegalArgumentException("Username or password is an empty string!");
        }

        User newUser = new User(username, password);
        _userRepository.addUser(newUser);
        return true;
    }

    public boolean logIn(String username, String password) {
        if (!_validator.validateUser(username, password)) {
            throw new IllegalArgumentException("Username or password is an empty string!");
        }

        if (_userRepository.userExists(username)) {
            User user = _userRepository.getUser(username);
            if (user.getPassword().equals(password)) {
               _loggedUserRepository.addUser(user);
                System.out.println("User " + username + " logged in successfully!");
                return true;
            } else {
                throw new IllegalArgumentException("Incorrect password!");
            }
        } else {
            throw new UnsupportedOperationException("User: " + username + " doesn't exist");
        }
    }

    public boolean logOut(User user) {
        if (!_userRepository.userExists(user.getUsername())) {
            throw new IllegalArgumentException("User does not exist!");
        }

        User userToLogout = _loggedUserRepository.getUser(user.getUsername());
        if (userToLogout == null) {
            throw new UnsupportedOperationException("User is currently not logged in, can't perform logout!");
        } else {
            _loggedUserRepository.deleteUser(userToLogout.getUsername());
            System.out.println("User " + user.getUsername() + " logged out successfully!");
            return true;
        }
    }

    public boolean userBookGame(User user, String gameName, String sDate, String sTime) throws ParseException, IOException {
        _validator.validateGameName(gameName);

        Game game = _gameRepository.getGame(gameName);
        if (game == null) {
            throw new UnsupportedOperationException("This game does not exist in our database!");
        }

        LocalDate date = _validator.validateBookingDate(game, sDate);

        int time = _validator.validateBookingTime(game, sTime);

        if (_loggedUserRepository.userExists(user.getUsername())) {
            User loggedUser = _loggedUserRepository.getUser(user.getUsername());

            if (_bookedGameRepository.getBookedGame(loggedUser.getUsername(), gameName) != null) {
                throw new UnsupportedOperationException("User " + user.getUsername() + "already booked the game " + gameName);
            }

            List<BookedGame> bookedGames = _bookedGameRepository.getBookedGames();
            for (BookedGame item : bookedGames) {
                    if (item.getGame().getName().equals(gameName) && item.getDate().equals(date) && item.getTime() == time) {
                        throw new UnsupportedOperationException("User " + item.getUsername() + " already booked the game " + gameName + " on " + item.getDate().toString() + ", at " + time);
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
                BookedGame bookedGame = new BookedGame(loggedUser.getUsername(), game, date, time, bookingID);

                _bookedGameRepository.addBookedGame(bookedGame);
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

    public void showUsersBookedGamesList(String username) {
        List<BookedGame> list = _bookedGameRepository.getBookedGames();

        if (list.isEmpty()) {
            System.out.println("The list is currently empty");
        }

        for (BookedGame item : list) {
            if (item.getUsername().equals(username)) {
                System.out.println("ID: " + item.getBookingID()
                        + "; booked game name: " + item.getGame().getName()
                        + "; booked game description: " + item.getGame().getDescription()
                        + "; date: " + item.getDate()
                        + "; time: " + item.getTime());
            }
        }
    }

    public User findLoggedUser(String username) {
        return _loggedUserRepository.getUser(username);
    }

    public boolean userIsLoggedIn(User user) {
        if (user == null) return false;
        else return _loggedUserRepository.userExists(user.getUsername());
    }

    public boolean gameExists(String gameName) {
        return _gameRepository.gameExists(gameName);
    }

    public boolean addMoneyToUsersAccount(String username, String sAmount) {
        if(findLoggedUser(username) == null) return false;
        _loggedUserRepository.addMoneyToUsersAccount(username, sAmount);
        return true;
    }

    public String showGamesList() {
        StringBuilder gamesListBuilder = new StringBuilder();
        List<Game> list = _gameRepository.getGames();

        if (list.isEmpty()) {
            return "The list is currently empty";
        }

        for (Game item : list) {
            gamesListBuilder.append("Name: " + item.getName()
                    + " | price: " + item.getPrice()
                    + " | hours: from " + item.getAvailableHoursStart() + " 'till " + item.getAvailableHoursEnd()
                    + " | unavailable: " + _validator.transformDaysToWords(item.getDays())
                    + "| unavailable hours: from " + item.getUnavailableHoursStart() + " 'till " + item.getUnavailableHoursEnd() + "\n");
        }

        String gamesList = gamesListBuilder.toString();
        return gamesList;
    }
}