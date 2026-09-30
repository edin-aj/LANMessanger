using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows.Forms;

namespace LANMessanger
{
    public partial class Form1 : Form
    {
        public void centerize_elements()
        {
            this.label1.Left = (this.ClientSize.Width - this.label1.Width) / 2;
            this.textUser.Left = (this.ClientSize.Width - this.textUser.Width) / 2;
            this.buttonConnect.Left = (this.ClientSize.Width - this.buttonConnect.Width) / 2;
            this.label2.Left = (this.ClientSize.Width - this.label2.Width) / 2;
            this.label3.Left = (this.ClientSize.Width - this.label3.Width) / 2;
            this.label4.Left = (this.ClientSize.Width - this.label4.Width) / 2;
            this.pictureBox1.Left = (this.ClientSize.Width - this.pictureBox1.Width) / 2;
        }
        public Form1()
        {
            InitializeComponent();
            centerize_elements();

            if(!is_subnet_communicable())
            {
                MessageBox.Show("Your subnet mask is not communicable, you wont be able chat with specific devices in other subnets!", "WARNING!!!", MessageBoxButtons.OK);
            }
        }
        private bool is_subnet_communicable()
        {
            foreach (NetworkInterface network_interface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (network_interface.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip_info in network_interface.GetIPProperties().UnicastAddresses)
                    {
                        if (ip_info.Address.AddressFamily == AddressFamily.InterNetwork)
                            if (ip_info.IPv4Mask.ToString() == "255.255.0.0")
                                return true;
                    }
                }
            }
            return false;
        }
        private bool is_user_connected()
        {
            foreach (NetworkInterface network_interface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (network_interface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 && network_interface.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in network_interface.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                            return true;
                    }
                }
            }
            return false;
        }
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textUser.Text))
            {
                MessageBox.Show("You did not provide a name!", "Warning");
                return;
            }
            if(textUser.Text.Contains(' '))
            {
                MessageBox.Show("Make sure your name is a one word name, we dont allow spaces!", "Warning");
                return;
            }
            if(textUser.Text.Any(character => !char.IsLetterOrDigit(character) || char.IsPunctuation(character)))
            {
                MessageBox.Show("We dont allow symbols or punctuations in names, please rewrite your name!", "Warning");
                return;
            }

            label4.Text = "Connecting...";
            label4.Left = (this.ClientSize.Width - this.label4.Width) / 2;
            label4.Refresh();

            textUser.ReadOnly = true;
            buttonConnect.Enabled = false;

            if(!is_user_connected())
            {
                MessageBox.Show("You are not connected to a WiFi network, please try again!", "Warning!", MessageBoxButtons.OK);

                label4.Text = "";
                textUser.ReadOnly = false;
                buttonConnect.Enabled = true;
                return;
            }

            ChatRoom window = new ChatRoom(this, textUser.Text);
            window.Show();
        }
    }
}
