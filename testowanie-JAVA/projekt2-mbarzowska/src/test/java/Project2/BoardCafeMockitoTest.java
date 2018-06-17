package Project2;

import Project2.Controllers.BoardCafe;
import Project2.Models.Interfaces.Validation;
import Project2.Models.User;
import Project2.Repositories.Interfaces.BookedGameHandling;
import Project2.Repositories.Interfaces.GameInfoHandling;
import Project2.Repositories.Interfaces.LoggedUserInfoHandling;
import Project2.Repositories.Interfaces.UserInfoHandling;

import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.extension.ExtendWith;

import static org.junit.jupiter.api.Assertions.*;

import Project2.Extensions.MockitoExtension;
import org.mockito.Mockito;
import static org.mockito.Mockito.doReturn;

@SuppressWarnings("deprecation")
@ExtendWith(MockitoExtension.class)
public class BoardCafeMockitoTest {

    UserInfoHandling userRepository;
    LoggedUserInfoHandling loggedUserRepository;
    GameInfoHandling gameRepository;
    BookedGameHandling bookedGameRepository;
    Validation validator;

    BoardCafe bc;

    @BeforeEach
    public void setUp() {
        userRepository = Mockito.mock(UserInfoHandling.class);
        loggedUserRepository = Mockito.mock(LoggedUserInfoHandling.class);
        gameRepository = Mockito.mock(GameInfoHandling.class);
        bookedGameRepository = Mockito.mock(BookedGameHandling.class);
        validator = Mockito.mock(Validation.class);
        bc = new BoardCafe(gameRepository, bookedGameRepository, userRepository, loggedUserRepository, validator);
    }

    @Test
    public void logInUserWithInvalidDataCaseNullThrowsException() {
        User mika = new User(null, null);
        doReturn(false).when(validator).validateUser(mika.getUsername(), mika.getPassword());

        assertThrows(IllegalArgumentException.class, () -> { bc.logIn(mika.getUsername(), mika.getPassword()); });
    }

    @Test
    public void logInUserWithInvalidDataCaseNullThrowsProperException() {
        User mika = new User(null, null);
        doReturn(false).when(validator).validateUser(mika.getUsername(), mika.getPassword());

        Throwable e = assertThrows(IllegalArgumentException.class, () -> { bc.logIn(mika.getUsername(), mika.getPassword()); });

        assertEquals("Username or password is an empty string!", e.getMessage());
    }

    @Test
    public void logInUserWithInvalidDataCaseEmptyThrowsException() {
        User mika = new User("", "");
        doReturn(false).when(validator).validateUser(mika.getUsername(), mika.getPassword());

        assertThrows(IllegalArgumentException.class, () -> { bc.logIn(mika.getUsername(), mika.getPassword()); });
    }

    @Test
    public void logInUserWithInvalidDataCaseEmptyThrowsProperException() {
        User mika = new User("", "");
        doReturn(false).when(validator).validateUser(mika.getUsername(), mika.getPassword());

        Throwable e = assertThrows(IllegalArgumentException.class, () -> { bc.logIn(mika.getUsername(), mika.getPassword()); });

        assertEquals("Username or password is an empty string!", e.getMessage());
    }

    @Test
    public void logInUserWhenUserDoesExistReturnsTrue() {
        User mika = new User("Mika", "psswd");
        doReturn(true).when(validator).validateUser(mika.getUsername(), mika.getPassword());
        doReturn(true).when(userRepository).userExists(mika.getUsername());
        doReturn(mika).when(userRepository).getUser(mika.getUsername());

        boolean result = bc.logIn(mika.getUsername(), mika.getPassword());

        assertTrue(result);
    }

    @Test
    public void logInUserWhenUserDoesNotExistThrowsException() {
        User mika = new User("Mika", "psswd");
        doReturn(true).when(validator).validateUser(mika.getUsername(), mika.getPassword());
        doReturn(false).when(userRepository).userExists(mika.getUsername());

        assertThrows(UnsupportedOperationException.class, () -> { bc.logIn(mika.getUsername(), mika.getPassword()); });
    }

    @Test
    public void logInUserWhenUserDoesNotExistThrowsProperException() {
        User mika = new User("Mika", "psswd");
        doReturn(true).when(validator).validateUser(mika.getUsername(), mika.getPassword());
        doReturn(false).when(userRepository).userExists(mika.getUsername());

        Throwable e = assertThrows(UnsupportedOperationException.class, () -> { bc.logIn(mika.getUsername(), mika.getPassword()); });

        assertEquals("User: " + mika.getUsername() + " doesn't exist", e.getMessage());
    }

    @Test
    public void logOutUserWhenUserDoesNotExistThrowsException() {
        User mika = new User("Mika", "psswd");
        doReturn(false).when(userRepository).userExists(mika.getUsername());

        assertThrows(IllegalArgumentException.class, () -> { bc.logOut(mika); });
    }

    @Test
    public void logOutUserWhenUserDoesNotExistThrowsProperException() {
        User mika = new User("Mika", "psswd");
        doReturn(false).when(userRepository).userExists(mika.getUsername());

        Throwable e = assertThrows(IllegalArgumentException.class, () -> { bc.logOut(mika); });

        assertEquals("User does not exist!", e.getMessage());
    }

    @Test
    public void logOutUserWhenUserDoesExistIsNotLoggedInThrowsException() {
        User mika = new User("Mika", "psswd");
        doReturn(true).when(userRepository).userExists(mika.getUsername());
        doReturn(null).when(loggedUserRepository).getUser(mika.getUsername());

        assertThrows(UnsupportedOperationException.class, () -> { bc.logOut(mika); });
    }

    @Test
    public void logOutUserWhenUserDoesExistIsNotLoggedInThrowsProperException() {
        User mika = new User("Mika", "psswd");
        doReturn(true).when(userRepository).userExists(mika.getUsername());
        doReturn(null).when(loggedUserRepository).getUser(mika.getUsername());

        Throwable e = assertThrows(UnsupportedOperationException.class, () -> { bc.logOut(mika); });

        assertEquals("User is currently not logged in, can't perform logout!", e.getMessage());
    }

    @Test
    public void logOutUserWhenUserDoesExistIsLoggedInReturnsTrue() {
        User mika = new User("Mika", "psswd");
        doReturn(true).when(userRepository).userExists(mika.getUsername());
        doReturn(mika).when(loggedUserRepository).getUser(mika.getUsername());

        boolean result = bc.logOut(mika);

        assertTrue(result);
    }

    @AfterEach
    public void tearDown() {
        bc = null;
        bookedGameRepository = null;
        gameRepository = null;
        userRepository = null;
        loggedUserRepository = null;
        validator = null;
    }
}
