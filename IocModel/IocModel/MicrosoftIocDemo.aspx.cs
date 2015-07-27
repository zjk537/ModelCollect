using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IocModel.MicrosoftIoc;

namespace IocModel
{
    public partial class MicrosoftIocDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnMicrosoftIoc_Click(object sender, EventArgs e)
        {
            // 1.2版本
            ILogger logger1 = IocUnityManager.GetInstance().Resolve<ILogger>();
            ILogger logger2 = IocUnityManager.GetInstance().Resolve<ILogger>();

            // ResolveAll<T>() 不返回 unnamed 的注册 所以Loggers只包含一个实例 FlatFileLogger
            IEnumerable<ILogger> loggers = IocUnityManager.GetInstance().ResolveAll<ILogger>();

            this.lbMessage.Text = logger1.Writer("zjk1") + "  " + logger1.GetHashCode() + "<br />" + logger2.Writer("zjk2") + "  " + logger2.GetHashCode() + "<br />";

            foreach (ILogger logger in loggers)
            {
                this.lbMessage.Text += logger.GetType().ToString() + "<br />";
            }
        }
    }
}
