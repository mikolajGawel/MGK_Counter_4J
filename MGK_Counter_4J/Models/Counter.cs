using System.Xml.Linq;

namespace MGK_Counter_4J.Models
{
    public class CounterModel
    {
        public string Name { get; set; }
        public int Value { get; set; }

        private static readonly string configPath = "counters.xml";

        public CounterModel(string name, int value = 0)
        {
            Name = name;
            Value = value;
        }

        public void Save()
        {
            if (!File.Exists(configPath))
            {
                var newXml = new XDocument(new XElement("Counters"));
                newXml.Save(configPath);
            }

            var xml = XDocument.Load(configPath);
            var counterXML = xml.Descendants("Counter")
                .FirstOrDefault(e => (string)e.Attribute("name") == Name);

            if (counterXML == null)
            {
                var newCounter = new XElement("Counter",
                    new XAttribute("name", Name),
                    new XAttribute("value", Value));
                xml.Root?.Add(newCounter);
            }
            else
            {
                counterXML.SetAttributeValue("value", Value);
            }

            xml.Save(configPath);
        }

        public void Delete()
        {
            if (!File.Exists(configPath)) return;

            var xml = XDocument.Load(configPath);
            var counterXML = xml.Descendants("Counter")
                .FirstOrDefault(e => (string)e.Attribute("name") == Name);

            if (counterXML != null)
            {
                counterXML.Remove();
                xml.Save(configPath);
            }
        }
    }
}
