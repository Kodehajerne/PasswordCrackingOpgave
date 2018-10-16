using Master.Controller;
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
            List<SlaveInfo> slaveInfos = new List<SlaveInfo>
            { new SlaveInfo("Ian", "192.168.1.15", 1234),
                new SlaveInfo("Christoffer", "100.200.30.2", 7777)};

            foreach (var item in slaveInfos)
            {
                MasterServer master = new MasterServer();
                Task.Factory.StartNew(() => master.StartConnection(item.getIp, item.getPort));
            }
            Console.ReadLine();

            //Hvis ovenstående ikke virker, prøv at lave flere Mastere, da der er protocollen. Grunden til at 
            //Forbindelsen blev tvangs afbrudt er nok fordi vi kun brugte en?! prøv det!  se eks. OBS! det skal tilrettes. 
            //MasterServer master = new MasterServer();
            //MasterServer master1 = new MasterServer();
            //Task.Factory.StartNew(() => master.StartConnection("172.20.10.6", 1111));
            //Task.Factory.StartNew(() => master1.StartConnection("172.20.10.7", 7777));


            //MasterServer master = new MasterServer();
            //master.StartConnection("172.20.10.6", 1111);
            //master.StartConnection("172.20.10.7", 7777);

            //Task.Run(( ) => master.StartConnection("172.20.10.6", 1111));
            ////Console.ReadLine();
            //Task.Run(( ) => master.StartConnection("172.20.10.7", 7777));

            //Task.Factory.StartNew(() => master.StartConnection("172.20.10.6", 1111));
            //Task.Factory.StartNew(() => master.StartConnection("172.20.10.7", 7777));

            //Console.ReadLine();
        }
    }
}

