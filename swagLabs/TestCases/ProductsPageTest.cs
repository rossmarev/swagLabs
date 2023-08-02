using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SwagLabs.PageObjects;
using SeleniumExtras.WaitHelpers;
using System.Collections;

namespace SwagLabs.TestCases
{
    public class ProductsPageTestStandardUser
    {
        IWebDriver _driver;
        WebDriverWait _wait;

        [SetUp]
        public void Initialize()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            var loginPage = new LoginPage(_driver);
            loginPage.SubmitLoginForm("standard_user", "secret_sauce");
        }

        [Test]
        public void OpenBurgerMenu()
        {
            var burgerMenu = new ProductsPage(_driver);
            burgerMenu.OpenBurgerMenu();
            Assert.AreEqual(_driver.FindElement(By.ClassName("bm-menu-wrap")).GetAttribute("aria-hidden"), "false");
        }

        [Test]
        public void CloseBurgerMenu()
        {
            var burgerMenu = new ProductsPage(_driver);
            burgerMenu.OpenBurgerMenu();
            _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("react-burger-cross-btn")));
            burgerMenu.CloseMenu();
            Assert.AreEqual(_driver.FindElement(By.ClassName("bm-menu-wrap")).GetAttribute("aria-hidden"), "true");
        }

        [Test]
        public void OpenAboutUs()
        {
            var burgerMenu = new ProductsPage(_driver);
            burgerMenu.OpenBurgerMenu();
            _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("react-burger-cross-btn")));
            burgerMenu.OpenAboutUs();
            Assert.AreEqual("https://saucelabs.com/", _driver.Url, "About us page not found");
        }

        [Test]
        public void LogOut()
        {
            var burgerMenu = new ProductsPage(_driver);
            burgerMenu.OpenBurgerMenu();
            _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("react-burger-cross-btn")));
            burgerMenu.LogOut();
            Assert.AreEqual("https://www.saucedemo.com/", _driver.Url, "User not logged out");
        }

        [Test]
        public void SortingValues()
        {
            SelectElement oSelect = new SelectElement(_driver.FindElement(By.ClassName("product_sort_container")));
            IList<IWebElement> values = oSelect.Options;

            List<string> expectedValues = new List<string> { "Name (A to Z)", "Name (Z to A)", "Price (low to high)", "Price (high to low)" };
            int optSize = values.Count;

            for (int i = 0; i < optSize; i++)
            {
                string sValue = values.ElementAt(i).Text;
                Assert.Contains(sValue, expectedValues, "wrong values");
            }

        }


        [Test]
        public void DefaultSortingValue()
        {
            SelectElement oSelect = new SelectElement(_driver.FindElement(By.ClassName("product_sort_container")));
            IWebElement initialValue = oSelect.SelectedOption;

            Assert.AreEqual(initialValue.Text, "Name (A to Z)", "Wrong default sorting");

        }

        [Test]
        public void CorrectSortingValueDisplayed()
        {
            var sortingComp = new ProductsPage(_driver);
             sortingComp.SelectSorting("lohi");
               var selectedOption = (_driver.FindElement(By.ClassName("product_sort_container"))).GetAttribute("value");
              Assert.AreEqual("lohi", selectedOption);         
        }

        [Test]
        public void OpenPdpTitle()
        {
            var productList = new ProductsPage(_driver);
            productList.BackPackTitleClick();
            Assert.AreEqual(_driver.Url, "https://www.saucedemo.com/inventory-item.html?id=4", "PDP not opened");
        }

        [Test]
        public void OpenPdpImage()
        {
            var productList = new ProductsPage(_driver);
            _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='item_4_img_link']/img")));
            productList.BackPackImageClick();
            Assert.AreEqual(_driver.Url, "https://www.saucedemo.com/inventory-item.html?id=4", "PDP not opened");
        }


        [Test]
        public void OneItemCartBadge()
        {
            var productList = new ProductsPage(_driver);
            productList.BackPackAddToCart();
            Assert.AreEqual("1",_driver.FindElement(By.ClassName("shopping_cart_badge")).Text, "Incorrect badge value");
            Assert.IsTrue(_driver.FindElement(By.Id("remove-sauce-labs-backpack")).Displayed);
        }

        [Test]
        public void TwoItemsCartBadge()
        {
            var productList = new ProductsPage(_driver);
            productList.BackPackAddToCart();
            _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("add-to-cart-sauce-labs-fleece-jacket")));
            productList.JacketAddToCart();
            Assert.AreEqual("2",_driver.FindElement(By.ClassName("shopping_cart_badge")).Text, "Incorrect badge value");
        }

        [Test]
        public void RemoveFromCart()
        {
            var productList = new ProductsPage(_driver);
            productList.BackPackAddToCart();
            _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("remove-sauce-labs-backpack")));
            productList.BackPackRemoveCart();
            Assert.AreEqual("",_driver.FindElement(By.ClassName("shopping_cart_link")).Text, "Item not removed from cart");
            Assert.IsTrue(_driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack")).Displayed);

        }

        [Test]
        public void TwitterLink()
        {
            var productList = new ProductsPage(_driver);
            productList.Twitter();
            string newTab = _driver.WindowHandles.Last();
            _driver.SwitchTo().Window(newTab);
            Assert.AreEqual(_driver.Url, "https://twitter.com/saucelabs", "Wrong redirect Twitter link");
        }




        [TearDown]
        public void EndTest()
        {
             _driver.Close();

        }


        public class ProductsPageTestProblemdUser
        {
            IWebDriver _driver;
            WebDriverWait _wait;

            [SetUp]
            public void Initialize()
            {
                _driver = new ChromeDriver();
                _driver.Manage().Window.Maximize();
                _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                _driver.Navigate().GoToUrl("https://www.saucedemo.com/");
                var loginPage = new LoginPage(_driver);
                loginPage.SubmitLoginForm("problem_user", "secret_sauce");
            }



            [Test]
            public void OneItemCartBadgePU()
            {
                var productList = new ProductsPage(_driver);
                productList.BackPackAddToCart();
                Assert.AreEqual("1",_driver.FindElement(By.ClassName("shopping_cart_badge")).Text, "Incorrect badge value");
                Assert.IsTrue(_driver.FindElement(By.Id("remove-sauce-labs-backpack")).Displayed);
            }

            [Test]
            public void RemoveFromCartPU()
            {
                var productList = new ProductsPage(_driver);
                productList.BackPackAddToCart();
                _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("remove-sauce-labs-backpack")));
                productList.BackPackRemoveCart();
                Assert.AreEqual("",_driver.FindElement(By.ClassName("shopping_cart_link")).Text, "Item not removed from cart");
                Assert.IsTrue(_driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack")).Displayed);
                
            }

            [Test]
            public void OpenPdpTitlePU()
            {
                var productList = new ProductsPage(_driver);
                productList.BackPackTitleClick();
                Assert.AreEqual(_driver.Url, "https://www.saucedemo.com/inventory-item.html?id=4", "Wrong PDP opened");
            }

            [Test]
            public void CorrectSortingValueDisplayed()
            {
                var sortingComp = new ProductsPage(_driver);
                sortingComp.SelectSorting("lohi");
                var selectedOption = (_driver.FindElement(By.ClassName("product_sort_container"))).GetAttribute("value");
                Assert.AreEqual("lohi", selectedOption, "Sorting option not selected");
            }


            [TearDown]
            public void EndTest()
            {
                 _driver.Close();

            }
        }
    }
}
