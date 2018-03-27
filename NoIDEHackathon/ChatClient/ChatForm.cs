using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class ChatForm : Form
    {
        ChatClient chatClient;

        public ChatForm()
        {
            InitializeComponent();
        }

        private void joinButton_Click(object sender, EventArgs e)
        {
            //check if room exists, if it doesn't create it
            //clear chat window and fill with new room chat
            if (chatClient != null)
            {
                PopulateRoomsComboBox(activeRoomsComboBox.Text);
                chatClient.CurrentRoom = activeRoomsComboBox.Text;
                chatWindowTextBox.Text = chatClient.GetChat();
            }
        }

        private void PopulateRoomsComboBox(string selectedElement)
        {
            string[] roomList = chatClient.GetRooms().Split(new string[] { "</br>" }, StringSplitOptions.RemoveEmptyEntries);

            activeRoomsComboBox.Items.Clear();

            for (int i = 1; i < roomList.Length - 1; i++)
            {
                activeRoomsComboBox.Items.Add(roomList[i]);
                if (roomList[i] == selectedElement)
                {
                    chatClient.CurrentRoom = roomList[i];
                    chatClient.GetChat();
                    activeRoomsComboBox.SelectedIndex = i - 1;
                }
            }
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            if(chatClient != null)
            {
                chatClient.SendMessage(messageTextBox.Text);
                chatWindowTextBox.Text = chatClient.GetChat();
            }
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            chatClient = new ChatClient(hostTextBox.Text, usernameTextBox.Text);
            PopulateRoomsComboBox(null);
        }
    }
}
