using Newtonsoft.Json.Linq;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using Test_buying_book.Enums;
using Test_buying_book.Pages;
using Test_buying_book.Utilities;

namespace Test_buying_book.Tests
{
    public class BaseTests
    {
        String url = "https://www.amazon.co.uk/";
        private IWebDriver driver;
        private NavigationPanePage navigationPanePage;
        private LandingPage landingPage;
        private BotDetectionPage botDetectionPage;
        private SearchResultsPage searchResultsPage;
        private ProductDetailsPage productDetailsPage;
        private BasketListItemPage shoppingBasketPage;
        private string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

        private string firstResultPrice;
        private string bookName = "Harry Potter and the Cursed Child - Parts One and Two: The Official Playscript of the Original West End Production";

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver(path + @"\webDrivers\");
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url);

            Utils.WaitUntilDocumentIsReady(driver, 30);
            botDetectionPage = new BotDetectionPage(driver);
            if (botDetectionPage.isOmitBotDetectionElementInitialized())
            {
                botDetectionPage.omitBotDetection();
            }
            landingPage = new LandingPage(driver);
            navigationPanePage = new NavigationPanePage(driver);
        }

        [Test]
        public void UA1_NavigateInChromeBrowserToAmazon()
        {
            Assert.AreEqual(url, driver.Url.ToString());
        }

        [Test]
        public void UA2_SearchInSectionBooksForHarryPotterAndTheCursedChild()
        {
            landingPage.clickAcceptCookies();
            navigationPanePage.selecrSearchCategory("Books");
            navigationPanePage.Search("Harry Potter and the Cursed Child");

            searchResultsPage = new SearchResultsPage(driver);
            string firstResultName = searchResultsPage.getFirstResultItemName();
            firstResultPrice = searchResultsPage.getFirstResultPrice();
            Match match = Regex.Match(firstResultPrice, @"\S(\d+(?:\.\d+)?)\D*$");

            Assert.Multiple(() =>
            {
                Assert.AreEqual(firstResultName, bookName, "The first result is not as expected.");
                Assert.IsTrue(match.Success, "A price is not displayed correctly or not at All.");
                Assert.IsTrue(searchResultsPage.isBookCarrierTypeAsExpected(CarrierType.Paperback.ToString()));
            });
        }

        [Test]
        public void UA3_FromTheAvailableEditionsChoosePaperback()
        {
            searchResultsPage.clickBookCarrierType(CarrierType.Paperback.ToString());
            productDetailsPage = new ProductDetailsPage(driver);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(productDetailsPage.getProductTitle(), bookName, "The productName is not as expected.");
                Assert.AreEqual(firstResultPrice, productDetailsPage.getProductPrice(), "Price is not as expected.");
            });
        }

        [Test]
        public void UA4_AddItToTheShoppingBasketAsGift()
        {
            productDetailsPage.clickGiftCheckbox(true);
            productDetailsPage.clickAddToShoppingBasket();
            new NavigationPanePage(driver).clickShoppingBaskt();
            shoppingBasketPage = new BasketListItemPage(driver, bookName);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(shoppingBasketPage.getProductName(), bookName, "The product name is not as expected.");
                Assert.AreEqual(firstResultPrice, shoppingBasketPage.getProductPrice(), "Price is not as expected.");
                Assert.IsTrue(shoppingBasketPage.isGiftCheckboxChecked(), "BAsket item is not marked as a gift.");
            });
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}