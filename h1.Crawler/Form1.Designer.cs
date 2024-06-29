namespace h1.Crawler
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.textBoxUrl = new System.Windows.Forms.TextBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.listBoxCrawledUrls = new System.Windows.Forms.ListBox();
            this.listBoxErrorUrls = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
       


            this.textBoxUrl.Location = new System.Drawing.Point(12, 12);
            this.textBoxUrl.Name = "textBoxUrl";
            this.textBoxUrl.Size = new System.Drawing.Size(776, 20);
            this.textBoxUrl.TabIndex = 0;
            this.textBoxUrl.Text = "http://www.whu.edu.cn/";
     


            this.buttonStart.Location = new System.Drawing.Point(713, 38);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
      


            this.listBoxCrawledUrls.FormattingEnabled = true;
            this.listBoxCrawledUrls.Location = new System.Drawing.Point(12, 67);
            this.listBoxCrawledUrls.Name = "listBoxCrawledUrls";
            this.listBoxCrawledUrls.Size = new System.Drawing.Size(776, 173);
            this.listBoxCrawledUrls.TabIndex = 2;
            


            this.listBoxErrorUrls.FormattingEnabled = true;
            this.listBoxErrorUrls.Location = new System.Drawing.Point(12, 246);
            this.listBoxErrorUrls.Name = "listBoxErrorUrls";
            this.listBoxErrorUrls.Size = new System.Drawing.Size(776, 173);
            this.listBoxErrorUrls.TabIndex = 3;
         


            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listBoxErrorUrls);
            this.Controls.Add(this.listBoxCrawledUrls);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.textBoxUrl);
            this.Name = "Form1";
            this.Text = "Crawler";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox textBoxUrl;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.ListBox listBoxCrawledUrls;
        private System.Windows.Forms.ListBox listBoxErrorUrls;
    }
}
