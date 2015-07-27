using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace ConfigModel.ConfigXML
{
    /// <summary>
    /// 推荐业务
    /// </summary>
    [XmlRoot("CommendService")]
    public class CommendService
    {
        /// <summary>
        /// 业务Guid
        /// </summary>
        [XmlAttribute("Guid")]
        public string Guid { get; set; }

        /// <summary>
        /// 业务名称
        /// </summary>
        [XmlAttribute("Name")]
        public string Name { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        [XmlAttribute("Enable")]
        public bool Enable { get; set; }

        /// <summary>
        /// 业务描述
        /// </summary>
        [XmlAttribute("Description")]
        public string Description { get; set; }

        /// <summary>
        /// 业务Log图标
        /// </summary>
        [XmlAttribute("LogoUrl")]
        public string LogoUrl { get; set; }

    }
}
