package Projekt1;

import junitparams.FileParameters;
import junitparams.JUnitParamsRunner;
import org.junit.After;
import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;

import static org.junit.Assert.assertEquals;

@RunWith(JUnitParamsRunner.class)
public class UserTest_ParametrizedFromFIle {
    User testDefaultUser;
    String testDefaultUsersUsername, testDefaultUsersPassword;

    @Before
    public void SetUp() {
        testDefaultUser = new User("Mika", "qwerty");
    }

    @Test
    @FileParameters("src/test/resources/userAddMoney.csv")
    public void AddMoney_FromFile_ShouldAddMoneyToUsersAccount_testJUnit(String sAmount, double expectedResult) {
        testDefaultUser.AddMoney(sAmount);
        assertEquals(expectedResult, testDefaultUser.getAccountBalance(), 0.0);
    }

    @After
    public void tearDown() {
        testDefaultUser = null;
    }
}
