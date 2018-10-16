using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Slave
{
    class SlaveServer
    {
        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse("192.168.43.238");
            TcpListener serverSocket = new TcpListener(ip, 1234);

            serverSocket.Start();


            while (true)
            {
                TcpClient connectionSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine("Server activated now");
                Service service = new Service(connectionSocket);

                Task.Factory.StartNew(() => service.DoIt());
                
            }

            serverSocket.Stop();
        }
    }
}
