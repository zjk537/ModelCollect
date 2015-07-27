using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportLightAd
{
    class LightNewsModel
    {
//        @ArticleTitle NVARCHAR(100) ,标题
//@ReporterName NVARCHAR(50) ,作者名
//@SourceName NVARCHAR(50) ,来源名称
//@ArticleThumb NVARCHAR(200) ,封面图，可为空
//@ArticleLength INT , 新闻长度
//@ArticleNotes NVARCHAR(500) , 编者按或叫摘要的那段文字
//@Keywords NVARCHAR(100),关键字，每个关键字用空格分割
//@CreateDate SMALLDATETIME, 创建时间
//@CreateDateInt INT, 创建时间的年月日int型，如20150203
//@ArticleBody NVARCHAR(MAX) 文章正文
        /// <summary>
        /// Id
        /// </summary>
        public string PublishId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string ArticleTitle { get; set; }

        /// <summary>
        /// 作者名
        /// </summary>
        public string ReporterName { get; set; }

        /// <summary>
        /// 来源名称
        /// </summary>
        public string SourceName { get; set; }

        /// <summary>
        /// 封面图，可为空
        /// </summary>
        public string ArticleThumb { get; set; }

        /// <summary>
        /// 新闻长度
        /// </summary>
        public int ArticleLength { get; set; }

        /// <summary>
        /// 编者按或叫摘要的那段文字
        /// </summary>
        public string ArticleNotes { get; set; }

       
        /// <summary>
        /// 关键字，每个关键字用空格分割
        /// </summary>
        public string Keywords { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 创建时间的年月日int型，如20150203
        /// </summary>
        public int CreateDateInt { get; set; }

        /// <summary>
        /// 文章正文
        /// </summary>
        public string ArticleBody { get; set; }
    }
}
