using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FilesModel;
using FilesModel.EquipmentConfig;

namespace FilesModelUI
{
    public partial class Form1 : Form
    {
        int count = 0;
        List<string> imagePath = new List<string>();
        public Form1()
        {
            InitializeComponent();
            imagePath.Add(@"D:\a1.jpg");
            imagePath.Add(@"D:\a2.jpg");
            imagePath.Add(@"D:\a3.jpg");
            this.pictureBox1.ImageLocation = imagePath[count];
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (count >= this.imageList1.Images.Count)
                count = 0;
            this.pictureBox1.ImageLocation = imagePath[count];
            count++;
        }

        //文本文件中的全部内容
        List<FileDataDetail> fileDataDetailList = null;

        private void btnRead_Click(object sender, EventArgs e)
        {
            //fileDataDetailList = FileManager.FileDataDetailList;
            //foreach (FileDataDetail fileDataDetail in fileDataDetailList)
            //{
            //    TreeNode fistNode = new TreeNode();
            //    fistNode.Text = fileDataDetail.Name;
            //    tvMenu.Nodes.Add(fistNode);

            //    if (fileDataDetail.Name != "串口配置")
            //    {
            //        foreach (EquipmentInfo equipmentInfo in fileDataDetail.EquipmentList)
            //        {
            //            TreeNode secondNode = new TreeNode();
            //            secondNode.Text = equipmentInfo.Name;
            //            fistNode.Nodes.Add(secondNode);
            //        }
            //    }
            //}
            string[] oldArray = new string[] {"一","三","二","四","五","六" };
             List<string> newList=oldArray.ToList();
             newList.Sort();
        }

        private void cboMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvDetail.DataSource = fileDataDetailList.FirstOrDefault(g => g.Name == "串口配置").SerialPortList;

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string savePath = string.Empty;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "(*.txt)|*.txt";
            saveFileDialog.FileName = "temp.txt";
            saveFileDialog.DefaultExt = ".txt";
            saveFileDialog.AddExtension = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                savePath = saveFileDialog.FileName;
                try
                {
                    FileManager.ResponseData(savePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }

        private void tvMenu_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string name = e.Node.Text;
            if (name == "串口配置")
            {
                dgvDetail.DataSource = fileDataDetailList.FirstOrDefault(g => g.Name == name).SerialPortList;
            }
            else
            {
                if (e.Node.Level == 1)
                {
                    dgvDetail.DataSource = fileDataDetailList.FirstOrDefault(g => g.Name == e.Node.Parent.Text)
                        .EquipmentList.FirstOrDefault(g => g.Name == name).EquipmentConfigList;
                }
            }
        }

        
    }
}
