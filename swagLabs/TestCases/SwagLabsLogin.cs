using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SwagLabs.PageObjects;

namespace SwagLabs.TestCases
{
    
    public class SwagLabsLogin
    {
        IWebDriver _driver;
        WebDriverWait _wait;


        [SetUp]
        public void Initialize()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));


        }

        [Test]
        public void ValidCredentials()
        {
            var loginPage = new LoginPage(_driver);
            loginPage.SubmitLoginForm("standard_user", "secret_sauce");
            Assert.AreEqual("https://www.saucedemo.com/inventory.html", _driver.Url, "Invalid credentials");
        }

        [Test]
        public void InvalidUsername()
        {
            var loginPage = new LoginPage(_driver);
            loginPage.SubmitLoginForm("invalid_user", "secret_sauce");
            Assert.AreEqual("https://www.saucedemo.com/", _driver.Url);
            string errorMsg = _driver.FindElement(By.XPath("//div/form/div[3]/h3")).Text;
            Assert.AreEqual("Epic sadface: Username and password do not match any user in this service", errorMsg, "Wrong error message displayed");
        }

        [Test]
        public void InvalidPass()
        {
            var loginPage = new LoginPage(_driver);
            loginPage.SubmitLoginForm("standard_user", "WrongPassword");
            Assert.AreEqual("https://www.saucedemo.com/", _driver.Url);
            string errorMsg = _driver.FindElement(By.XPath("//div/form/div[3]/h3")).Text;
            Assert.AreEqual("Epic sadface: Username and password do not match any user in this service", errorMsg, "Wrong error message displayed");
            
        }

        [Test]
        public void EmptyUserPass()
        {
            var loginPage = new LoginPage(_driver);
            loginPage.SubmitLoginForm("", "");
            Assert.AreEqual("https://www.saucedemo.com/", _driver.Url);
            string errorMsg = _driver.FindElement(By.XPath("//div/form/div[3]/h3")).Text;
            Assert.AreEqual("Epic sadface: Username is required", errorMsg, "Wrong error message displayed");

        }

        [Test]
        public void AccessAccountAfterLogout()
        {
            var loginPage = new LoginPage(_driver);
            loginPage.SubmitLoginForm("standard_user", "secret_sauce");

            var burgerMenu = new ProductsPage(_driver);
            burgerMenu.OpenBurgerMenu();
            _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("react-burger-cross-btn")));
            burgerMenu.LogOut();
            _driver.Navigate().GoToUrl("https://www.saucedemo.com/inventory.html");
            string errorMsg = _driver.FindElement(By.XPath("//div/form/div[3]/h3")).Text;
            Assert.AreEqual("Epic sadface: You can only access '/inventory.html' when you are logged in.", errorMsg, "Wrong error message displayed");
        }


        [Test]
        public void LockedOutUser()
        {
            var loginPage = new LoginPage(_driver);
            loginPage.SubmitLoginForm("locked_out_user", "secret_sauce");
            Assert.AreEqual("https://www.saucedemo.com/", _driver.Url);
            string errorMsg = _driver.FindElement(By.XPath("//div/form/div[3]/h3")).Text;
            Assert.AreEqual("Epic sadface: Sorry, this user has been locked out.", errorMsg, "Wrong error message displayed");

            

        }


        [TearDown]
        public void EndTest()
        {
            _driver.Close();
          
        }


    }
}
