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
            Console.WriteLine("Masters is connection to slaves");

            List<TcpClient> connections = new List<TcpClient>
            {
              new TcpClient("172.20.10.2", 6789), new TcpClient("172.20.10.2", 6789),
              new TcpClient("172.20.10.2", 6789),  new TcpClient("172.20.10.2", 6789)
            };

            foreach (var connection in connections)
            {
                Stream ns = connection.GetStream();
                StreamReader sr = new StreamReader(ns);
                StreamWriter sw = new StreamWriter(ns);

                for (int i = 0; i < 5; i++)
                {

                    string serverAnswer = sr.ReadLine();

                    Console.WriteLine("Server: " + serverAnswer);

                }
            }

            //Stream ns = clientSocket.GetStream();        
            //StreamReader sr = new StreamReader(ns);      
            //StreamWriter sw = new StreamWriter(ns);     
            //sw.AutoFlush = true;                        

            Console.WriteLine("No more from server. Press Enter");
            Console.ReadLine();

            ns.Close();
            clientSocket.Close();
        }
        
    }
}

