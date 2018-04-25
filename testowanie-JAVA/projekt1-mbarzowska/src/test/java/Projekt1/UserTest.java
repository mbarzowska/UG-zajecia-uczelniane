package Projekt1;

import static org.junit.Assert.*;
import org.junit.Before;
import org.junit.Test;
import org.junit.After;

import static org.mockito.Mockito.spy;
import static org.mockito.Mockito.verify;

public class UserTest {
    User testDefaultUser;
    String testDefaultUsersUsername, testDefaultUsersPassword;

    @Before
    public void setUp() {
        testDefaultUser = new User("Mika", "qwerty");
        testDefaultUsersUsername = testDefaultUser.getUsername();
        testDefaultUsersPassword = testDefaultUser.getPassword();
    }

    @Test
    public void AddMoney_WhenValidAmountFormat_ShouldAddMoneyToUsersAccount_testJUnit() {
        testDefaultUser.AddMoney("1000");
        assertEquals(1000, testDefaultUser.getAccountBalance(), 0.0);
    }

    @Test
    public void AddMoney_WhenInvalidAmountFormat_ShouldNotAddMoneyToUsersAccount_testJUnit() {
        testDefaultUser.AddMoney("100ddsd");
        assertEquals(0.0, testDefaultUser.getAccountBalance(), 0.0);
    }

    @Test
    public void AddMoney_WhenInvalidAmountFormat_ShouldPrintWarning_testMockito() {
        User testUser = spy(testDefaultUser);
        testUser.AddMoney("100ddsd");
        verify(testUser).PrintFormatWarning();
    }

    @After
    public void tearDown() {
        testDefaultUser = null;
        testDefaultUsersUsername = null;
        testDefaultUsersPassword = null;
    }
}
