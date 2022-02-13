using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XlsToXmlIdentityData
{
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
