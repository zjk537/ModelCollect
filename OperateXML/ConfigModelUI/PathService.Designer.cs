namespace ConfigModelUI
{
    partial class PathService
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGetContent = new System.Windows.Forms.Button();
            this.ltbContent = new System.Windows.Forms.ListBox();
            this.btnGetMessage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGetContent
            // 
            this.btnGetContent.Location = new System.Drawing.Point(42, 21);
            this.btnGetContent.Name = "btnGetContent";
            this.btnGetContent.Size = new System.Drawing.Size(115, 23);
            this.btnGetContent.TabIndex = 1;
            this.btnGetContent.Text = "读取配置文件内容";
            this.btnGetContent.UseVisualStyleBackColor = true;
            this.btnGetContent.Click += new System.EventHandler(this.btnGetContent_Click);
            // 
            // ltbContent
            // 
            this.ltbContent.FormattingEnabled = true;
            this.ltbContent.ItemHeight = 12;
            this.ltbContent.Location = new System.Drawing.Point(42, 60);
            this.ltbContent.Name = "ltbContent";
            this.ltbContent.Size = new System.Drawing.Size(416, 484);
            this.ltbContent.TabIndex = 3;
            // 
            // btnGetMessage
            // 
            this.btnGetMessage.Location = new System.Drawing.Point(192, 20);
            this.btnGetMessage.Name = "btnGetMessage";
            this.btnGetMessage.Size = new System.Drawing.Size(182, 23);
            this.btnGetMessage.TabIndex = 4;
            this.btnGetMessage.Text = "读取资源文件内的配置信息";
            this.btnGetMessage.UseVisualStyleBackColor = true;
            this.btnGetMessage.Click += new System.EventHandler(this.btnGetMessage_Click);
            // 
            // PathService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 573);
            this.Controls.Add(this.btnGetMessage);
            this.Controls.Add(this.ltbContent);
            this.Controls.Add(this.btnGetContent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "PathService";
            this.Text = "读取配置文件中的内容";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGetContent;
        private System.Windows.Forms.ListBox ltbContent;
        private System.Windows.Forms.Button btnGetMessage;
    }
}