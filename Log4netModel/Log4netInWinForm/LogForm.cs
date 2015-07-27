using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using log4net;
using System.IO;
using System.Configuration;

namespace Log4netInWinForm
{
    public partial class LogForm : Form
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(LogForm));
        public LogForm()
        {
            //// 方法一：读取App.config文件中的log4net的配置信息
            //log4net.Config.XmlConfigurator.Configure();
            //log4net.Config.DOMConfigurator.Configure();

            //// 方法二：log4net的配置信息写在单独的配置文件log4net.config中，用ConfigureAndWatch()读取并监视它的变化
            //log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(ConfigurationManager.AppSettings["log4netConfig"]));

            // 方法三：在程序集文件中读取log4net的配置信息
            //  1、这样写要保证 log4net.config跟程序 exe 文件在同一目录下 
            // [assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]

            // 2、或者按如下配置，这样配置 读取方式跟方法二一样，要在App.config文件中加入节点log4netConfig
            // [assembly: log4net.Config.XmlConfigurator(ConfigFile = "Log4netInWinForm.exe.config", Watch = true)]
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            if (_log.IsDebugEnabled)
                _log.Debug("错误类型：Debug");
            if (_log.IsErrorEnabled)
                _log.Error("错误类型：Error");
            if (_log.IsFatalEnabled)
                _log.Fatal("错误类型：Fatal");
            if (_log.IsInfoEnabled)
                _log.Info("错误类型：Info");
            if (_log.IsWarnEnabled)
                _log.Warn("错误类型：Warn");

            button1.Enabled = true;
        }
    }
}
