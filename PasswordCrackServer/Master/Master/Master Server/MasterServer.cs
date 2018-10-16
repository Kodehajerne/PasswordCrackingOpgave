using Master.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Master.Util;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Master.Master_Server
{
    class MasterServer
    {


        public void StartConnection()
        {
            //We reads all the password and username Pairs and saves them to a list<UserInfo>
            List<UserInfo> list = PasswordFileHandler.ReadPasswordFile("passwords.txt");
            int chucnkSize = 10000; //adjust after need 

            //Creating a TcpClient
            TcpClient clientSocket = new TcpClient("10.200.120.16", 1234);
            Console.WriteLine("Connection to slaves");

            Stream ns = clientSocket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;

            for (int i = 0; i < 1; i++)
            {
                //Sends a hole list
                var SendList = JsonConvert.SerializeObject(list);  
                sw.WriteLine(SendList);                            
                Console.WriteLine("Brugerlist er sendt");

                //Confirm message from slaves
                string message = sr.ReadLine();
                Console.WriteLine(message);

                ////sends chunck size to slaves.
                sw.WriteLine(chucnkSize);
                string confirmChunckSize = sr.ReadLine();
                Console.WriteLine(confirmChunckSize);

                Console.WriteLine("communication established");
            }


            //Start Cracking 
            Console.WriteLine("-----------------------");
            Console.WriteLine("Type 'Start' to crack");
            string commandStartCrack = Console.ReadLine(); ;
            sw.WriteLine(commandStartCrack);

            Console.WriteLine("--- Cracking is running, please wait ---");
            string result = sr.ReadLine();
            Console.WriteLine(result);

            IList<UserInfo> resivedResult = JsonConvert.DeserializeObject<List<UserInfo>>(result);

            foreach (var item in resivedResult)
            {
                item.ToString();
            }

            Console.WriteLine("Done");
            Console.WriteLine("No more from server. Press Enter");
            Console.ReadLine();

            ns.Close();
        }
    }
}

