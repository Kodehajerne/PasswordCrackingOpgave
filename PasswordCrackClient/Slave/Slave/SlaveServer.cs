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
            IPAddress ip = IPAddress.Parse("10.200.120.159");
            TcpListener serverSocket = new TcpListener(ip, 1234);

            serverSocket.Start();


            while (true)
            {
                TcpClient connectionSocket = serverSocket.AcceptTcpClient();
                Service service = new Service(connectionSocket);
                Task.Factory.StartNew(() => service.DoIt());

                //Start cracking method. 
            }

            serverSocket.Stop();
        }
    }
}
