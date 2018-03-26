using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.RouterHandlers
{
    class FilePutHandler : FileGetHandler
    {
        public FilePutHandler(string defaultResponse, string workingDirectory) : base(defaultResponse, workingDirectory)
        {
        }

        public override void Process(HttpListenerContext context)
        {
            if (context.Request.HttpMethod == "PUT")
            {
                if (Log)
                {
                    Console.WriteLine("Receiving file from " + context.Request.RemoteEndPoint);
                }

                Stream body = context.Request.InputStream;
                Encoding encoding = context.Request.ContentEncoding;
                BinaryReader reader = new System.IO.BinaryReader(body, encoding);
                if (Log)
                {
                    if (context.Request.ContentType != null)
                    {
                        Console.WriteLine("Client data content type {0}", context.Request.ContentType);
                    }
                    Console.WriteLine("Client data content length {0}", context.Request.ContentLength64);
                    
                    
                }
                byte[] allData = reader.ReadBytes((int)context.Request.ContentLength64);

                if(allData.Length == 0)
                {
                    ResponseAsString("Empty files are not permitted", context, 403);
                    return;
                }

                body.Close();
                reader.Close();

                string filePath = GetFilePath(GetFileName(context.Request.Url.AbsolutePath));
                int httpCode = 201;
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    httpCode = 200;
                }
                File.WriteAllBytes(filePath, allData);
                ResponseAsString("", context, httpCode);
            }
            else
            {
                ResponseAsString("PUT method must be used to upload files to this endpoint", context, 405);
            }


        }
    }
}
