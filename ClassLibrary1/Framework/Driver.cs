using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Framework
{
    public class Driver
    {
        private static IWebDriver _driver;

        public static IWebDriver getdriver()
        {
            if(_driver==null)
            {
                initiatebrowser(ConfigurationManager.AppSettings["browser"].ToString());
                launchapp();
            }
            return _driver;
        }

        public static void initiatebrowser(string browsertype)
        {
            switch (browsertype)
            {
                case "chrome":
                    _driver = new ChromeDriver(@"c:\users\koplu\documents\visual studio 2015\Projects\Mng830Project\ClassLibrary1\Driverfiles");
                    _driver.Manage().Window.Maximize();
                    break;
                case "firefox":
                    FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(@"c:\users\koplu\documents\visual studio 2015\Projects\Mng830Project\ClassLibrary1\Driverfiles");
                    service.FirefoxBinaryPath = @"C:\Program Files\Mozilla Firefox\firefox.exe";
                    _driver = new FirefoxDriver(service);
                    _driver.Manage().Window.Maximize();
                    break;
                default:
                    throw new Exception(browsertype + " is not supported yet, execution terminated");
                   
            }
        }

        public static string getappurl()
        {
            return ConfigurationManager.AppSettings["appurl"].ToString();
        }

        public static void launchapp()
        {
            _driver.Navigate().GoToUrl(getappurl());
        }

    }
}
