import Helpers.BrowserManager;
import PageObjects.PageObjectDuckDuckGo;
import io.github.bonigarcia.SeleniumExtension;
import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.openqa.selenium.WebDriver;

import static org.junit.jupiter.api.Assertions.*;

@ExtendWith(SeleniumExtension.class)
public class DuckDuckGoSearchTest {

    private WebDriver driver;
    public PageObjectDuckDuckGo page;

    @BeforeEach
    public void setUp() {
        driver = BrowserManager.initializeBrowser(driver, "Edge");
        driver.manage().window().maximize();
    }

    @Test
    public void searchForExistingSite() {
        page = new PageObjectDuckDuckGo(driver);

        page.searchForSite("GitHub");

        assertTrue(page.getFoundSitesCount() > 0);
    }

    @Test
    public void searchForNonExistingSite() {
        page = new PageObjectDuckDuckGo(driver);

        page.searchForSite("...........................................");

        assertFalse(page.getFoundSitesCount() > 0);
    }

    @Test
    public void enterOnOneOfExistingResultSites() {
        page = new PageObjectDuckDuckGo(driver);

        page.searchForSiteAndEnter("R2-D2 - Wikipedia", "wikipedia.org/wiki/R2-D2");

        assertTrue(page.getCurrentTitle().contains("Wikipedia"));
    }

    @AfterEach
    public void tearDown() {
        driver.quit();
    }
}
