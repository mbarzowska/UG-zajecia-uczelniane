import Helpers.BrowserManager;
import PageObjects.PageObjectHeroku;
import io.github.bonigarcia.SeleniumExtension;
import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.openqa.selenium.WebDriver;

import org.assertj.core.api.Assertions;
import static org.junit.jupiter.api.Assertions.*;

@ExtendWith(SeleniumExtension.class)
public class HerokuCRUDTest {

    private WebDriver driver;
    public PageObjectHeroku page;

    @BeforeEach
    public void setUp() {
        driver = BrowserManager.initializeBrowser(driver, "Edge");
        driver.manage().window().maximize();
    }

    @Test
    public void addValidBoardGame() {
        page = new PageObjectHeroku(driver);
        int entriesCountBeforeAddition = page.getTableRowsCount();

        page.addGame("Monopoly", "Hasbro", "2", "4", "59.99");

        int entriesCountAfterAddition = page.getTableRowsCount();
        assertNotEquals(entriesCountBeforeAddition, entriesCountAfterAddition);
    }

    @Test
    public void addInvalidBoardGame() {
        page = new PageObjectHeroku(driver);
        int entriesBefore = page.getTableRowsCount();

        page.addGame("", "", "1", "1", "1111-11111");

        int entriesAfter = page.getTableRowsCount();
        assertEquals(entriesBefore, entriesAfter);
    }

    @Test
    public void formWithInvalidName() {
        page = new PageObjectHeroku(driver);

        page.addGameForm("", "Hasbro", "2", "4", "29.99");

        int result = page.getErrorCount();
        assertEquals(2, result);
    }

    @Test
    public void formWithInvalidPublisher() {
        page = new PageObjectHeroku(driver);

        page.addGameForm("Monopoly", "", "2", "4", "29.99");

        int result = page.getErrorCount();
        assertEquals(3, result);
    }

    @Test
    public void formWithInvalidMinPlayers() {
        page = new PageObjectHeroku(driver);

        page.addGameForm("Monopoly", "Hasbro", "1", "4", "29.99");

        int result = page.getErrorCount();
        assertEquals(1, result);
    }

    @Test
    public void formWithInvalidMaxPlayers() {
        page = new PageObjectHeroku(driver);

        page.addGameForm("Monopoly", "Hasbro", "2", "1", "29.99");

        int result = page.getErrorCount();
        assertEquals(2, result);
    }

    @Test
    public void formWithInvalidPrice() {
        page = new PageObjectHeroku(driver);

        page.addGameForm("Monopoly", "Hasbro", "2", "4", "11--33");

        int result = page.getErrorCount();
        assertEquals(1, result);
    }

    @Test
    public void editBoardGameWithValidInformation() throws InterruptedException {
        page = new PageObjectHeroku(driver);

        page.addGame("Mnply", "Hasbro", "2", "4", "29.99");
        page.editGame("Mnply", "Monopoly", "Hasbro", "2", "6", "39.99");

        Assertions.assertThat(page.isOnDetailsPage());
    }

    @Test
    public void editBoardGameWithInvalidInformation() throws InterruptedException {
        page = new PageObjectHeroku(driver);

        page.addGame("Mnply", "Hasbro", "2", "4", "29.99");
        page.editGame("", "", "", "", "", "");

        Assertions.assertThat(page.getErrorCount() > 0);
    }

    @Test
    public void deleteGame() {
        page = new PageObjectHeroku(driver);

        page.addGame("Usun", "Hasbro", "2", "4", "29.99");
        int entriesBefore = page.getTableRowsCount();
        page.deleteGame("Usun");
        int entriesAfter = page.getTableRowsCount();

        Assertions.assertThat(entriesAfter < entriesBefore);
    }

    @AfterEach
    public void tearDown() {
        driver.quit();
    }
}
