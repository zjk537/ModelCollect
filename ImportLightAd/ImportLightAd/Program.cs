using System;
using System.Linq;

using ImportLightAd.com.yicai.newsport;
using System.Data;
using System.Data.SqlClient;
using log4net;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using System.Configuration;
using System.Threading;

namespace ImportLightAd
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger("ImportLightAd");
        static void Main(string[] args)
        {
            var periodRange = ConfigurationManager.AppSettings["PeriodRange"];
            if (string.IsNullOrEmpty(periodRange)) // 导入当天的数据
            {
                var startTime = DateTime.Now;//.AddDays(-1);
                var endTime = startTime.AddMinutes(10);
                GetData(startTime, endTime);

            }
            else
            {
                var periodRangeArr = periodRange.Split(',');
                for (var i = 0; i < periodRangeArr.Length; i++)
                {
                    var rangeStr = periodRangeArr[i];
                    if (rangeStr.Contains("~")) //导入某段时间的数据
                    {

                        GetRangeData(rangeStr);
                    }
                    else // 导入指定某天的数据
                    {
                        var startTime = Convert.ToDateTime(rangeStr);
                        var endTime = startTime.AddDays(1).AddMinutes(-1);
                        GetData(startTime, endTime);
                    }
                }
            }

            Console.WriteLine("导入数据结束！5秒后 程序自动退出。");
            //Console.ReadLine();
            Thread.Sleep(5 * 1000);
        }

        static void GetRangeData(string rangeStr)
        {
            var dateRange = rangeStr.Split('~');
            var startDate = Convert.ToDateTime(dateRange[0]);
            var endDate = Convert.ToDateTime(dateRange[1]);
            int i = 0;
            var curDate = startDate;
            while (curDate <= endDate)
            {
                var startTime = curDate;
                var endTime = curDate.AddDays(1).AddMinutes(-1);
                GetData(startTime, endTime);
                i++;
                curDate = startDate.AddDays(i);
            }
        }

        static void GetData(DateTime startTime, DateTime endTime)
        {
            DownLoadZipFile(startTime, endTime);
            string curPath = System.AppDomain.CurrentDomain.BaseDirectory;

            string xmlFileDir = curPath + "tempData/";
            string packagePath = curPath + "tempData.zip";
            //string packagePath = string.Format(@"{0}u38{1}.zip", curPath, startTime.ToString("HHmmss"));
            //string xmlFileDir = string.Format(@"{0}u38{1}\", curPath, startTime.ToString("HHmmss"));
            if (!File.Exists(packagePath))
            {
                WriteLog("未找到文件：" + packagePath);
                return;
            }
            UnZip(packagePath, xmlFileDir);

            SaveXmlToDB(xmlFileDir);
            RemoveDownLoadFiles();
        }



        static void WriteLog(string msg)
        {
            log.Error(msg);
            Console.WriteLine(msg);
        }

        static void RemoveDownLoadFiles()
        {
            WriteLog("移除下载的文件！");
            string curPath = System.AppDomain.CurrentDomain.BaseDirectory;
            string xmlFileDir = curPath + "tempData/";
            File.Delete(curPath + "tempData.zip");
            DirectoryInfo dir = new DirectoryInfo(xmlFileDir);
            if (dir.Exists)
            {
                dir.Delete(true);
            }
        }

        static void DownLoadZipFile(DateTime startTime, DateTime endTime)
        {
            try
            {
                WriteLog(string.Format("开始下载zip文件！时间段：[{0}]--[{1}]", startTime.ToString("yyyyMMdd HH:mm:ss"), endTime.ToString("yyyyMMdd HH:mm:ss")));
                using (NewIWinService service = new NewIWinService())
                {
                    //startTime = Convert.ToDateTime("2015-03-11 16:20:32");
                    //endTime = Convert.ToDateTime("2015-03-11 16:30:32");
                    DataTable responseData = service.GetZipFileData("cbncaishang", "E91480C0", 0, startTime, endTime, "", "", "");
                    if (responseData == null || responseData.Rows.Count == 0)
                    {
                        WriteLog("接口返回数据为空！");
                        return;
                    }

                    bool isValid = Convert.ToBoolean(responseData.Rows[0]["IsValid"]);
                    var fileData = responseData.Rows[0]["FilePathData"];
                    string errorMsg = responseData.Rows[0]["ErrorMessage"].ToString();
                    if (!isValid)
                    {
                        WriteLog(string.Format("时间段：{0}---{1} 内无数据：{2}", startTime.ToString("yyyyMMdd hh:mm:ss"), endTime.ToString("yyyyMMdd hh:mm:ss"), errorMsg));
                        return;
                    }

                    string curPath = System.AppDomain.CurrentDomain.BaseDirectory;
                    byte[] fileBytes = (byte[])fileData;
                    using (FileStream fs = new FileStream(curPath + "tempData.zip", FileMode.Create))
                    //using (FileStream fs = new FileStream(string.Format(@"{0}u38{1}.zip", curPath, startTime.ToString("HHmmss")), FileMode.Create))
                    {
                        using (BinaryWriter bw = new BinaryWriter(fs))
                        {
                            bw.Write(fileBytes);
                        }
                    }
                    WriteLog("文件下载成功！" + curPath + "tempData.zip");
                    //WriteLog(string.Format(@"文件下载成功！{0}u38{1}.zip", curPath, startTime.ToString("HHmmss")));
                }
            }
            catch (Exception e)
            {
                WriteLog(e.Message);
            }
        }

        static void UnZip(string packagePath, string toPath)
        {
            WriteLog("开始解压zip文件至目录！" + toPath);
            try
            {
                using (ZipInputStream s = new ZipInputStream(File.OpenRead(packagePath)))
                {

                    ZipEntry theEntry;
                    while ((theEntry = s.GetNextEntry()) != null)
                    {

                        string directoryName = Path.GetDirectoryName(toPath);
                        string fileName = Path.GetFileName(theEntry.Name);

                        //生成解压目录
                        Directory.CreateDirectory(directoryName);

                        if (fileName != String.Empty)
                        {
                            //解压文件到指定的目录
                            using (FileStream streamWriter = File.Create(toPath + theEntry.Name))
                            {
                                int size = 2048;
                                byte[] data = new byte[2048];
                                while (true)
                                {
                                    size = s.Read(data, 0, data.Length);
                                    if (size == 0)
                                    {
                                        break;
                                    }
                                    streamWriter.Write(data, 0, size);
                                }
                            }
                        }
                    }
                }

                WriteLog("文件解压完成！");
            }
            catch (Exception ex)
            {
                WriteLog("文件解压失败：" + ex.Message);
            }

        }


        static void SaveXmlToDB(string xmlFileDir)
        {
            WriteLog("开始保存Xml to DB！");
            DirectoryInfo dir = new DirectoryInfo(xmlFileDir);
            FileInfo[] files = dir.GetFiles("*.xml");
            if (files.Count() == 0)
            {
                WriteLog(string.Format("目录：{0} 下未找到 xml文件！", xmlFileDir));
                return;
            }
            LightNewsModel model = null;
            string connString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            WriteLog(string.Format("共有文件：{0} 个需要导入!", files.Count()));
            string specialTitle;
            foreach (FileInfo file in files)
            {
                model = TranslateXmlToModel(file.FullName, out specialTitle);
                if (model == null)
                {
                    WriteLog(string.Format("屏蔽标题：{0},文件名：{1}", specialTitle, file.Name));
                }
                else
                {
                    SaveNewsPaper(connString, model);
                }
            }

            WriteLog("保存Xml to DB 成功！");
        }

        static bool IsExist(string publishId)
        {
            string sql = @"select PublishId from ArticleAd
                            where
                            PublishId = @PublishId 
                            and CreateDate = @CreateDate";
            object[] sqlParams = new object[]
                {
                   publishId,
                   DateTime.Now.ToString("yyyy-MM-dd")
                };
            var result = SQLiteHelper.Instance.ExecuteScalar(sql, sqlParams);
            if (result == null || result is DBNull)
            {
                return false;
            }
            return true;
        }

        static void SaveToArticleAd(LightNewsModel model)
        {
            string sql = @"insert into ArticleAd (
                                PublishId,
                                Title,
                                ArticleDate,
                                CreateDate
                            ) values (
                                @PublishId,
                                @Title,
                                @ArticleDate,
                                @CreateDate
                            )";
            object[] sqlParams = new object[]
                {
                   model.PublishId,
                   model.ArticleTitle,
                   model.CreateDate.ToString("yyyy-MM-dd hh:mm:ss"),
                   DateTime.Now.ToString("yyyy-MM-dd")
                };
            SQLiteHelper.Instance.ExecuteNonQuery(sql, sqlParams);
        }


        static LightNewsModel TranslateXmlToModel(string filePath, out string outValue)
        {
            StreamReader objReader = new StreamReader(filePath);
            string fileValue = objReader.ReadToEnd();
            objReader.Close();
            LightNewsModel model = new LightNewsModel();
            fileValue = fileValue.Replace("\r", "").Replace("\n", "");

            model.PublishId = GetValue(fileValue, "PublicId").Trim();

            model.ArticleTitle = GetValue(fileValue, "HeadLine").Trim();
            outValue = model.ArticleTitle;
            // 屏蔽重复标题：原则：只要标题中出现英文格式的方括号“[ ]”，该标题即舍弃不用
            if (Regex.Match(model.ArticleTitle, @"\[.*?\]").Success)
            {
                return null;
            }


            model.ReporterName = GetValue(fileValue, "Creator .*?");
            model.ReporterName = GetValue(model.ReporterName, "FullName .*?").Trim();

            model.SourceName = GetValue(fileValue, "NameTopic .*?");
            model.SourceName = GetValue(model.SourceName, "Name").Trim();

            model.ArticleThumb = GetValue(fileValue, "ContentItem xsi:type=\"AppCIType\" .*?");
            model.ArticleThumb = GetValue(model.ArticleThumb, "DataContent").Trim();

            model.ArticleNotes = GetValue(fileValue, "Abstract").Trim();
            model.Keywords = GetValue(fileValue, "Keyword").Trim();

            string createDate = GetValue(fileValue, "PublishedTime");

            model.CreateDate = Convert.ToDateTime(createDate);
            model.CreateDateInt = Convert.ToInt32(model.CreateDate.ToString("yyyyMMdd"));

            model.ArticleBody = GetValue(fileValue, "ContentItem xsi:type=\"TextCIType\" .*?");
            model.ArticleBody = GetValue(model.ArticleBody, "DataContent").Trim();

            model.ArticleLength = GetBodyLength(model.ArticleBody);
            return model;
        }



        static string GetValue(string strBody, string keyWord)
        {
            string str = string.Format(@"<{0}>(.*?)</{1}>", keyWord, keyWord.Split(' ')[0]);
            Regex regStr = new Regex(str, RegexOptions.IgnoreCase);

            var match = regStr.Matches(strBody);
            string matchValue = "";
            if (match.Count > 0)
            {
                var ma1 = match[0];
                matchValue = ma1.Groups[1].Value;
            }
            matchValue = Regex.Replace(matchValue, @"^<!\[CDATA\[", "", RegexOptions.IgnoreCase);
            matchValue = Regex.Replace(matchValue, @"\]\]>$", "", RegexOptions.IgnoreCase);

            return matchValue;

        }




        static void SaveNewsPaper(string connectionString, LightNewsModel newsModel)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                return;
            }
            try
            {
                if (IsExist(newsModel.PublishId))
                {
                    WriteLog(string.Format("重复数据   - {0} ", newsModel.PublishId));
                    return;
                }


                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@ArticleTitle", newsModel.ArticleTitle),//标题
                    new SqlParameter("@ReporterName",newsModel.ReporterName),//作者名
                    new SqlParameter("@SourceName",newsModel.SourceName),//来源名称
                    new SqlParameter("@ArticleThumb",newsModel.ArticleThumb),//封面图，可为空
                    new SqlParameter("@ArticleLength",newsModel.ArticleLength),//新闻长度
                    new SqlParameter("@ArticleNotes", newsModel.ArticleNotes),//编者按或叫摘要的那段文字
                    new SqlParameter("@Keywords",newsModel.Keywords),//关键字，每个关键字用空格分割
                    new SqlParameter("@CreateDate",newsModel.CreateDate),//创建时间
                    new SqlParameter("@CreateDateInt",newsModel.CreateDateInt),//创建时间的年月日int型，如20150203
                    new SqlParameter("@ArticleBody",newsModel.ArticleBody)//文章正文
                };
                // 写入数据库操作，测试时要注释，生产时要放开
                //SqlHelper.ExecteNonQuery(connectionString, System.Data.CommandType.StoredProcedure, "Store_Articles_CreateImport_AEF", sqlParams);
                SaveToArticleAd(newsModel);
            }
            catch (Exception e)
            {
                WriteLog(string.Format("数据库错误： {0}", e.Message));
            }

        }

        static int GetBodyLength(string strBody)
        {
            string body = Regex.Replace(strBody, @"<[\s\S]*?>", "", RegexOptions.IgnoreCase);
            body = HttpUtility.HtmlDecode(body);
            var bodyLength = Regex.Matches(body, @"(?i)([a-z-])+").Count + Regex.Matches(body, @"[\u4e00-\u9fa5]").Count + Regex.Matches(body, @"([\d])+").Count + Regex.Matches(body, @"[，。：、“”！,:!]").Count;
            return bodyLength;
        }



    }
}
