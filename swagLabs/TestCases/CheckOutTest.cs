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
    public class CheckOutTest
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
        public void MissingFirstNameCO()
        {
            var productList = new ProductsPage(_driver);
            var cart = new ShoppingCart(_driver);
            var checkout = new CheckOut(_driver);
            productList.BackPackAddToCart();
            productList.OpenCart();
            cart.StartCheckout();
            checkout.EnterBuyerDetails("", "Doe", "11968");

            string errorMsg = _driver.FindElement(By.XPath("//*[@id='checkout_info_container']/div/form/div[1]/div[4]/h3")).Text;
            Assert.AreEqual("Error: First Name is required", errorMsg, "Wrong error message displayed");
            Assert.AreEqual(_driver.Url, "https://www.saucedemo.com/checkout-step-one.html");

        }

        [Test]
        public void MissingLastNameCO()
        {
            var productList = new ProductsPage(_driver);
            var cart = new ShoppingCart(_driver);
            var checkout = new CheckOut(_driver);
            productList.BackPackAddToCart();
            productList.OpenCart();
            cart.StartCheckout();
            checkout.EnterBuyerDetails("John", "", "11968");

            string errorMsg = _driver.FindElement(By.XPath("//*[@id='checkout_info_container']/div/form/div[1]/div[4]/h3")).Text;
            Assert.AreEqual("Error: Last Name is required", errorMsg, "Wrong error message displayed");
            Assert.AreEqual(_driver.Url, "https://www.saucedemo.com/checkout-step-one.html");

        }

        [Test]
        public void MissingPostalCO()
        {
            var productList = new ProductsPage(_driver);
            var cart = new ShoppingCart(_driver);
            var checkout = new CheckOut(_driver);
            productList.BackPackAddToCart();
            productList.OpenCart();
            cart.StartCheckout();
            checkout.EnterBuyerDetails("John", "Doe", "");

            string errorMsg = _driver.FindElement(By.XPath("//*[@id='checkout_info_container']/div/form/div[1]/div[4]/h3")).Text;
            Assert.AreEqual("Error: Postal Code is required", errorMsg, "Wrong error message displayed");
            Assert.AreEqual(_driver.Url, "https://www.saucedemo.com/checkout-step-one.html");

        }

        [Test]
        public void ContinueStepTwoCO()
        {
            var productList = new ProductsPage(_driver);
            var cart = new ShoppingCart(_driver);
            var checkout = new CheckOut(_driver);
            productList.BackPackAddToCart();
            productList.OpenCart();
            cart.StartCheckout();
            checkout.EnterBuyerDetails("John", "Doe", "11968");
            Assert.AreEqual(_driver.Url, "https://www.saucedemo.com/checkout-step-two.html");

        }

        [Test]
        public void PlaceOrderSuccess()
        {
            var productList = new ProductsPage(_driver);
            var cart = new ShoppingCart(_driver);
            var checkout = new CheckOut(_driver);
            productList.BackPackAddToCart();
            productList.OpenCart();
            cart.StartCheckout();
            checkout.EnterBuyerDetails("John", "Doe", "11968");
            checkout.PlaceOrder();
            Assert.AreEqual(_driver.Url, "https://www.saucedemo.com/checkout-complete.html");
            Assert.IsTrue(_driver.FindElement(By.ClassName("complete-header")).Displayed);
            checkout.BackHomePlacedOrder();
            Assert.AreEqual(_driver.Url, "https://www.saucedemo.com/inventory.html");

        }

        [Test]
        public void CancelOrderSuccess()
        {
            var productList = new ProductsPage(_driver);
            var cart = new ShoppingCart(_driver);
            var checkout = new CheckOut(_driver);
            productList.BackPackAddToCart();
            productList.OpenCart();
            cart.StartCheckout();
            checkout.EnterBuyerDetails("John", "Doe", "11968");
            checkout.CancelOrder();
            Assert.AreEqual(_driver.Url, "https://www.saucedemo.com/inventory.html");

        }



        

        [TearDown]
        public void EndTest()
        {
             _driver.Close();

        }
    }
}
