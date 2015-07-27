using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;

namespace ImportDailyData
{
    [XmlRoot("MP")]
    public class NewspaperMP
    {
        [XmlArray("Article"), XmlArrayItem("item")]
        public List<Newspaper> Newspapers { get; set; }

        public NewspaperMP()
        {
            Newspapers = new List<Newspaper>();
        }
    }

    [XmlRoot("item")]
    public class Newspaper
    {
        /// <summary>
        /// 报纸ID
        /// </summary>
        [XmlElement("报纸ID")]
        public int Id { get; set; }

        /// <summary>
        /// 报纸名称
        /// </summary>
        [XmlElement("报纸名称")]
        public string Name { get; set; }

        /// <summary>
        /// 报纸简称
        /// </summary>
        [XmlElement("报纸简称")]
        public string ShortName { get; set; }

        /// <summary>
        /// 报纸UID
        /// </summary>
        [XmlElement("报纸UID")]
        public string PaperUID { get; set; }

        /// <summary>
        /// 报纸版面Id
        /// </summary>
        [XmlElement("PageRangeId")]
        public int PageRangeId { get; set; }

        /// <summary>
        /// 报纸版面名称
        /// </summary>
        [XmlElement("PageRangeName")]
        public string PageRangeName { get; set; }

        /// <summary>
        /// PageNodeId
        /// </summary>
        [XmlElement("PageNodeId")]
        public int PageNodeId { get; set; }

        /// <summary>
        /// FitPageNumber
        /// </summary>
        [XmlElement("FitPageNumber")]
        public int FitPageNumber { get; set; }


        /// <summary>
        /// 期次ID
        /// </summary>
        [XmlElement("期次ID")]
        public int PeriodId { get; set; }

        /// <summary>
        /// 期次日期
        /// </summary>
        [XmlElement("日期")]
        public DateTime PeriodDate { get; set; }

        /// <summary>
        /// 期次Uid
        /// </summary>
        [XmlElement("periodUid")]
        public string PeriodUid { get; set; }

        /// <summary>
        /// 版次id
        /// </summary>
        [XmlElement("版次id")]
        public int EditionId { get; set; }

        /// <summary>
        /// 版次栏目ID
        /// </summary>
        [XmlElement("版次栏目ID")]
        public int EditionColumnId { get; set; }

        /// <summary>
        /// 版次号
        /// </summary>
        [XmlElement("版次号")]
        public string Edition { get; set; }

        /// <summary>
        /// 版名
        /// </summary>
        [XmlElement("版名")]
        public string EditionName { get; set; }

        /// <summary>
        /// 是否发布
        /// </summary>
        [XmlElement("是否发布")]
        public int IsPublish { get; set; }

        /// <summary>
        /// 版面图
        /// </summary>
        [XmlElement("版面图")]
        public string EditionImage { get; set; }

        /// <summary>
        /// pdf
        /// </summary>
        [XmlElement("pdf")]
        public string EditionPDF { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [XmlElement("articleTitle")]
        public string ArticleTitle { get; set; }

        /// <summary>
        /// 简报
        /// </summary>
        [XmlElement("brief")]
        public string Brief { get; set; }

        /// <summary>
        /// 子标题
        /// </summary>
        [XmlElement("subTitle")]
        public string SubTitle { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        [XmlElement("author")]
        public string Author { get; set; }

        /// <summary>
        /// guid
        /// </summary>
        [XmlElement("guid")]
        public string Guid { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        [XmlElement("pubDate")]
        public string PubDate { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [XmlElement("description")]
        public string Description { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [XmlElement("content")]
        public string Content { get; set; }

        /// <summary>
        /// nsid
        /// </summary>
        [XmlElement("nsid")]
        public long NSid { get; set; }

        /// <summary>
        /// CnmlId
        /// </summary>
        [XmlElement("CnmlId")]
        public string CnmlId { get; set; }

        /// <summary>
        /// 全文坐标
        /// </summary>
        [XmlElement("全文坐标")]
        public string Location { get; set; }

        /// <summary>
        /// 物料总数
        /// </summary>
        [XmlElement("materialCount")]
        public int MaterialCount { get; set; }

        [XmlArray("Materials"),XmlArrayItem("Material")]
        public List<Material> Materials { get; set; }


        public Newspaper()
        {
            Materials = new List<Material>();
        }
    }

    public class Material
    {
        /// <summary>
        /// 物料内容
        /// </summary>
        [XmlElement("PicContent")]
        public string PicContent { get; set; }

        /// <summary>
        /// 物料描述
        /// </summary>
        [XmlElement("description")]
        public string Description { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }

    }
}
