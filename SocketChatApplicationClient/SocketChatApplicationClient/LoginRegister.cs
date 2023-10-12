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
    public partial class LoginRegister : Form
    {
        public SocketClientAsync client;
        public LoginRegister(SocketClientAsync mclient)
        {
            
            InitializeComponent();
            client = mclient;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Login(client).Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Register(client).Show();
            this.Hide();
        }

        private void LoginRegister_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void LoginRegister_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
