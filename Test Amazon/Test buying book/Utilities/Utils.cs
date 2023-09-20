using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace Test_buying_book.Utilities
{
    public static class Utils
    {
        public static void WaitUntilDocumentIsReady(this IWebDriver driver, int timeoutInSeconds)
        {
            var javaScriptExecutor = driver as IJavaScriptExecutor;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));

            try
            {
                Func<IWebDriver, bool> readyCondition = webDriver => (bool)javaScriptExecutor.ExecuteScript("return (document.readyState == 'complete')");
                wait.Until(readyCondition);
            }
            catch (InvalidOperationException)
            {
                wait.Until(wd => javaScriptExecutor.ExecuteScript("return document.readyState").ToString() == "complete");
            }
        }
    }
}
