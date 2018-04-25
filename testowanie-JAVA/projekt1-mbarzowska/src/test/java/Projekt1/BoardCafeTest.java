package Projekt1;

import java.io.*;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.text.ParseException;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;

import static org.junit.Assert.*;
import org.junit.Before;
import org.junit.Test;
import org.junit.After;

import static org.hamcrest.Matchers.*;
import static org.hamcrest.CoreMatchers.is;
import static org.hamcrest.MatcherAssert.assertThat;

import org.assertj.core.api.Assertions;

import static org.mockito.Mockito.spy;
import static org.mockito.Mockito.verify;

public class BoardCafeTest {
    BoardCafe bc;
    User testDefaultUser;
    String testDefaultUsersUsername, testDefaultUsersPassword;
    User testDefaultSecondUser;
    String testDefaultSecondUsersUsername, testDefaultSecondUsersPassword;
    Game testDefaultGame;
    String testDefaultGamesName; double testDefaultGamesPrice; int testDefaultGamesAHS; int testDefaultGamesAHE;
    boolean[] testDefaultGamesDays; int testDefaultGamesUHS; int testDefaultGamesUHE; String testDefaultGamesDescription;
    String testDefaultGamesBookDateUnavailDay, testDefaultGamesBookDateAvailDay,
            testDefaultGamesBookAvailTime, testDefaultGamesBookUnavailTime, testDefaultGamesBookTimeClosed;
    String pathToMixedDatabase, pathToCorrectDatabase, pathToIncorrectDatabase, incorrectDatabasePath;

    @Before
    public void setUp(){
        bc = new BoardCafe();
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
    public void LoadDatabase_WhenCorrectData_ShouldAddGamesToList_testHamcrest() throws FileNotFoundException {
        bc.LoadDatabase(pathToCorrectDatabase);
        assertThat(bc.GamesList, hasSize(1));
    }

    @Test
    public void LoadDatabase_WhenIncorrectData_ShouldntAddGamesToList_testHamcrest() throws FileNotFoundException {
        bc.LoadDatabase(pathToIncorrectDatabase);
        assertThat(bc.GamesList, hasSize(0));
    }

    @Test
    public void LoadDatabase_WhenMixedData_ShouldAddOnlyValidGamesToList_testHamcrest() throws FileNotFoundException {
        bc.LoadDatabase(pathToMixedDatabase);
        assertThat(bc.GamesList, hasSize(3));
    }

    @Test
    public void LoadDatabase_WhenNoSuchDbExists_ShouldThrowException_testAssertJ() throws FileNotFoundException {
        Assertions.assertThatExceptionOfType(FileNotFoundException.class).isThrownBy(() -> { bc.LoadDatabase(incorrectDatabasePath); });
    }



    @Test
    public void FindUser_IfUserExistsOnList_ShouldReturnUser_testHamcrest() {
        bc.UsersList.add(testDefaultUser);
        User result = bc.FindUser(testDefaultUsersUsername);
        assertThat(result, sameInstance(testDefaultUser));
    }

    @Test
    public void FindUser_IfUserDoesntExistOnList_ShouldReturnNull_testJUnit() {
        User result = bc.FindUser(testDefaultUsersUsername);
        assertNull(result);
    }

    @Test
    public void FindUser_IfUserDoesntExistOnList_ShouldReturnNull_testAssertJ() {
        User result = bc.FindUser(testDefaultUsersUsername);
        Assertions.assertThat(result).isNull();
    }

    @Test (expected = IllegalArgumentException.class)
    public void FindUser_IfGivenUsernameIsNull_ShouldThrowException_testJUnit() {
        bc.FindUser(null);
    }

    @Test (expected = IllegalArgumentException.class)
    public void FindUser_IfGivenUsernameIsEmpty_ShouldThrowException_testJUnit() {
        bc.FindUser("");
    }



    @Test
    public void FindLoggedUser_IfUserExistsOnList_ShouldReturnUser_testHamcrest() {
        bc.LoggedUsersList.add(testDefaultUser);
        User result = bc.FindLoggedUser(testDefaultUsersUsername);
        assertThat(result, sameInstance(testDefaultUser));
    }

    @Test
    public void FindLoggedUser_IfUserDoesntExistOnList_ShouldReturnNull_testJUnit() {
        User result = bc.FindLoggedUser(testDefaultUsersUsername);
        assertNull(result);
    }

    @Test
    public void FindLoggedUser_IfUserDoesntExistOnList_ShouldReturnNull_testAssertJ() {
        User result = bc.FindLoggedUser(testDefaultUsersUsername);
        Assertions.assertThat(result).isNull();
    }

    @Test (expected = IllegalArgumentException.class)
    public void FindLoggedUser_IfGivenUsernameIsNull_ShouldThrowException_testJUnit() {
        bc.FindUser(null);
    }

    @Test (expected = IllegalArgumentException.class)
    public void FindLoggedUser_IfGivenUsernameIsEmpty_ShouldThrowException_testJUnit() {
        bc.FindUser("");
    }



    @Test
    public void UserExists_IfUserExistsOnList_ShouldReturnTrue_testJUnit() {
        bc.UsersList.add(testDefaultUser);
        boolean result = bc.UserExists(testDefaultUsersUsername);
        assertTrue(result);
    }

    @Test
    public void UserExists_IfUserDoesntExistOnList_ShouldReturnFalse_testJUnit() {
        boolean result = bc.UserExists(testDefaultUsersUsername);
        assertFalse(result);
    }

    @Test
    public void UserExists_IfGivenUsernameIsNull_ShouldReturnFalse_testJUnit() {
        boolean result = bc.UserExists(null);
        assertFalse(result);
    }

    @Test
    public void UserExists_IfGivenUsernameIsEmpty_ShouldReturnFalse_testJUnit() {
        boolean result = bc.UserExists("");
        assertFalse(result);
    }



    @Test
    public void UserIsLoggedIn_IfUserIsLoggedIn_ShouldReturnTrue_testAssertJ() {
        bc.LoggedUsersList.add(testDefaultUser);
        boolean result = bc.UserIsLoggedIn(testDefaultUser);
        Assertions.assertThat(result).isTrue();
    }

    @Test
    public void UserIsLoggedIn_IfUserIsNotLoggedIn_ShouldReturnFalse_testAssertJ() {
        boolean result = bc.UserIsLoggedIn(testDefaultUser);
        Assertions.assertThat(result).isFalse();
    }

    @Test
    public void UserIsLoggedIn_IfGivenUsernameIsNull_ShouldReturnFalse_testAssertJ() {
        boolean result = bc.UserIsLoggedIn(null);
        Assertions.assertThat(result).isFalse();
    }

    @Test
    public void UserIsLoggedIn_IfGivenUsernameIsEmpty_ShouldReturnFalse_testAssertJ() {
        boolean result = bc.UserExists("");
        Assertions.assertThat(result).isFalse();
    }



    @Test
    public void FindGame_IfGameExistsOnList_ShouldReturnGame_testHamcrest() {
        bc.GamesList.add(testDefaultGame);
        Game result = bc.FindGame(testDefaultGamesName);
        assertThat(result, sameInstance(testDefaultGame));
    }

    @Test
    public void FindGame_IfGameDoesntExistOnList_ShouldReturnNull_testJUnit() {
        Game result = bc.FindGame(testDefaultGamesName);
        assertNull(result);
    }

    @Test
    public void FindGame_IfGameDoesntExistOnList_ShouldReturnNull_testAssertJ() {
        Game result = bc.FindGame(testDefaultGamesName);
        Assertions.assertThat(result).isNull();
    }

    @Test (expected = IllegalArgumentException.class)
    public void FindGame_IfGivenGameNameIsNull_ShouldThrowException_testJUnit() {
        bc.FindGame(null);
    }

    @Test (expected = IllegalArgumentException.class)
    public void FindGame_IfGivenGameNameIsEmpty_ShouldThrowException_testJUnit() {
        bc.FindGame("");
    }



    @Test
    public void GameExists_IfGameExistsOnList_ShouldReturnTrue_testAssertJ() {
        bc.UsersList.add(testDefaultUser);
        boolean result = bc.UserExists(testDefaultUsersUsername);
        Assertions.assertThat(result).isTrue();
    }

    @Test
    public void GameExists_IfGameDoesntExistOnList_ShouldReturnFalse_testAssertJ() {
        boolean result = bc.UserExists(testDefaultUsersUsername);
        Assertions.assertThat(result).isFalse();
    }

    @Test
    public void GameExists_IfGivenGameNameIsNull_ShouldReturnFalse_testAssertJ() {
        boolean result = bc.UserExists(null);
        Assertions.assertThat(result).isFalse();
    }

    @Test
    public void GameExists_IfGivenGameNameIsEmpty_ShouldReturnFalse_testAssertJ() {
        boolean result = bc.UserExists("");
        Assertions.assertThat(result).isFalse();
    }



    @Test
    public void Register_WithValidDataWhenUserDoesntAlreadyExist_ShouldAddUserToUsersList_testAssertJ() {
        bc.Register(testDefaultUsersUsername, testDefaultUsersPassword);
        Assertions.assertThat(bc.UsersList).extracting("Username").contains(testDefaultUsersUsername);
    }

    @Test
    public void Register_WithValidDataWhenUserAlreadyExists_ShouldThrowException_testAssertJ() {
        bc.Register(testDefaultUsersUsername, testDefaultUsersPassword);
        Assertions.assertThatIllegalArgumentException().isThrownBy(() -> { bc.Register(testDefaultUsersUsername, testDefaultUsersPassword); })
                .withMessage("%s", "Username " + testDefaultUsersUsername + " is already taken!")
                .withMessageContaining(testDefaultUsersUsername);
    }

    @Test
    public void Register_WhenGivenNameIsNull_ShouldThrowException_testAssertJ() {
        Assertions.assertThatIllegalArgumentException().isThrownBy(() -> { bc.Register(null, testDefaultUsersPassword); })
                .withMessage("%s", "Username or password is an empty string!");
    }

    @Test
    public void Register_WhenGivenNameIsEmpty_ShouldThrowException_testAssertJ() {
        Assertions.assertThatIllegalArgumentException().isThrownBy(() -> { bc.Register("", testDefaultUsersPassword); })
                .withMessage("%s", "Username or password is an empty string!");
    }

    @Test
    public void Register_WhenGivenPasswordIsNull_ShouldThrowException_testAssertJ() {
        Assertions.assertThatIllegalArgumentException().isThrownBy(() -> { bc.Register(testDefaultUsersUsername, null); })
                .withMessage("%s", "Username or password is an empty string!");
    }

    @Test
    public void Register_WhenGivenPasswordIsEmpty_ShouldThrowException_testAssertJ() {
        Assertions.assertThatIllegalArgumentException().isThrownBy(() -> { bc.Register(testDefaultUsersUsername, ""); })
                .withMessage("%s", "Username or password is an empty string!");
    }



    @Test
    public void LogIn_WithValidDataWhenUserIsRegistered_ShouldAddUserToLoggedUsersList_testAssertJ() {
        bc.Register(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.LogIn(testDefaultUsersUsername, testDefaultUsersPassword);
        Assertions.assertThat(bc.LoggedUsersList).extracting("Username").contains(testDefaultUsersUsername);
    }

    @Test
    public void LogIn_WithValidDataWhenUserIsntRegistered_ShouldThrowException_testAssertJ() {
        Assertions.assertThatExceptionOfType(UnsupportedOperationException.class).isThrownBy(() -> { bc.LogIn(testDefaultUsersUsername, testDefaultUsersPassword); })
                .withMessage("%s", "User: " + testDefaultUsersUsername + " doesn't exist");
    }

    @Test
    public void LogIn_WithInvalidPassword_ShouldThrowException_testAssertJ() {
        bc.Register(testDefaultUsersUsername, testDefaultUsersPassword);
        Assertions.assertThatIllegalArgumentException().isThrownBy(() -> { bc.LogIn(testDefaultUsersUsername, "randomPassword"); })
                .withMessage("%s", "Incorrect password!");
    }

    @Test
    public void LogIn_WhenGivenNameIsNull_ShouldThrowException_testAssertJ() {
        Assertions.assertThatIllegalArgumentException().isThrownBy(() -> { bc.LogIn(null, testDefaultUsersPassword); })
                .withMessage("%s", "Username or password is an empty string!");
    }

    @Test
    public void LogIn_WhenGivenNameIsEmpty_ShouldThrowException_testAssertJ() {
        Assertions.assertThatIllegalArgumentException().isThrownBy(() -> { bc.LogIn("", testDefaultUsersPassword); })
                .withMessage("%s", "Username or password is an empty string!");
    }

    @Test
    public void LogIn_WhenGivenPasswordIsNull_ShouldThrowException_testAssertJ() {
        Assertions.assertThatIllegalArgumentException().isThrownBy(() -> { bc.LogIn(testDefaultUsersUsername, null); })
                .withMessage("%s", "Username or password is an empty string!");
    }

    @Test
    public void LogIn_WhenGivenPasswordIsEmpty_ShouldThrowException_testAssertJ() {
        Assertions.assertThatIllegalArgumentException().isThrownBy(() -> { bc.LogIn(testDefaultUsersUsername, ""); })
                .withMessage("%s", "Username or password is an empty string!");
    }



    @Test
    public void LogOut_WithValidDataWhenUserIsLoggedIn_ShouldRemoveUserFromLoggedUsersList_testAssertJ() {
        bc.Register(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.LogIn(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.LogOut(testDefaultUser);
        Assertions.assertThat(bc.LoggedUsersList).isEmpty();
    }

    @Test
    public void LogOut_WithValidDataWhenUserIsLoggedIn_ShouldRemoveUserFromLoggedUsersList_testHamcrest() {
        bc.Register(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.LogIn(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.LogOut(testDefaultUser);
        assertThat(bc.LoggedUsersList, is(emptyCollectionOf(User.class)));
    }

    @Test
    public void LogOut_WithValidDataWhenUserIsntLoggedIn_ShouldThrowException_testAssertJ() {
        bc.Register(testDefaultUsersUsername, testDefaultUsersPassword);
        Assertions.assertThatExceptionOfType(UnsupportedOperationException.class).isThrownBy(() -> { bc.LogOut(testDefaultUser); })
                .withMessage("%s", "User is currently not logged in, can't perform logout!");
    }

    @Test
    public void LogOut_WithValidDataWhenUserIsntRegistered_ShouldThrowException_testAssertJ() {
        Assertions.assertThatIllegalArgumentException().isThrownBy(() -> { bc.LogOut(testDefaultUser); })
                .withMessage("%s", "User does not exist!");
    }



    @Test
    public void UserBookGame_WhenAllConditionsAreMet_ShouldCreateConfirmationFile_TestJUnitAndPlainJava() throws IOException, ParseException {
        bc.Register(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.LogIn(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.FindUser(testDefaultUsersUsername).AddMoney("1000");
        bc.GamesList.add(testDefaultGame);

        Path file = Paths.get("src/main/resources/confirmations/");
        File parentDir =  file.toFile();
        int filesBefore = parentDir.list().length;

        bc.UserBookGame(testDefaultUser, testDefaultGamesName, testDefaultGamesBookDateAvailDay, testDefaultGamesBookAvailTime);
        assertEquals(filesBefore + 1, parentDir.list().length);
    }

    @Test
    public void UserBookGame_WhenAllConditionsAreMet_ShouldAddGameToUsersAccount_testHamcrest() throws IOException, ParseException {
        bc.Register(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.LogIn(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.FindUser(testDefaultUsersUsername).AddMoney("1000");
        bc.GamesList.add(testDefaultGame);

        bc.UserBookGame(testDefaultUser, testDefaultGamesName, testDefaultGamesBookDateAvailDay, testDefaultGamesBookAvailTime);
        assertThat(bc.GamesList, hasItem(testDefaultGame));
    }

    @Test
    public void UserBookGame_WhenGameAlreadyBookedByUser_ShouldThrowException_testAssertJ() throws IOException, ParseException {
        bc.Register(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.LogIn(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.FindUser(testDefaultUsersUsername).AddMoney("1000");
        bc.GamesList.add(testDefaultGame);

        bc.UserBookGame(testDefaultUser, testDefaultGamesName, testDefaultGamesBookDateAvailDay, "11");
        Assertions.assertThatExceptionOfType(UnsupportedOperationException.class).isThrownBy(() -> { bc.UserBookGame(testDefaultUser, testDefaultGamesName, testDefaultGamesBookDateAvailDay, "12"); })
                .withMessage("%s", "User " + testDefaultUsersUsername + "already booked the game " + testDefaultGamesName);
    }

    @Test
    public void UserBookGame_WhenGameAlreadyBookedBySomeone_ShouldThrowException_testAssertJ() throws IOException, ParseException {
        bc.Register(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.LogIn(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.FindUser(testDefaultUsersUsername).AddMoney("1000");
        bc.Register(testDefaultSecondUsersUsername, testDefaultSecondUsersPassword);
        bc.LogIn(testDefaultSecondUsersUsername, testDefaultSecondUsersPassword);
        bc.FindUser(testDefaultSecondUsersUsername).AddMoney("1000");
        bc.GamesList.add(testDefaultGame);

        bc.UserBookGame(testDefaultSecondUser, testDefaultGamesName, testDefaultGamesBookDateAvailDay, testDefaultGamesBookAvailTime);
        Assertions.assertThatExceptionOfType(UnsupportedOperationException.class).isThrownBy(() -> { bc.UserBookGame(testDefaultUser, testDefaultGamesName, testDefaultGamesBookDateAvailDay, testDefaultGamesBookAvailTime); })
                .withMessage("%s", "User " + testDefaultSecondUsersUsername + " already booked the game " + testDefaultGamesName + " on " + LocalDate.parse(testDefaultGamesBookDateAvailDay, DateTimeFormatter.ofPattern("dd/MM/yyyy")).toString() + ", at " + "11");
    }

    @Test
    public void UserBookGame_WhenUserDontHaveMoney_ShouldThrowException_testAssertJ() throws IOException, ParseException {
        bc.Register(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.LogIn(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.GamesList.add(testDefaultGame);
        Assertions.assertThatExceptionOfType(UnsupportedOperationException.class).isThrownBy(() -> { bc.UserBookGame(testDefaultUser, testDefaultGamesName, testDefaultGamesBookDateAvailDay, "11"); })
                .withMessage("%s", "User " + testDefaultUsersUsername + " doesn't have enough money to book " + testDefaultGamesName);
    }

    @Test
    public void UserBookGame_WhenTimeUnavailable_ShouldThrowException_testAssertJ() throws IOException, ParseException {
        bc.Register(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.LogIn(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.FindUser(testDefaultUsersUsername).AddMoney("1000");
        bc.GamesList.add(testDefaultGame);
        Assertions.assertThatExceptionOfType(UnsupportedOperationException.class).isThrownBy(() -> { bc.UserBookGame(testDefaultUser, testDefaultGamesName, testDefaultGamesBookDateAvailDay, testDefaultGamesBookUnavailTime); })
                .withMessage("%s", "You can't book a game for a time period when it's not available to book, sorry!");
    }

    @Test
    public void UserBookGame_WhenTimeClosed_ShouldThrowException_testAssertJ() throws IOException, ParseException {
        bc.Register(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.LogIn(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.FindUser(testDefaultUsersUsername).AddMoney("1000");
        bc.GamesList.add(testDefaultGame);
        Assertions.assertThatExceptionOfType(UnsupportedOperationException.class).isThrownBy(() -> { bc.UserBookGame(testDefaultUser, testDefaultGamesName, testDefaultGamesBookDateAvailDay, testDefaultGamesBookTimeClosed); })
                .withMessage("%s", "You can't book a game for a time period when we're closed, sorry!");
    }

    @Test
    public void UserBookGame_WhenDateUnavailable_ShouldThrowException_testAssertJ() throws IOException, ParseException {
        bc.Register(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.LogIn(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.FindUser(testDefaultUsersUsername).AddMoney("1000");
        bc.GamesList.add(testDefaultGame);
        Assertions.assertThatExceptionOfType(UnsupportedOperationException.class).isThrownBy(() -> { bc.UserBookGame(testDefaultUser, testDefaultGamesName, testDefaultGamesBookDateUnavailDay, testDefaultGamesBookAvailTime); })
                .withMessage("%s", "You can't book a game for a date it's not available to book, sorry!");
    }

    @Test
    public void UserBookGame_WhenShopDoesntOwnTheGame_ShouldThrowException_testAssertJ() throws IOException, ParseException {
        bc.Register(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.LogIn(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.FindUser(testDefaultUsersUsername).AddMoney("1000");
        Assertions.assertThatExceptionOfType(UnsupportedOperationException.class).isThrownBy(() -> { bc.UserBookGame(testDefaultUser, testDefaultGamesName, testDefaultGamesBookDateAvailDay, testDefaultGamesBookAvailTime); })
                .withMessage("%s", "This game does not exist in our database!");
    }

    @Test
    public void UserBookGame_WhenGivenNullGameName_ShouldThrowException_testAssertJ() throws IOException, ParseException {
        bc.Register(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.LogIn(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.FindUser(testDefaultUsersUsername).AddMoney("1000");
        Assertions.assertThatIllegalArgumentException().isThrownBy(() -> { bc.UserBookGame(testDefaultUser, null, testDefaultGamesBookDateAvailDay, testDefaultGamesBookAvailTime); })
                .withMessage("%s", "Wrong input!\nGame name is an empty string!");
    }

    @Test
    public void UserBookGame_WhenGivenEmptyGameName_ShouldThrowException_testAssertJ() throws IOException, ParseException {
        bc.Register(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.LogIn(testDefaultUsersUsername, testDefaultUsersPassword);
        bc.FindUser(testDefaultUsersUsername).AddMoney("1000");
        Assertions.assertThatIllegalArgumentException().isThrownBy(() -> { bc.UserBookGame(testDefaultUser, null, testDefaultGamesBookDateAvailDay, testDefaultGamesBookAvailTime); })
                .withMessage("%s", "Wrong input!\nGame name is an empty string!");
    }



    @Test
    public void ShowGamesList_WhenSomeGamesExists_ShouldReturnGamesList_testMockito() {
        bc.GamesList.add(testDefaultGame);
        BoardCafe service = spy(bc);
        service.ShowGamesList();
        verify(service).ShowGamesList();
    }



    @After
    public void tearDown() {
        bc = null;
        testDefaultUser = null;
        testDefaultUsersUsername = null;
        testDefaultUsersPassword = null;
        testDefaultSecondUser = null;
        testDefaultSecondUsersUsername = null;
        testDefaultSecondUsersPassword = null;
        testDefaultGame = null;
        testDefaultGamesName = null;
        testDefaultGamesDays = null;
        testDefaultGamesDescription = null;
        testDefaultGamesBookDateUnavailDay = null;
        testDefaultGamesBookDateAvailDay = null;
        testDefaultGamesBookAvailTime = null;
        testDefaultGamesBookUnavailTime = null;
        testDefaultGamesBookTimeClosed = null;
        pathToMixedDatabase = null;
        pathToCorrectDatabase = null;
        pathToIncorrectDatabase = null;
        incorrectDatabasePath = null;
    }
}
