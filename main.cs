using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task {
    class main {
        public static void Main() {
            Log log = Log.getInstance();

            new WebServer(AppDomain.CurrentDomain.BaseDirectory + "htdocs\\" ,8080);
            while(true) {
            }
        }
    }
}