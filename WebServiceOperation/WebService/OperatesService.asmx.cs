using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebServiceOperation.WebService
{
    /// <summary>
    /// OperatesService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class OperatesService : System.Web.Services.WebService
    {
        [WebMethod]
        public bool AddUser(int DepartmentId)
        {
            bool flag = false;

            try
            {
                //SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
                //connection.Open();

                //SqlCommand command = new SqlCommand(string.Format("insert into T_User(UserName, DepartmentId) values('{0}', {1})", "user" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), DepartmentId), connection);

                //command.ExecuteNonQuery();

                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
            }

            return flag;
        }
    }
}
