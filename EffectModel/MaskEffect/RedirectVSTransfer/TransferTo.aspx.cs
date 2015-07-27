using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Text;

namespace MaskEffect.RedirectVSTransfer
{
    public partial class TransferTo : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            TransferFrom oFrom = (TransferFrom)this.Context.Handler;
            sb.Append("Of TextBox:" + Request.Form["txtContext"] + "<br />"); //因为这个重定向了，所以找不到form的值了
            sb.Append("Time Property:"+oFrom.Time+"<br />");
            sb.Append("Context String :" + Context.Items["Context"].ToString() + "<br />");
            sb.Append(oFrom.TestFun() + "<br />");
            LitGetMessage.Text = sb.ToString();
        }
    }
}
