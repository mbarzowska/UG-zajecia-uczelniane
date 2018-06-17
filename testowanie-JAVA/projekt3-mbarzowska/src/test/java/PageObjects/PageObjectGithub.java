package PageObjects;

import io.github.bonigarcia.SeleniumExtension;
import org.junit.jupiter.api.extension.ExtendWith;
import org.openqa.selenium.By;
import org.openqa.selenium.Keys;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;

@ExtendWith(SeleniumExtension.class)
public class PageObjectGithub {

	public WebDriver driver;
	private WebDriverWait wait;

	public PageObjectGithub(WebDriver driver) {
		this.driver = driver;
		driver.get("https://github.com");
		this.wait = new WebDriverWait(driver,10);
	}

	public String getCurrentUrl() {
		return driver.getCurrentUrl();
	}

	public String getCurrentTitle() {
		return driver.getTitle();
	}

	public void loginWithInvalidData(String username, String password) {
		login(username, password);
		wait.until(ExpectedConditions.urlContains("session"));
	}

    public void loginWithValidData(String username, String password) {
       	login(username, password);
       	wait.until(ExpectedConditions.urlToBe("https://github.com/"));
    }

	public void logoutFromGithub() {
//		Edge saves cache when testing. Because tests sometimes run in random order i have to logout after them
        driver.get("https://github.com/logout");
        wait.until(ExpectedConditions.urlContains("https://github.com/logout"));
		WebElement element = driver.findElement(By.xpath("//input[@class='btn btn-block f4 py-3 mt-5']"));
		wait.until(ExpectedConditions.elementToBeClickable(element));
		element.sendKeys(Keys.ENTER);
    }

	private void login(String username, String password) {
		driver.get("https://github.com/login");
		wait.until(ExpectedConditions.urlToBe("https://github.com/login"));
		WebElement element = driver.findElement(By.name("login"));
		element.sendKeys(username);
		wait.until(ExpectedConditions.textToBePresentInElementValue(element, username));
		element = driver.findElement(By.name("password"));
		element.sendKeys(password);
		wait.until(ExpectedConditions.textToBePresentInElementValue(element, password));
		element = driver.findElement(By.name("commit"));
		wait.until(ExpectedConditions.elementToBeClickable(element));
		element.sendKeys(Keys.ENTER);
	}
}
