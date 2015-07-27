using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LangKeyExpert
{
    public partial class KeyChange : Form
    {
        private DataSet dsNewXML;
        public KeyChange(string id,DataSet d)
        {
            InitializeComponent();
            dsNewXML =d;
            foreach(DataRow dsRow in dsNewXML.Tables["UserKey"].Rows)
            {
                if (dsRow["Number"].ToString() == id)
                {
                    this.label1.Text = id;
                    this.txtTitle.Text = dsRow["Title"].ToString();
                    this.txtNet.Text = dsRow["NetAdd"].ToString();
                    this.txtName.Text = dsRow["Name"].ToString();
                    this.txtPwd.Text = dsRow["Key"].ToString();
                    return;
                }
            }
        }

        private void KeyChange_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.txtTitle.Text == "")
            {
                MessageBox.Show("对不起，标题不能为空！", "浪曦友情提示");
                return;
            }
            if (this.txtNet.Text == "")
            {
                MessageBox.Show("对不起，网址不能为空！", "浪曦友情提示");
                return;
            }
            foreach (DataRow dsRow in dsNewXML.Tables["UserKey"].Rows)
            {
                if (dsRow["Number"].ToString() == this.label1.Text)
                {
                    dsRow["Title"] = this.txtTitle.Text;
                    dsRow["NetAdd"] = this.txtNet.Text;
                    dsRow["Name"] = this.txtName.Text;
                    dsRow["Key"] = this.txtPwd.Text;
                    dsNewXML.WriteXml(@"UserKey.xml");
                    MessageBox.Show("修改成功！", "浪曦友情提示");
                    this.Close();
                    return;
                }
            }
        }
    }
}