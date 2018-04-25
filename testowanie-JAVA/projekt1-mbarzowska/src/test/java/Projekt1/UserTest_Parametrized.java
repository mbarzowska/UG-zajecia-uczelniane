package Projekt1;

import java.util.Arrays;
import java.util.Collection;

import org.junit.After;
import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.junit.runners.Parameterized;
import static org.junit.Assert.assertEquals;

@RunWith(Parameterized.class)
public class UserTest_Parametrized {
    User testDefaultUser;
    String testDefaultUsersUsername, testDefaultUsersPassword;

    private String inputAmount;
    private int expectedResult;

    @Before
    public void SetUp() {
        testDefaultUser = new User("Mika", "qwerty");
    }

    public UserTest_Parametrized(String inputAmount, int expectedResult) {
        this.inputAmount = inputAmount;
        this.expectedResult = expectedResult;
    }

    @Parameterized.Parameters
    public static Collection AddMoney() {
        return Arrays.asList(new Object[][] {
                { "2", 2 },
                { "66", 66 },
                { "19542", 19542 },
                { "780", 780 },
        });
    }

    @Test
    public void AddMoney_WhenValidAmountFormat_ShouldAddMoneyToUsersAccount_testJUnit() {
        testDefaultUser.AddMoney(inputAmount);
        assertEquals(expectedResult, testDefaultUser.getAccountBalance(), 0.0);
    }

    @After
    public void tearDown() {
        testDefaultUser = null;
    }
}
