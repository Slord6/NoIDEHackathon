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
        public ServerHostForm()
        {
            InitializeComponent();
            Func<HttpListenerRequest, string> requestFunc = (HttpListenerRequest request) =>
            {
                return "<html><body><h1>HELLO WORLD</h1></body></html>";
            };
            server = new HttpServer(new string[] { "http://localhost/" }, requestFunc);
            server.Verbose = true;

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
