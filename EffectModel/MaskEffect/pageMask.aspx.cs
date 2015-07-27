using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Threading;

namespace MaskEffect
{
    public partial class pageMask : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnPageMask_Click(object sender, EventArgs e)
        {
            Thread.Sleep(10000);
        }
        protected void btnNotice_Click(object sender, EventArgs e)
        {
            Thread.Sleep(10000);
        }
    }
}
