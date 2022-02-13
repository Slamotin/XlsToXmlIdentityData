using System;
using OfficeOpenXml;

namespace XlsToXmlIdentityData
{
    class Program
    {
        static void Main(string[] args)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var package = new ExcelPackage(new System.IO.FileInfo(@"C:\Users\Admi\source\repos\XlsToXmlIdentityData\XlsToXmlIdentityData\Identity.xlsx"));
            var worksheets = package.Workbook.Worksheets;
            var worksheet = worksheets[0];
            var lastRow = worksheet.Dimension.End.Row;
            PersonIdentity[] person = new PersonIdentity[lastRow - 1];

            for (int counter = 1; counter < lastRow; counter++)
            {
                try
                {
                    string FIO = worksheet.Cells[counter + 1, 1].Value.ToString();
                    string birthDate = worksheet.Cells[counter + 1, 5].Value.ToString();
                    person[counter-1] = new PersonIdentity(FIO, birthDate);
                }
                catch(NullReferenceException ex)
                {
                    Console.WriteLine("No birthdate in " + counter + " row");
                }
                
                
            }
            Console.ReadKey();
        }
    }
}
