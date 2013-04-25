namespace Thoughtology.GameOfLife.AcceptanceTests.Infrastructure
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Internal;

    public static class WebElementExtensions
    {
        public static void SetAttribute(this IWebElement element, string attributeName, string value)
        {
            const string setAttribute = "arguments[0].setAttribute(arguments[1], arguments[2])";
            JavaScript(element).ExecuteScript(setAttribute, element, attributeName, value);
        }

        private static IJavaScriptExecutor JavaScript(IWebElement element)
        {
            var driverWrapper = (IWrapsDriver)element;
            return (IJavaScriptExecutor)driverWrapper.WrappedDriver;
        }
    }
}
