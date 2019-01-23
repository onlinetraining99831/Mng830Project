using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace ClassLibrary1.Framework
{
    public class ExcelReader
    {
        public static Excel.Application xlapp;
        public static Excel.Workbook xlworkbook;
        public static Excel.Worksheet xlworksheet;
        public static Excel.Range xlrange;

        public static int lastrowno;
        public static int lastcolno;

        public static string projectdllpath = Assembly.GetExecutingAssembly().Location;
        public static string projectpath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(projectdllpath)));

        // 1. Load excel file and store no. of rows and no. of columns
        public static void loadexcel(string filename)
        {
            xlapp = new Excel.Application();
            xlworkbook = xlapp.Workbooks.Open(projectpath + @"\Tests\Testdata\" + filename + @".xlsx");
            xlworksheet = xlworkbook.Sheets[1];
            xlrange = xlworksheet.UsedRange;
            lastrowno = xlrange.Rows.Count;
            lastcolno = xlrange.Columns.Count;
        }

        //2. Read excel file, store values in "Key value" pairs
        public static Dictionary<string,string> getdata(string filename)
        {
            Dictionary<string, string> dicdata = new Dictionary<string, string>();
            try
            {
                loadexcel(filename);
                lastrowno = xlrange.Rows.Count;
                lastcolno = xlrange.Columns.Count;

                for (int i = 1; i <= lastcolno; i++)
                {
                   // Console.WriteLine(xlrange.Cells[1, i].Value2.ToString() + "--" +
                   //                   xlrange.Cells[2, i].Value2.ToString());
                    dicdata.Add(xlrange.Cells[1, i].Value2.ToString(), xlrange.Cells[2, i].Value2.ToString());
                }
            }
            catch (Exception) { }
            finally
            {
                closeexcel();
            }
            return dicdata;
        }

        public static void closeexcel()
        {
            xlworkbook.Close();
            xlapp.Quit();
            System.Threading.Thread.Sleep(5000);
        }

    }
}
