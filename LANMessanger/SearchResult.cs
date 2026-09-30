using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace LANMessanger
{
    public partial class SearchResult : Form
    {
        private Button search_button;
        public SearchResult(Button button, RichTextBox textMessages, string search_word)
        {
            InitializeComponent();

            search_button = button;
            this.FormClosing += SearchResult_FormClosing;

            search_button.Enabled = false;

            string[] lines = textMessages.Text.Split(new[] { "\n" }, StringSplitOptions.None);
            Regex regex = new Regex($@"\b{Regex.Escape(search_word)}\b");

            Panel scroll_panel = new Panel { AutoScroll = true, Dock = DockStyle.Fill };
            this.Controls.Add(scroll_panel);

            int iterative = 0; bool is_empty = true;
            for (int i = 0; i < lines.Length; i++)
            {
                if (regex.IsMatch(lines[i]))
                {
                    System.Windows.Forms.RichTextBox textbox = new System.Windows.Forms.RichTextBox();
                    textbox.Location = new System.Drawing.Point(10, 10);
                    textbox.Size = new System.Drawing.Size(this.ClientSize.Width - (this.ClientSize.Width / 4), 40);

                    textbox.Left = (this.ClientSize.Width - textbox.Width) / 2;
                    textbox.Top = (iterative * textbox.Height) + (15 * iterative);

                    textbox.AutoSize = true;

                    if (i > 0) textbox.Text += lines[i - 1] + "\n";
                    textbox.Text += lines[i] + "\n";
                    if (i < lines.Length - 1) textbox.Text += lines[i + 1] + "\n";

                    scroll_panel.Controls.Add(textbox);
                    ++iterative; is_empty = false;
                }
            }

            System.Windows.Forms.Label label_result = new System.Windows.Forms.Label();
            if (is_empty)
            {
                label_result.Location = new System.Drawing.Point(30, 30);
                label_result.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
                label_result.AutoSize = true;
                label_result.Text = "We couldn't find any results";
                label_result.TabIndex = 4;
                label_result.Left = (this.ClientSize.Width - (int)(label_result.Font.Size * label_result.Text.Length * 0.6f)) / 2;
            }
            else
            {
                // Add empty label as a space in the end so it can look better when the scrollbar is added
                label_result.Location = new System.Drawing.Point(10, 10);
                label_result.Size = new System.Drawing.Size(40, 40);
                label_result.Top = (iterative * label_result.Height) + (15 * (iterative - 1));
            }

            scroll_panel.Controls.Add(label_result);
        }
        private void SearchResult_FormClosing(object sender, FormClosingEventArgs e)
        {
            search_button.Enabled = true;
        }
    }
}
