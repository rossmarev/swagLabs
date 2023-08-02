using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SwagLabs.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwagLabs.TestCases
{
    public class ShoppingCartTest
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
        public void CartItems()
        {
            var productList = new ProductsPage(_driver);
            productList.BackPackAddToCart();
            productList.OpenCart();

            Assert.AreEqual(_driver.Url, "https://www.saucedemo.com/cart.html");
            Assert.AreEqual(_driver.FindElement(By.ClassName("inventory_item_name")).Text, "Sauce Labs Backpack", "Item not added");
            Assert.IsTrue(_driver.FindElement(By.Id("remove-sauce-labs-backpack")).Displayed);
        }

        [Test]
        public void ItemRemoved()
        {
            var productList = new ProductsPage(_driver);
            var cart = new ShoppingCart(_driver);
            productList.BackPackAddToCart();
            productList.OpenCart();
            cart.RemoveItemCart();

            Assert.AreEqual(_driver.Url, "https://www.saucedemo.com/cart.html");
            Assert.IsTrue(_driver.FindElement(By.ClassName("removed_cart_item")).Enabled);
        }
        [Test]
        public void CartItemQTY()
        {
            var productList = new ProductsPage(_driver);
            productList.BackPackAddToCart();
            productList.OpenCart();

            Assert.AreEqual(_driver.Url, "https://www.saucedemo.com/cart.html");
            Assert.AreEqual(_driver.FindElement(By.ClassName("cart_quantity")).Text, "1", "Item count not correct");
            Assert.IsTrue(_driver.FindElement(By.Id("remove-sauce-labs-backpack")).Displayed);
        }

        [Test]
        public void BackToProductsList()
        {
            var productList = new ProductsPage(_driver);
            var cart = new ShoppingCart(_driver);
            productList.BackPackAddToCart();
            productList.OpenCart();
            cart.BackToProducts();
            Assert.AreEqual(_driver.Url, "https://www.saucedemo.com/inventory.html");
            Assert.IsTrue(_driver.FindElement(By.ClassName("inventory_list")).Displayed);
        }

        [Test]
        public void OpenProductDetailsPage()
        {
            var productList = new ProductsPage(_driver);
            var cart = new ShoppingCart(_driver);
            productList.BackPackAddToCart();
            productList.OpenCart();
            cart.OpenItemDetailsFromCart();

            Assert.AreEqual(_driver.Url, "https://www.saucedemo.com/inventory-item.html?id=4");
        }

        [TearDown]
        public void EndTest()
        {
             _driver.Close();

        }
    }
}
