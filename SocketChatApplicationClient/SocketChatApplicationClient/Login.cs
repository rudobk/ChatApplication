using Newtonsoft.Json;
using SocketAsync;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocketChatApplicationClient
{
    
    public partial class Login : Form
    {
        public SocketClientAsync client;
        public Login(SocketClientAsync mclient)
        {
            client = mclient;
            InitializeComponent();
            client.ReceiveMessageFromServer += ReceiveMessage;
            
        }

        private void LoginBT_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer
            {
                Type = "Login",
                Username = UsernameBox.Text,
                Password = PasswordBox.Text,
                Message = null
            };
            string CustomerJson = JsonConvert.SerializeObject(customer);
            client.SendToServer(CustomerJson);
        }
        private void ReceiveMessage(object sender, ReceiveMessageEventArgs e)
        {
            Customer t = JsonConvert.DeserializeObject<Customer>(e.TextSent);
            if (t.Type == "Login")
            {
                if (t.Message == "Successfull")
                {
                    DialogResult dlrs =  MessageBox.Show("Đăng nhập thành công.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (dlrs == DialogResult.OK)
                        {
                            this.Hide();
                            new Form1(client,UsernameBox.Text).Show();
                        }
                }
                else
                    MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            new LoginRegister(client).Show();
        }
    }
}
