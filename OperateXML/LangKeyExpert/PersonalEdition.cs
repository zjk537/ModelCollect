using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LangKeyExpert
{
    public partial class PersonalEdition : Form
    {
        public PersonalEdition()
        {
            InitializeComponent();
            this.skinEngine1.SkinFile = "DiamondBlue.ssk";
            dsXML.ReadXml("UserKey.xml");
            dgUserKey.DataSource = dsXML.Tables["UserKey"];
            dgUserKey.Columns[0].HeaderText = "序号";
            dgUserKey.Columns[1].HeaderText = "标题";
            dgUserKey.Columns[2].HeaderText = "网址";
            dgUserKey.Columns[3].HeaderText = "用户名";
            dgUserKey.Columns[4].HeaderText = "密码";
            dgUserKey.Columns[5].HeaderText = "更新日期";
            dgUserKey.Columns[0].Width = 52;
            dgUserKey.Columns[1].Width = 85;
            dgUserKey.Columns[2].Width = 155;
            dgUserKey.Columns[3].Width = 75;
            dgUserKey.Columns[4].Width = 75;
            dgUserKey.Columns[5].Width = 120;

        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            if(diaFile.ShowDialog()==DialogResult.OK)
            {
                if(diaFile.OpenFile()!=null)
                {
                    twoXML.ReadXml(@diaFile.FileName);
                    foreach(DataRow twoRow in twoXML.Tables["UserKey"].Rows)
                    {
                        DataRow newRow = dsXML.Tables["UserKey"].NewRow();
                        newRow["Number"] = twoRow["Number"];
                        newRow["Title"] = twoRow["Title"];
                        newRow["NetAdd"] = twoRow["NetAdd"];
                        newRow["Name"] = twoRow["Name"];
                        newRow["Key"] = twoRow["Key"];
                        newRow["UpdateTime"] = twoRow["UpdateTime"];
                        dsXML.Tables["UserKey"].Rows.Add(newRow);
                    }
                    int n = dsXML.Tables["UserKey"].Rows.Count;
                    for (int i = 0; i < n; i++)
                    {
                        dsXML.Tables["UserKey"].Rows[i]["Number"] = i + 1;
                    }
                    dsXML.WriteXml(@"UserKey.xml");
                    this.Visible = true;
                    MessageBox.Show("文件导入成功！", "浪曦提醒");                    
                }
            }
            else
            {
                this.Visible=true;
            }
        }

        private void 水晶绿ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.skinEngine1.SkinFile = "DiamondGreen.ssk";
        }

        private void 水晶蓝ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.skinEngine1.SkinFile = "DiamondBlue.ssk";
        }

        private void 导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (diaSaveFile.ShowDialog() == DialogResult.OK)
            {
                dsXML.WriteXml(@diaSaveFile.FileName);
                this.Visible = true;
                MessageBox.Show("文件导出成功！", "浪曦提醒");
            }
            else
            {
                this.Visible = true;
            }
        }

        private void PersonalEdition_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            //this.Close();
            //this.Dispose();
        }

        private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KeyAdd keyAdd = new KeyAdd(dsXML);
            keyAdd.ShowDialog();
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int n = dgUserKey.CurrentRow.Index;
            this.dgUserKey.CurrentCell=dgUserKey[0,n];
            string id = dgUserKey.CurrentCell.Value.ToString();
            KeyChange keyChange = new KeyChange(id,dsXML);
            keyChange.ShowDialog();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = "您确实要删除选定记录吗？";
            string caption = "浪曦提醒";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;
            result = MessageBox.Show(this,message,caption,buttons,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1,MessageBoxOptions.RightAlign);

            if(result==DialogResult.Yes)
            {
                int m = dgUserKey.CurrentRow.Index;
                int n = dsXML.Tables["UserKey"].Rows.Count;
                if (n == 1)
                {
                    MessageBox.Show("对不起，至少预留一条记录！", "浪曦提醒");
                }
                else
                {
                    foreach (DataRow dsRow in dsXML.Tables["UserKey"].Rows)
                    {
                        if (dsRow["Number"].ToString() == Convert.ToString(m + 1))
                        {
                            dsRow.Delete();
                            for (int i = 0; i < n - 1; i++)
                            {
                                dsXML.Tables["UserKey"].Rows[i]["Number"] = i + 1;
                            }
                            dsXML.WriteXml(@"UserKey.xml");
                            MessageBox.Show("删除成功", "浪曦提醒");
                            return;
                        }
                    }
                }
            }
        }

        private void dgUserKey_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgUserKey.Columns[e.ColumnIndex].HeaderText == "网址")
            {
                string url = dgUserKey.CurrentCell.Value.ToString();
                System.Diagnostics.Process.Start(url);
                ShowInf showForm = new ShowInf("正在打开网页，请稍等...");
                showForm.Show();
                return;
            }

            if (dgUserKey.Columns[e.ColumnIndex].HeaderText == "用户名")
            {
                string name = dgUserKey.CurrentCell.Value.ToString();
                Clipboard.SetText(name);
                ShowInf showForm = new ShowInf("用户名已复制到剪贴板");
                showForm.Show();
                return;
            }

            if (dgUserKey.Columns[e.ColumnIndex].HeaderText == "密码")
            {
                string key = dgUserKey.CurrentCell.Value.ToString();
                Clipboard.SetText(key);
                ShowInf showForm = new ShowInf("密码已复制到剪贴板");
                showForm.Show();
                return;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            导入ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            导出ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            添加ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            修改ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            删除ToolStripMenuItem_Click(sender, e);
        }

        private void 帮助ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string help = "langsin.chm";
            Help.ShowHelp(this,help);
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Info info = new Info();
            info.Show();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            帮助ToolStripMenuItem1_Click(sender, e);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            关于ToolStripMenuItem_Click(sender, e);
        }

        private void PersonalEdition_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                this.notIcon.Visible = true;
            }
        }

        private void notIcon_MouseDown(object sender, MouseEventArgs e)
        {
            //判断鼠标左键单击
            if (e.Button == MouseButtons.Left)
            {
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
                this.notIcon.Visible = false;
            }
        }

        private void 显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            this.notIcon.Visible = false;
        }

        private void 退出ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}