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
    public partial class ConnectServer : Form
    {
        public SocketClientAsync client;
        public ConnectServer()
        {
            InitializeComponent();
            client = new SocketClientAsync();
            client.ConnectedToServer += ConnectSuccess;
            client.CannotConnectToServer += ConnectFail;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int Port = -1;
            int.TryParse(PortBox.Text, out Port);
            if (!client.SetServerIPAddress(IPAdressBox.Text))
            {
                MessageBox.Show("IP không hợp lệ.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!client.SetServerPort(Port))
            {
                MessageBox.Show("Port không hợp lệ.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                client.ConnectToServer();
        }
        private void ConnectSuccess(object sender, EventArgs e)
        {
            DialogResult dlrs = MessageBox.Show("Kết nối thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if(dlrs == DialogResult.OK)
            {
                this.Hide();
                new LoginRegister(client).Show();


            }
        }
        private void ConnectFail(object sender, EventArgs e)
        {
            MessageBox.Show("Kết nối không thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
