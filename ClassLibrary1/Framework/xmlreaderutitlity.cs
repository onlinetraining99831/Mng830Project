using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ClassLibrary1.Framework
{
    public class xmlreaderutitlity
    {
        static string projectdllpath = Assembly.GetExecutingAssembly().Location;
        static string projectpath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(projectdllpath)));

        public static Dictionary<string,string> getcredentials(string filename, string usertype)
        {
            Dictionary<string, string> credentials = new Dictionary<string, string>();
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(projectpath + "//Credentials//" + filename + ".xml");

            foreach (XmlNode node in xmldoc.DocumentElement)
            {
                if(node.Attributes["type"].InnerText.Equals(usertype))
                {
                    foreach(XmlNode child in node.ChildNodes)
                    {
                        if(child.Name.Equals("username"))
                        {
                            //Console.WriteLine(child.InnerText);
                            credentials.Add("username", child.InnerText);
                        }
                        if(child.Name.Equals("password"))
                        {
                            //Console.WriteLine(child.InnerText);
                            credentials.Add("password", child.InnerText);
                        }
                    }
                    break;
                }
            }
            return credentials;
        }


    }
}
