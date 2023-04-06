using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFileServer
{
    class clsTest
    {
        static void Main()
        {

            Server s = new Server(1234);
            s.StartServer();

            Console.Read();
            s.StopServer();
        }
    }
}
