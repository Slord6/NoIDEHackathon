using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace WebServer.RouterHandlers
{
    interface IRouteHandler
    {
        void Process(HttpListenerContext context);
    }
}
