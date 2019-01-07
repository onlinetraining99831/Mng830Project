using ClassLibrary1.POM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Framework
{
    public class Page
    {
        public static LoginPage Loginpage = new LoginPage(Driver.getdriver());
    }
}
