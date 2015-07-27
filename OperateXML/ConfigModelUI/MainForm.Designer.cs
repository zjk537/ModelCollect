namespace ConfigModelUI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnPathMap = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnOperateXML = new System.Windows.Forms.ToolStripButton();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPathMap,
            this.toolStripSeparator1,
            this.btnOperateXML});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(492, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "ToolStrip";
            // 
            // btnPathMap
            // 
            this.btnPathMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnPathMap.Image = ((System.Drawing.Image)(resources.GetObject("btnPathMap.Image")));
            this.btnPathMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPathMap.Name = "btnPathMap";
            this.btnPathMap.Size = new System.Drawing.Size(117, 22);
            this.btnPathMap.Text = "读取配置文件的内容";
            this.btnPathMap.Click += new System.EventHandler(this.btnPathMap_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnOperateXML
            // 
            this.btnOperateXML.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnOperateXML.Image = ((System.Drawing.Image)(resources.GetObject("btnOperateXML.Image")));
            this.btnOperateXML.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOperateXML.Name = "btnOperateXML";
            this.btnOperateXML.Size = new System.Drawing.Size(75, 22);
            this.btnOperateXML.Text = "操作XML文档";
            this.btnOperateXML.Click += new System.EventHandler(this.btnOperateXML_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 573);
            this.Controls.Add(this.toolStrip);
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripButton btnPathMap;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnOperateXML;
    }
}



