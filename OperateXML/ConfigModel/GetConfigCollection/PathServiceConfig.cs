using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;

namespace ConfigModel.GetConfigCollection
{
    public class PathServiceConfig : ConfigurationSection
    {
        [ConfigurationProperty("PathMaps")]
        public PathMapCollection PathMaps
        {
            get
            {
                return base["PathMaps"] as PathMapCollection;
            }
        }

        [ConfigurationProperty("hostAddress")]
        public string HostAddress
        {
            get
            {
                return base["hostAddress"] as string;
            }
        }

        [ConfigurationProperty("host")]
        public string Host
        {
            get { return (String)base["host"]; }
        }
    }
}
