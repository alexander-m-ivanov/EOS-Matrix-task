﻿using OpenQA.Selenium;
using Test_buying_book.Utilities;

namespace Test_buying_book.Pages
{
    class BotDetectionPage : BasePage
    {
        private IWebElement onclick;

        public BotDetectionPage(IWebDriver driver) : base(driver)
        {
            onclick = this.conditionalInitializationOfElement("a[onclick]");
        }

        public void omitBotDetection()
        {
            onclick.Click();
            Utils.WaitUntilDocumentIsReady(driver, 30);
        }

        public bool isOmitBotDetectionElementInitialized()
        {
            if (onclick != null)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
