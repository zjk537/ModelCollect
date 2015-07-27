using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LangKeyExpert
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.skinEngine1.SkinFile = "DiamondGreen.ssk";
        }

        private void timerStart_Tick(object sender, EventArgs e)
        {
            if (this.Opacity > 0.01)
            {
                this.Opacity = this.Opacity - 0.01;
            }
            else
            {
                this.timerStart.Enabled = false;
                this.Hide();
                PersonalEdition personalEdition = new PersonalEdition();
                personalEdition.Show();
            }
        }
    }
}