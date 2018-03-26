using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebServer.RouterHandlers;

namespace WebServer
{
    class Router
    {
        public bool Log { get; set; }

        private Dictionary<string, IRouteHandler> routeLookup = new Dictionary<string, IRouteHandler>();
        
        public Router(Dictionary<string, IRouteHandler> routeLookup)
        {
            this.routeLookup = routeLookup;
        }

        public void Route(HttpListenerContext context)
        {
            Output("Request for " + context.Request.Url.AbsolutePath + " (" + context.Request.HttpMethod + ") from " + context.Request.RemoteEndPoint.Address);

            string[] path = context.Request.Url.AbsolutePath.Split('/');
            if(path.Length == 0 || !routeLookup.ContainsKey(path[1]))
            {
                new DefaultRouteHandler("<h1>Available routes</h1></br>" + GetRoutes().Replace(Environment.NewLine, "</br>"), null).Process(context);
                return;
            }

            routeLookup[path[1]].Process(context);
        }

        private string GetRoutes()
        {
            string routes = "";
            foreach (KeyValuePair<string,IRouteHandler> routeHandler in routeLookup)
            {
                routes += routeHandler.Key + Environment.NewLine;
            }
            return routes;
        }

        private void Output(string output)
        {
            if (Log)
            {
                Console.WriteLine(output);
            }
        }
    }
}
