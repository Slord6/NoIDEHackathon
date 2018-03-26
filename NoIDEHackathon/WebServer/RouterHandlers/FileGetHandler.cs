using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.RouterHandlers
{
    class FileGetHandler : DefaultRouteHandler
    {
        protected string directory;

        public FileGetHandler(string defaultMessage, string workingDirectory) : base(defaultMessage)
        {
            if (workingDirectory[workingDirectory.Length - 1] != '/')
            {
                workingDirectory += "/";
            }

            this.directory = workingDirectory;
        }

        public override void Process(HttpListenerContext context)
        {
            try
            {
                string filePath = GetFilePath(GetFileName(context.Request.Url.AbsolutePath));

                if (File.Exists(filePath))
                {
                    string[] fileNameSplit = GetFileNameSplit(context.Request.Url.AbsolutePath);
                    if (fileNameSplit.Length < 2)
                    {
                        ResponseAsString("Could not return file as no extension was provided", context, 406);
                        return;
                    }
                    string extension = "." + fileNameSplit[1];
                    ResponseFile(File.ReadAllBytes(filePath), MimeLookup.Lookup(extension), context);
                }
                else
                {
                    ResponseAsString("No file found named " + GetFileName(context.Request.Url.AbsolutePath) + " in the working directory", context, 404);
                }
            }
            catch (Exception ex)
            {
                ResponseAsString("Couldn't deliver request due to " + ex.GetType().ToString(), context, 500);
            }
        }

        protected string GetFilePath(string fileName)
        {
            return directory + fileName;
        }

        protected string GetFileName(string absolutePath)
        {
            string[] fullPath = absolutePath.Split('/');
            string filePath = "";

            for (int i = 2; i < fullPath.Length; i++)
            {
                filePath += fullPath[i];
                if (i <= fullPath.Length - 2)
                {
                    filePath += "\\";
                }
            }
            return filePath;
        }
        
        protected string[] GetFileNameSplit(string absolutePath)
        {
            return GetFileName(absolutePath).Split('.');
        }

        protected virtual void ResponseFile(byte[] file, string mimeType, HttpListenerContext context)
        {
            if (Log)
            {
                Console.WriteLine("Sending file response (" + file.Length + " bytes, " + mimeType + ") for " + context.Request.RemoteEndPoint);
            }
            context.Response.ContentLength64 = file.Length;
            context.Response.ContentType = mimeType;
            context.Response.OutputStream.Write(file, 0, file.Length);
            context.Response.StatusCode = 200;
        }
    }
}
