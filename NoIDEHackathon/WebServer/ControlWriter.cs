using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebServer
{
    public class ControlWriter : TextWriter
    {
        private Control textbox;
        private bool threadSafeReq = false;
        public ControlWriter(Control textbox, bool threadSafeReq = false)
        {
            this.textbox = textbox;
            this.threadSafeReq = threadSafeReq;
        }

        public override void Write(char value)
        {
            if (threadSafeReq)
            {
                textbox.BeginInvoke((MethodInvoker)delegate () { textbox.Text += value; });
            }
            else
            {
                textbox.Text += value;
            }
        }

        public override void Write(string value)
        {
            if (threadSafeReq)
            {
                textbox.BeginInvoke((MethodInvoker)delegate () { textbox.Text += value; });
            }
            else
            {
                textbox.Text += value;
            }
        }

        public override Encoding Encoding
        {
            get { return Encoding.ASCII; }
        }
    }

}
