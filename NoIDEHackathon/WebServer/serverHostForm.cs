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

namespace WebServer
{
    public partial class ServerHostForm : Form
    {
        HttpServer server;
        Router router;

        public ServerHostForm()
        {
            InitializeComponent();
            router = new Router();
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
                //Console.WriteLine("Testing with client:");
                //outputTextbox.Text += client.DownloadString("http://localhost");
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
