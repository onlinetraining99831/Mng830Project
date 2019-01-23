using ClassLibrary1.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.POM
{
    public class LoginPage : Basepage
    {
        IWebDriver driver; // global variable.
        Dictionary<string, string> credentials;

        public LoginPage(IWebDriver driver) : base(driver) // local variable.
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        // POM - Page Object model design 1.
        //By txt_username = By.Id("ctl00_MainContent_username");
        //By txt_password = By.Name("ctl00$MainContent$password");
        //By btn_login = By.Id("ctl00_MainContent_login_button");

        [FindsBy(How = How.Id, Using = "ctl00_MainContent_username")]
        IWebElement txt_username;

        [FindsBy(How = How.Name, Using = "ctl00$MainContent$password")]
        IWebElement txt_password;

        [FindsBy(How = How.Id, Using = "ctl00_MainContent_login_button")]
        IWebElement btn_login;


        // Two parts -> 1) will contain locators / properties 
        // 2) methods to interact with those locators.

        //public void login(string uname, string pwd)
        //{
        //    driver.FindElement(txt_username).SendKeys(uname);
        //    driver.FindElement(txt_password).SendKeys(pwd);
        //    driver.FindElement(btn_login).Click();
        //}

        public void login(string uname,string pwd)
        {
            txt_username.SendKeys(uname);
            txt_password.SendKeys(pwd);
            btn_login.Click();
        }

        public void loginas(string typeofuser)
        {
           credentials =xmlreaderutitlity.getcredentials("login", typeofuser);
           login(credentials["username"], credentials["password"]);
        }

    }
}
