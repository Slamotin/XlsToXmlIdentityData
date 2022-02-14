using System;
using OfficeOpenXml;
using System.Xml;

namespace XlsToXmlIdentityData
{
    class Program
    {
        static void Main(string[] args)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var package = new ExcelPackage(new System.IO.FileInfo(@"Identity.xlsx"));
            var worksheets = package.Workbook.Worksheets;
            var worksheet = worksheets[0];
            var lastRow = worksheet.Dimension.End.Row;
            PersonIdentity[] person = new PersonIdentity[lastRow - 1];

            for (int curRow = 1; curRow < lastRow; curRow++)
            {
                try
                {
                    string FIO = worksheet.Cells[curRow + 1, 1].Value.ToString();
                    string birthDate = worksheet.Cells[curRow + 1, 5].Value.ToString();
                    person[curRow - 1] = new PersonIdentity(FIO, birthDate);
                }
                catch(NullReferenceException ex)
                {

                    Console.WriteLine("No birthdate in " + curRow + " row");
                }
                
                
            }

            XmlDocument xmlDocument = new XmlDocument();
            //XmlElement peoples = new XmlElement("peoples");



            xmlDocument.Save("example.xml");




            Console.ReadKey();
        }
    }
}
