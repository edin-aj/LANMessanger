using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LANMessanger
{
    public partial class ChatRoom : Form
    {
        private int old_width = 0, old_height = 0, user_port = 27000;
        private Form parent; private bool bugfix_screen_layout = false;
        private String user_name = "Unknown", previous_text = "";
        private Clients client; private UDP udp_connection;
        private bool just_joined = true;

        public ChatRoom(Form Form1, String user)
        {
            InitializeComponent();

            parent = Form1;
            user_name = user;

            this.FormClosing += ChatRoom_FormClosing;
            this.SizeChanged += ChatRoom_SizeChanged;
            this.Load += new EventHandler(ChatRoom_Load);

            old_width = this.ClientSize.Width;
            old_height = this.ClientSize.Height;

            label_search.Left = textConnected.Left + ((textConnected.Width - label_search.Width) / 2);
            label_search.Refresh();

            client = new Clients(user_name, user_port);
            udp_connection = new UDP(user_port, this);
        }

        public void update_label_users()
        {
            label_users.Text = "Active users: " + (textConnected.Lines.Length - 1);
            label_users.Left = textConnected.Left + (label_users.Width / 5);
            label_users.Refresh();
        }
        public void resize_elements()
        {
            // When i minimized the winform, for some reason it screws up the size of the elements
            if (bugfix_screen_layout)
            {
                bugfix_screen_layout = false;
                return;
            }
            if (this.ClientSize.Width == this.ClientSize.Height && this.ClientSize.Width == 0)
            {
                bugfix_screen_layout = true;
                return;
            }

            int new_width = this.ClientSize.Width, new_height = this.ClientSize.Height, counter = 0;

            if (new_width > old_width) counter = new_width - old_width;
            else if (new_width < old_width) counter -= old_width - new_width;

            textMessages.Width += counter;
            this.textUser.Width += counter;
            buttonSendText.Left += counter;

            counter = 0;

            if (new_height > old_height) counter = new_height - old_height;
            else if (new_height < old_height) counter -= old_height - new_height;

            textMessages.Height += counter;
            buttonSendText.Top += counter;
        }
        private void ChatRoom_Load(object sender, System.EventArgs e)
        {
            udp_connection.send_listening_chats("CHECK_AVAILABILITY@" + user_name + "@" + client.get_user_ipaddress(user_port, user_name));
            parent.Hide();

            if (just_joined != true)
                return;

            udp_connection.send_listening_chats("LOOP_USERS@" + user_name);
            udp_connection.send_listening_chats("CONNECTED@" + user_name);

            just_joined = false;
        }
        private string list_format_names(List<string> names)
        {
            if (names.Count == 0) 
                return "";

            string buffer;
            switch (names.Count)
            {
                case 1:
                    buffer = names[0];
                    break;
                case 2:
                    buffer = $"{names[0]} and {names[1]}";
                    break;
                default:
                    buffer = string.Join(", ", names.Take(names.Count - 1)) + $" and {names.Last()}";
                    break;
            }

            return $"Typing: {buffer}...";
        }
        public void receive_data(string message)
        {
            string[] parts = message.Split('@');

            if (parts.Length < 2)
                return;

            switch (parts[0]) 
            {
                case "MESSAGE":
                    {
                        textMessages.SelectionColor = System.Drawing.Color.DarkGreen;
                        textMessages.AppendText("[" + DateTime.Now.ToString("hh:mm:ss tt") + "] ");
                        textMessages.SelectionColor = (parts[1] == user_name) ? System.Drawing.Color.Red : System.Drawing.Color.Blue;
                        textMessages.AppendText((parts[1] == user_name) ? "You: " : (parts[1] + ": "));
                        textMessages.SelectionColor = System.Drawing.Color.Black;
                        textMessages.AppendText(parts[2]);
                        textMessages.SelectionColor = textMessages.ForeColor;
                        textMessages.AppendText(Environment.NewLine);
                        break;
                    }
                case "DISCONNECTED":
                    {
                        textMessages.AppendText("User named '");
                        textMessages.SelectionColor = System.Drawing.Color.Red;
                        textMessages.AppendText(parts[1]);
                        textMessages.SelectionColor = System.Drawing.Color.Black;
                        textMessages.AppendText("' has been disconnected!");

                        if(parts.Length == 3)
                        {
                            textMessages.SelectionColor = System.Drawing.Color.Red;
                            textMessages.AppendText("\n" + parts[2]);
                        }

                        textMessages.AppendText(Environment.NewLine);

                        if(textConnected.Text.Contains(parts[1]))
                            textConnected.Text = textConnected.Text.Replace(parts[1], "");

                        update_label_users();
                        break;
                    }
                case "CONNECTED":
                    {
                        textMessages.AppendText("User named '");
                        textMessages.SelectionColor = System.Drawing.Color.Green;
                        textMessages.AppendText(parts[1]);
                        textMessages.SelectionColor = System.Drawing.Color.Black;
                        textMessages.AppendText("' has been connected!");
                        textMessages.AppendText(Environment.NewLine);

                        textConnected.AppendText(parts[1] + "\n");
                        update_label_users();
                        break;
                    }
                case "LOOP_USERS":
                    {
                        udp_connection.send_listening_chats("RETURN@" + parts[1] + "@" + user_name);
                        break;
                    }
                case "RETURN":
                    {
                        if (parts[1] == user_name && parts[2] != user_name)
                        {
                            textConnected.AppendText(parts[2]);
                            textConnected.AppendText(Environment.NewLine);
                            update_label_users();
                        }
                        break;
                    }
                case "CHECK_AVAILABILITY":
                    {
                        if (parts[2] == client.get_user_ipaddress(user_port, user_name) && just_joined == false)
                            udp_connection.send_listening_chats("KICK_USER_SAME_IP@" + parts[2]);

                        if (parts[1] == user_name && parts[2] != client.get_user_ipaddress(user_port, user_name))
                            udp_connection.send_listening_chats("KICK_USER_SAME_NAME@" + parts[2]);

                        break;
                    }
                case "KICK_USER_SAME_IP":
                    {
                        if (parts[1] == client.get_user_ipaddress(user_port, user_name) && just_joined == true)
                        {
                            just_joined = false;
                            udp_connection.send_listening_chats("DISCONNECTED@" + user_name + "@Tried joining while already having an instance of the application!");
                            MessageBox.Show(user_name + ", you cannot run more than one instance of the application!\nNow you will be kicked!", "Confirmation", MessageBoxButtons.OK);
                            udp_connection.client_close_connection();
                            Application.Exit();
                        }
                        break;
                    }
                case "KICK_USER_SAME_NAME":
                    {
                        if (parts[1] == client.get_user_ipaddress(user_port, user_name))
                        {
                            udp_connection.send_listening_chats("DISCONNECTED@" + user_name + "@Tried joining with someone elses name!");
                            MessageBox.Show(user_name + ", there is another person with your name!\nNow you will be kicked!", "Confirmation", MessageBoxButtons.OK); 
                            udp_connection.client_close_connection();
                            Application.Exit();
                        }
                        break;
                    }
                case "POKE_MESSAGE":
                    {
                        if (parts[1] == user_name)
                        {
                            textMessages.SelectionColor = System.Drawing.Color.Red;
                            textMessages.AppendText(parts[2]);
                            textMessages.SelectionColor = System.Drawing.Color.Black;
                            textMessages.AppendText(" poked you.");
                            textMessages.AppendText(Environment.NewLine);
                        }
                        break;
                    }
                case "TEXT_MESSAGE":
                    {
                        if (parts[1] == "UNTYPING")
                        {
                            string clean_text = label_typing.Text.Replace("Typing: ", "").Replace("...", "");

                            var currents = clean_text.Split(new[] { ", ", " and " }, StringSplitOptions.RemoveEmptyEntries).Select(name => name.Trim()).ToList();

                            string tobe_removed = parts[2].Trim();
                            currents.RemoveAll(name => name.Equals(tobe_removed, StringComparison.OrdinalIgnoreCase));

                            label_typing.Text = list_format_names(currents);
                        }
                        else if (parts[1] == "TYPING")
                        {
                            var current_names = label_typing.Text.Replace("Typing: ", "").Split(new[] { ", ", " and " }, StringSplitOptions.RemoveEmptyEntries).ToList();

                            if (!current_names.Any(name => name.Equals(parts[2], StringComparison.OrdinalIgnoreCase)))
                            {
                                current_names.Add(parts[2]);
                                label_typing.Text = list_format_names(current_names);
                            }
                        }
                        break;
                    }
                default:
                    {
                        textMessages.AppendText("Someone sent a corrupted message.");
                        break;
                    }
            }
            
        }
        private void ChatRoom_SizeChanged(object sender, EventArgs e)
        {
            resize_elements();
            old_width = this.ClientSize.Width;
            old_height = this.ClientSize.Height;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textSearch.Text)) {
                MessageBox.Show("Please write a valid message in your search box.", "Warning");
                return;
            }

            SearchResult buffer = new SearchResult(buttonSearch, textMessages, textSearch.Text);
            buffer.Show();
        }

        internal class context_render_color : ToolStripProfessionalRenderer
        {
            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                TextFormatFlags flags = e.TextFormat;

                flags &= ~(TextFormatFlags.Left | TextFormatFlags.Right | TextFormatFlags.HorizontalCenter | TextFormatFlags.Top | TextFormatFlags.Bottom | TextFormatFlags.VerticalCenter);
                flags |= TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
                e.TextFormat = flags;

                base.OnRenderItemText(e);
            }
            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                var rect = new Rectangle(Point.Empty, e.Item.Size);
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240)), rect);
                if (e.Item.Selected)
                    e.Graphics.FillRectangle(new SolidBrush(SystemColors.Highlight), rect);
            }
        }

        private void buttonPoke_Click(object sender, EventArgs e)
        {
            pokeMenuList.Items.Clear();

            //List<String> names = "pro\nabcasdsada\nbcd\ncde\ndefsadas\nefg\nfgh\nghi\nhij".Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(name => name.Trim()).ToList();
            List<String> names = textConnected.Text.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(name => name.Trim()).ToList();

            if (names.Count < 2) {
                MessageBox.Show("Unfortenately there are no users to poke with!", "No enough users");
                return;
            }

            pokeMenuList.Renderer = new context_render_color();
            
            foreach (String name in names)
            {
                if (name == user_name)
                    continue;

                ToolStripMenuItem item = new ToolStripMenuItem(name)
                {
                    AutoSize = false,
                    Width = buttonPoke.Width,
                    Height = buttonPoke.Height,
                    Image = null,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                item.Click += (s, args) => user_send_poke(name);
                pokeMenuList.Items.Add(item);
            }

            pokeMenuList.AutoSize = true;
            pokeMenuList.MaximumSize = new Size(buttonPoke.Width, (buttonPoke.Height * 5) - buttonPoke.Margin.Size.Height - (buttonPoke.Margin.Size.Height / 2));
            pokeMenuList.MinimumSize = new Size(buttonPoke.Width, 0);
            pokeMenuList.Show(buttonPoke, new Point(0, -pokeMenuList.Height));
        }
        private void user_send_poke(string user)
        {
            textMessages.SelectionColor = System.Drawing.Color.Black;
            textMessages.AppendText("You poked \"");
            textMessages.SelectionColor = System.Drawing.Color.Red;
            textMessages.AppendText(user);
            textMessages.SelectionColor = System.Drawing.Color.Black;
            textMessages.AppendText("\".");
            textMessages.AppendText(Environment.NewLine);

            udp_connection.send_listening_chats("POKE_MESSAGE@" + user + "@" + user_name);
        }

        private void textUser_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textUser.Text)) udp_connection.send_listening_chats("TEXT_MESSAGE@UNTYPING@" + user_name);
            else
            {
                if(string.IsNullOrEmpty(previous_text))
                    udp_connection.send_listening_chats("TEXT_MESSAGE@TYPING@" + user_name);
            }

            previous_text = textUser.Text;
        }

        private void buttonSendText_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textUser.Text) || textUser.Text.Contains("|"))
            {
                MessageBox.Show("Please write a valid message in your box and dont use '|' sign.", "Warning");
                return;
            }

            udp_connection.send_listening_chats("MESSAGE@" + user_name + "@" + textUser.Text);
            textUser.Clear();
            textMessages.ScrollToCaret();
        }
        private void ChatRoom_FormClosing(object sender, FormClosingEventArgs e)
        {
            udp_connection.send_listening_chats("DISCONNECTED@" + user_name);
            udp_connection.client_close_connection();
            Application.Exit();
        }
    }
}
