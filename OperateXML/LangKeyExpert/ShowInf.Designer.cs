namespace LangKeyExpert
{
    partial class ShowInf
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblInfo = new System.Windows.Forms.Label();
            this.timUp = new System.Windows.Forms.Timer(this.components);
            this.timMid = new System.Windows.Forms.Timer(this.components);
            this.timDow = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(38, 54);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(0, 12);
            this.lblInfo.TabIndex = 0;
            // 
            // timUp
            // 
            this.timUp.Enabled = true;
            this.timUp.Interval = 20;
            this.timUp.Tick += new System.EventHandler(this.timUp_Tick);
            // 
            // timMid
            // 
            this.timMid.Interval = 3000;
            this.timMid.Tick += new System.EventHandler(this.timMid_Tick);
            // 
            // timDow
            // 
            this.timDow.Interval = 20;
            this.timDow.Tick += new System.EventHandler(this.timDow_Tick);
            // 
            // ShowInf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(202, 128);
            this.Controls.Add(this.lblInfo);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShowInf";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "浪曦提醒";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Timer timUp;
        private System.Windows.Forms.Timer timMid;
        private System.Windows.Forms.Timer timDow;
    }
}