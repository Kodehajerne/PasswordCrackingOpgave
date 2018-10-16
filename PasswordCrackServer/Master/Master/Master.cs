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
            //master.StartConnection("172.20.10.7", 6789);
            //master.StartConnection("172.20.10.6", 7777);
            Task.Factory.StartNew(() => master.StartConnection("172.20.10.6", 7777));
            Task.Factory.StartNew(() => master.StartConnection("172.20.10.7", 6789));


            Console.ReadLine();
        }
    }
}

