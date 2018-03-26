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
        private readonly Action<HttpListenerContext> responderMethod;

        public bool Log { get; set; }

        public HttpServer(string[] prefixes, Action<HttpListenerContext> responderMethod)
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
                Output("Webserver running...");

                try
                {
                    Task listeningTask = null;
                    do
                    {
                        if(listeningTask != null)
                        {
                            listeningTask.Wait();
                            if (listeningTask.Exception != null)
                            {
                                Output("Closed");
                                return;
                            }
                        }

                        listeningTask = Task.Run(() =>
                        {
                            Output("New listening task");
                            
                            HttpListenerContext context = null;
                            try
                            {
                                context = listener.GetContext();
                                responderMethod(context);
                            }
                            catch (Exception ex)
                            {
                                Output("EXCEPTION: " + ex.Message);
                            }
                            finally
                            {
                                // always close the stream
                                if (context != null)
                                {
                                    context.Response.OutputStream.Close();
                                }
                            }
                        });
                    } while (listener.IsListening);
                }
                catch (Exception ex)
                {
                    Output("EXCEPTION: " + ex.Message);
                }

                Console.WriteLine("Server Closed");
            });
        }

        public void Stop()
        {
            Output("Server shutdown");
            
            listener.Stop();
        }

        private void Output(string value)
        {
            if (Log)
            {
                Console.WriteLine(value);
            }
        }
    }
}
