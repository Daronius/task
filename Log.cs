using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task {
    class Log {
        private volatile static Log uniqueInstance;
        private static readonly object padlock = new object();

        private Log() {
            debugWrite("[Log] Log created.");
        }

        public static Log getInstance()
        {
            if (uniqueInstance == null)
            {
                lock (padlock)
                {
                    if (uniqueInstance == null)
                    {
                        uniqueInstance = new Log();
                    }
                }
            }
            return uniqueInstance;
        }

        public void debugWrite(string message) {
            Console.WriteLine(message);
        }
    }
}
