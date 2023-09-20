using OpenQA.Selenium;
using System.Collections.ObjectModel;
using Test_buying_book.Utilities;

namespace Test_buying_book.Pages
{
    class SearchResultsPage : NavigationPanePage
    {
        private IWebElement firstResultContainer;
        private IWebElement firstResultName;
        private IWebElement firstResultPriceContainer;
        private ReadOnlyCollection<IWebElement> firstResultInformationCarrierTypes;
        private IWebElement firstResultPrice;

        public SearchResultsPage(IWebDriver driver) : base(driver)
        {
            firstResultContainer = initializeElement(By.CssSelector("div[data-cel-widget='search_result_2']"));
            firstResultName = initializeElement(By.CssSelector("h2 span"), firstResultContainer);

            firstResultPriceContainer = initializeElement(By.CssSelector(".puis-price-instructions-style"), firstResultContainer);
            firstResultInformationCarrierTypes = initializeElements(By.CssSelector("a"), firstResultPriceContainer);
            firstResultPrice = initializeElement(By.CssSelector("span[data-a-size='xl'] .a-offscreen"), firstResultPriceContainer);
        }

        public string getFirstResultItemName()
        {
            return firstResultName.Text;
        }

        public string getFirstResultPrice()
        {
            return GetInnerHtml(firstResultPrice);
        }

        public bool isBookCarrierTypeAsExpected(string type)
        {
            foreach (var item in firstResultInformationCarrierTypes)
            {
                if (item.Text == type)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public void clickBookCarrierType(string type)
        {
            foreach (var item in firstResultInformationCarrierTypes)
            {
                if (item.Text == type)
                {
                    item.Click();
                    Utils.WaitUntilDocumentIsReady(driver, 30);
                    break;
                }
            }
        }
    }
}
