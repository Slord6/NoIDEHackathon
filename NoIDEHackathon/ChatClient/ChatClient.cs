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
            return client.DownloadString(hostname + "/chat/create/" + roomName);
        }

        public string SendMessage(string contents)
        {
            client.DownloadString(hostname + "/chat/message/" + currentRoom + "/" + userName + "/" + contents);

            return GetChatRaw();
        }

        public string GetChatRaw()
        {
            return Uri.UnescapeDataString(client.DownloadString(hostname + "/chat/room/" + currentRoom));
        }

        public string[] GetChat()
        {
            string rawChat = GetChatRaw().Replace("</br>",Environment.NewLine);
            string[] messages = rawChat.Split('\n');

            //Remove all html tags
            string[] tags = { "body", "h1", "html" };
            for(int i = 0; i< messages.Length; i++)
            {
                string messageLine = messages[i];
                foreach (string tag in tags)
                {
                    string openTag = "<" + tag + ">";
                    string endTag = "</" + tag + ">";

                    messageLine = messageLine.Replace(openTag, " ");
                    messageLine = messageLine.Replace(endTag, " ");
                }
                messages[i] = messageLine;
            }

            messages = messages.Where((x) => x != "\r" && !string.IsNullOrEmpty(x)).ToArray();

            return messages;
        }

        public string GetRooms()
        {
            return Uri.UnescapeDataString(client.DownloadString(hostname + "/chat/" + "list"));
        }
    }
}
