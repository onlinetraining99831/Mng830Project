using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ClassLibrary1.POM;
using ClassLibrary1.Framework;
using System.Reflection;
using System.IO;
using AventStack.ExtentReports;
using OpenQA.Selenium;

namespace ClassLibrary1.Tests
{
    [TestFixture]  // Optional if we are using Nunit above 2.5 version.
    public class smoketest : reports
    {

        // [SetUp]
        [OneTimeSetUp]
        public void initialize()
        {
            // Console.WriteLine("Initialize the test....");
            // Page.Loginpage.login("Tester", "test");
            //Console.WriteLine(Assembly.GetExecutingAssembly().Location);
            ////Assembly.GetExecutingAssembly().Location -> Gets the complete
            //// path of the project dll location (path)
            //string projectdllpath = Assembly.GetExecutingAssembly().Location;
            //string projectpath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(projectdllpath)));
            //Console.WriteLine(projectpath);

            Page.Loginpage.loginas("admin");
            extentreport.AttachReporter(extenthtmlreporter);

            
        }

        [Test,Description("verify view all products page is loaded correctly or not.")]
        public void verify_all_products_page()
        {
            extenttest = extentreport.CreateTest(TestContext.CurrentContext.Test.Name.ToString());
            Page.Homepage.click_view_all_orders();
            Assert.IsFalse(Page.Homepage.isexpectedtextvisible("List of Products"),"All products page is not loaded correctly.");
        }

        //[Test,Description("verify user is able to insert a new order.")]
        //[TestCase("10","Nathan","KKPT","Hyderabad","Telangana","500500","1234432112344321","10/20")]
        //[TestCase("20", "James", "KKPT", "Hyderabad", "Telangana", "500501", "1234432112344321", "10/20")]

        [Test,TestCaseSource("getdata")]
        public void verify_inserting_new_order(string qty, string customer, string stname, string city,
                                 string state, string zip, string card, string expiry)
        {
            extenttest  = extentreport.CreateTest(TestContext.CurrentContext.Test.MethodName.ToString());
            Page.Homepage.click_orders();
            Assert.IsTrue(Page.Orderspage.isexpectedtextvisible("Payment Information"),
                "Orders page is not loaded correctly");
            Page.Orderspage.enterdetails(qty, customer, stname, city, state, zip, card, expiry);
            Page.Orderspage.click_Processbtn();
            Assert.IsTrue(Page.Orderspage.isorderprocessedcorrectly(),"Order is not processed correctly");
        }

        [Test]
        public void verifyregister()
        {
            extenttest = extentreport.CreateTest(TestContext.CurrentContext.Test.MethodName.ToString());
            Dictionary<string,string> testdata =ExcelReader.getdata(TestContext.CurrentContext.Test.MethodName.ToString());
            Page.Homepage.click_orders();
            extenttest.Log(Status.Info, "Orders page is loaded");
            Page.Orderspage.enterdetails(testdata["quantity"], testdata["custname"], testdata["street"], testdata["city"],
                 testdata["state"],testdata["zipcode"], testdata["cardno"], testdata["expdate"]);
            extenttest.Log(Status.Info, "Passed test data to the application..");
            Page.Orderspage.click_Processbtn();
            Assert.IsTrue(Page.Orderspage.isorderprocessedcorrectly(), "Order is not processed correctly");
            extenttest.Log(Status.Info, "Order is inserted");
            Page.Homepage.click_view_all_orders();
            //Page.Orderspage.getallcolnames();
            Assert.IsTrue(Page.Orderspage.iscustomerinthetable(testdata["custname"]));
            extenttest.Log(Status.Pass, testdata["custname"] + " - Customer is added in the table");
        }


        public static object[][] getdata()
        {
            // We have to declare no. of rows in jagged array.. no. of columns will be 1
            // no. of rows indicate how many times, we are passing the data.
            object[][] data = new object[2][];

            data[0] = new object[8] { "10", "Nathan", "KKPT", "Hyderabad", "Telangana", "500500", "1234432112344321", "10/20" };
            data[1] = new object[8] { "20", "James", "KKPT", "Hyderabad", "Telangana", "500501", "1234432112344321", "10/20" };

            return data;
        }

        [TearDown]
        public void testcasestatus()
        {
            string testname = TestContext.CurrentContext.Test.MethodName.ToString();
            string testcasestatus = TestContext.CurrentContext.Result.Outcome.ToString();
            Console.WriteLine(testname+"--"+ testcasestatus);
            switch (testcasestatus)
            {
                case "Passed":
                  extenttest.Pass(testname);
                break;
                case "Failed:Error":
                    var message = TestContext.CurrentContext.Result.Message;
                    var stacktrace = TestContext.CurrentContext.Result.StackTrace.ToString();
                    var screenshot = ((ITakesScreenshot)Driver.getdriver()).GetScreenshot();
                    screenshot.SaveAsFile(@"D:\temp\automation\" + TestContext.CurrentContext.Test.MethodName.ToString());
                    System.Threading.Thread.Sleep(3000);
                    extenttest.AddScreenCaptureFromPath(@"D:\temp\automation\" + TestContext.CurrentContext.Test.MethodName.ToString());
                    extenttest.Log(Status.Error, message);
                    extenttest.Fail(stacktrace);
                    break;

                case "Failed":
                    var message1 = TestContext.CurrentContext.Result.Message;
                    var stracktrace1=TestContext.CurrentContext.Result.StackTrace.ToString();
                    var failedreport = extentreport.CreateTest(testname, stracktrace1);
                    extenttest.Log(Status.Error, message1);
                    failedreport.Error(testname);
                    break;
                 default:
                    break;
            }
            extentreport.Flush();
        }

       // [TearDown]
        [OneTimeTearDown]         
        public void closebrowser()
        {
            Page.Homepage.click_logout();
            Driver.close_browser();

            
        }
        
    }
}
