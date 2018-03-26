using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebServer
{
    class HttpServer
    {
        private readonly HttpListener listener;
        private readonly Func<HttpListenerRequest, string> responderMethod;

        public bool Verbose { get; set; }

        public HttpServer(string[] prefixes, Func<HttpListenerRequest, string> responderMethod)
        {
            if(prefixes == null || prefixes.Length == 0)
            {
                throw new ArgumentException("Must have prefixes");
            }

            if(responderMethod == null)
            {
                throw new ArgumentException("Must have a responder method");
            }

            listener = new HttpListener();
            this.responderMethod = responderMethod;

            foreach (string prefix in prefixes)
            {
                listener.Prefixes.Add(prefix);
            }
            
        }

        public void Start()
        {
            Task.Run(() =>
            {
                listener.Start();
                if (this.Verbose)
                {
                    Console.WriteLine("Webserver running...");
                }

                try
                {
                    Task listeningTask = null;
                    do
                    {
                        listeningTask = Task.Run(() =>
                        {
                            if (this.Verbose)
                            {
                                Console.WriteLine("New listening task");
                            }
                            HttpListenerContext context = listener.GetContext();
                            try
                            {
                                string rstr = responderMethod(context.Request);
                                byte[] buf = Encoding.UTF8.GetBytes(rstr);
                                context.Response.ContentLength64 = buf.Length;
                                context.Response.OutputStream.Write(buf, 0, buf.Length);
                                if (this.Verbose)
                                {
                                    Console.WriteLine("Sent " + buf.Length + " bytes");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("EXCEPTION: " + ex.Message);
                            }
                            finally
                            {
                                // always close the stream
                                context.Response.OutputStream.Close();
                            }
                        });
                    } while (listener.IsListening && (listeningTask == null || listeningTask.IsCompleted));
                }
                catch { } // suppress any exceptions

                Console.WriteLine("Server Closed");
            });
        }

        public void Stop()
        {
            if (this.Verbose)
            {
                Console.WriteLine("Server shutdown");
            }
            listener.Stop();
            listener.Close();
        }
    }
}
