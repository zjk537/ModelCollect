using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WebServiceOperation.WebService;

namespace WebServiceOperation
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            OperatesService service = new OperatesService();
            bool isOk = service.AddUser(int.Parse(ddlDepartment.SelectedValue));

            if (isOk)
            {
                lblInfo.Text = "新增成功";
            }
            else
            {
                lblError.Text = "新增失败";
            }
        }
    }
}
