using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class ChatForm : Form
    {
        ChatClient chatClient;
        Task messageGetTask;

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
                try
                {
                    chatClient.CurrentRoom = activeRoomsComboBox.Text;
                    if (!activeRoomsComboBox.Items.Contains(activeRoomsComboBox.Text))
                    {
                        chatClient.CreateRoom(activeRoomsComboBox.Text);
                        PopulateRoomsComboBox(activeRoomsComboBox.Text);
                    }
                    chatWindowTextBox.Text = chatClient.GetChatRaw();
                    //Then start a task to periodically get new messages
                    if (messageGetTask == null)
                    {
                        messageGetTask = Task.Run(() => GetMessageIndefinite());
                    }
                }
                catch(WebException ex)
                {
                    ExceptionMessageBox(ex);
                }
            }
        }

        private void GetMessageIndefinite()
        {
            while (true)
            {
                if(chatClient != null)
                {
                    string newMessages = "";
                    try
                    {
                        newMessages = chatClient.GetChat().Aggregate((current, next) => current + Environment.NewLine + next);
                    }
                    catch (WebException ex)
                    {
                        ExceptionMessageBox(ex);
                        MessageBox.Show("Disconnected");
                        chatClient = null;
                        return; 
                    }
                    if (newMessages != chatWindowTextBox.Text)
                    {
                        chatWindowTextBox.Invoke((MethodInvoker)delegate { chatWindowTextBox.Text = newMessages; });
                    }
                }
                System.Threading.Thread.Sleep(1000);
            }
        }

        private void PopulateRoomsComboBox(string selectedElement)
        {
            try
            {
                string[] roomList = chatClient.GetRooms().Split(new string[] { "</br>" }, StringSplitOptions.RemoveEmptyEntries);

                activeRoomsComboBox.Items.Clear();

                for (int i = 1; i < roomList.Length - 1; i++)
                {
                    activeRoomsComboBox.Items.Add(roomList[i]);
                    if (roomList[i] == selectedElement)
                    {
                        chatClient.CurrentRoom = roomList[i];
                        chatClient.GetChatRaw();
                        activeRoomsComboBox.SelectedIndex = i - 1;
                    }
                }
            }
            catch (WebException ex)
            {
                ExceptionMessageBox(ex);
            }
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            if(chatClient != null)
            {
                try
                {
                    chatClient.SendMessage(messageTextBox.Text);
                    chatWindowTextBox.Text = chatClient.GetChatRaw();
                    messageTextBox.Text = "";
                }
                catch (WebException ex)
                {
                    ExceptionMessageBox(ex);
                }
            }
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            chatClient = new ChatClient(hostTextBox.Text, usernameTextBox.Text);
            PopulateRoomsComboBox(null);
        }

        private void refreshRoomsButton_Click(object sender, EventArgs e)
        {
            PopulateRoomsComboBox(null);
        }

        private void ExceptionMessageBox(Exception ex)
        {
            MessageBox.Show("An error occured: " + ex.Message, "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
