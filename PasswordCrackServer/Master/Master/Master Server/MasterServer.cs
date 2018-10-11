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

namespace Master.Master_Server
{
    class MasterServer
    {
        public void Start()
        {
            //int counter = 0;   Måske en gode ide, så man kan se hvor mange man har connecet til?

            //We reads all the password and username Pairs and saves them to a list<UserInfo>
            List<UserInfo> list = PasswordFileHandler.ReadPasswordFile("passwords.txt");

            //Take the list and turnes it into a byte[]
            var binFormatter = new BinaryFormatter();
            var mStream = new MemoryStream();
            binFormatter.Serialize(mStream, list);

            //This gives you the byte array.
            var SendListAsBytes = mStream.ToArray();


            TcpClient clientSocket = new TcpClient("10.200.123.60", 7777);
            Console.WriteLine("Connection to slaves");

            Stream ns = clientSocket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;

            while (true)
            {
                
                sw.WriteLine(SendListAsBytes);
                //string serverAnswer = sr.ReadLine();

            }
            Console.WriteLine("No more from server. Press Enter");
            Console.ReadLine();

            ns.Close();
            //clientSocket.Close();
        }
    }
}

