using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Task {
    class WebServer {
        Log log = Log.getInstance();
        HttpListener listener;

        public WebServer(string[] prefixes) {
            try {
                if(!HttpListener.IsSupported) {
                    log.debugWrite("[WebServer] Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                    return;
                }
                if(prefixes == null || prefixes.Length == 0)
                    throw new ArgumentException("missing prefixes");

                listener = new HttpListener();

                foreach(string s in prefixes) {
                    listener.Prefixes.Add(s);
                    log.debugWrite("[WebServer] listen on " + s);
                }

                listener.Start();
                log.debugWrite("[WebServer] Listening");

                listener.BeginGetContext(new AsyncCallback(ListenerCallback), listener);
            }
            catch(Exception e) {
                log.debugWrite("[WebServer] " + e.ToString());
            }
        }

        private void ListenerCallback(IAsyncResult Result) {
            try {
                HttpListenerContext context = listener.EndGetContext(Result);
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(createResponse());
                response.ContentLength64 = buffer.Length;

                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
            catch(InvalidOperationException eR) {
                Console.WriteLine("[WebServer]" + eR.ToString());
            }
            try {
                listener.BeginGetContext(new AsyncCallback(ListenerCallback), listener);
            }
            catch(InvalidOperationException eR) {
                Console.WriteLine("[WebServer]" + eR.ToString());
            }
        }

        private string createResponse() {
            string response;
            response = "<HTML><HEAD><TITLE>WebServer</TITLE></HEAD><BODY><b>Hello</b> Task!</BODY></HTML>";
            return response;
        }

        ~WebServer() {
            listener.Stop();
        }
    }
}
