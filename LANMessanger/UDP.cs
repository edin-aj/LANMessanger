using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LANMessanger
{
    internal class UDP
    {
        private UdpClient udp_client;
        private int connection_port = 27000;
        private ChatRoom client_room;

        public UDP(int port, ChatRoom room)
        {
            client_room = room;

            udp_client = new UdpClient();
            udp_client.EnableBroadcast = true;

            IPEndPoint local_endpoint = new IPEndPoint(IPAddress.Any, connection_port = port);
            udp_client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            udp_client.Client.Bind(local_endpoint);
            Task.Run(() => start_listening_chats());
        }
        public UdpClient get_connection_client()
        {
            return udp_client;
        }
        public int get_connection_port()
        {
            return connection_port;
        }
        public void client_close_connection()
        {
            udp_client?.Dispose();
            udp_client?.Close();
        }
        public void send_listening_chats(string text)
        {
            try
            {
                if (udp_client == null || udp_client.Client == null)
                    return;

                byte[] data = Encoding.UTF8.GetBytes(text);
                IPEndPoint multicast_endpoint = new IPEndPoint(IPAddress.Broadcast, connection_port);
                udp_client.Send(data, data.Length, multicast_endpoint);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending message: {ex.Message}");
            }
        }
        private void start_listening_chats()
        {
            while (true)
            {
                try
                {
                    IPEndPoint remote_endpoint = new IPEndPoint(IPAddress.Any, connection_port);
                    byte[] data = udp_client.Receive(ref remote_endpoint);
                    string received_message = Encoding.UTF8.GetString(data);
                    client_room.receive_data(received_message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error sending message: {ex.Message}");
                }
            }
        }
    }
}
