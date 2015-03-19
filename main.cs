using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task {
    class main {
        public static void Main() {
            string[] prefixes = { "http://*:8080/" };
            new WebServer(prefixes);
            while(true) {
            }
        }
    }
}