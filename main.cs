using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task {
    class main {
        public static void Main() {
            string[] prefixes = { "http://localhost:8080/" };
            new WebServer(prefixes);
            while(true) {
            }
        }
    }
}