using System.Collections.Generic;
using System.Xml.Serialization;

namespace BCEdit180.AppSettings {
    public class AppSettingsXML {
        [XmlElement]
        public int Theme { get; set; }

        [XmlElement]
        public bool ShowClassListByDefault { get; set; }

        public List<string> ClassPath { get; set; }
    }
}
