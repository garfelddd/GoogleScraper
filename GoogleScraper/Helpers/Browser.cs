using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace GoogleScraper.Helpers
{
    static class Browser
    {
        public static IWebDriver webDriver;
        public static IWebDriver Initialize()
        {
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;

            var options = new ChromeOptions();
            webDriver = new ChromeDriver(service, options);
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            return webDriver;
        }

    }
}
