using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ConfigModel;
using ConfigModel.GetConfigCollection;
using ConfigModel.GetFromResources;

namespace ConfigModelUI
{
    public partial class PathService : Form
    {
        public PathService()
        {
            InitializeComponent();
        }

        private void btnGetContent_Click(object sender, EventArgs e)
        {
            PathServiceConfig pathConfig = ConfigManager.GetPathServiceConfig();
            this.ltbContent.Items.Add("读取配置文件中的信息：");
            this.ltbContent.Items.Add(pathConfig.Host);
            for (int i = 0; i < pathConfig.PathMaps.Count; i++)
            {
                PathMap pathMap = pathConfig.PathMaps[i];
                this.ltbContent.Items.Add("  --" + pathMap.Name + " " + pathMap.Source + " " + pathMap.Destination);
            }

            this.ltbContent.Items.Add("");
        }

        private void btnGetMessage_Click(object sender, EventArgs e)
        {
            UserConfig userInfo = ConfigManager.GetUserInfo();
            this.ltbContent.Items.Add(userInfo.ResourceMessage);
            this.ltbContent.Items.Add("  用户名称：" + userInfo.FirstName + userInfo.LastName);
            this.ltbContent.Items.Add("");
        }
    }
}
