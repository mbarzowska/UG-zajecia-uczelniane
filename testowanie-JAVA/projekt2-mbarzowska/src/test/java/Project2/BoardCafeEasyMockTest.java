package Project2;

import Project2.Controllers.BoardCafe;
import Project2.Models.Interfaces.Validation;
import Project2.Models.User;
import Project2.Repositories.Interfaces.BookedGameHandling;
import Project2.Repositories.Interfaces.GameInfoHandling;
import Project2.Repositories.Interfaces.LoggedUserInfoHandling;
import Project2.Repositories.Interfaces.UserInfoHandling;

import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;

import static org.junit.jupiter.api.Assertions.*;

import Project2.Extensions.EasyMockExtension;
import org.easymock.EasyMock;
import static org.easymock.EasyMock.expect;
import static org.easymock.EasyMock.replay;

@ExtendWith(EasyMockExtension.class)
public class BoardCafeEasyMockTest {

    private UserInfoHandling userRepository;
    private LoggedUserInfoHandling loggedUserRepository;
    private GameInfoHandling gameRepository;
    private BookedGameHandling bookedGameRepository;
    private Validation validator;

    private BoardCafe bc;

    @BeforeEach
    public void setUp() {
        userRepository = EasyMock.createNiceMock(UserInfoHandling.class);
        loggedUserRepository = EasyMock.createNiceMock(LoggedUserInfoHandling.class);
        gameRepository = EasyMock.createNiceMock(GameInfoHandling.class);
        bookedGameRepository = EasyMock.createNiceMock(BookedGameHandling.class);
        validator = EasyMock.createNiceMock(Validation.class);
        bc = new BoardCafe(gameRepository, bookedGameRepository, userRepository, loggedUserRepository, validator);
    }

    @Test
    public void registerUserIfUserDoesNotAlreadyExistReturnsTrue() {
        User mika = new User("Mika", "psswd");
        expect(userRepository.userExists(mika.getUsername())).andReturn(false);
        expect(validator.validateUser(mika.getUsername(), mika.getPassword())).andReturn(true);
        replay(userRepository);
        replay(validator);

        boolean result = bc.register(mika.getUsername(), mika.getPassword());

        assertTrue(result);
    }

    @Test
    public void registerUserIfUserDoesAlreadyExistThrowsException() {
        User mika = new User("Mika", "psswd");
        expect(userRepository.userExists(mika.getUsername())).andReturn(true);
        expect(validator.validateUser(mika.getUsername(), mika.getPassword())).andReturn(true);
        replay(userRepository);
        replay(validator);

        assertThrows(IllegalArgumentException.class, () -> { bc.register(mika.getUsername(), mika.getPassword()); });
    }

    @Test
    public void registerUserIfUserDoesAlreadyExistThrowsProperException() {
        User mika = new User("Mika", "psswd");
        expect(userRepository.userExists(mika.getUsername())).andReturn(true);
        expect(validator.validateUser(mika.getUsername(), mika.getPassword())).andReturn(true);
        replay(userRepository);
        replay(validator);

        Throwable e = assertThrows(IllegalArgumentException.class, () -> { bc.register(mika.getUsername(), mika.getPassword()); });

        assertEquals("Username " + mika.getUsername() + " is already taken!", e.getMessage());
    }

    @Test
    public void registerUserWithInvalidDataCaseNullThrowsException() {
        User mika = new User(null, null);
        expect(validator.validateUser(mika.getUsername(), mika.getPassword())).andReturn(false);
        replay(validator);

        assertThrows(IllegalArgumentException.class, () -> { bc.register(mika.getUsername(), mika.getPassword()); });
    }

    @Test
    public void registerUserWithInvalidDataCaseNullThrowsProperException() {
        User mika = new User(null, null);
        expect(validator.validateUser(mika.getUsername(), mika.getPassword())).andReturn(false);
        replay(validator);

        Throwable e = assertThrows(IllegalArgumentException.class, () -> { bc.register(mika.getUsername(), mika.getPassword()); });

        assertEquals("Username or password is an empty string!", e.getMessage());
    }

    @Test
    public void registerUserWithInvalidDataCaseEmptyThrowsException() {
        User mika = new User("", "");
        expect(validator.validateUser(mika.getUsername(), mika.getPassword())).andReturn(false);
        replay(validator);

        assertThrows(IllegalArgumentException.class, () -> { bc.register(mika.getUsername(), mika.getPassword()); });
    }

    @Test
    public void registerUserWithInvalidDataCaseEmptyThrowsProperException() {
        User mika = new User("", "");
        expect(validator.validateUser(mika.getUsername(), mika.getPassword())).andReturn(false);
        replay(validator);

        Throwable e = assertThrows(IllegalArgumentException.class, () -> { bc.register(mika.getUsername(), mika.getPassword()); });

        assertEquals("Username or password is an empty string!", e.getMessage());
    }

    @Test
    public void addMoneyToUsersAccountWhenUserIsNotLoggedInReturnsFalse() {
        User mika = new User("Mika", "psswd");
        expect(userRepository.userExists(mika.getUsername())).andReturn(true);
        expect(loggedUserRepository.getUser(mika.getUsername())).andReturn(null);
        replay(userRepository);
        replay(loggedUserRepository);

        boolean result = bc.addMoneyToUsersAccount(mika.getUsername(), "1000");

        assertFalse(result);
    }

    @Test
    public void addMoneyToUsersAccountWhenUserIsLoggedInReturnsTrue() {
        User mika = new User("Mika", "psswd");
        expect(userRepository.userExists(mika.getUsername())).andReturn(true);
        expect(loggedUserRepository.getUser(mika.getUsername())).andReturn(mika);
        replay(userRepository);
        replay(loggedUserRepository);

        boolean result = bc.addMoneyToUsersAccount(mika.getUsername(), "1000");

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
