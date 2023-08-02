using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwagLabs.PageObjects
{
    class ShoppingCart
    {
        public ShoppingCart(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = ("remove-sauce-labs-backpack"))]
        [CacheLookup]
        IWebElement RemoveBtnCart;
        [FindsBy(How = How.Id, Using = ("continue-shopping"))]
        [CacheLookup]
        IWebElement ContinueShoppingBtn;
        [FindsBy(How = How.Id, Using = ("checkout"))]
        [CacheLookup]
        IWebElement CheckoutBtn;
        [FindsBy(How = How.ClassName, Using = ("inventory_item_name"))]
        [CacheLookup]
        IWebElement ProductTitle;

        public void RemoveItemCart()
        {
            RemoveBtnCart.Click();
        }
        public void BackToProducts()
        {
            ContinueShoppingBtn.Click();
        }

        public void OpenItemDetailsFromCart()
        {
            ProductTitle.Click();
           
        }

        public void StartCheckout()
        {
            CheckoutBtn.Click();

        }
    }
}
