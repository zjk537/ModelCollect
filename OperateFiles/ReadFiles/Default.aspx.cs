using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;

namespace ReadFiles
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = @"D:\BlackBerry";

            this.rptFileList.DataSource = FindFile(path);
            this.rptFileList.DataBind();

            foreach (string name in GetBusinessType(path))
            {
                this.DropDownList1.Items.Add(name);
            }
            //SetCatch();
            //this.lbTest.Text = _minutes.ToString();
        }
        //private static int _minutes;

        //protected void SetCatch()
        //{
        //    string minutes = ConfigurationManager.AppSettings["CacheSlidingTime"];
        //    if (string.IsNullOrEmpty(minutes) || !int.TryParse(minutes, out _minutes))
        //    {
        //        _minutes = 10; // 默认为10分钟。
        //    }
        //}
        public List<FilesInfo> FindFile(string dir) //参数为指定的目录
        {
            //在指定目录及子目录下查找文件,在listBox1中列出子目录及文件
            DirectoryInfo Dir = new DirectoryInfo(dir);
            List<FilesInfo> fileList = new List<FilesInfo>();
            try
            {
                //foreach (DirectoryInfo d in Dir.GetDirectories())     //查找子目录   
                //{
                //    FindFile(Dir + d.ToString() + "\\");
                //    lbResult.Items.Add(Dir.Name);       //listBox1中填加目录名
                //}
                foreach (FileInfo f in Dir.GetFiles("*.*"))             //查找文件
                {
                    FilesInfo fileInfo = new FilesInfo();
                    fileInfo.FileName = f.Name;
                    fileInfo.FileSize = f.Length/1024;
                    fileInfo.CreateDate = f.LastWriteTime;
                    fileInfo.UrlQueryString =Server.UrlEncode(f.Name);
                    fileList.Add(fileInfo);
                }
            }
            catch (Exception e)
            {
                
            }
            return fileList;

        }

        private List<string> GetBusinessType(string dir)
        {
            DirectoryInfo Dir = new DirectoryInfo(dir);
            List<string> business = new List<string>();

            try
            {
                foreach (FileInfo f in Dir.GetFiles("*" + 2000201000 + "*"))
                {
                    string name = f.Name.Substring(0, f.Name.IndexOf('-'));
                    if (!business.Contains(name))
                    {
                        business.Add(name);
                    }
                }
            }
            catch (Exception)
            {
            }
            return business;
        }

    }
}
