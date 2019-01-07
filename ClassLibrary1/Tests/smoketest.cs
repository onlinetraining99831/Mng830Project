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

namespace ClassLibrary1.Tests
{
    [TestFixture]  // Optional if we are using Nunit above 2.5 version.
    public class smoketest
    {
        [Test]
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

            Page.Loginpage.loginas("customer");

        }


    }
}
