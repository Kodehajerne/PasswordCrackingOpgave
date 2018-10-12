using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Slave.Model;
using Slave.CrackingMethods;
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
        private IList<UserInfo> _receivedDataString;
        int _confirmChunckSize;
        public Service(TcpClient connectionSocket)
        {
            // TODO: Complete member initialization
            this.connectionSocket = connectionSocket;
        }

        public void EnvokeCracking()
        {
            Stream ns = connectionSocket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true; // enable automatic flushing

            string message = sr.ReadLine();
            while (message != null && message != "")
            {
                if (message == "Start")
                {
                    for (int i = 0; i <= _confirmChunckSize; i++)
                    {

                    }
                    Cracking cracker = new Cracking();
                    cracker.RunCracking();
                    Console.WriteLine("Cracking is started");
                }
                else { throw new ArgumentException(); }
            }
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
                //Recieve data from Master
                Console.WriteLine("Receiving data");
                _receivedDataString = JsonConvert.DeserializeObject<IList<UserInfo>>(message);
                //Showing data
                foreach (var item in _receivedDataString)
                {
                    Console.WriteLine(item);
                }

                //Sends back an confirmation
                sw.WriteLine("ok");

                //Confirmes chunck size
                _confirmChunckSize = Convert.ToInt32(sr.ReadLine());
                sw.WriteLine($"chucnk size is set to: {_confirmChunckSize}");

                foreach (var item in _receivedDataString)
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

