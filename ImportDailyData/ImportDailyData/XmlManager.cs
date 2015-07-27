using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Net;
using log4net;

namespace ImportDailyData
{
    public class XmlManager
    {

        public static NewspaperMP ReadXmlToModel(string url, ILog log)
        {
            NewspaperMP newspaperMP = null;
            //XmlReader xmlReader = null;
            //try
            //{
            WebRequest wReq = WebRequest.Create(url);
            Stream respStream = wReq.GetResponse().GetResponseStream();
            //FileStream respStream = new FileStream("D:\\dycjrb_20150112.xml",FileMode.Open,FileAccess.Read);

            log.Info("读取网络XML文件成功！Url:" + url);
            using (StreamReader sr = new StreamReader(respStream, Encoding.GetEncoding("utf-8")))
            {
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sr.ReadToEnd());
                sr.Close();
                // 反序列化
                log.Info("开始反序列化数据...");
                XmlSerializer serializer = new XmlSerializer(typeof(NewspaperMP));
                newspaperMP = (NewspaperMP)serializer.Deserialize(new MemoryStream(buffer));
                log.Info("反序列化数据成功！");
                return newspaperMP;
            }
            //}
            //catch (Exception e)
            //{
            //    log.Error("读出Xml时出错：" + e);
            //    if (e.Message.Contains("404"))
            //    {
            //        log.Error(string.Format("Xml 文档未生成，下次重试：{0}", DateTime.Now.AddMinutes(30).ToString("yyyy-MM-dd hh:mm:ss")));

            //    }

            //}

            //return null;
        }


    }
}
