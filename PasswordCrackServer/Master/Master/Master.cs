using Master.Master_Server;
using Master.Model;
using Master.Util;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Master
{
    class Master
    {
        static void Main(string[] args)
        {
            MasterServer master = new MasterServer();
            //master.StartConnection("172.20.10.6", 1111);
            ////Console.ReadLine();
            //master.StartConnection("172.20.10.7", 7777);



            //Task.Run(( ) => master.StartConnection("172.20.10.6", 1111));
            ////Console.ReadLine();
            //Task.Run(( ) => master.StartConnection("172.20.10.7", 7777));

            Task.Factory.StartNew(() => master.StartConnection("172.20.10.6", 1111));
            Console.WriteLine("TestTEst");
            Task.Factory.StartNew(() => master.StartConnection("172.20.10.7", 7777));


            Console.ReadLine();
        }
    }
}

