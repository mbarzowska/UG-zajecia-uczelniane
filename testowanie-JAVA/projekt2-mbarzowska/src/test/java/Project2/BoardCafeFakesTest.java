package Project2;

import Project2.Controllers.BoardCafe;
import Project2.Models.Game;
import Project2.Models.Interfaces.Validation;
import Project2.Models.User;
import Project2.Models.Validator;
import Project2.Repositories.ListDependent.BookedGameRepository;
import Project2.Repositories.ListDependent.GameRepository;
import Project2.Repositories.Interfaces.BookedGameHandling;
import Project2.Repositories.Interfaces.GameInfoHandling;
import Project2.Repositories.Interfaces.LoggedUserInfoHandling;
import Project2.Repositories.Interfaces.UserInfoHandling;
import Project2.Repositories.ListDependent.LoggedUserRepository;
import Project2.Repositories.ListDependent.UserRepository;

import java.io.File;
import java.io.IOException;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.text.ParseException;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;

import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import org.assertj.core.api.Assertions;

public class BoardCafeFakesTest {

    private UserInfoHandling userRepository;
    private LoggedUserInfoHandling loggedUserRepository;
    private GameInfoHandling gameRepository;
    private BookedGameHandling bookedGameRepository;
    private Validation validator;

    private BoardCafe bc;

    private User testDefaultUser;
    private String testDefaultUsersUsername, testDefaultUsersPassword;
    private User testDefaultSecondUser;
    private String testDefaultSecondUsersUsername, testDefaultSecondUsersPassword;
    private Game testDefaultGame;
    private String testDefaultGamesName; private double testDefaultGamesPrice; private int testDefaultGamesAHS; private int testDefaultGamesAHE;
    private boolean[] testDefaultGamesDays; private int testDefaultGamesUHS; private int testDefaultGamesUHE; private String testDefaultGamesDescription;
    private String testDefaultGamesBookDateUnavailDay, testDefaultGamesBookDateAvailDay,
            testDefaultGamesBookAvailTime, testDefaultGamesBookUnavailTime, testDefaultGamesBookTimeClosed;
    private String pathToMixedDatabase, pathToCorrectDatabase, pathToIncorrectDatabase, incorrectDatabasePath;

    @BeforeEach
    public void setUp() {
        userRepository = new UserRepository();
        loggedUserRepository = new LoggedUserRepository();
        gameRepository = new GameRepository();
        bookedGameRepository = new BookedGameRepository();
        validator = new Validator();
        bc = new BoardCafe(gameRepository, bookedGameRepository, userRepository, loggedUserRepository, validator);

        testDefaultUser = new User("Mika", "qwerty");
        testDefaultUsersUsername = testDefaultUser.getUsername();
        testDefaultUsersPassword = testDefaultUser.getPassword();
        testDefaultSecondUser = new User("Thor", "qwerty");
        testDefaultSecondUsersUsername = testDefaultSecondUser.getUsername();
        testDefaultSecondUsersPassword = testDefaultSecondUser.getPassword();
        testDefaultGame = new Game ("Ego", 55.0, 10, 20, new boolean[]{false, false, false, true, true, true, false}, 16, 20, "OK");
        testDefaultGamesName = testDefaultGame.getName();
        testDefaultGamesPrice = testDefaultGame.getPrice();
        testDefaultGamesAHS = testDefaultGame.getAvailableHoursStart();
        testDefaultGamesAHE = testDefaultGame.getAvailableHoursEnd();
        testDefaultGamesDays = testDefaultGame.getDays();
        testDefaultGamesUHS = testDefaultGame.getUnavailableHoursStart();
        testDefaultGamesUHE = testDefaultGame.getUnavailableHoursEnd();
        testDefaultGamesDescription = testDefaultGame.getDescription();
        testDefaultGamesBookDateUnavailDay = "28/04/2018";
        testDefaultGamesBookDateAvailDay = "29/04/2018";
        testDefaultGamesBookAvailTime = "11";
        testDefaultGamesBookUnavailTime = "16";
        testDefaultGamesBookTimeClosed = "21";
        pathToMixedDatabase = "src/test/resources/database.csv";
        pathToCorrectDatabase = "src/test/resources/dbCorrectRecord.csv";
        pathToIncorrectDatabase = "src/test/resources/dbIncorrectRecord.csv";
        incorrectDatabasePath = "src/test/resources/doesNotExist.csv";
    }

    @Test
    public void userBookGameWhenAllConditionsAreMetCreatesConfirmationFile() throws IOException, ParseException {
        bc.register(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.logIn(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.addMoneyToUsersAccount(testDefaultUsersUsername, "1000");
        gameRepository.addGame(testDefaultGame);
        Path file = Paths.get("src/main/resources/confirmations/");
        File parentDir =  file.toFile();
        int filesBefore = parentDir.list().length;

        bc.userBookGame(testDefaultUser, testDefaultGamesName, testDefaultGamesBookDateAvailDay, testDefaultGamesBookAvailTime);

        Assertions.assertThat(filesBefore + 1).isEqualTo(parentDir.list().length);
    }

   @Test
    public void userBookGameWhenAllConditionsAreMetAddsGameToUsersAccount() throws IOException, ParseException {
        bc.register(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.logIn(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.addMoneyToUsersAccount(testDefaultUsersUsername, "1000");
        gameRepository.addGame(testDefaultGame);

        bc.userBookGame(testDefaultUser, testDefaultGamesName, testDefaultGamesBookDateAvailDay, testDefaultGamesBookAvailTime);

        Assertions.assertThat(bookedGameRepository.getBookedGame(testDefaultUsersUsername, testDefaultGamesName)).isNotNull();
    }

    @Test
    public void userBookGameWhenGameAlreadyBookedByUserThrowsException() throws IOException, ParseException {
        bc.register(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.logIn(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.addMoneyToUsersAccount(testDefaultUsersUsername, "1000");
        gameRepository.addGame(testDefaultGame);

        bc.userBookGame(testDefaultUser, testDefaultGamesName, testDefaultGamesBookDateAvailDay, "11");

        Assertions.assertThatExceptionOfType(UnsupportedOperationException.class).isThrownBy(() -> { bc.userBookGame(testDefaultUser, testDefaultGamesName, testDefaultGamesBookDateAvailDay, "12"); })
                .withMessage("%s", "User " + testDefaultUsersUsername + "already booked the game " + testDefaultGamesName);
    }


    @Test
    public void userBookGameWhenGameAlreadyBookedBySomeoneThrowsException() throws IOException, ParseException {
        bc.register(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.logIn(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.addMoneyToUsersAccount(testDefaultUsersUsername, "1000");
        bc.register(testDefaultSecondUsersUsername, testDefaultSecondUsersPassword);
        bc.logIn(testDefaultSecondUsersUsername, testDefaultSecondUsersPassword);
        bc.addMoneyToUsersAccount(testDefaultSecondUsersUsername, "1000");
        gameRepository.addGame(testDefaultGame);

        bc.userBookGame(testDefaultSecondUser, testDefaultGamesName, testDefaultGamesBookDateAvailDay, testDefaultGamesBookAvailTime);

        Assertions.assertThatExceptionOfType(UnsupportedOperationException.class).isThrownBy(() -> { bc.userBookGame(testDefaultUser, testDefaultGamesName, testDefaultGamesBookDateAvailDay, testDefaultGamesBookAvailTime); })
                .withMessage("%s", "User " + testDefaultSecondUsersUsername + " already booked the game " + testDefaultGamesName + " on " + LocalDate.parse(testDefaultGamesBookDateAvailDay, DateTimeFormatter.ofPattern("dd/MM/yyyy")).toString() + ", at " + "11");
    }

    @Test
    public void userBookGameWhenUserDoesntHaveMoneyThrowsException() throws IOException, ParseException {
        bc.register(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.logIn(testDefaultUsersUsername, testDefaultUsersPassword);
        gameRepository.addGame(testDefaultGame);

        Assertions.assertThatExceptionOfType(UnsupportedOperationException.class).isThrownBy(() -> { bc.userBookGame(testDefaultUser, testDefaultGamesName, testDefaultGamesBookDateAvailDay, "11"); })
                .withMessage("%s", "User " + testDefaultUsersUsername + " doesn't have enough money to book " + testDefaultGamesName);
    }

    @Test
    public void userBookGameWhenTimeUnavailableThrowsException() throws IOException, ParseException {
        bc.register(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.logIn(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.addMoneyToUsersAccount(testDefaultUsersUsername, "1000");
        gameRepository.addGame(testDefaultGame);

        Assertions.assertThatExceptionOfType(UnsupportedOperationException.class).isThrownBy(() -> { bc.userBookGame(testDefaultUser, testDefaultGamesName, testDefaultGamesBookDateAvailDay, testDefaultGamesBookUnavailTime); })
                .withMessage("%s", "You can't book a game for a time period when it's not available to book, sorry!");
    }

    @Test
    public void userBookGameWhenTimeClosedThrowsException() throws IOException, ParseException {
        bc.register(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.logIn(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.addMoneyToUsersAccount(testDefaultUsersUsername, "1000");
        gameRepository.addGame(testDefaultGame);

        Assertions.assertThatExceptionOfType(UnsupportedOperationException.class).isThrownBy(() -> { bc.userBookGame(testDefaultUser, testDefaultGamesName, testDefaultGamesBookDateAvailDay, testDefaultGamesBookTimeClosed); })
                .withMessage("%s", "You can't book a game for a time period when we're closed, sorry!");
    }

    @Test
    public void userBookGameWhenDateUnavailableThrowsException() throws IOException, ParseException {
        bc.register(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.logIn(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.addMoneyToUsersAccount(testDefaultUsersUsername, "1000");
        gameRepository.addGame(testDefaultGame);

        Assertions.assertThatExceptionOfType(UnsupportedOperationException.class).isThrownBy(() -> { bc.userBookGame(testDefaultUser, testDefaultGamesName, testDefaultGamesBookDateUnavailDay, testDefaultGamesBookAvailTime); })
                .withMessage("%s", "You can't book a game for a date it's not available to book, sorry!");
    }

    @Test
    public void userBookGameWhenShopDoesntOwnTheGameThrowsException() throws IOException, ParseException {
        bc.register(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.logIn(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.addMoneyToUsersAccount(testDefaultUsersUsername, "1000");

        Assertions.assertThatExceptionOfType(UnsupportedOperationException.class).isThrownBy(() -> { bc.userBookGame(testDefaultUser, testDefaultGamesName, testDefaultGamesBookDateAvailDay, testDefaultGamesBookAvailTime); })
                .withMessage("%s", "This game does not exist in our database!");
    }

    @Test
    public void userBookGameWhenGivenNullGameNameThrowsException() throws IOException, ParseException {
        bc.register(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.logIn(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.addMoneyToUsersAccount(testDefaultUsersUsername, "1000");

        Assertions.assertThatIllegalArgumentException().isThrownBy(() -> { bc.userBookGame(testDefaultUser, null, testDefaultGamesBookDateAvailDay, testDefaultGamesBookAvailTime); })
                .withMessage("%s", "Wrong input!\nGame name is an empty string!");
    }

    @Test
    public void userBookGameWhenGivenEmptyGameNameThrowsException() throws IOException, ParseException {
        bc.register(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.logIn(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.addMoneyToUsersAccount(testDefaultUsersUsername, "1000");

        Assertions.assertThatIllegalArgumentException().isThrownBy(() -> { bc.userBookGame(testDefaultUser, null, testDefaultGamesBookDateAvailDay, testDefaultGamesBookAvailTime); })
                .withMessage("%s", "Wrong input!\nGame name is an empty string!");
    }

    @Test
    public void showGamesListWhenListIsEmptyReturnsStringSayingThat() {
        String result = bc.showGamesList();

        Assertions.assertThat(result).isEqualTo("The list is currently empty");
    }

    @Test
    public void showGamesListWhenListContainsGameXReturnsStringContainingGameX() {
        Game game = new Game("Eurobiznes", 40.0, 10, 20, new boolean[]{false, false, false, false, true, true, false}, 14, 18, "OK");
        gameRepository.addGame(game);

        String result = bc.showGamesList();

        Assertions.assertThat(result).contains(game.getName());
    }

    @Test
    public void findLoggedUserWhenUserIsNotLoggedInReturnsNull() {
        User mika = new User("Mika", "psswd");

        User result = bc.findLoggedUser(mika.getUsername());

        Assertions.assertThat(result).isNull();
    }

    @Test
    public void findLoggedUserWhenUserIsLoggedInReturnsUser() {
        User mika = new User("Mika", "psswd");
        loggedUserRepository.addUser(mika);

        User result = bc.findLoggedUser(mika.getUsername());

        Assertions.assertThat(result).isEqualTo(mika);
    }

    @Test
    public void userIsLoggedInWithInvalidDataCaseNullReturnsFalse() {
        boolean result = bc.userIsLoggedIn(null);

        Assertions.assertThat(result).isFalse();
    }

    @Test
    public void userIsLoggedInWhenUserIsNotLoggedInReturnsFalse() {
        User mika = new User("Mika", "psswd");

        boolean result = bc.userIsLoggedIn(mika);

        Assertions.assertThat(result).isFalse();
    }

    @Test
    public void userIsLoggedInWhenUserIsLoggedInReturnsTrue() {
        User mika = new User("Mika", "psswd");
        loggedUserRepository.addUser(mika);

        boolean result = bc.userIsLoggedIn(mika);

        Assertions.assertThat(result).isTrue();
    }

    @Test
    public void gameExistsWhenGameDoesNotExistReturnsFalse() {
        Game game = new Game("Eurobiznes", 40.0, 10, 20, new boolean[]{false, false, false, false, true, true, false}, 14, 18, "OK");

        boolean result = bc.gameExists(game.getName());

        Assertions.assertThat(result).isFalse();
    }

    @Test
    public void gameExistsWhenGameDoesExistReturnsTrue() {
        Game game = new Game("Eurobiznes", 40.0, 10, 20, new boolean[]{false, false, false, false, true, true, false}, 14, 18, "OK");
        gameRepository.addGame(game);

        boolean result = bc.gameExists(game.getName());

        Assertions.assertThat(result).isTrue();
    }

    @AfterEach
    public void tearDown() {
        userRepository = null;
        loggedUserRepository = null;
        gameRepository = null;
        bookedGameRepository = null;
        validator = null;
        bc = null;
    }
}
