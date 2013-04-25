namespace Thoughtology.GameOfLife.AcceptanceTests.Infrastructure
{
    using System;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    public class WebBrowser
    {
        private readonly IWebDriver driver;

        public WebBrowser()
        {
            driver = new ChromeDriver();
        }

        public void NavigateTo(string relativeUrl)
        {
            var url = GetAbsoluteUrl(relativeUrl);
            driver.Navigate().GoToUrl(url);
        }

        public IWebElement FindElementById(string id)
        {
            return driver.FindElement(By.Id(id));
        }

        public void Close()
        {
            driver.Quit();
        }

        private static Uri GetAbsoluteUrl(string relativeUrl)
        {
            return new Uri(WebServer.RootUri, relativeUrl);
        }
    }
}
