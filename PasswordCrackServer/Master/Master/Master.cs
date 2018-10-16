using Master.Master_Server;
using Master.Model;
using Master.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Master
{
    class Master
    {
        static void Main(string[] args)
        {
            MasterServer master = new MasterServer();
            //master.StartConnection("10.200.120.16");
            master.StartConnection();
            //Task.Factory.StartNew(() => master.StartConnection("10.200.120.16"));


            Console.ReadLine();
        }
    }
}

