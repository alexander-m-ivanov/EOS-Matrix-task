using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_buying_book.Utilities;

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
