using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Slave.Model;
using System.Xml.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

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
                //Works as a normal TCP connection 
                    //Console.WriteLine("Client: " + message);
                    //sw.WriteLine(message);
                    //Console.ReadLine();

                //Reciec
                Console.WriteLine("Receiving data");
                IList<UserInfo> receivedDataString = JsonConvert.DeserializeObject<IList<UserInfo>>(message);
                Console.WriteLine(receivedDataString);

                foreach (var item in receivedDataString)
                {
                    Console.WriteLine(item);
                }

                //Sends back an confirmation
                sw.WriteLine("ok");

                //Confirmes chunck size
                string confirmChunckSize = sr.ReadLine();
                sw.WriteLine($"chucnk size is set to: {confirmChunckSize}");

                foreach (var item in receivedDataString)
                {
                    Console.WriteLine();
                }

                Console.ReadLine();
            }
            ns.Close();
            connectionSocket.Close();
        }
    }
}
