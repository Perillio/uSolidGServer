using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uSolidGServer
{
    static class General
    {
        public static void InitializeServer()
        {
            ServerTCP.initializeNetwork();
            Console.WriteLine("Server started !");
        }
    }
}
