namespace LANMessanger
{
    partial class ChatRoom
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatRoom));
            this.textUser = new System.Windows.Forms.TextBox();
            this.buttonSendText = new System.Windows.Forms.Button();
            this.textMessages = new System.Windows.Forms.RichTextBox();
            this.textConnected = new System.Windows.Forms.RichTextBox();
            this.label_users = new System.Windows.Forms.Label();
            this.label_search = new System.Windows.Forms.Label();
            this.textSearch = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.label_typing = new System.Windows.Forms.Label();
            this.label_poke = new System.Windows.Forms.Label();
            this.buttonPoke = new System.Windows.Forms.Button();
            this.pokeMenuList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuspendLayout();
            // 
            // textUser
            // 
            this.textUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textUser.Location = new System.Drawing.Point(11, 370);
            this.textUser.Name = "textUser";
            this.textUser.Size = new System.Drawing.Size(450, 22);
            this.textUser.TabIndex = 1;
            this.textUser.TextChanged += new System.EventHandler(this.textUser_TextChanged);
            // 
            // buttonSendText
            // 
            this.buttonSendText.Location = new System.Drawing.Point(469, 369);
            this.buttonSendText.MinimumSize = new System.Drawing.Size(65, 23);
            this.buttonSendText.Name = "buttonSendText";
            this.buttonSendText.Size = new System.Drawing.Size(138, 28);
            this.buttonSendText.TabIndex = 2;
            this.buttonSendText.Text = "Send";
            this.buttonSendText.UseVisualStyleBackColor = true;
            this.buttonSendText.Click += new System.EventHandler(this.buttonSendText_Click);
            // 
            // textMessages
            // 
            this.textMessages.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textMessages.Font = new System.Drawing.Font("Segoe UI Emoji", 10F);
            this.textMessages.Location = new System.Drawing.Point(11, 12);
            this.textMessages.Name = "textMessages";
            this.textMessages.ReadOnly = true;
            this.textMessages.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.textMessages.Size = new System.Drawing.Size(593, 306);
            this.textMessages.TabIndex = 3;
            this.textMessages.Text = "";
            // 
            // textConnected
            // 
            this.textConnected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textConnected.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textConnected.Font = new System.Drawing.Font("Segoe UI Emoji", 10F);
            this.textConnected.Location = new System.Drawing.Point(610, 42);
            this.textConnected.MaximumSize = new System.Drawing.Size(178, 89);
            this.textConnected.MinimumSize = new System.Drawing.Size(178, 89);
            this.textConnected.Name = "textConnected";
            this.textConnected.ReadOnly = true;
            this.textConnected.Size = new System.Drawing.Size(178, 89);
            this.textConnected.TabIndex = 4;
            this.textConnected.Text = "";
            // 
            // label_users
            // 
            this.label_users.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_users.AutoSize = true;
            this.label_users.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label_users.Location = new System.Drawing.Point(647, 19);
            this.label_users.Name = "label_users";
            this.label_users.Size = new System.Drawing.Size(112, 20);
            this.label_users.TabIndex = 5;
            this.label_users.Text = "Active users: ";
            // 
            // label_search
            // 
            this.label_search.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_search.AutoSize = true;
            this.label_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label_search.Location = new System.Drawing.Point(631, 153);
            this.label_search.Name = "label_search";
            this.label_search.Size = new System.Drawing.Size(138, 20);
            this.label_search.TabIndex = 6;
            this.label_search.Text = "Search from chat";
            // 
            // textSearch
            // 
            this.textSearch.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textSearch.Location = new System.Drawing.Point(609, 176);
            this.textSearch.MaximumSize = new System.Drawing.Size(178, 22);
            this.textSearch.MinimumSize = new System.Drawing.Size(178, 22);
            this.textSearch.Name = "textSearch";
            this.textSearch.Size = new System.Drawing.Size(178, 22);
            this.textSearch.TabIndex = 7;
            this.textSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonSearch.Location = new System.Drawing.Point(610, 204);
            this.buttonSearch.MaximumSize = new System.Drawing.Size(177, 31);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(177, 31);
            this.buttonSearch.TabIndex = 8;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // label_typing
            // 
            this.label_typing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_typing.AutoSize = true;
            this.label_typing.Location = new System.Drawing.Point(13, 348);
            this.label_typing.Name = "label_typing";
            this.label_typing.Size = new System.Drawing.Size(0, 16);
            this.label_typing.TabIndex = 9;
            // 
            // label_poke
            // 
            this.label_poke.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label_poke.AutoSize = true;
            this.label_poke.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label_poke.Location = new System.Drawing.Point(631, 264);
            this.label_poke.Name = "label_poke";
            this.label_poke.Size = new System.Drawing.Size(119, 20);
            this.label_poke.TabIndex = 11;
            this.label_poke.Text = "Poke someone";
            // 
            // buttonPoke
            // 
            this.buttonPoke.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPoke.Location = new System.Drawing.Point(609, 287);
            this.buttonPoke.MaximumSize = new System.Drawing.Size(177, 31);
            this.buttonPoke.Name = "buttonPoke";
            this.buttonPoke.Size = new System.Drawing.Size(177, 31);
            this.buttonPoke.TabIndex = 10;
            this.buttonPoke.Text = "Poke";
            this.buttonPoke.UseVisualStyleBackColor = true;
            this.buttonPoke.Click += new System.EventHandler(this.buttonPoke_Click);
            // 
            // pokeMenuList
            // 
            this.pokeMenuList.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.pokeMenuList.Name = "pokeMenuList";
            this.pokeMenuList.Size = new System.Drawing.Size(61, 4);
            // 
            // ChatRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label_poke);
            this.Controls.Add(this.buttonPoke);
            this.Controls.Add(this.label_typing);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.textSearch);
            this.Controls.Add(this.label_search);
            this.Controls.Add(this.label_users);
            this.Controls.Add(this.textConnected);
            this.Controls.Add(this.textMessages);
            this.Controls.Add(this.buttonSendText);
            this.Controls.Add(this.textUser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(612, 497);
            this.Name = "ChatRoom";
            this.Text = "ChatRoom";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textUser;
        private System.Windows.Forms.Button buttonSendText;
        private System.Windows.Forms.RichTextBox textMessages;
        private System.Windows.Forms.RichTextBox textConnected;
        private System.Windows.Forms.Label label_users;
        private System.Windows.Forms.Label label_search;
        private System.Windows.Forms.TextBox textSearch;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Label label_typing;
        private System.Windows.Forms.Label label_poke;
        private System.Windows.Forms.Button buttonPoke;
        private System.Windows.Forms.ContextMenuStrip pokeMenuList;
    }
}