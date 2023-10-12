using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SocketAsync;
using Newtonsoft.Json;

namespace SocketChatApplicationClient
{
    public partial class Form1 : Form
    {
        public SocketClientAsync client;
        string name;
        public Form1(SocketClientAsync mclient, string mname)
        {
            InitializeComponent();
            client = mclient;
            name = mname;
            client.ReceiveMessageFromServer += ReceiveMessage;
            client.ServerDisconnected += ServerIsDisconnected;
            
        }

        private void ServerIsDisconnected(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Mất kết nối tới Server!\r\n See You Again <3", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (d == DialogResult.OK)
                this.Close();
        }

        private void ReceiveMessage(object sender, ReceiveMessageEventArgs e)
        {
            Customer t = JsonConvert.DeserializeObject<Customer>(e.TextSent);
            KhungChat.Items.Add(name + ": " + t.Message);
            //KhungChat.SelectedIndex = KhungChat.Items.Count - 1;
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            client.DisconnectedToServer();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (client.DisconnectedToServer())
            {
                DialogResult d = MessageBox.Show("Ngắt Kết Nối Thành Công!\r\n See You Again <3", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (d == DialogResult.OK)
                    this.Close();
            }
            else MessageBox.Show("Bạn chưa kết nối tới Server!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void GuiTin_Click(object sender, EventArgs e)
        {
            Customer t = new Customer
            {
                Type = "SendMessage",
                Username = name,
                Message = TinNhan.Text
            };
            string MessageSend = JsonConvert.SerializeObject(t);
            byte[] buffMessage = Encoding.Unicode.GetBytes(MessageSend);
            await client.SendToServer(MessageSend);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            new LoginRegister(client).Show();
        }
    }
    [Serializable]
    public class Customer
    {
        public string Type { set; get; }
        public string Username { set; get; }
        public string Password { set; get; }
        public string Message { set; get; }

    }
}
