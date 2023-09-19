using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_buying_book.Utilities;

namespace Test_buying_book.Pages
{
    class ProductDetailsPage : BasePage
    {
        private IWebElement productTitle;
        private IWebElement price;
        private IWebElement giftCheckbox;
        private IWebElement addToBasket;

        public ProductDetailsPage(IWebDriver driver) : base(driver)
        {
            productTitle = initializeElement(By.Id("productTitle"));
            price = initializeElement(By.CssSelector("#formats .a-button-selected .a-color-price"));
            giftCheckbox = initializeElement(By.Id("gift-wrap"));
            addToBasket = initializeElement(By.Id("add-to-cart-button"));
        }

        public string getProductTitle ()
        {
            return productTitle.Text.Trim();
        }

        public string getProductPrice()
        {
            return GetInnerHtml(price).Trim();
        }

        public void clickGiftCheckbox(bool check)
        {
            if (check && (!giftCheckbox.Selected))
            {
                giftCheckbox.Click();
            }
            else if ((!check) && giftCheckbox.Selected)
            {
                giftCheckbox.Click();
            }
        }

        public void clickAddToShoppingBasket()
        {
            addToBasket.Click();
            Utils.WaitUntilDocumentIsReady(driver, 30);
        }
    }
}
