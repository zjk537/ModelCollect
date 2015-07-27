using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;

namespace ConfigModel.GetConfigCollection
{
    [ConfigurationCollection(typeof(PathMap), AddItemName = "PathMap")]
    public class PathMapCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new PathMap();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((PathMap)element).Name;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        protected override string ElementName
        {
            get
            {
                return "PathMap";
            }
        }

        public PathMap this[int index]
        {
            get
            {
                return this.BaseGet(index) as PathMap;
            }
        }
    }
}
