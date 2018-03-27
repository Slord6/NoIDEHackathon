using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient
{
    class ChatClient
    {
        private WebClient client;
        private string currentRoom;
        private string hostname;
        private string userName;

        public string CurrentRoom
        {
            set
            {
                currentRoom = value;
            }
        }
        
        public ChatClient(string hostname, string userName)
        {
            this.hostname = hostname;
            this.userName = userName;
            client = new WebClient();
        }

        public string CreateRoom(string roomName)
        {
            return client.DownloadString(hostname + "/create/" + roomName);
        }

        public string SendMessage(string contents)
        {
            client.DownloadString(hostname + "/chat/message/" + currentRoom + "/" + userName + "/" + contents);

            return GetChat();
        }

        public string GetChat()
        {
            return client.DownloadString(hostname + "/chat/room/" + currentRoom);
        }

        public string GetRooms()
        {
            return Uri.UnescapeDataString(client.DownloadString(hostname + "/chat/" + "list"));
        }
    }
}
