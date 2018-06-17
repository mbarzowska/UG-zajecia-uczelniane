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
public class PageObjectDuckDuckGo {

    public WebDriver driver;
    private WebDriverWait wait;

    public PageObjectDuckDuckGo(WebDriver driver) {
        this.driver = driver;
        driver.get("https://duckduckgo.com");
        this.wait = new WebDriverWait(driver,10);
    }

    public int getFoundSitesCount() {
        return driver.findElements(By.xpath("//div[@id='links']/div[contains(@id, 'r1')]")).size();
    }

    public String getCurrentTitle() {
        return driver.getTitle();
    }

    public void searchForSite(String name) {
        wait.until(ExpectedConditions.presenceOfElementLocated(By.id("search_form_input_homepage")));
        WebElement element = driver.findElement(By.id("search_form_input_homepage"));
        element.sendKeys(name);
        wait.until(ExpectedConditions.textToBePresentInElementValue(element, name));
        element.sendKeys(Keys.ENTER);
        wait.until(ExpectedConditions.presenceOfElementLocated(By.xpath("//div[@id='links']/div")));
    }

    public void searchForSiteAndEnter(String siteLinkText, String urlToBe) {
        searchForSite(siteLinkText);
        WebElement element = driver.findElement(By.partialLinkText(siteLinkText));
        wait.until(ExpectedConditions.elementToBeClickable(element));
        element.sendKeys(Keys.ENTER);
        wait.until(ExpectedConditions.urlContains(urlToBe));
    }
}
