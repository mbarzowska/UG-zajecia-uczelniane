import Helpers.BrowserManager;
import PageObjects.PageObjectGithub;
import io.github.bonigarcia.SeleniumExtension;
import org.hamcrest.MatcherAssert;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.extension.ExtendWith;
import org.openqa.selenium.WebDriver;

import static org.hamcrest.CoreMatchers.containsString;
import static org.hamcrest.CoreMatchers.endsWith;

@ExtendWith(SeleniumExtension.class)
public class GithubLoginTest {
	
	private WebDriver driver;
	public PageObjectGithub page;

	@BeforeEach
	public void setUp() {
		driver = BrowserManager.initializeBrowser(driver, "Edge");
		driver.manage().window().maximize();
	}

	@Test
	public void checkTitlePage() {
		page = new PageObjectGithub(driver);

        MatcherAssert.assertThat(page.getCurrentTitle(), containsString("GitHub"));
	}

    @Test
    public void logInWithInvalidData() {
        page = new PageObjectGithub(driver);

        page.loginWithInvalidData("testowadlajavy", "testowa");

        MatcherAssert.assertThat(page.getCurrentUrl(), containsString("github.com/session"));
    }

	@Test
	public void logInWithValidData() {
		page = new PageObjectGithub(driver);

		page.loginWithValidData("testowadlajavy", "testowadlajavy1");

        MatcherAssert.assertThat(page.getCurrentUrl(), endsWith("github.com/"));
		page.logoutFromGithub();
	}

	@AfterEach
	public void tearDown() {
		driver.quit();
	}
}
