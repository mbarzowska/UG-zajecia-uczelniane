package Helpers;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.chrome.ChromeOptions;
import org.openqa.selenium.edge.EdgeDriver;
import org.openqa.selenium.opera.OperaDriver;
import org.openqa.selenium.opera.OperaOptions;

public class BrowserManager {
    public static WebDriver initializeBrowser(WebDriver driver, String browser) {
        String browserTruncated = browser;
        if (browser.endsWith("Headless")) {
            browserTruncated = browser.substring(0, browser.length() - 8);
        }
        BrowserSelector.setBrowserProperty(browserTruncated);
        switch (browser) {
            case "Chrome":
                driver = new ChromeDriver();
                break;
            case "ChromeHeadless":
                ChromeOptions optionsChrome = new ChromeOptions();
                optionsChrome.addArguments("headless");
                optionsChrome.addArguments("window-size=1200x600");
                driver = new ChromeDriver(optionsChrome);
                break;
//            case "Firefox":
//                driver = new FirefoxDriver();
//                break;
//            case "FirefoxHeadless":
//                FirefoxOptions optionsFirefox = new FirefoxOptions();
//                optionsFirefox.setHeadless(true);
//                driver = new FirefoxDriver(optionsFirefox);
//                break;
//            case "Explorer":
//                driver = new InternetExplorerDriver();
//                break;
            case "Edge":
                driver = new EdgeDriver();
                break;
            case "Opera":
                OperaOptions operaOptions = new OperaOptions();
                //if Windows
                operaOptions.setBinary("C:\\Program Files\\Opera\\53.0.2907.68\\opera.exe");
                //else
                //operaOptions.setBinary(opera location on linux, etc)
                driver = new OperaDriver(operaOptions);
                break;
//            case "HtmlUnit":
//                driver = new HtmlUnitDriver();
//                break;
//            case "PhantomJS":
//                DesiredCapabilities capsPhantomJS = new DesiredCapabilities();
//                capsPhantomJS.setJavascriptEnabled(true);
//                capsPhantomJS.setCapability("takesScreenshot", true);
//                if(System.getProperty("os.name").contains("Windows")){
//                    capsPhantomJS.setCapability(
//                            PhantomJSDriverService.PHANTOMJS_EXECUTABLE_PATH_PROPERTY,
//                            "src/test/resources/phantomjs.exe"
//                    );
//                } else {
//                    capsPhantomJS.setCapability(
//                            PhantomJSDriverService.PHANTOMJS_EXECUTABLE_PATH_PROPERTY,
//                            "src/test/resources/phantomjs"
//                    );
//                }
//                driver = new PhantomJSDriver(capsPhantomJS);
        }
        return driver;
    }
}
