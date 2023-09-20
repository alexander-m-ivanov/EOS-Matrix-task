using OpenQA.Selenium;

namespace Test_buying_book.Pages
{
    class LandingPage : NavigationPanePage
    {
        private IWebElement acceptCookies;

        public LandingPage(IWebDriver driver) : base(driver)
        {
            acceptCookies = initializeElement(By.Id("sp-cc-accept"));
        }

        public void clickAcceptCookies()
        {
            acceptCookies.Click();
        }
    }       
}
