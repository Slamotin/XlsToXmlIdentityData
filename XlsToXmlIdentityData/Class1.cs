using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.Xml;

namespace XlsToXmlIdentityData
{
    class ExcelReader
    {
        public ExcelReader(string filepath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var package = new ExcelPackage(new System.IO.FileInfo(@"Identity.xlsx"));
            var worksheets = package.Workbook.Worksheets;
            var worksheet = worksheets[0];
            var lastRow = worksheet.Dimension.End.Row;
            insuredPerson[] person = new insuredPerson[lastRow - 1];

            for (int curRow = 1; curRow < lastRow; curRow++)
            {
                try
                {
                    string FIO = worksheet.Cells[curRow + 1, 1].Value.ToString();
                    string birthDate = worksheet.Cells[curRow + 1, 5].Value.ToString();
                    person[curRow - 1] = new insuredPerson();
                }
                catch (NullReferenceException ex)
                {

                    Console.WriteLine("No birthdate in " + curRow + " row");
                }
            }
        }

    class PersonIdentity
    {
        public string fullName { get; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string middleName { get; set; }
        public string birthDate { get; set; }

        public PersonIdentity(string firstName, string lastName, string middleName, string birthDate)
        {
            this.fullName = getFullName();
            this.firstName = firstName;
            this.lastName = lastName;
            this.middleName = middleName;
            this.birthDate = birthDate;
        }

        public PersonIdentity(string fullName, string birthDate)
        {
            string[] Names =  getPartNames(fullName);
            this.fullName = fullName;
            this.lastName = Names[0];
            this.firstName = Names[1];
            try { 
            this.middleName = Names[2];
            }
            catch
            {
                Console.WriteLine("No middle name");
                this.middleName = "";
            }
            this.birthDate = birthDate;
        }

        private string getFullName()
        {
            return "" + this.lastName + this.firstName + this.middleName;
        }

        private string[] getPartNames(string fullName)
        {
            return fullName.Split(' ');
        }

    }

    /*class Date
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public Date() 
        {
            this.Day = Day;
            this.Month = Month;
            this.Year = Year;
        }
    }*/
}
