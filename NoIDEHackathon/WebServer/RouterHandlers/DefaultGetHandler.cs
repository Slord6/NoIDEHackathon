using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.RouterHandlers
{
    class DefaultRouteHandler : IRouteHandler
    {
        protected string defaultMessage;

        private Dictionary<string, string> routingLookup;

        public bool Log { get; set; }

        public DefaultRouteHandler(string defaultMessage, Dictionary<string,string> lookup)
        {
            this.defaultMessage = defaultMessage;
            this.routingLookup = lookup;
        }

        public DefaultRouteHandler(string defaultMessage)
        {
            this.defaultMessage = defaultMessage;
            this.routingLookup = null;
        }

        public virtual void Process(HttpListenerContext context)
        {
            string[] path = context.Request.Url.AbsolutePath.Split('/');
            if (routingLookup != null && path.Length > 0 && routingLookup.ContainsKey(path[0]))
            {
                ResponseAsString(WrapAsHtml(routingLookup[path[0]]), context);
            }
            else
            {
                ResponseAsString(WrapAsHtml(defaultMessage), context);
            }
        }
        
        protected void ResponseAsString(string response, HttpListenerContext context, int statusCode = 200)
        {
            byte[] buf = Encoding.UTF8.GetBytes(response);

            if (Log)
            {
                Console.WriteLine("Sending response (" + buf.Length + " bytes) for " + context.Request.RemoteEndPoint);
            }

            context.Response.ContentLength64 = buf.Length;
            context.Response.OutputStream.Write(buf, 0, buf.Length);
            context.Response.StatusCode = 200;
        }

        protected string WrapAsHtml(string rawString)
        {
            string html = "<html><body>" + rawString + "</body></html>";
            return html.Replace(Environment.NewLine, "</br>");
        }
    }
}
