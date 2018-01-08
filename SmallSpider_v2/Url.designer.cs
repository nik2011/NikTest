namespace SmallSpider_v2
{
    partial class Url
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.confirm = new System.Windows.Forms.Button();
            this.image = new System.Windows.Forms.Button();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLoad = new System.Windows.Forms.TextBox();
            this.loadImage = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.ckbLogin = new System.Windows.Forms.CheckBox();
            this.laber10 = new System.Windows.Forms.Label();
            this.txtLoginUrl = new System.Windows.Forms.TextBox();
            this.txtPostData = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(98, 158);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "网站首页";
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(164, 155);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(308, 21);
            this.txtUrl.TabIndex = 1;
            // 
            // confirm
            // 
            this.confirm.Location = new System.Drawing.Point(227, 204);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(104, 23);
            this.confirm.TabIndex = 2;
            this.confirm.Text = "获取url";
            this.confirm.UseVisualStyleBackColor = true;
            this.confirm.Click += new System.EventHandler(this.confirm_Click);
            // 
            // image
            // 
            this.image.Location = new System.Drawing.Point(227, 295);
            this.image.Name = "image";
            this.image.Size = new System.Drawing.Size(104, 23);
            this.image.TabIndex = 3;
            this.image.Text = "获取图片url";
            this.image.UseVisualStyleBackColor = true;
            this.image.Click += new System.EventHandler(this.image_Click);
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(164, 255);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(308, 21);
            this.txtHost.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(98, 255);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "主机";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(98, 365);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "主机";
            // 
            // txtLoad
            // 
            this.txtLoad.Location = new System.Drawing.Point(164, 365);
            this.txtLoad.Name = "txtLoad";
            this.txtLoad.Size = new System.Drawing.Size(308, 21);
            this.txtLoad.TabIndex = 7;
            // 
            // loadImage
            // 
            this.loadImage.Location = new System.Drawing.Point(227, 410);
            this.loadImage.Name = "loadImage";
            this.loadImage.Size = new System.Drawing.Size(104, 23);
            this.loadImage.TabIndex = 6;
            this.loadImage.Text = "下载图片";
            this.loadImage.UseVisualStyleBackColor = true;
            this.loadImage.Click += new System.EventHandler(this.loadImage_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(98, 481);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "版本号：20160623";
            // 
            // ckbLogin
            // 
            this.ckbLogin.AutoSize = true;
            this.ckbLogin.Location = new System.Drawing.Point(100, 12);
            this.ckbLogin.Name = "ckbLogin";
            this.ckbLogin.Size = new System.Drawing.Size(72, 16);
            this.ckbLogin.TabIndex = 10;
            this.ckbLogin.Text = "是否登录";
            this.ckbLogin.UseVisualStyleBackColor = true;
            // 
            // laber10
            // 
            this.laber10.AutoSize = true;
            this.laber10.Location = new System.Drawing.Point(98, 54);
            this.laber10.Name = "laber10";
            this.laber10.Size = new System.Drawing.Size(53, 12);
            this.laber10.TabIndex = 12;
            this.laber10.Text = "登录路径";
            // 
            // txtLoginUrl
            // 
            this.txtLoginUrl.Location = new System.Drawing.Point(164, 54);
            this.txtLoginUrl.Name = "txtLoginUrl";
            this.txtLoginUrl.Size = new System.Drawing.Size(308, 21);
            this.txtLoginUrl.TabIndex = 13;
            // 
            // txtPostData
            // 
            this.txtPostData.Location = new System.Drawing.Point(164, 98);
            this.txtPostData.Name = "txtPostData";
            this.txtPostData.Size = new System.Drawing.Size(308, 21);
            this.txtPostData.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(98, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "请求数据";
            // 
            // Url
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 632);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPostData);
            this.Controls.Add(this.txtLoginUrl);
            this.Controls.Add(this.laber10);
            this.Controls.Add(this.ckbLogin);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtLoad);
            this.Controls.Add(this.loadImage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtHost);
            this.Controls.Add(this.image);
            this.Controls.Add(this.confirm);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.label1);
            this.Name = "Url";
            this.Text = "Spider";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button confirm;
        private System.Windows.Forms.Button image;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLoad;
        private System.Windows.Forms.Button loadImage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox ckbLogin;
        private System.Windows.Forms.Label laber10;
        private System.Windows.Forms.TextBox txtLoginUrl;
        private System.Windows.Forms.TextBox txtPostData;
        private System.Windows.Forms.Label label5;
    }
}

