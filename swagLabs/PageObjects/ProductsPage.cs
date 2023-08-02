using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace SwagLabs.PageObjects
{
    public class ProductsPage
    {
        public ProductsPage(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = ("react-burger-menu-btn"))]
        [CacheLookup]
        IWebElement BurgerMenu;
        [FindsBy(How = How.Id, Using = ("about_sidebar_link"))]
        [CacheLookup]
        IWebElement AboutLink;
        [FindsBy(How = How.Id, Using = ("logout_sidebar_link"))]
        [CacheLookup]
        IWebElement LogoutButton;
        [FindsBy(How = How.Id, Using = ("react-burger-cross-btn"))]
        [CacheLookup]
        IWebElement CloseMenuBtn;
        [FindsBy(How = How.ClassName, Using = ("shopping_cart_link"))]
        [CacheLookup]
        IWebElement CartButton;
        [FindsBy(How = How.ClassName, Using = ("product_sort_container"))]
        [CacheLookup]
       public SelectElement SortingComp;
        [FindsBy(How = How.Id, Using = ("item_4_title_link"))]
        [CacheLookup]
        IWebElement BackPackTitle;
        [FindsBy(How = How.XPath, Using = ("//*[@id='item_4_img_link']/img"))]
        [CacheLookup]
        IWebElement BackPackImage;
        [FindsBy(How = How.Id, Using = ("add-to-cart-sauce-labs-backpack"))]
        [CacheLookup]
        IWebElement PackCart;
        [FindsBy(How = How.Id, Using = ("remove-sauce-labs-backpack"))]
        [CacheLookup]
        IWebElement PackCartRemove;
        [FindsBy(How = How.Id, Using = ("add-to-cart-sauce-labs-fleece-jacket"))]
        [CacheLookup]
        IWebElement LabsJacketCart;
        [FindsBy(How = How.ClassName, Using = ("social_twitter"))]
        [CacheLookup]
        IWebElement TwitterLink;



        public void OpenBurgerMenu()
        {
            BurgerMenu.Click();
        }

        public void LogOut()
        {
            LogoutButton.Click();
        }

        public void OpenAboutUs()
        {
            AboutLink.Click();
         }

        public void CloseMenu()
        {
            CloseMenuBtn.Click();
        }

        public void OpenCart()
        {
            CartButton.Click();
        }
        public void SelectSorting(string rule)
        {
            SortingComp.SelectByValue(rule);
        }

        public void BackPackTitleClick()
        {
            BackPackTitle.Click();
        }

        public void BackPackImageClick()
        {
            BackPackImage.Click();
        }

        public void BackPackAddToCart()
        {
            PackCart.Click();
        }

        public void JacketAddToCart()
        {
            LabsJacketCart.Click();
        }

        public void BackPackRemoveCart()
        {
            PackCartRemove.Click();
        }

        public void Twitter()
        {
            TwitterLink.Click();
        }

    }
}
