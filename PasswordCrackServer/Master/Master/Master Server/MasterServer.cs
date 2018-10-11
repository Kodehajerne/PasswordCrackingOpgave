using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Master.Master_Server
{
    class MasterServer
    {
        public void Start()
        {
            //int counter = 0;   Måske en gode ide, så man kan se hvor mange man har connecet til?

            TcpClient clientSocket = new TcpClient("10.200.123.60", 7777);
            TcpClient clientSocket1 = new TcpClient("10.200.123.60", 7777);
            Console.WriteLine("Connection to slaves");

            Stream ns = clientSocket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;

            Stream ns1 = clientSocket1.GetStream();
            StreamReader sr1 = new StreamReader(ns);
            StreamWriter sw1 = new StreamWriter(ns);
            sw1.AutoFlush = true;

            while (true)
            {

                string message = Console.ReadLine();

                sw.WriteLine(message);
                string serverAnswer = sr.ReadLine();

                Console.WriteLine("Server: " + serverAnswer);

            }
            Console.WriteLine("No more from server. Press Enter");
            Console.ReadLine();

            ns.Close();
            //clientSocket.Close();
        }
    }
}

