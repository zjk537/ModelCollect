using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

using System.IO;
using System.Configuration;

namespace Log4netInWebForm
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        { 
            // 方法一：装载一个全局的log4net配置文件
            log4net.Config.XmlConfigurator.Configure();

            //方法二：使用ConfigureAndWatch(..)可以指定一个配置文件并且监视该文件的变化
            //log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(ConfigurationManager.AppSettings["log4netConfig"]));
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}