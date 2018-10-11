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
            IPAddress ip = IPAddress.Parse("10.200.123.60");
            TcpListener serverSocket = new TcpListener(ip, 7777);

            //TcpListener serverSocket = new TcpListener(6789);
            serverSocket.Start();


            while (true)
            {
                TcpClient connectionSocket = serverSocket.AcceptTcpClient();
                Service service = new Service(connectionSocket);
                Task.Factory.StartNew(() => service.DoIt());
            }

            serverSocket.Stop();
        }
    }
}
