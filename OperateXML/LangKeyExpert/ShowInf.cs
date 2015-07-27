using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LangKeyExpert
{
    public partial class ShowInf : Form
    {
        static int n;

        public ShowInf(string lblInf)
        {
            InitializeComponent();
            this.TopMost = true;
            this.lblInfo.Text = lblInf;
            n = Screen.PrimaryScreen.WorkingArea.Height;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - 210, Screen.PrimaryScreen.WorkingArea.Height);
        }

        private void timUp_Tick(object sender, EventArgs e)
        {
            if (this.Location.Y > Screen.PrimaryScreen.WorkingArea.Height - 160)
            {
                this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width-210,n-1);
                n -= 1;
            }
            else
            {
                timUp.Enabled = false;
                timMid.Enabled = true;
            }
        }

        private void timMid_Tick(object sender, EventArgs e)
        {
            timMid.Enabled = false;
            timDow.Enabled = true;
        }

        private void timDow_Tick(object sender, EventArgs e)
        {
            if (this.Location.Y < Screen.PrimaryScreen.WorkingArea.Height)
            {
                this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - 210, n + 1);
                n += 1;
            }
            else
            {
                timDow.Enabled = false;
                this.Close();
            }
        }
    }
}