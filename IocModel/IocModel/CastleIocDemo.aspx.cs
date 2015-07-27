using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IocModel.CastleIoc;

namespace IocModel
{
    public partial class CastleIocDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCastleIoc_Click(object sender, EventArgs e)
        {
            //IWindsorContainer container = new WindsorContainer("CastleIoc/BasicUsage.xml");
            //container.AddComponent("txtLog", typeof(ILog), typeof(TextFileLog));
            //container.AddComponent("textFormat", typeof(ILogFormatter), typeof(TextFormat));

            ////这里组件的别名"txtLog" 要跟配置文档中组件的ID相同才能找到配置文件中的信息
            //ILog log = (ILog)container["txtLog"];
            //ILog log2 = (ILog)container["txtLog"];


            //这里组件的别名"txtLog" 要跟配置文档中组件的ID相同才能找到配置文件中的信息
            ILog log = (ILog)CastleIocManager.GetInstance()["txtLog"];
            ILog log2 = (ILog)CastleIocManager.GetInstance()["txtLog"];

            this.lbMessage.Text = log.Writer("First Castle IOC Demo") + " <br /> " + log.GetHashCode() + "<br />" + log2.GetHashCode();
        }
    }
}
