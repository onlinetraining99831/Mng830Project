using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace ClassLibrary1.Framework
{
    public class reports
    {
        public static ExtentHtmlReporter extenthtmlreporter = new ExtentHtmlReporter(xmlreaderutitlity.projectpath+@"\Reports\Reports.html");
        public static ExtentReports extentreport = new ExtentReports();
        public static ExtentTest extenttest;
    }
}
