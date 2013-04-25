namespace Thoughtology.GameOfLife.AcceptanceTests.Pages
{
    using System.Linq;
    using Infrastructure;
    using OpenQA.Selenium;

    public class UniversePage
    {
        private readonly WebBrowser browser;

        public UniversePage(WebBrowser browser)
        {
            this.browser = browser;
            browser.NavigateTo("views/universe.html");
        }

        public void ResurrectCell(int row, int column)
        {
            Cell(row, column).SetAttribute("class", "alive");
        }

        public void KillCell(int row, int column)
        {
            Cell(row, column).SetAttribute("class", "dead");
        }

        public bool IsCellAlive(int row, int column)
        {
            return Cell(row, column).GetAttribute("class") == "alive";
        }

        public void DisplayNextGeneration()
        {
            var button = browser.FindElementById("nextGeneration");
            button.Click();
        }

        private IWebElement Cell(int row, int column)
        {
            var table = browser.FindElementById("grid");
            var tr = table.FindElements(By.TagName("tr")).ElementAt(row);
            return tr.FindElements(By.TagName("td")).ElementAt(column);
        }
    }
}
