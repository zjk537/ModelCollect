using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ConfigModel;
using ConfigModel.ConfigXML;
using CommonHelper.Results;


namespace ConfigModelUI
{
    public partial class OperateXML : Form
    {
        public OperateXML()
        {
            InitializeComponent();
        }

        static string path = string.Empty;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CommendService commendService = new CommendService();
            commendService.Guid = txtGuid.Text.Trim();
            commendService.Name = txtName.Text.Trim();
            commendService.Enable = chkEnable.Checked;
            commendService.Description = txtDescription.Text.Trim();
            commendService.LogoUrl = txtLogoUrl.Text.Trim();
            BusinessType businessType = new BusinessType();
            businessType.Name = cboBusinessTypeName.Text.Trim();
            businessType.CommentService.Add(commendService);

            ResultInfo resultInfo = XMLManager.InserRecordIntoXml(path, businessType);
            if (!resultInfo.IsSuccessed)
            {
                MessageBox.Show(resultInfo.ReturnMessage, resultInfo.ReturnCode);
            }
        }

        private void OperateXML_Load(object sender, EventArgs e)
        {
            path = @"TestData\XmlCommendService.xml";
            XmlCommendService xmlCommendService = XMLManager.ReadXmlToObject(path);
            List<string> businessType = new List<string>();
            foreach (BusinessType business in xmlCommendService.BusinessTypes)
            {
                businessType.Add(business.Name);
            }
            this.cboBusinessTypeName.DataSource = businessType;
            this.cboBusinessTypeName.SelectedIndex = -1;
        }

        private void txtGuid_Enter(object sender, EventArgs e)
        {
            this.txtGuid.Text = Guid.NewGuid().ToString();
        }
    }
}
