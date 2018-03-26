using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebServer
{
    class DefaultRouteHandler : IRouteHandler
    {
        private string defaultMessage;

        public DefaultRouteHandler(string defaultMessage)
        {
            this.defaultMessage = defaultMessage;
        }

        public HttpListenerContext Process(HttpListenerContext context)
        {
            ResponseAsString(WrapAsHtml(defaultMessage), context);
        }
        
        private void ResponseAsString(string response, HttpListenerContext context, int statusCode = 200)
        {
            byte[] buf = Encoding.UTF8.GetBytes(response);
            context.Response.ContentLength64 = buf.Length;
            context.Response.OutputStream.Write(buf, 0, buf.Length);
            context.Response.StatusCode = 200;
        }

        private string WrapAsHtml(string rawString)
        {
            return "<html><body>" + rawString + "</body></html>";
        }
    }
}
