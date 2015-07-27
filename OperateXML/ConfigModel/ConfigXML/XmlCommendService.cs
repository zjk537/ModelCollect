using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace ConfigModel.ConfigXML
{
    [XmlRoot("XmlCommendService")]
    public class XmlCommendService
    {
        [XmlArray("BusinessTypes"), XmlArrayItem("BusinessType")]
        public List<BusinessType> BusinessTypes { get; set; }

        public XmlCommendService()
        {
            BusinessTypes = new List<BusinessType>();
        }
    }
}
