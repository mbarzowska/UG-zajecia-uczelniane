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
public class PageObjectHeroku {

    public WebDriver driver;
    private WebDriverWait wait;

    public PageObjectHeroku(WebDriver driver) {
        this.driver = driver;
        driver.get("http://boardgames.herokuapp.com/");
        this.wait = new WebDriverWait(driver,10);
    }

    public int getTableRowsCount() {
        return driver.findElements(By.xpath("//tbody/tr")).size();
    }

    public boolean isOnDetailsPage() {
        wait.until(ExpectedConditions.visibilityOfElementLocated(By.xpath("/html/body/div[@class='container']/h2")));
        WebElement element = driver.findElement(By.xpath("/html/body/div[@class='container']/h2"));
        wait.until(ExpectedConditions.visibilityOf(element));
        return element.getText().contains("Game: ");
    }

    public int getErrorCount() {
        wait.until(ExpectedConditions.visibilityOfElementLocated(By.className("error_explanation")));
        String errorCount = driver.findElement(By.className("error_explanation")).getText();
        String[] s = errorCount.split(" ");
        return Integer.parseInt(s[0]);
    }

    public void addGame(String name, String publisher, String minPlayers, String maxPlayers, String price) {
        addGameForm(name, publisher, minPlayers, maxPlayers, price);
        driver.get("http://boardgames.herokuapp.com/");
        wait.until(ExpectedConditions.urlToBe("http://boardgames.herokuapp.com/"));
    }

    public void addGameForm(String name, String publisher, String minPlayers, String maxPlayers, String price) {
        driver.get("http://boardgames.herokuapp.com/games/new");
        wait.until(ExpectedConditions.urlToBe("http://boardgames.herokuapp.com/games/new"));
        fillForm(name, publisher, minPlayers, maxPlayers, price);
        WebElement element = driver.findElement(By.name("commit"));
        wait.until(ExpectedConditions.elementToBeClickable(element));
        element.sendKeys(Keys.ENTER);
    }

    public void editGame(String name, String newName, String publisher,
                         String minPlayers, String maxPlayers, String price) throws InterruptedException {
        WebElement element = driver.findElement(By.xpath("//tr[td[1]//text()[contains(., '" + name + "')]]" +
                "//a[@class='btn btn-warning minebtn btn-sm']"));
        element.sendKeys(Keys.ENTER);
        //wait.until(ExpectedConditions.urlContains("edit")); //Nie czeka :(
        Thread.sleep(5000);
        fillForm(newName, publisher, minPlayers, maxPlayers, price);
        element = driver.findElement(By.name("commit"));
        wait.until(ExpectedConditions.elementToBeClickable(element));
        element.sendKeys(Keys.ENTER);
        Thread.sleep(5000);
    }

    public void deleteGame(String name) {
        WebElement element = driver.findElement(By.xpath("//tr[td[1]//text()[contains(., '" + name + "')]]" +
                "//a[@class='btn btn-danger minebtn btn-sm']"));
        wait.until(ExpectedConditions.elementToBeClickable(element));
        element.sendKeys(Keys.ENTER);
    }

    private void fillForm(String name, String publisher, String minPlayers, String maxPlayers, String price) {
        WebElement element = driver.findElement(By.id("game_name"));
        element.clear();
        element.sendKeys(name);
        wait.until(ExpectedConditions.textToBePresentInElementValue(element, name));
        element = driver.findElement(By.id("game_publisher"));
        element.clear();
        element.sendKeys(publisher);
        wait.until(ExpectedConditions.textToBePresentInElementValue(element, publisher));
        element = driver.findElement(By.id("game_min_players"));
        element.clear();
        element.sendKeys(minPlayers);
        wait.until(ExpectedConditions.textToBePresentInElementValue(element, minPlayers));
        element = driver.findElement(By.id("game_max_players"));
        element.clear();
        element.sendKeys(maxPlayers);
        wait.until(ExpectedConditions.textToBePresentInElementValue(element, maxPlayers));
        element = driver.findElement(By.id("game_price"));
        element.clear();
        element.sendKeys(price);
        wait.until(ExpectedConditions.textToBePresentInElementValue(element, price));
    }
}
