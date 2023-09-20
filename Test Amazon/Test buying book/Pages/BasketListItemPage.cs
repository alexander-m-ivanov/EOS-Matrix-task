using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace Test_buying_book.Pages
{
    class BasketListItemPage : NavigationPanePage
    {
        private ReadOnlyCollection<IWebElement> basketListItems;
        private IWebElement listItem;
        private string listItemTitle;
        private IWebElement listItemPrice;
        private IWebElement giftCheckbox;
        private IWebElement itemQuantity;

        public BasketListItemPage(IWebDriver driver, string itemName) : base(driver)
        {
            basketListItems = initializeElements(By.CssSelector("#activeCartViewForm .sc-list-item"));
            foreach (var item in basketListItems)
            {
                listItemTitle = GetInnerHtml(initializeElement(By.CssSelector(".sc-grid-item-product-title .a-offscreen"), item)).Trim();
                if (listItemTitle == itemName)
                {
                    listItem = item;
                    break;
                }
                else
                {
                    listItem = null;
                }
            }

            listItemPrice = initializeElement(By.CssSelector(".sc-product-price"), listItem);
            giftCheckbox = initializeElement(By.CssSelector(".a-checkbox input"), listItem);
            itemQuantity = initializeElement(By.CssSelector(".a-dropdown-prompt"), listItem);
        }

        public string getItemQuantity()
        {
            return GetInnerHtml(itemQuantity);
        }

        public int getNumberOfItemsInShoppingBasket()
        {
            return basketListItems.Count;
        }

        public string getProductName()
        {
            return listItemTitle;
        }

        public string getProductPrice()
        {
            return GetInnerHtml(listItemPrice);
        }

        public bool isGiftCheckboxChecked()
        {
            return giftCheckbox.Selected;
        }
    }
}
