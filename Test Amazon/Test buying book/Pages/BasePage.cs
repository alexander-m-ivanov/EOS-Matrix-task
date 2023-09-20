using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace Test_buying_book.Pages
{
    class BasePage
    {
        protected IWebDriver driver;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement conditionalInitializationOfElement(string element)
        {
            IWebElement initializedElement;

            try
            {
                initializedElement = this.driver.FindElement(By.CssSelector(element));
            }
            catch (NoSuchElementException ex)
            {
                initializedElement = null;
            }
            return initializedElement;
        }

        public string GetInnerHtml(IWebElement element)
        {
            IJavaScriptExecutor javaScriptExecutor = this.driver as IJavaScriptExecutor;
            string innerHtml = javaScriptExecutor.ExecuteScript("return arguments[0].innerHTML;", element).ToString();

            return innerHtml;
        }

        public IWebElement initializeElement(By locator)
        {
            return initializeElement(locator, null);
        }
        public IWebElement initializeElement(By locator, IWebElement parentChainedElement)
        {
            IWebElement element = null;

            int repeat = 0;
            while (repeat <= 20)
            {
                try
                {
                    if (parentChainedElement != null)
                    {
                        element = parentChainedElement.FindElement(locator);
                    }
                    else
                    {
                        element = this.driver.FindElement(locator);
                    }

                    _ = element.Displayed;
                    break;
                } catch (StaleElementReferenceException ex)
                {
                    repeat++;
                }
            }
            return element;
        }

        public ReadOnlyCollection<IWebElement> initializeElements(By locator)
        {
            return initializeElements(locator, null);
        }
        public ReadOnlyCollection<IWebElement> initializeElements(By locator, IWebElement parentChainedElement)
        {
            ReadOnlyCollection<IWebElement> elements = null;

            int repeat = 0;
            while (repeat <= 5)
            {
                try
                {
                    bool isDisplayed = false;
                    if (parentChainedElement != null)
                    {
                        elements = parentChainedElement.FindElements(locator);
                    }
                    else
                    {
                        elements = this.driver.FindElements(locator);
                    }

                    foreach (var item in elements)
                    {
                        isDisplayed = item.Displayed;
                        if (!isDisplayed)
                        {
                            break;
                        }

                    }

                    if (isDisplayed)
                    {
                        break;
                    }
                }
                catch (StaleElementReferenceException ex)
                {
                    repeat++;
                }
            }
            return elements;
        }
    }
}
