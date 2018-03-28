namespace ChatClient
{
    partial class ChatForm
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
            this.activeRoomsComboBox = new System.Windows.Forms.ComboBox();
            this.roomLabel = new System.Windows.Forms.Label();
            this.chatWindowTextBox = new System.Windows.Forms.TextBox();
            this.joinCreateButton = new System.Windows.Forms.Button();
            this.messageTextBox = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.hostLabel = new System.Windows.Forms.Label();
            this.hostTextBox = new System.Windows.Forms.TextBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.refreshRoomsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // activeRoomsComboBox
            // 
            this.activeRoomsComboBox.FormattingEnabled = true;
            this.activeRoomsComboBox.Location = new System.Drawing.Point(12, 137);
            this.activeRoomsComboBox.Name = "activeRoomsComboBox";
            this.activeRoomsComboBox.Size = new System.Drawing.Size(121, 21);
            this.activeRoomsComboBox.TabIndex = 3;
            // 
            // roomLabel
            // 
            this.roomLabel.AutoSize = true;
            this.roomLabel.Location = new System.Drawing.Point(9, 121);
            this.roomLabel.Name = "roomLabel";
            this.roomLabel.Size = new System.Drawing.Size(43, 13);
            this.roomLabel.TabIndex = 1;
            this.roomLabel.Text = "Rooms:";
            // 
            // chatWindowTextBox
            // 
            this.chatWindowTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chatWindowTextBox.Location = new System.Drawing.Point(12, 164);
            this.chatWindowTextBox.Multiline = true;
            this.chatWindowTextBox.Name = "chatWindowTextBox";
            this.chatWindowTextBox.Size = new System.Drawing.Size(461, 267);
            this.chatWindowTextBox.TabIndex = 7;
            // 
            // joinCreateButton
            // 
            this.joinCreateButton.Location = new System.Drawing.Point(139, 135);
            this.joinCreateButton.Name = "joinCreateButton";
            this.joinCreateButton.Size = new System.Drawing.Size(75, 23);
            this.joinCreateButton.TabIndex = 4;
            this.joinCreateButton.Text = "Join/Create";
            this.joinCreateButton.UseVisualStyleBackColor = true;
            this.joinCreateButton.Click += new System.EventHandler(this.joinButton_Click);
            // 
            // messageTextBox
            // 
            this.messageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageTextBox.Location = new System.Drawing.Point(12, 439);
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.Size = new System.Drawing.Size(379, 20);
            this.messageTextBox.TabIndex = 5;
            // 
            // sendButton
            // 
            this.sendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sendButton.Location = new System.Drawing.Point(398, 437);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(75, 23);
            this.sendButton.TabIndex = 6;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // hostLabel
            // 
            this.hostLabel.AutoSize = true;
            this.hostLabel.Location = new System.Drawing.Point(9, 30);
            this.hostLabel.Name = "hostLabel";
            this.hostLabel.Size = new System.Drawing.Size(32, 13);
            this.hostLabel.TabIndex = 6;
            this.hostLabel.Text = "Host:";
            // 
            // hostTextBox
            // 
            this.hostTextBox.Location = new System.Drawing.Point(12, 46);
            this.hostTextBox.Name = "hostTextBox";
            this.hostTextBox.Size = new System.Drawing.Size(121, 20);
            this.hostTextBox.TabIndex = 0;
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(244, 44);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 23);
            this.connectButton.TabIndex = 2;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Location = new System.Drawing.Point(139, 46);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(100, 20);
            this.usernameTextBox.TabIndex = 1;
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(136, 30);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(58, 13);
            this.usernameLabel.TabIndex = 10;
            this.usernameLabel.Text = "Username:";
            // 
            // refreshRoomsButton
            // 
            this.refreshRoomsButton.Location = new System.Drawing.Point(220, 135);
            this.refreshRoomsButton.Name = "refreshRoomsButton";
            this.refreshRoomsButton.Size = new System.Drawing.Size(75, 23);
            this.refreshRoomsButton.TabIndex = 11;
            this.refreshRoomsButton.Text = "Refresh";
            this.refreshRoomsButton.UseVisualStyleBackColor = true;
            this.refreshRoomsButton.Click += new System.EventHandler(this.refreshRoomsButton_Click);
            // 
            // ChatForm
            // 
            this.AcceptButton = this.sendButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 471);
            this.Controls.Add(this.refreshRoomsButton);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.usernameTextBox);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.hostTextBox);
            this.Controls.Add(this.hostLabel);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.messageTextBox);
            this.Controls.Add(this.joinCreateButton);
            this.Controls.Add(this.chatWindowTextBox);
            this.Controls.Add(this.roomLabel);
            this.Controls.Add(this.activeRoomsComboBox);
            this.Name = "ChatForm";
            this.Text = "Chat";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox activeRoomsComboBox;
        private System.Windows.Forms.Label roomLabel;
        private System.Windows.Forms.TextBox chatWindowTextBox;
        private System.Windows.Forms.Button joinCreateButton;
        private System.Windows.Forms.TextBox messageTextBox;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Label hostLabel;
        private System.Windows.Forms.TextBox hostTextBox;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Button refreshRoomsButton;
    }
}

