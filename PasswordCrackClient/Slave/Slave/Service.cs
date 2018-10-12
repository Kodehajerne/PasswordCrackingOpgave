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
        private List<UserInfo> listOfUserInfo;
        private UserInfo userInfo;
        private BinaryFormatter binaryFormatter = new BinaryFormatter();
        private XmlSerializer xmlSerializer = new XmlSerializer(typeof(UserInfo));

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

                Console.WriteLine("Receiving data");
                List<UserInfo> receivedDataString =  (List<UserInfo>) JsonConvert.DeserializeObject(message);
                Console.WriteLine(receivedDataString);

                // We split the string arrays on ":" dividing it: username : password.
                //listOfUserInfo.Add(new UserInfo(receivedDataString.SelectToken("Username").ToString()
                //    , receivedDataString.SelectToken("EncryptedPasswordBase64").ToString()));

                foreach (var item in receivedDataString)
                {
                    Console.WriteLine(item);
                }

                //Sends back an confirmation
                sw.WriteLine("ok");

               



                Console.ReadLine();
            }
            ns.Close();
            connectionSocket.Close();
        }
    }
}
