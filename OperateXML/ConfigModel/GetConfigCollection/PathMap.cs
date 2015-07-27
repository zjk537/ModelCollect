using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;

namespace ConfigModel.GetConfigCollection
{
    public class PathMap : ConfigurationElement
    {
        /// <summary>
        /// 名称
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get
            {
                return this["name"] as string;
            }
            set
            {
                this["name"] = value;
            }
        }

        /// <summary>
        /// 源路径
        /// </summary>
        [ConfigurationProperty("source", IsRequired = true)]
        public string Source
        {
            get
            {
                return this["source"] as string;
            }
            set
            {
                this["source"] = value;
            }
        }

        /// <summary>
        /// 目标路径
        /// </summary>
        [ConfigurationProperty("destination", IsRequired = true)]
        public string Destination
        {
            get
            {
                return this["destination"] as string;
            }
            set
            {
                this["destination"] = value;
            }
        }

        public PathMap() { }

        public PathMap(string name)
        {
            this.Name = name;
        }
    }
}
