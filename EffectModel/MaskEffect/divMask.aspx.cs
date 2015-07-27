using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Threading;

namespace MaskEffect
{
    public partial class divMask : System.Web.UI.Page, ICallbackEventHandler
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string cbReference = Page.ClientScript.GetCallbackEventReference(this, "arg", "unLock", "context");
            string callbackScript = @"function clientClick(arg, context){" + cbReference + "};";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "clientClick", callbackScript, true);

        }

        protected void btnDivMask_Click(object sender, EventArgs e)
        {
            Thread.Sleep(5000);
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "updateScript", "$('div#divOverlay').unblockUI;", true);
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "updateScript", "function unBlockMask() {jQuery('div#divOverlay').unblock();}", true);

        }

        #region ICallbackEventHandler 成员

        public string GetCallbackResult()
        {
            return "test";
        }

        public void RaiseCallbackEvent(string eventArgument)
        {
            Thread.Sleep(10000);
        }

        #endregion
    }
}
