namespace NoIDEHackathon
{
    partial class WebBrowser
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
            this.webBrowserGroupBox = new System.Windows.Forms.GroupBox();
            this._webBrowser = new System.Windows.Forms.WebBrowser();
            this.addressBar = new System.Windows.Forms.TextBox();
            this.forwardButton = new System.Windows.Forms.Button();
            this.backwardsButton = new System.Windows.Forms.Button();
            this.goButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.webBrowserGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowserGroupBox
            // 
            this.webBrowserGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowserGroupBox.Controls.Add(this._webBrowser);
            this.webBrowserGroupBox.Location = new System.Drawing.Point(12, 35);
            this.webBrowserGroupBox.Name = "webBrowserGroupBox";
            this.webBrowserGroupBox.Size = new System.Drawing.Size(1529, 874);
            this.webBrowserGroupBox.TabIndex = 0;
            this.webBrowserGroupBox.TabStop = false;
            // 
            // _webBrowser
            // 
            this._webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this._webBrowser.Location = new System.Drawing.Point(3, 16);
            this._webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this._webBrowser.Name = "_webBrowser";
            this._webBrowser.Size = new System.Drawing.Size(1523, 855);
            this._webBrowser.TabIndex = 0;
            this._webBrowser.Url = new System.Uri("http://www.google.com", System.UriKind.Absolute);
            this._webBrowser.ProgressChanged += new System.Windows.Forms.WebBrowserProgressChangedEventHandler(this._webBrowser_ProgressChanged);
            // 
            // addressBar
            // 
            this.addressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addressBar.Location = new System.Drawing.Point(12, 13);
            this.addressBar.Name = "addressBar";
            this.addressBar.Size = new System.Drawing.Size(1407, 20);
            this.addressBar.TabIndex = 1;
            // 
            // forwardButton
            // 
            this.forwardButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.forwardButton.Location = new System.Drawing.Point(1507, 10);
            this.forwardButton.Name = "forwardButton";
            this.forwardButton.Size = new System.Drawing.Size(31, 23);
            this.forwardButton.TabIndex = 2;
            this.forwardButton.Text = ">>";
            this.forwardButton.UseVisualStyleBackColor = true;
            this.forwardButton.Click += new System.EventHandler(this.forwardButton_Click);
            // 
            // backwardsButton
            // 
            this.backwardsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.backwardsButton.Location = new System.Drawing.Point(1470, 10);
            this.backwardsButton.Name = "backwardsButton";
            this.backwardsButton.Size = new System.Drawing.Size(31, 23);
            this.backwardsButton.TabIndex = 3;
            this.backwardsButton.Text = "<<";
            this.backwardsButton.UseVisualStyleBackColor = true;
            this.backwardsButton.Click += new System.EventHandler(this.backwardsButton_Click);
            // 
            // goButton
            // 
            this.goButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.goButton.Location = new System.Drawing.Point(1425, 10);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(39, 23);
            this.goButton.TabIndex = 4;
            this.goButton.Text = "Go!";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(12, 915);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1526, 23);
            this.progressBar.TabIndex = 1;
            // 
            // WebBrowser
            // 
            this.AcceptButton = this.goButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1553, 950);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.backwardsButton);
            this.Controls.Add(this.forwardButton);
            this.Controls.Add(this.addressBar);
            this.Controls.Add(this.webBrowserGroupBox);
            this.Name = "WebBrowser";
            this.Text = "WebBrowser";
            this.webBrowserGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox webBrowserGroupBox;
        private System.Windows.Forms.WebBrowser _webBrowser;
        private System.Windows.Forms.TextBox addressBar;
        private System.Windows.Forms.Button forwardButton;
        private System.Windows.Forms.Button backwardsButton;
        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}

