using System;
using OfficeOpenXml;
using System.Xml;

namespace XlsToXmlIdentityData
{
    class Program
    {
        private static void AddChildNode(string childName, string childText, XmlElement parentNode, XmlDocument doc)
        {
            var child = doc.CreateElement(childName);
            child.InnerText = childText;
            parentNode.AppendChild(child);
        }

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
            var xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDocument.AppendChild(xmlDeclaration);

            var root = xmlDocument.CreateElement("peoples");
            foreach (PersonIdentity personIdentity in person)
            {
                try
                {
                    var node = xmlDocument.CreateElement("person");
                    var attribute = xmlDocument.CreateAttribute("FIO");
                    attribute.InnerText = personIdentity.fullName;
                    node.Attributes.Append(attribute);
                    AddChildNode("lastName", personIdentity.lastName, node, xmlDocument);
                    AddChildNode("firstName", personIdentity.firstName, node, xmlDocument);
                    AddChildNode("middleName", personIdentity.middleName, node, xmlDocument);
                    AddChildNode("birthdate", personIdentity.birthDate, node, xmlDocument);
                    root.AppendChild(node);
                }
                catch
                {
                    Console.WriteLine("null element");
                }

                
            }
            xmlDocument.AppendChild(root);


            xmlDocument.Save("example.xml");




            Console.ReadKey();
        }
    }
}
