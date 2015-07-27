using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;

namespace MaskEffect
{
    public partial class CallBackEvent : System.Web.UI.Page,ICallbackEventHandler
    {

        //回调的结果
        private string callBackResult = string.Empty;



        protected void Page_Load(object sender, EventArgs e)
        {
            ClientScriptManager cs = Page.ClientScript;

            // 定义客户端成功调用后的处理函数
            StringBuilder context1 = new StringBuilder();
            context1.Append("function ReceiveServerData1(arg, context)");
            context1.Append("{");
            context1.Append("king.innerHTML =  arg;");
            context1.Append("}");

            // 定义客户端错误处理函数
            StringBuilder context2 = new StringBuilder();
            context2.Append("function ProcessCallBackError(arg, context)");
            context2.Append("{");
            context2.Append("alert('An error has occurred.');");
            context2.Append("}");

            //向客户端注册脚本
            cs.RegisterClientScriptBlock(this.GetType(), "ProcessCallBackError",
                context2.ToString(), true);

            //返回客户调用服务事件的脚本代码，结果如下:WebForm_DoCallback('__Page',arg,ReceiveServerData1,function ReceiveServerData1(arg, context){king.innerHTML =  arg;},ProcessCallBackError,false);
            //其中的content值为一段脚本函数
            String cbReference1 = cs.GetCallbackEventReference("'" +
                Page.UniqueID + "'", "arg", "ReceiveServerData1", context1.ToString(),
                "ProcessCallBackError", false);

            String callbackScript1 = "function CallTheServer1(arg, context) {" +
                cbReference1 + "; }";

            // 注册触发回调事件的脚本块
            cs.RegisterClientScriptBlock(this.GetType(), "CallTheServer1",
                callbackScript1, true);
    

        }

        #region ICallbackEventHandler 成员

        public string GetCallbackResult()
        {
            string[] str = new string[] { "11", "22", "33", "44" };
            ListItem item = null;
            this.drpList1.Items.Clear();
            foreach (string tmp in str)
            {
                item = new ListItem(tmp, tmp);
                this.drpList1.Items.Add(item);
            }

            StringWriter writer1 = new StringWriter(System.Globalization.CultureInfo.InvariantCulture);
            HtmlTextWriter writer2 = new HtmlTextWriter(writer1);
            this.drpList1.RenderControl(writer2);

            writer2.Flush();
            writer2.Close();
            return writer1.ToString();

        }

        public void RaiseCallbackEvent(string eventArgument)
        {
            callBackResult = eventArgument + System.DateTime.Now.ToString();
        }

        #endregion
    }
}
