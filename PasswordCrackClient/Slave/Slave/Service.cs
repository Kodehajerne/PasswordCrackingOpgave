using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.IO;
using Slave.Model;
using Slave.Util;
using Slave.CrackingMethods;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Slave
{
    /// <summary>
    /// Service class controll all the thing related to comunication. 
    /// </summary>
    public class Service
    {
        private TcpClient connectionSocket;
        private IList<UserInfo> _receivedDataString;
        int _confirmChunckSize;
        private List<string> arrayOfName = new List<string>();  //Contains alle usernames 
        private List<string> arrayOfPassword = new List<string>();
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
                _receivedDataString = JsonConvert.DeserializeObject<List<UserInfo>>(message);
               
                foreach (var item in _receivedDataString)
                {
                    string[] arrayUSerAndPAss = item.ToString().Split(':');
                    string name = arrayUSerAndPAss[0];
                    string password = arrayUSerAndPAss[1];
                    arrayOfName.Add(name);
                    arrayOfPassword.Add(password);
                }

                //Writes a passwordfile and saves it in the debug folder
                PasswordFileHandler.WritePasswordFile("PasswordCreatedFile", arrayOfName.ToArray(), arrayOfPassword.ToArray());



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

