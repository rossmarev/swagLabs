using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;


namespace SwagLabs.PageObjects
{
    public class LoginPage
    {
        public LoginPage(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = ("user-name")) ]
        [CacheLookup]
        IWebElement Username;
        [FindsBy(How = How.Id, Using = ("password"))]
        [CacheLookup]
        IWebElement Password;
        [FindsBy(How = How.Id, Using = ("login-button"))]
        [CacheLookup]
        IWebElement LoginButton;


        public void SubmitLoginForm(string username, string pass)
        {
            Username.SendKeys(username);
            Password.SendKeys(pass);
            LoginButton.Click();
        }

        public void SubmitLoginFormValidCred()
        {
            Username.SendKeys("standard_user");
            Password.SendKeys("secret_sauce");
            LoginButton.Click();
        }


    }
}
