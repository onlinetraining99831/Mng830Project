using ClassLibrary1.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.POM
{
    public class OrdersPage : Basepage
    {
        IWebDriver driver; // global variable.

        public OrdersPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "ctl00_MainContent_fmwOrder_txtQuantity")]
        private IWebElement _txtquantity;

        [FindsBy(How = How.Id, Using = "ctl00_MainContent_fmwOrder_txtName")]
        private IWebElement _txtcustname;

        [FindsBy(How = How.Id, Using = "ctl00_MainContent_fmwOrder_TextBox2")]
        private IWebElement _txtstreet;

        [FindsBy(How = How.Id, Using = "ctl00_MainContent_fmwOrder_TextBox3")]
        private IWebElement _txtcity;

        [FindsBy(How = How.Id, Using = "ctl00_MainContent_fmwOrder_TextBox4")]
        private IWebElement _txtstate;

        [FindsBy(How = How.Id, Using = "ctl00_MainContent_fmwOrder_TextBox5")]
        private IWebElement _txtzip;

        [FindsBy(How = How.Id, Using = "ctl00_MainContent_fmwOrder_cardList_0")]
        private IWebElement _radvisa;

        [FindsBy(How = How.Id, Using = "ctl00_MainContent_fmwOrder_TextBox6")]
        private IWebElement _txtcardno;

        [FindsBy(How = How.Id, Using = "ctl00_MainContent_fmwOrder_TextBox1")]
        private IWebElement _txtexpiry;

        [FindsBy(How = How.Id, Using = "ctl00_MainContent_fmwOrder_InsertButton")]
        private IWebElement _btnprocess;

        [FindsBy(How = How.Id, Using = "ctl00_MainContent_orderGrid")]
        private IWebElement _tblorders;

        By msg = By.XPath("//strong[contains(text(),'New order has been successfully added')]");
        //****************************************************************************

        public void enterdetails(string qty, string customer, string stname, string city,
                                 string state, string zip, string card, string expiry)
        {
            _txtquantity.SendKeys(qty);
            _txtcustname.SendKeys(customer);
            _txtstreet.SendKeys(stname);
            _txtcity.SendKeys(city);
            _txtstate.SendKeys(state);
            _txtzip.SendKeys(zip);
            _radvisa.Click();
            _txtcardno.SendKeys(card);
            _txtexpiry.SendKeys(expiry);

        }
        public void click_Processbtn()
        {
            _btnprocess.Click();
           // waitfor(msg, 30);
        }
        public bool isorderprocessedcorrectly()
        {
            return isexpectedtextvisible("New order has been successfully added.");
        }

        public ArrayList getallcolnames()
        {
            IReadOnlyCollection<IWebElement> allcols = 
                driver.FindElements(By.XPath("//table[@id='ctl00_MainContent_orderGrid']/tbody/tr[1]/child::th"));
            ArrayList colnames = new ArrayList();
            Console.WriteLine("No. of cols are " + allcols.Count);

            for(int i=0; i<allcols.Count; i++)
            {
                Console.WriteLine(allcols.ElementAt<IWebElement>(i).Text);
            }
            return colnames;
        }

        public ArrayList getcoldata(string colname)
        {
            ArrayList entirecoldata = new ArrayList();
            IReadOnlyCollection<IWebElement> allrows =
                 driver.FindElements(By.XPath(".//*[@id='ctl00_MainContent_orderGrid']/tbody/child::tr"));
            int noofrows = allrows.Count - 1;  //excluding headers
            int colno = 2;
            ArrayList colnames = getallcolnames();
            for (int i = 0; i < colnames.Count; i++)
            {
                if (colnames[i].Equals(colname))
                {
                    Console.WriteLine("Col no. for " + colname + " is " + colno);
                    break;
                }
                colno++;
            }

            for (int i = 0; i < noofrows; i++)
            {
                IReadOnlyCollection<IWebElement> coldata =
                   driver.FindElements(By.XPath(".//*[@id='ctl00_MainContent_orderGrid']/tbody/tr/td[" + colno + "]"));
                Console.WriteLine(coldata.ElementAt<IWebElement>(i).Text);
                entirecoldata.Add(coldata.ElementAt<IWebElement>(i).Text);
            }
            return entirecoldata;
        }

        public bool iscustomerinthetable(string name)
        {
            ArrayList data = getcoldata("Name");
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].Equals(name))
                {
                    return true;
                }
            }
            return false;
        }



    }
}
