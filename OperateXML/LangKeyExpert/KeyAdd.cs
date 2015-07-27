using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LangKeyExpert
{
    public partial class KeyAdd : Form
    {
        private DataSet dsXML;
        public KeyAdd(DataSet d)
        {
            InitializeComponent();
            dsXML = d;
            int n = dsXML.Tables["UserKey"].Rows.Count;
            this.lblNumber.Text = Convert.ToString(n + 1);
            this.txtNet.Text = "http://";
        }

        private void KeyAdd_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text == "")
            {
                MessageBox.Show("对不起，标题不能为空","浪曦提醒");
                return;
            }
            DataRow newRow = dsXML.Tables["UserKey"].NewRow();
            newRow["Number"] = this.lblNumber.Text;
            newRow["Title"] = this.txtTitle.Text;
            newRow["NetAdd"] = txtNet.Text;
            newRow["Name"] = txtName.Text;
            newRow["Key"] = txtPwd.Text;
            newRow["UpdateTime"] = System.DateTime.Now.ToString();
            dsXML.Tables["UserKey"].Rows.Add(newRow);
            dsXML.WriteXml(@"UserKey.xml");
            MessageBox.Show("添加成功！", "浪曦提醒");
            this.Close();
        }
    }
}