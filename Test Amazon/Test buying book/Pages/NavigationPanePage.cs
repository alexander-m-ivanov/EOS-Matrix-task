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
    class NavigationPanePage : BasePage
    {
        private IWebElement searchField;
        private SelectElement dropDownSelectCategory;
        private IWebElement shoppingBasket;

        public NavigationPanePage(IWebDriver driver) : base(driver)
        {
            searchField = initializeElement(By.Id("twotabsearchtextbox"));
            dropDownSelectCategory = new SelectElement(initializeElement(By.CssSelector("#nav-search-dropdown-card select")));
            shoppingBasket = initializeElement(By.Id("nav-cart-count"));
        }

        public void selecrSearchCategory(string searchCategory)
        {
            dropDownSelectCategory.SelectByText(searchCategory);
        }

        public void Search(string searchString)
        {
            searchField.SendKeys(searchString);
            searchField.Submit();
            Utils.WaitUntilDocumentIsReady(driver, 30);
        }

        public void clickShoppingBaskt()
        {
            shoppingBasket.Click();
            Utils.WaitUntilDocumentIsReady(driver, 30);
        }
    }
}
