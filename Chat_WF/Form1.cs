using System;
using System.IO;
using System.Windows.Forms;

namespace Chat_WF
{
    public partial class Form1 : Form
    {
        private const string ChatFileName = "chat.txt";
        private string _nickName;
        private ChatReader _chatReader;
        private int oldLabelY = 30;

        public Form1(string nickname)
        {
            InitializeComponent();
            InitFileChat();
            
            _nickName = nickname;
            _chatReader = new ChatReader(ChatFileName, this);
        }
   
        private async void button1_Click(object sender, EventArgs e)
        {
            var chatWriter = new ChatWriter(ChatFileName, _nickName);
            await chatWriter.SaveAsync(textBox1.Text);
            textBox1.Text = string.Empty;
        }

        public void CreateMessage_User(string user, string finalMessage)
        {         
            Label newMessage = new Label();
            newMessage.Text = finalMessage;
            newMessage.MaximumSize = new System.Drawing.Size(200, 0);
            newMessage.AutoSize = true;
            newMessage.BackColor = System.Drawing.Color.PaleTurquoise;
            newMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            newMessage.Name = "label2";
            newMessage.Size = new System.Drawing.Size(79, 29);
            newMessage.TabIndex = 5;
            newMessage.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            newMessage.Location = new System.Drawing.Point(0, oldLabelY + panel1.AutoScrollPosition.Y );
            oldLabelY = oldLabelY + 40 + newMessage.PreferredHeight;
            this.panel1.Controls.Add(newMessage);
            Label newUser = new Label();
            newUser.Text = user;
            newUser.AutoSize = true;
            newUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            newUser.ForeColor = System.Drawing.Color.DimGray;
            newUser.Location = new System.Drawing.Point(0, newMessage.Location.Y-20);
            newUser.Name = "label1";
            newUser.Size = new System.Drawing.Size(64, 25);
            newUser.TabIndex = 5;
            this.panel1.Controls.Add(newUser);
        }
        public void CreateMessage_Companion(string user, string finalMessage)
        {

            Label newMessage = new Label();
            newMessage.Text = finalMessage;
            newMessage.MaximumSize = new System.Drawing.Size(200, 0);
            newMessage.AutoSize = true;
            newMessage.BackColor = System.Drawing.Color.PaleGreen;
            newMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            newMessage.Name = "label2";
            newMessage.Size = new System.Drawing.Size(79, 29);
            newMessage.TabIndex = 5;
            newMessage.TextAlign = System.Drawing.ContentAlignment.TopRight;
            newMessage.Location = new System.Drawing.Point(panel1.Width-newMessage.PreferredWidth-20, oldLabelY + panel1.AutoScrollPosition.Y);
            oldLabelY = oldLabelY + 40 + newMessage.PreferredHeight;
            this.panel1.Controls.Add(newMessage);
            Label newUser = new Label();
            newUser.Text = user;
            newUser.AutoSize = true;
            newUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            newUser.ForeColor = System.Drawing.Color.DimGray;
            newUser.Name = "label1";
            newUser.Size = new System.Drawing.Size(64, 25);
            newUser.TabIndex = 5;
            newUser.Location = new System.Drawing.Point(panel1.Width - newUser.PreferredWidth-20, newMessage.Location.Y - 20);
            this.panel1.Controls.Add(newUser);
        }
        public void CreateMessage(string message)
        {
            string user = "";
            string finalMessage = "";
            try
            {
                user = message.Substring(0, message.IndexOf('>')-1);
                finalMessage = message.Substring(message.IndexOf('>')+2);
            }
            catch { }
            if (user == _nickName)
            {
                CreateMessage_User(user, finalMessage);
            }
            else 
            {
                CreateMessage_Companion(user, finalMessage);
            }
        }

        private void InitFileChat()
        {
            if (!File.Exists(ChatFileName))
            {
                File.Create(ChatFileName);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _chatReader.ReadNewMessages();
        }
    }
}
