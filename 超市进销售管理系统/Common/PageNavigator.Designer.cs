namespace Common
{
    partial class PageNavigator
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageNavigator));
            this.lblPageCount = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ReturnBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.LastBtn = new System.Windows.Forms.Button();
            this.NextBtn = new System.Windows.Forms.Button();
            this.UpBtn = new System.Windows.Forms.Button();
            this.FristBtn = new System.Windows.Forms.Button();
            this.PageIndex = new System.Windows.Forms.Label();
            this.txtNum = new Common.textbox(this.components);
            this.SuspendLayout();
            // 
            // lblPageCount
            // 
            this.lblPageCount.AutoSize = true;
            this.lblPageCount.ForeColor = System.Drawing.Color.Green;
            this.lblPageCount.Location = new System.Drawing.Point(522, 11);
            this.lblPageCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPageCount.Name = "lblPageCount";
            this.lblPageCount.Size = new System.Drawing.Size(15, 15);
            this.lblPageCount.TabIndex = 34;
            this.lblPageCount.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(555, 11);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 15);
            this.label5.TabIndex = 33;
            this.label5.Text = "页";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(484, 11);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 32;
            this.label2.Text = "共：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 13);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 15);
            this.label3.TabIndex = 25;
            this.label3.Text = "页";
            // 
            // ReturnBtn
            // 
            this.ReturnBtn.Image = ((System.Drawing.Image)(resources.GetObject("ReturnBtn.Image")));
            this.ReturnBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ReturnBtn.Location = new System.Drawing.Point(799, 4);
            this.ReturnBtn.Margin = new System.Windows.Forms.Padding(4);
            this.ReturnBtn.Name = "ReturnBtn";
            this.ReturnBtn.Size = new System.Drawing.Size(75, 29);
            this.ReturnBtn.TabIndex = 31;
            this.ReturnBtn.Text = "跳转";
            this.ReturnBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ReturnBtn.UseVisualStyleBackColor = true;
            this.ReturnBtn.Click += new System.EventHandler(this.ReturnBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(619, 10);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 30;
            this.label4.Text = "跳转到：";
            // 
            // LastBtn
            // 
            this.LastBtn.Image = ((System.Drawing.Image)(resources.GetObject("LastBtn.Image")));
            this.LastBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LastBtn.Location = new System.Drawing.Point(360, 4);
            this.LastBtn.Margin = new System.Windows.Forms.Padding(4);
            this.LastBtn.Name = "LastBtn";
            this.LastBtn.Size = new System.Drawing.Size(88, 29);
            this.LastBtn.TabIndex = 29;
            this.LastBtn.Text = "最后页";
            this.LastBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LastBtn.UseVisualStyleBackColor = true;
            this.LastBtn.Click += new System.EventHandler(this.LastBtn_Click);
            // 
            // NextBtn
            // 
            this.NextBtn.Image = ((System.Drawing.Image)(resources.GetObject("NextBtn.Image")));
            this.NextBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NextBtn.Location = new System.Drawing.Point(267, 4);
            this.NextBtn.Margin = new System.Windows.Forms.Padding(4);
            this.NextBtn.Name = "NextBtn";
            this.NextBtn.Size = new System.Drawing.Size(88, 29);
            this.NextBtn.TabIndex = 28;
            this.NextBtn.Text = "下一页";
            this.NextBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.NextBtn.UseVisualStyleBackColor = true;
            this.NextBtn.Click += new System.EventHandler(this.NextBtn_Click);
            // 
            // UpBtn
            // 
            this.UpBtn.Image = ((System.Drawing.Image)(resources.GetObject("UpBtn.Image")));
            this.UpBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.UpBtn.Location = new System.Drawing.Point(171, 4);
            this.UpBtn.Margin = new System.Windows.Forms.Padding(4);
            this.UpBtn.Name = "UpBtn";
            this.UpBtn.Size = new System.Drawing.Size(88, 29);
            this.UpBtn.TabIndex = 27;
            this.UpBtn.Text = "上一页";
            this.UpBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.UpBtn.UseVisualStyleBackColor = true;
            this.UpBtn.Click += new System.EventHandler(this.UpBtn_Click);
            // 
            // FristBtn
            // 
            this.FristBtn.Image = ((System.Drawing.Image)(resources.GetObject("FristBtn.Image")));
            this.FristBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.FristBtn.Location = new System.Drawing.Point(75, 4);
            this.FristBtn.Margin = new System.Windows.Forms.Padding(4);
            this.FristBtn.Name = "FristBtn";
            this.FristBtn.Size = new System.Drawing.Size(88, 29);
            this.FristBtn.TabIndex = 26;
            this.FristBtn.Text = "第一页";
            this.FristBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.FristBtn.UseVisualStyleBackColor = true;
            this.FristBtn.Click += new System.EventHandler(this.FristBtn_Click);
            // 
            // PageIndex
            // 
            this.PageIndex.AutoSize = true;
            this.PageIndex.ForeColor = System.Drawing.Color.Red;
            this.PageIndex.Location = new System.Drawing.Point(7, 13);
            this.PageIndex.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PageIndex.Name = "PageIndex";
            this.PageIndex.Size = new System.Drawing.Size(15, 15);
            this.PageIndex.TabIndex = 24;
            this.PageIndex.Text = "0";
            // 
            // txtNum
            // 
            this.txtNum.Location = new System.Drawing.Point(682, 7);
            this.txtNum.Name = "txtNum";
            this.txtNum.Size = new System.Drawing.Size(100, 25);
            this.txtNum.TabIndex = 35;
            // 
            // PageNavigator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtNum);
            this.Controls.Add(this.lblPageCount);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ReturnBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.LastBtn);
            this.Controls.Add(this.NextBtn);
            this.Controls.Add(this.UpBtn);
            this.Controls.Add(this.FristBtn);
            this.Controls.Add(this.PageIndex);
            this.Name = "PageNavigator";
            this.Size = new System.Drawing.Size(880, 37);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPageCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ReturnBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button LastBtn;
        private System.Windows.Forms.Button NextBtn;
        private System.Windows.Forms.Button UpBtn;
        private System.Windows.Forms.Button FristBtn;
        private System.Windows.Forms.Label PageIndex;
        private textbox txtNum;
    }
}
