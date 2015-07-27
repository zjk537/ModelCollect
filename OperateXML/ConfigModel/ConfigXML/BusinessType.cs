using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using System.Xml.Serialization;

namespace ConfigModel.ConfigXML
{
    /// <summary>
    /// 业务种类分类
    /// </summary>
    [XmlRoot("BusinessType")]
    public class BusinessType
    {
        /// <summary>
        /// 业务名称
        /// </summary>
        [XmlAttribute("Name")]
        public string Name { get; set; }
        
        /// <summary>
        /// 推荐业务集合
        /// </summary>
        [XmlArray("CommendServices"), XmlArrayItem("CommendService")]
        public List<CommendService> CommentService { get; set; }

        public BusinessType()
        {
            CommentService = new List<CommendService>();
        }
    }
}
