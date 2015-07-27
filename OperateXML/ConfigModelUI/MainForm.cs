using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ConfigModelUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnPathMap_Click(object sender, EventArgs e)
        {
            CreateNewForm(new PathService());
        }

        private void btnOperateXML_Click(object sender, EventArgs e)
        {
            CreateNewForm(new OperateXML());
        }

        private void CreateNewForm(Form form)
        {
            form.MdiParent = this;
            form.WindowState = FormWindowState.Maximized;
            form.Show();
        }
    }
}
