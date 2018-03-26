using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace WebServer
{
    interface IRouteHandler
    {
        HttpListenerContext Process(HttpListenerContext context);
    }
}
