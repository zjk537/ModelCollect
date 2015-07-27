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
            dgUserKey.Columns[0].HeaderText = "���";
            dgUserKey.Columns[1].HeaderText = "����";
            dgUserKey.Columns[2].HeaderText = "��ַ";
            dgUserKey.Columns[3].HeaderText = "�û���";
            dgUserKey.Columns[4].HeaderText = "����";
            dgUserKey.Columns[5].HeaderText = "��������";
            dgUserKey.Columns[0].Width = 52;
            dgUserKey.Columns[1].Width = 85;
            dgUserKey.Columns[2].Width = 155;
            dgUserKey.Columns[3].Width = 75;
            dgUserKey.Columns[4].Width = 75;
            dgUserKey.Columns[5].Width = 120;

        }

        private void �˳�ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void �༭ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ����ToolStripMenuItem_Click(object sender, EventArgs e)
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
                    MessageBox.Show("�ļ�����ɹ���", "��������");                    
                }
            }
            else
            {
                this.Visible=true;
            }
        }

        private void ˮ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.skinEngine1.SkinFile = "DiamondGreen.ssk";
        }

        private void ˮ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.skinEngine1.SkinFile = "DiamondBlue.ssk";
        }

        private void ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (diaSaveFile.ShowDialog() == DialogResult.OK)
            {
                dsXML.WriteXml(@diaSaveFile.FileName);
                this.Visible = true;
                MessageBox.Show("�ļ������ɹ���", "��������");
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

        private void ���ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KeyAdd keyAdd = new KeyAdd(dsXML);
            keyAdd.ShowDialog();
        }

        private void �޸�ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int n = dgUserKey.CurrentRow.Index;
            this.dgUserKey.CurrentCell=dgUserKey[0,n];
            string id = dgUserKey.CurrentCell.Value.ToString();
            KeyChange keyChange = new KeyChange(id,dsXML);
            keyChange.ShowDialog();
        }

        private void ɾ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = "��ȷʵҪɾ��ѡ����¼��";
            string caption = "��������";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;
            result = MessageBox.Show(this,message,caption,buttons,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1,MessageBoxOptions.RightAlign);

            if(result==DialogResult.Yes)
            {
                int m = dgUserKey.CurrentRow.Index;
                int n = dsXML.Tables["UserKey"].Rows.Count;
                if (n == 1)
                {
                    MessageBox.Show("�Բ�������Ԥ��һ����¼��", "��������");
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
                            MessageBox.Show("ɾ���ɹ�", "��������");
                            return;
                        }
                    }
                }
            }
        }

        private void dgUserKey_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgUserKey.Columns[e.ColumnIndex].HeaderText == "��ַ")
            {
                string url = dgUserKey.CurrentCell.Value.ToString();
                System.Diagnostics.Process.Start(url);
                ShowInf showForm = new ShowInf("���ڴ���ҳ�����Ե�...");
                showForm.Show();
                return;
            }

            if (dgUserKey.Columns[e.ColumnIndex].HeaderText == "�û���")
            {
                string name = dgUserKey.CurrentCell.Value.ToString();
                Clipboard.SetText(name);
                ShowInf showForm = new ShowInf("�û����Ѹ��Ƶ�������");
                showForm.Show();
                return;
            }

            if (dgUserKey.Columns[e.ColumnIndex].HeaderText == "����")
            {
                string key = dgUserKey.CurrentCell.Value.ToString();
                Clipboard.SetText(key);
                ShowInf showForm = new ShowInf("�����Ѹ��Ƶ�������");
                showForm.Show();
                return;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ����ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ����ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ���ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            �޸�ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            ɾ��ToolStripMenuItem_Click(sender, e);
        }

        private void ����ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string help = "langsin.chm";
            Help.ShowHelp(this,help);
        }

        private void ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Info info = new Info();
            info.Show();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            ����ToolStripMenuItem1_Click(sender, e);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            ����ToolStripMenuItem_Click(sender, e);
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
            //�ж�����������
            if (e.Button == MouseButtons.Left)
            {
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
                this.notIcon.Visible = false;
            }
        }

        private void ��ʾToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            this.notIcon.Visible = false;
        }

        private void �˳�ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}