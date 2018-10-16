using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Model
{
    public class SlaveInfo
    {
        private string _name;
        private string _ip;
        private int _port;

        public SlaveInfo(string name, string ip, int port)
        {
            _name = name;
            _ip = ip;
            _port = port;
        }

        public string getName
        {
            get { return _ip; }
            set { _ip = value; }
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
