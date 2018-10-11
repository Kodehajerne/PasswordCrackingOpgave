using Master.Master_Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Master
{
    class Master
    {
        static void Main(string[] args)
        {
            MasterServer master = new MasterServer();
            master.Start();

            Console.ReadLine();
        }
    }
}

