using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Task {
    class WebServer {
        WebServer(string[] prefixes) {
            if(!HttpListener.IsSupported) {
                Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }
            if(prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes");

            HttpListener listener = new HttpListener();
            foreach(string s in prefixes) {
                listener.Prefixes.Add(s);
            }
            listener.Start();

            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            string responseString = "<HTML><BODY> Hello Task!</BODY></HTML>";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);

            output.Close();
            listener.Stop();
        }

        ~WebServer() {
        }
    }
}
