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

namespace Master.Master_Server
{
    class MasterServer
    {
        public void Start()
        {
            //We reads all the password and username Pairs and saves them to a list<UserInfo>
            List<UserInfo> list = PasswordFileHandler.ReadPasswordFile("passwords.txt");
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(UserInfo));

            ////Take the list and turnes it into a byte[]
            //var binFormatter = new BinaryFormatter();
            //var mStream = new MemoryStream();
            //binFormatter.Serialize(mStream, list);

            ////This gives you the byte array.
            //var SendListAsBytes = mStream.ToArray();


            TcpClient clientSocket = new TcpClient("10.200.120.159", 1234);
            Console.WriteLine("Connection to slaves");

            Stream ns = clientSocket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;

            for (int i = 0; i < list.Count; i++)
            {
                foreach (var item in list)
                {
                    //binaryFormatter.Serialize(ns, "jonas");
                    string message = Console.ReadLine();
                    //sw.WriteLine("Jonas");
                    //ns.Flush();
                    //Console.WriteLine(item);
                    //i++;
                }


                //string serverAnswer = sr.ReadLine();

            }
            Console.WriteLine("No more from server. Press Enter");
            Console.ReadLine();

            ns.Close();
            //clientSocket.Close();
        }
    }
}

