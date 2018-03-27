using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.RouterHandlers
{
    class ChatHandler : DefaultRouteHandler
    {
        private Dictionary<string, Action<HttpListenerContext,string[]>> functionMap;
        private Dictionary<string, RoomInfo> roomLookup;

        public ChatHandler(string defaultMessage) : base(defaultMessage)
        {
            functionMap = new Dictionary<string, Action<HttpListenerContext, string[]>>();
            functionMap.Add("create", NewChat);
            functionMap.Add("message", NewMessage);
            functionMap.Add("room", GetRoom);
            functionMap.Add("list", GetRooms);

            roomLookup = new Dictionary<string, RoomInfo>();
        }

        public override void Process(HttpListenerContext context)
        {
            string absoluteURL = context.Request.Url.AbsolutePath;
            string[] splitURL = absoluteURL.Split('/');
            if(splitURL.Length < 3)
            {
                UnrecognisedRequest(context);
                return;
            }

            string chatRequest = splitURL[2];
            if (functionMap.ContainsKey(chatRequest))
            {
                functionMap[chatRequest].Invoke(context, splitURL);
            }
            else
            {
                UnrecognisedRequest(context);
                return;
            }
        }

        private void UnrecognisedRequest(HttpListenerContext context, string extraInfo = "")
        {
            ResponseAsString("Unrecognised chatroom request. " + extraInfo, context, 404);
        }

        private void NewChat(HttpListenerContext context, string[] splitURL)
        {
            //Expected - */create/[roomname]
            if(splitURL.Length < 4)
            {
                UnrecognisedRequest(context, "/chat/create/[roomname] expected");
                return;
            }

            string newRoomName = splitURL[3];
            RoomInfo newRoom = new RoomInfo(newRoomName);
            roomLookup.Add(newRoomName, newRoom);

            ResponseAsString("Room created", context, 200);
        }

        private void NewMessage(HttpListenerContext context, string[] splitURL)
        {
            //Expected - */message/roomname/username/messageText
            if(splitURL.Length < 6)
            {
                UnrecognisedRequest(context, "/chat/message/roomname/username/messageText expected");
                return;
            }
            string roomName = splitURL[3];
            string userName = splitURL[4];
            string messageText = Uri.UnescapeDataString(splitURL[5]);

            if (roomLookup.ContainsKey(roomName))
            {
                Message newMessage = new Message(messageText, userName, context.Request.RemoteEndPoint.Address, DateTime.UtcNow);
                roomLookup[roomName].AddMessage(newMessage);
                ResponseAsString("Message added to room", context);
            }
            else
            {
                ResponseAsString("No such room", context, 404);
            }
        }

        private void GetRoom(HttpListenerContext context, string[] splitURL)
        {
            //Expected */room/[roomname]
            if (splitURL.Length < 4)
            {
                UnrecognisedRequest(context, "/chat/room/[roomname] expected");
                return;
            }
            string roomName = splitURL[3];
            if (roomLookup.ContainsKey(roomName))
            {
                RoomInfo room = roomLookup[roomName];
                string responseContent = "<h1>" + room.Name + "</h1>" + WrapAsHtml(room.ToString());
                ResponseAsString(responseContent, context);
            }
            else
            {
                ResponseAsString("No such room", context, 404);
            }
        }

        private void GetRooms(HttpListenerContext context, string[] splitURL)
        {
            string roomList = "<h1>Rooms</h1>" + Environment.NewLine;

            foreach (KeyValuePair<string,RoomInfo> room in roomLookup)
            {
                roomList += room.Key + Environment.NewLine;
            }

            ResponseAsString(WrapAsHtml(roomList), context);
        }

        class RoomInfo
        {
            private string name;
            private List<Message> messages;

            public string Name
            {
                get
                {
                    return name;
                }
            }

            public RoomInfo(string name)
            {
                this.name = name;
                messages = new List<Message>();
            }

            public void AddMessage(Message newMessage)
            {
                messages.Add(newMessage);
            }

            public override string ToString()
            {
                string stringRep = "";

                foreach (Message message in messages)
                {
                    stringRep += message.senderName + " @ " + message.received + ":" + Environment.NewLine;
                    stringRep += message.body + Environment.NewLine + Environment.NewLine;
                }

                return stringRep;
            }
        }

        class Message
        {
            public string body;
            public IPAddress source;
            public string senderName;
            public DateTime received;

            public Message(string body, string senderName, IPAddress source, DateTime received)
            {
                this.body = body;
                this.senderName = senderName;
                this.source = source;
                this.received = received;
            }
        }
    }
}
