using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using log4net;
using System.IO;
using System.Configuration;

namespace Log4netInWebForm
{
    public partial class _Default : System.Web.UI.Page
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(_Default));

        // 也可以用下面这种方式声明
        //private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected void Page_Load(object sender, EventArgs e)
        {
            //// 方法一： 如果放在这里，那每个页面加载的时候，都会去为log4net装载配置文件，不可取，应该放在Global.asax里
            //// 在这个应该程序域没有失效之前，这个配置不会重新去读日志记录的也不是自己想要的
            //log4net.Config.XmlConfigurator.Configure(); 

            //if (!IsPostBack)
            //{
            //    // 方法二： 使用ConfigureAndWatch(..)可以指定一个配置文件并且监视该文件的变化
            //    log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(ConfigurationManager.AppSettings["log4netConfig"]));
            //    //log4net.Config.DOMConfigurator.ConfigureAndWatch(new FileInfo(ConfigurationManager.AppSettings["log4netConfig"]));
            //}

            //// 方法三：在程序集文件中加载log4net的配置信息,在这个应该程序域没有失效之前，这个配置不会重新去读日志记录的也不是自己想要的
            //// ConfigFileExtension 当应用程序会编译成不同扩展名称的程序集时，可以使用这个属性。如Sample程序将编译成Sample.exe，
            //// 则ConfigFileExtension设置为“config”，那么所使用的配置文件名称为：Sample.exe.config。注意不能和ConfigFile属性同时使用。
            //// 
            //// 只要保证log4net.config跟web.config在同一目录下，就可以做读到log4net的配置信息
            //[assembly: log4net.Config.DOMConfigurator(ConfigFile = "log4net.config", Watch = true)]
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (log.IsDebugEnabled)
                log.Debug("错误类型：DefaultDebug");
            if (log.IsErrorEnabled)
                log.Error("错误类型：DefaultError");
            if (log.IsFatalEnabled)
                log.Fatal("错误类型：DefaultFatal");
            if (log.IsInfoEnabled)
                log.Info("错误类型：DefaultInfo");
            if (log.IsWarnEnabled)
                log.Warn("错误类型：DefaultWarn");
        }
    }
}
