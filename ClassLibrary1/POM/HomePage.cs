using ClassLibrary1.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.POM
{
    public class HomePage : Basepage
    {
        IWebDriver driver; // global variable.
       
        public HomePage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }

        By lnk_viewallorders = By.LinkText("View all orders");
        By lnk_logout = By.Id("ctl00_logout");
        By lnk_orders = By.LinkText("Order");

        public void click_view_all_orders()
        {
            driver.FindElement(lnk_viewallorders).Click();
        }

        public void click_logout()
        {
            driver.FindElement(lnk_logout).Click();
        }

        public void click_orders()
        {
            driver.FindElement(lnk_orders).Click();
        }



    }
}
