using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Model
{
    public class SlaveInfo
    {
        private string _ip;
            private int _port;

        public SlaveInfo(string ip, int port)
        {
            _ip = ip;
            _port = port;
        }

        public string getIp
        {
            get { return _ip; }
            set { _ip = value; }
        }
        public int getPort
        {
            get { return _port; }
            set { _port = value; }
        }
    }
}
