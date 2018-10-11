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
            List<UserInfo> list = PasswordFileHandler.ReadPasswordFile("passwords");

            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());
            }

             MasterServer master = new MasterServer();
            master.Start();

            Console.ReadLine();
        }
    }
}

