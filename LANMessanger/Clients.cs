using System.Net.Sockets;
using System.Net;

namespace LANMessanger
{
    internal class Clients
    {
        private string user_name = "Unknown";
        private int user_port = 27000; 
        private string user_ipaddress = "127.0.0.1";
        
        public Clients(string name, int port)
        {
            user_name = name;
            user_port = port;
            user_ipaddress = generate_user_ipaddress();
        }

        public string get_user_name(int port, string ipaddress)
        {
            if (port != user_port && ipaddress != user_ipaddress)
                return "ERROR: User does not exist. IP: " + ipaddress + ", Port: " + port;

            return user_name;
        }
        public string get_user_ipaddress(int port, string name)
        {
            if (port != user_port && name != user_name)
                return "ERROR: User does not exist. Name: " + name + ", Port: " + port;

            return user_ipaddress;
        }
        public bool set_user_name(int port, string ipaddress, string name)
        {
            if (port != user_port && ipaddress != user_ipaddress)
                return false;

            user_name = name;
            return true;
        }
        public bool set_user_ipaddress(int port, string name, string ipaddress)
        {
            if (port != user_port && name != user_name)
                return false;

            user_ipaddress = ipaddress;
            return true;
        }
        private string generate_user_ipaddress()
        {
            string local_ipaddress = "", hostname = Dns.GetHostName();
            IPAddress[] ipadresses = Dns.GetHostAddresses(hostname);

            foreach (var ip in ipadresses)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork && !IPAddress.IsLoopback(ip))
                {
                    local_ipaddress = ip.ToString();
                    break;
                }
            }

            return local_ipaddress;
        }
    }
}
