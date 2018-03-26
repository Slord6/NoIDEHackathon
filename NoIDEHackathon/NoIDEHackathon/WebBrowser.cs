using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoIDEHackathon
{
    public partial class WebBrowser : Form
    {
        public WebBrowser()
        {
            InitializeComponent();
            _webBrowser.ScriptErrorsSuppressed = true;
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            string address = addressBar.Text;
            try
            {
                //if (!address.Contains("http"))
                {
                  //  address = "https://" + addressBar.Text;
                }
                _webBrowser.Url = new Uri(address);
            }
            catch(UriFormatException)
            {
                string googleSearch = @"http://www.google.com/search?q=";
                string[] words = address.Split(' ');
                foreach (string word in words)
                {
                    googleSearch += word + "+";
                }
                _webBrowser.Url = new Uri(googleSearch);
            }
        }

        private void backwardsButton_Click(object sender, EventArgs e)
        {
            if (_webBrowser.CanGoBack)
            {
                _webBrowser.GoBack();
            }
        }

        private void forwardButton_Click(object sender, EventArgs e)
        {
            if (_webBrowser.CanGoForward)
            {
                _webBrowser.GoForward();
            }
        }

        private void _webBrowser_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            if(e.MaximumProgress <= 0)
            {
                return;
            }
            progressBar.Value = (int)(e.CurrentProgress / e.MaximumProgress) * 100;
        }
    }
}
