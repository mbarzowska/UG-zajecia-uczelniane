package Helpers;

public class BrowserSelector {
    public static void setBrowserProperty(String browser) {
        if (System.getProperty("os.name").contains("Windows")) {
            switch (browser) {
                case "Chrome":
                    System.setProperty("webdriver.chrome.driver", "src/test/resources/chromedriver.exe");
                    break;
//                case "Firefox":
//                    System.setProperty("webdriver.gecko.driver", "src/test/resources/geckodriver.exe");
//                    break;
//                case "Explorer":
//                    System.setProperty("webdriver.ie.driver", "src/test/resources/IEDriverServer.exe");
//                    break;
                case "Opera":
                    System.setProperty("webdriver.opera.driver", "src/test/resources/operadriver.exe");
                    break;
                case "Edge":
                    System.setProperty("webdriver.edge.driver", "src/test/resources/MicrosoftWebDriver.exe");
                    break;
//                case "PhantomJS":
//                    break;
//                case "HtmlUnit":
//                    break;
            }
        } else {
            switch (browser) {
                case "Chrome":
                    System.setProperty("webdriver.chrome.driver", "src/test/resources/chromedriver");
                    break;
//                case "Firefox":
//                    System.setProperty("webdriver.gecko.driver", "src/test/resources/geckodriver");
//                    break;
                case "Explorer":
                    throw new IllegalArgumentException("Explorer browser is accesed only on Windows");
                case "Opera":
                    System.setProperty("webdriver.opera.driver", "src/test/resources/operadriver");
                    break;
                case "Edge":
                    throw new IllegalArgumentException("Edge browser is accesed only on Windows");
//                case "PhantomJS":
//                    break;
//                case "HtmlUnit":
//                    break;
            }
        }
    }
}
