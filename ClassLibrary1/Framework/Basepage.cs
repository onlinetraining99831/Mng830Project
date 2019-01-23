using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Framework
{
    public class Basepage
    {
        IWebDriver driver;
        public Basepage(IWebDriver driver)
        {
            this.driver = driver;
        }
        // commonly used methods in this page....

        public bool isexpectedtextvisible(string expectedtext)
        {
            if (driver.PageSource.Contains(expectedtext))
                return true;
              else
                return false;
        }

        public void waitfor(By locator, int timetowait)
        {
            WebDriverWait wait = new WebDriverWait(driver,TimeSpan.FromSeconds(timetowait));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(locator));
        }
       

    }
}
