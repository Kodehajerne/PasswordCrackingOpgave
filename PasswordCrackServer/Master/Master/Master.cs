using Master.Master_Server;
using Master.Model;
using Master.Util;
using System;
using System.Collections.Generic;

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

