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
    public partial class Register : Form
    {
        public SocketClientAsync client;
        public Register(SocketClientAsync mclient)
        {
            client = mclient;
            InitializeComponent();
            client.ReceiveMessageFromServer += ReceiveMessage;
        }

        private void RegisterBT_Click(object sender, EventArgs e)
        {
            if (PasswdReg.Text != RePasswdReg.Text)
            {
                MessageBox.Show("Mật khẩu phải giống nhau", "Thông Báo", MessageBoxButtons.OK);
            }
            else
            {
                Customer customer = new Customer
                {
                    Type = "Register",
                    Username = UsernameReg.Text,
                    Password = PasswdReg.Text,
                    Message = null
                };
                string CustomerJson = JsonConvert.SerializeObject(customer);
                client.SendToServer(CustomerJson);
            }
        }
        private void ReceiveMessage(object sender, ReceiveMessageEventArgs e)
        {
            Customer t = JsonConvert.DeserializeObject<Customer>(e.TextSent);
            if (t.Type == "Register")
            {
                if (t.Message == "Successfull")
                {
                    DialogResult dlrs =  MessageBox.Show("Đăng kí thành công. Bấm \"OK\" để quay lại.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (dlrs ==DialogResult.OK)
                        {
                        this.Hide();
                        new Login(client).Show(); }

                }
                else
                    MessageBox.Show("Tài khoản đã tồn tại!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Register_FormClosing(object sender, FormClosingEventArgs e)
        {
            new LoginRegister(client).Show();
        }
    }
}
