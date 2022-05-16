using System;
using OfficeOpenXml;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

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

        static void SchemaValidationEventHandler(object sender, ValidationEventArgs e)
        {
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                    Console.WriteLine("\nError: {0}", e.Message);
                    break;
                case XmlSeverityType.Warning:
                    Console.WriteLine("\nWarning: {0}", e.Message);
                    break;
            }
        }

        static void Main(string[] args)
        {
            XmlReader reader = XmlReader.Create("newfile.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(insuredPerson));
            insuredPerson persons = (insuredPerson)serializer.Deserialize(reader);

            XmlSchemaSet schemaSet = new XmlSchemaSet();
            schemaSet.Add("http://www.fss.ru/integration/types/person/v02", "examp.xsd");

            XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(reader.NameTable);

            XmlSchemaValidator xmlSchemaValidator = new XmlSchemaValidator(reader.NameTable, schemaSet, xmlNamespaceManager, XmlSchemaValidationFlags.None);
            xmlSchemaValidator.ValidationEventHandler += new ValidationEventHandler(SchemaValidationEventHandler);

            xmlSchemaValidator.Initialize();


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
