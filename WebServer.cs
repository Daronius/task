using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Task {
    class WebServer {
        HttpListener listener;

        public WebServer(string[] prefixes) {
            if(!HttpListener.IsSupported) {
                Console.WriteLine("[WebServer] Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }
            if(prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("missing prefixes");

            listener = new HttpListener();
            foreach(string s in prefixes) {
                listener.Prefixes.Add(s);
            }
            listener.Start();

            listener.BeginGetContext(new AsyncCallback(ListenerCallback), listener);
        }

        private string getResponse() {
            string response;
            response = "<HTML><HEAD><TITLE>WebServer</TITLE></HEAD><BODY><b>Hello</b> Task!</BODY></HTML>";
            return response;
        }

        private void ListenerCallback(IAsyncResult Result) {
            try {
                HttpListenerContext context = listener.EndGetContext(Result);
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(getResponse());
                response.ContentLength64 = buffer.Length;

                System.IO.Stream output = response.OutputStream;
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

        ~WebServer() {
            listener.Stop();
        }
    }
}
