using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaskEffect.RedirectVSTransfer
{
    public partial class TransferFrom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Context.Items.Add("Context", " TransferFrom 页面的 Context 内容");
            //Localize1.Text = "aaaaaaa<br />bbbbbb<br/>";
        }

        public string Time
        {
            get
            {
                return "TransferFrom 页面的时间" + DateTime.Now.ToString();
            }
        }

        public string TestFun()
        {
            return "TransferFrom页面的 TestFun() 方法 被执行了！";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string aa = Request.Form["Button1"];
            Server.Transfer("TransferTo.aspx", false);
        }

    }
}
