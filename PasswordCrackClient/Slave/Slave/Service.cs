using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Slave
{
    /// <summary>
    /// Service class controll all the thing related to comunication. 
    /// </summary>
    class Service
    {
        private TcpClient connectionSocket;

        public Service(TcpClient connectionSocket)
        {
            // TODO: Complete member initialization
            this.connectionSocket = connectionSocket;
        }

        internal void DoIt()
        {
            Stream ns = connectionSocket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true; // enable automatic flushing

            string message = sr.ReadLine();
            while (message != null && message != "")
            {
                foreach (var item in message)
                {
                    Console.WriteLine(item);
                }
                sw.WriteLine("ok");
            }
            ns.Close();
            connectionSocket.Close();
        }
    }
}
