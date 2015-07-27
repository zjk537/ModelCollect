using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Text;
using System.Globalization;

namespace ReadFiles
{
    public partial class DlFile : System.Web.UI.Page
    {
        string dlfile;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.ExportTxtByLine(@"D:\example.txt");//导出文本格式
        }

        /// <summary>
        /// 一行一行的读取数据
        /// </summary>
        /// <param name="path"></param>
        private void ExportTxtByLine(string path)
        {
            Response.Clear();

            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + dlfile);
            Response.Flush();
            using (StreamReader sr = new StreamReader(path + dlfile, Encoding.Default))
            {
                //一个字符一个字符的读
                char[] data = new char[1];
                while (sr.Read(data, 0, data.Length) > 0)
                {
                    if (data[0] == ' ')
                    {
                    }
                    else
                    {
                        string test = data[0].ToString();
                    }
                }
                //一行一行的读数据
                string line=string.Empty;
                while (!string.IsNullOrEmpty(line = sr.ReadLine()))
                {
                    string[] strArray = line.Split('|');//这里分隔的字符
                    //分隔成数据，这样就存储了每行的内容
                }
            }
        }

        /// <summary>
        /// 分断读取数据
        /// </summary>
        /// <param name="path"></param>
        private void ExportTxtByData(string path)
        {
            Response.Clear();

            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + dlfile);
            Response.Flush();

            Stream s = Response.OutputStream;
            using (FileStream fs = new FileStream(path + dlfile, FileMode.Open, FileAccess.Read))
            {
                long fileLength = fs.Length;
                byte[] data = new byte[1024 * 1024];
                int currentPosition = 0;

                while ((fs.Read(data, 0, data.Length) > 0))
                {
                    s.Write(data, 0, data.Length);
                    currentPosition += data.Length;
                    if ((fileLength - currentPosition) > data.Length)
                    {
                        Response.Flush();
                        continue;
                    }
                    else
                    {
                        data = new byte[fileLength - currentPosition];
                        fs.Read(data, 0, data.Length);
                        s.Write(data, 0, data.Length);
                        Response.Flush();
                    }
                }
            }
            s.Close();
        }

        // 这个是WinForm下的例子，码拷贝过来的
        //public void FindFile(string dir) //参数为指定的目录
        //{
        //    //在指定目录及子目录下查找文件,在listBox1中列出子目录及文件
        //    DirectoryInfo Dir = new DirectoryInfo(dir);
        //    try
        //    {
        //        foreach (DirectoryInfo d in Dir.GetDirectories())     //查找子目录   
        //        {
        //            FindFile(Dir + d.ToString() + "\\");
        //            lbResult.Items.Add(Dir.Name);       //listBox1中填加目录名
        //        }
        //        foreach (FileInfo f in Dir.GetFiles("*.*"))             //查找文件
        //        {
        //            lbResult.Items.Add(f.Name);     //listBox1中填加文件名
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.Message);
        //    }

        //}
    }
}
