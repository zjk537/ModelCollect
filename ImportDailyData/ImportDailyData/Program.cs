using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using log4net;
//using System.Threading;
using System.Text.RegularExpressions;
using System.Web;
using System.Timers;

namespace ImportDailyData
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger("ImportDailData");
        static void Main(string[] args)
        {

            var periodRange = ConfigurationManager.AppSettings["PeriodRange"];
            if (string.IsNullOrEmpty(periodRange)) // 导入当天的数据
            {
                ImportData(DateTime.Now);
            }
            else
            {
                var periodRangeArr = periodRange.Split(',');
                for (var i = 0; i < periodRangeArr.Length; i++)
                {
                    var rangeStr = periodRangeArr[i];
                    if (rangeStr.Contains("~")) //导入某段时间的数据
                    {
                        ImPortDateRangeData(rangeStr);
                    }
                    else // 导入指定某天的数据
                    {
                        var curDate = Convert.ToDateTime(rangeStr);
                        ImportData(curDate);
                    }
                }
            }
            Console.WriteLine("导入数据结束！5秒后 程序自动退出。");
            //Console.ReadLine();
            System.Threading.Thread.Sleep(5 * 1000);
        }

        static void ImPortDateRangeData(string rangeStr)
        {
            var dateRange = rangeStr.Split('~');
            var startDate = Convert.ToDateTime(dateRange[0]);
            var endDate = Convert.ToDateTime(dateRange[1]);
            int i = 0;
            var curDate = startDate;
            while (curDate <= endDate)
            {
                ImportData(curDate);
                i++;
                curDate = startDate.AddDays(i);
            }
        }

        static void ImportData(DateTime curDate)
        {
            string dateStr = curDate.ToString("yyyyMMdd");
            log.Info("开始导入数据！日期：" + dateStr);
            Console.WriteLine("开始导入数据：" + dateStr);
            var dw = curDate.DayOfWeek;
            if (dw == DayOfWeek.Saturday || dw == DayOfWeek.Sunday)
            {
                log.Info(string.Format("日期{1}, {0}不执行。", dateStr, dw.ToString()));
                Console.WriteLine(string.Format("日期{1}, {0}不执行。", dateStr, dw.ToString()));
            }
            else
            {
                string xmlPath = string.Format(ConfigurationManager.AppSettings["XMLUrl"], dateStr);
                //NewspaperMP mp = XmlManager.ReadXmlToModel(xmlPath, log);
                NewspaperMP mp = GetNewsPaperMP(curDate, xmlPath);
                SaveMPToDB(mp);
                AddNewsEvent(dateStr);
            }
            log.Info("导入数据结束！日期：" + dateStr);
            if (checkUpdateTimer != null)
            {
                checkUpdateTimer.Dispose();
                Console.WriteLine("导入数据结束！5秒后 程序自动退出。");
                //Console.ReadLine();
                System.Threading.Thread.Sleep(5 * 1000);
                Environment.Exit(0);
            }
        }

        #region GetNewsPaperMP
        static NewspaperMP GetNewsPaperMP(DateTime curDate, string xmlPath)
        {
            try
            {
                if (checkUpdateTimer != null && DateTime.Now.Hour > 20)
                {
                    checkUpdateTimer.Dispose();
                    log.Info("已过晚8点,不在等待数据！" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                    Console.WriteLine("已过晚8点,不在等待数据,5秒后 程序自动退出。");
                    System.Threading.Thread.Sleep(5 * 1000);
                    Environment.Exit(0);
                }
                else
                {
                    NewspaperMP mp = XmlManager.ReadXmlToModel(xmlPath, log);

                    return mp;
                }

            }
            catch (Exception e)
            {
                log.Error("读出Xml时出错：" + e);
                if (e.Message.Contains("404"))
                {
                    log.Info(string.Format("Xml 文档未生成，下次重试：{0}", DateTime.Now.AddMinutes(30).ToString("yyyy-MM-dd hh:mm:ss")));
                    Console.WriteLine(string.Format("Ipad数据未生成,请勿关闭窗口,下次重试：{0}", DateTime.Now.AddMinutes(30).ToString("yyyy-MM-dd hh:mm:ss")));
                    GetTimerStart(curDate);
                    Console.ReadLine();
                }
            }
            return null;
        }

        static Timer checkUpdateTimer = null; //定时器
        static void GetTimerStart(DateTime curDate)
        {
            if (checkUpdateTimer == null)
            {
                checkUpdateTimer = new Timer();
                checkUpdateTimer.Interval = 1000 * 60 * 30; // 30分钟
                checkUpdateTimer.Enabled = true;// 允许Timer执行
                checkUpdateTimer.AutoReset = true;// 多次循环
                checkUpdateTimer.Elapsed += new ElapsedEventHandler((s, e) => checkUpdateTimer_Elapsed(s, e, curDate));
            }
        }

        static void checkUpdateTimer_Elapsed(object sender, ElapsedEventArgs e, DateTime curDate)
        {
            ImportData(curDate);
        }
        #endregion





        static void AddNewsEvent(string dateStr)
        {
            try
            {
                log.Info("同步报纸期次.当前期次：" + dateStr);
                string sqlExit = "select count(0) from CMS_Pubs_News_Events where EBookEvents = @EBookEvents";
                SqlParameter paramExit = new SqlParameter("@EBookEvents", dateStr);
                bool isExit = Convert.ToInt32(SqlHelper.ExecuteScalarText(sqlExit, paramExit)) > 0;
                if (isExit)
                {
                    return;
                }
                string sql = "insert into CMS_Pubs_News_Events(EBookEvents,IsPub) values(@EBookEvents,0);";
                SqlParameter param = new SqlParameter("@EBookEvents", dateStr);
                SqlHelper.ExecteNonQueryText(sql, param);
            }
            catch (Exception e)
            {
                log.Info("同步报纸期次出错：" + e.Message);
                Console.WriteLine("同步报纸期次出错：" + e.Message);
            }
        }
        static void SaveMPToDB(NewspaperMP mp)
        {
            var connection1 = System.Configuration.ConfigurationManager.ConnectionStrings["conStr"];
            var connection2 = System.Configuration.ConfigurationManager.ConnectionStrings["conStr2"];

            string connectionString = connection1 == null ? string.Empty : connection1.ToString().Trim();
            string connectionString2 = connection2 == null ? string.Empty : connection2.ToString().Trim();

            if (mp == null || mp.Newspapers.Count == 0)
            {
                log.Info("XML文件为空或不包含任何一个版面！");
                Console.WriteLine("XML文件为空或不包含任何一个版面！");
                return;
            }
            log.Info("开始导入数据, 版面数：" + mp.Newspapers.Count);
            Console.WriteLine("开始导入数据, 版面数：" + mp.Newspapers.Count);

            foreach (var newspaper in mp.Newspapers)
            {
                SaveNewsPaper(connectionString, newspaper);
                SaveNewsPaper2(connectionString2, newspaper);
            }

        }

        static void SaveNewsPaper(string connectionString, Newspaper newsPaper)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                return;
            }
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@NewsType",1),// 新闻类型：1=文字新闻，2=图集，3=视频，5=文字新闻跳链，6=图集跳链，7=视频跳链，8=专题跳链，9=外链
                    new SqlParameter("@NewsTitle",newsPaper.ArticleTitle),//新闻标题
                    new SqlParameter("@TerminalID",4),//产品端编号：1=一财新媒体APP，4=日报IPAD
                    new SqlParameter("@ChannelRoot",GetChannelRootId(newsPaper.Edition)),//产品端栏目组编号  （如116	A叠）
                    new SqlParameter("@ChannelID",GetSubChannelId(newsPaper.Edition)),//产品端子栏目编号   （如119	A01_第A01版：头版）
                    new SqlParameter("@AdminID", Convert.ToInt32(ConfigurationManager.AppSettings["AdminId"])),//发布新闻的编辑ID：10197=胡志新
                    new SqlParameter("@AdminName",ConfigurationManager.AppSettings["AdminName"]),//发布新闻的编辑姓名：还是用胡志新吧
                    new SqlParameter("@Categorys",""),//新闻领域，比如“钢铁、房地产、银行”等
                    new SqlParameter("@SourceName",newsPaper.Name),//来源媒体名称
                    new SqlParameter("@ExpertName",newsPaper.Author),//专栏作家名称
                    new SqlParameter("@ProgramName",""),//细分的节目名称，如“谈股论金”，为了方便以后按节目汇总调取视频用
                    new SqlParameter("@NewsWeight",System.Data.SqlDbType.Int,4),//权重
                    new SqlParameter("@NewsLength", GetBodyLength(newsPaper.Content)),//新闻长度，文字新闻=字数，图集=图片张数，视频=时长单位秒
                    new SqlParameter("@Tags","") ,//新闻特殊标签，如APP用到的“独家”特殊字样
                    new SqlParameter("@NewsSigns",newsPaper.Author),//新闻署名
                    new SqlParameter("@NewsNotes",GetNewsNote(newsPaper.Content)),//编者按
                    new SqlParameter("@NewsThumb",GetNewsThumb(newsPaper.Materials,newsPaper.PeriodDate.ToString("yyyyMMdd"))),//新闻封面图
                    new SqlParameter("@ThumbNotes",GetThumbNotes(newsPaper.Materials)),//新闻封面图图说，就是封面图配的文字
                    new SqlParameter("@NewsProvince",-1),//省份，默认 -1=未选择
                    new SqlParameter("@StockCode",""),//股票代码，如“600123”
                    new SqlParameter("@Keywords",""),//关键字，如“我擦 牛叉”，用空格分割每个关键词
                    new SqlParameter("@NewsBody",removeEmptyStr(newsPaper.Content)),//新闻正文：如果是文字新闻 = 新闻正文   如果是图集 = 图集内图片json  如果是视频 = 视频播放地址
                    new SqlParameter("@NewsParaStr",""),//备用字段，现在没用
                    new SqlParameter("@NewsParaInt",""),//备用字段，现在没用
                    new SqlParameter("@OuterURL",""),//外链地址，必须以http:// 开头，如果新闻有外链地址，那么在页面或APP打开新闻时会直接打开该地址访问
                    new SqlParameter("@PushState",System.Data.SqlDbType.Int,4),//推送状态，暂时没用，默认=0即可
                    new SqlParameter("@IsPub",newsPaper.IsPublish == 2),//是否发布状态，即是否能被产品接口获取到
                    new SqlParameter("@PartnerID",100),//100=第一财经
                    new SqlParameter("@EBookEvents",newsPaper.PeriodDate.ToString("yyyyMMdd")),//期次
                    new SqlParameter("@EBookParas",newsPaper.ArticleTitle.Contains("导读") ? "1" : "0")//电子报类型0:原始状态 1 导读
                };
                sqlParams[11].Value = 0;
                sqlParams[25].Value = 0;

                SqlHelper.ExecteNonQuery(connectionString, System.Data.CommandType.StoredProcedure, "Pub_News_ImportNews", sqlParams);
                //SqlHelper.ExecteNonQueryProducts("Pub_News_ImportNews", sqlParams);
            }
            catch (Exception e)
            {
                string dataSource = connectionString.Split(';')[0];
                log.Error(string.Format("服务器：{0} 导入版面：{1} 时出错：{2}", dataSource, newsPaper.Edition, e.ToString()));
                Console.WriteLine(string.Format("服务器：{0} 导入版面：{1} 时出错：{2}", dataSource, newsPaper.Edition, e.ToString()));
            }
        }

        static string GetNewsNote(string newsBody)
        {
            if (string.IsNullOrEmpty(newsBody))
            {
                return "";
            }

            // 定义正则表达式用来匹配 img 标签 
            Regex regImg = new Regex(@"<p>(.*?)</p>", RegexOptions.IgnoreCase);

            MatchCollection mc = regImg.Matches(newsBody); //设定要查找的字符串
            if (mc.Count == 0)
            {
                var newString = removeEmptyStr(newsBody);
                if (string.IsNullOrEmpty(newString))
                {
                    return "";
                }
                newString = newString.Substring(0, newString.Length > 100 ? 99 : (newString.Length - 1));
                return newString;

            }
            string newsNote = mc[0].Groups[1].Value;
            if (mc.Count > 2)
            {
                newsNote += "<BR/>";
                newsNote += mc[1].Groups[1].Value;
            }

            return removeEmptyStr(newsNote);
        }

        static string removeEmptyStr(string oldString)
        {
            var newStr = Regex.Replace(oldString, @"\s", "");
            newStr = newStr.Replace("&nbsp;", "");
            return newStr;
        }

        static int GetChannelRootId(string channelRoot)
        {
            if (channelRoot.ToUpper().StartsWith("A"))
            {
                return 116; // A叠
            }
            if (channelRoot.ToUpper().StartsWith("B"))
            {
                return 117; // B叠
            }
            return 134; // Not used
        }

        static int GetSubChannelId(string channelRoot)
        {
            switch (channelRoot.ToUpper())
            {
                case "A01":
                case "A02":
                case "A03":
                case "A04":
                    return 119;
                case "A05":
                case "A06":
                case "A07":
                case "A08":
                    return 120;
                case "A09":
                case "A10":
                case "A11":
                case "A12":
                    return 121;
                case "A13":
                case "A14":
                case "A15":
                case "A16":
                    return 122;
                case "B01":
                case "B02":
                case "B03":
                case "B04":
                    return 135;
                case "B05":
                case "B06":
                case "B07":
                case "B08":

                case "C01":
                case "C02":
                case "C03":
                case "C04":

                case "C05":
                case "C06":
                case "C07":
                case "C08":

                case "C09":
                case "C10":
                case "C11":
                case "C12":

                case "C13":
                case "C14":
                case "C15":
                case "C16":
                    return 137;
                case "D01":
                case "D02":
                case "D03":
                case "D04":

                case "D05":
                case "D06":
                case "D07":
                case "D08":

                case "D09":
                case "D10":
                case "D11":
                case "D12":

                case "D13":
                case "D14":
                case "D15":
                case "D16":
                    return 163;
                default: return -1;
            }
        }

        static string GetNewsThumb(List<Material> materials, string dateStr)
        {
            if (materials == null || materials.Count == 0)
            {
                return "";
            }
            // 定义正则表达式用来匹配 img 标签 
            Regex regImg = new Regex(@"<img.*?src=""(?<src>[^""]*)""[^>]*>", RegexOptions.IgnoreCase);

            MatchCollection mc = regImg.Matches(materials[0].PicContent); //设定要查找的字符串
            string imgValue = mc[0].Groups["src"].Value;
            return string.Format("http://appdeveloper.yicai.com/xml/dycjrb/{0}/{1}", dateStr, imgValue);
        }

        static string GetThumbNotes(List<Material> materials)
        {
            if (materials == null || materials.Count == 0)
            {
                return "";
            }
            return materials[0].Description;
        }

        static int GetBodyLength(string strBody)
        {
            string body = Regex.Replace(strBody, @"<[\s\S]*?>", "", RegexOptions.IgnoreCase);
            body = HttpUtility.HtmlDecode(body);
            var bodyLength = Regex.Matches(body, @"(?i)([a-z-])+").Count + Regex.Matches(body, @"[\u4e00-\u9fa5]").Count + Regex.Matches(body, @"([\d])+").Count + Regex.Matches(body, @"[，。：、“”！,:!]").Count;
            return bodyLength;
        }


        static void SaveNewsPaper2(string connectionString, Newspaper newsPaper)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                return;
            }
            if (newsPaper.ArticleTitle.StartsWith("AD.", true, System.Globalization.CultureInfo.CurrentCulture))
            {
                log.Error("广告新闻不导入：" + newsPaper.ArticleTitle);
                return;
            }

            try
            {
                Regex regex = new Regex(@"<br\s*\/?>", RegexOptions.IgnoreCase);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@ArticleTitle",regex.Replace(newsPaper.ArticleTitle,"")),//新闻标题
                    new SqlParameter("@ReporterName",newsPaper.Author),//专栏作家名称
                    new SqlParameter("@SourceName",newsPaper.Name),// 来源
                    new SqlParameter("@ArticleThumb",GetNewsThumb(newsPaper.Materials,newsPaper.PeriodDate.ToString("yyyyMMdd"))),//新闻封面图
                    new SqlParameter("@ArticleLength",GetBodyLength(newsPaper.Content)),//新闻长度，文字新闻=字数，图集=图片张数，视频=时长单位秒
                    new SqlParameter("@ArticleNotes",GetNewsNote(newsPaper.Content)),//编者按
                    new SqlParameter("@ArticleBody",removeEmptyStr(newsPaper.Content)),//新闻正文：如果是文字新闻 = 新闻正文   如果是图集 = 图集内图片json  如果是视频 = 视频播放地址
                };

                SqlHelper.ExecteNonQuery(connectionString, System.Data.CommandType.StoredProcedure, "Store_Articles_CreateImport", sqlParams);
            }
            catch (Exception e)
            {
                string dataSource = connectionString.Split(';')[0];
                log.Error(string.Format("服务器：{0} 导入版面：{1} 时出错：{2}", dataSource, newsPaper.Edition, e.ToString()));
                Console.WriteLine(string.Format("服务器：{0} 导入版面：{1} 时出错：{2}", dataSource, newsPaper.Edition, e.ToString()));
            }
        }

    }
}
