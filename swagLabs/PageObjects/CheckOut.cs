using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;


namespace SwagLabs.PageObjects
{
    public class CheckOut
    {
        public CheckOut(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = ("first-name")) ]
        [CacheLookup]
        IWebElement FirstName;
        [FindsBy(How = How.Id, Using = ("last-name"))]
        [CacheLookup]
        IWebElement LastName;
        [FindsBy(How = How.Id, Using = ("postal-code"))]
        [CacheLookup]
        IWebElement PostalCode;
        [FindsBy(How = How.Id, Using = ("cancel"))]
        [CacheLookup]
        IWebElement CancelBtn;
        [FindsBy(How = How.Id, Using = ("continue"))]
        [CacheLookup]
        IWebElement ContinueBtn;
        [FindsBy(How = How.Id, Using = ("finish"))]
        [CacheLookup]
        IWebElement FinishButton;
        [FindsBy(How = How.Id, Using = ("back-to-products"))]
        [CacheLookup]
        IWebElement BackHomeBtn;

        public void EnterBuyerDetails(string username, string pass,string zip)
        {
            FirstName.SendKeys(username);
            LastName.SendKeys(pass);
            PostalCode.SendKeys(zip);
            ContinueBtn.Click();
        }

        public void CancelOrder()
        {
            
            CancelBtn.Click();
        }

        public void PlaceOrder()
        {

            FinishButton.Click();
        }

        public void BackHomePlacedOrder()
        {

            BackHomeBtn.Click();
        }
    }
}
