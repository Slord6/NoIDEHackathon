using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.WebSockets;
using System.Net;
using WebServer.RouterHandlers;

namespace WebServer
{
    public partial class ServerHostForm : Form
    {
        HttpServer server;
        Router router;

        private Dictionary<string, IRouteHandler> routing = new Dictionary<string, IRouteHandler> {
            { "file", new FileGetHandler("No file found", Application.LocalUserAppDataPath) },
            { "upload", new FilePutHandler("Failed to PUT file", Application.LocalUserAppDataPath) },
            { "chat", new ChatHandler("Unknown chat request") }
        };

        public ServerHostForm()
        {
            InitializeComponent();

            //Enable output for routers
            foreach (KeyValuePair<string,IRouteHandler> route in routing)
            {
                if (typeof(DefaultRouteHandler).IsAssignableFrom(route.Value.GetType()))
                {
                    DefaultRouteHandler tempHandler = (DefaultRouteHandler)route.Value;
                    tempHandler.Log = true;
                }
            }

            router = new Router(routing);
            Action<HttpListenerContext> requestFunc = router.Route;
            string hostname = "http://" + Dns.GetHostName() + "/";
            server = new HttpServer(new string[] { "http://localhost/", "https://localhost/", hostname }, requestFunc);
            server.Log = true;

            Console.SetOut(new ControlWriter(outputTextbox, true));
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = false;
            stopButton.Enabled = true;
            server.Start();

            TestServer();
        }

        private void TestServer()
        {
            using (WebClient client = new WebClient())
            {
                byte[] response = client.UploadData("http://localhost/upload/dir/uploadFile.txt", "PUT", System.IO.File.ReadAllBytes(@"C:\Users\samlo\AppData\Local\WebServer\WebServer\1.0.0.0\startText.txt"));
                
                Console.WriteLine("Test response = " + Encoding.UTF8.GetString(response));
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = true;
            stopButton.Enabled = false;
            server.Stop();
        }
    }
}
