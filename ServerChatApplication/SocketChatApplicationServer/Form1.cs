using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SocketAsync;
using Newtonsoft.Json;
using System.Data.OleDb;
namespace SocketChatApplicationServer
{
    public partial class Form1 : Form
    {
        List<string> OnlineUser = new List<string>();
        SocketServerAsync mServer;
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=db_user.mdb");
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataAdapter da = new OleDbDataAdapter();
        public Form1()
        {
            InitializeComponent();
            con.Open();
            mServer = new SocketServerAsync();
            mServer.RaiseClientConnectedEvent += NewClientConnected;
            mServer.RaiseClientDisconnectedEvent += ClientDisconnected;
            mServer.RaiseReceiveMessageEvent += ReceiveMessage;
            btnStopServer.Enabled = false;
        }


        private void btnAcceptIncomingAsync_Click(object sender, EventArgs e)
        {
            int PortNumber = 0;
            int.TryParse(textBox1.Text, out PortNumber);
            if (PortNumber <= 0 || PortNumber > 65535)
                MessageBox.Show("Port không hợp lệ");
            else
            {
                mServer.StartListeningForIncomingConnection(null, PortNumber);
                string text = string.Format("{0} - Khởi động máy chủ thành công!, Port:{1}", DateTime.Now, PortNumber);
                Hienthi(text);
                btnAcceptIncomingAsync.Enabled = false;
                btnStopServer.Enabled = true;
                timer1.Enabled = true;
            }
        }

        private void btnSendAll_Click(object sender, EventArgs e)
        {
            
        }

        private void btnStopServer_Click(object sender, EventArgs e)
        {
            mServer.StopServer();
            string text = string.Format("{0} - Dừng máy chủ thành công!", DateTime.Now);
            Hienthi(text);
            btnAcceptIncomingAsync.Enabled = true;
            btnStopServer.Enabled = false;
            timer1.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            mServer.StopServer();
        }
        private void NewClientConnected(object sender, ClientConnectedEventArgs e)
        {
            string text = string.Format("{0} - Client kết nối thành công: {1}", DateTime.Now, e.Newclient);
            Hienthi(text);
        }
        private void ClientDisconnected(object sender, ClientDisconnectedEventArgs e)
        {
            string text = string.Format("{0} - Client ngắt kết nối: {1}", DateTime.Now, e.DisconnectClient);
            Hienthi(text);
        }
        private async void ReceiveMessage(object sender, ReceiveMessageEventArgs e)
        {
            Customer customer = JsonConvert.DeserializeObject<Customer>(e.TextSent);
            if(customer.Type == "Logout")
            {
                if (OnlineUser.Contains(customer.Username))
                {
                    OnlineUser.Remove(customer.Username);
                    listBox1.DataSource = null;
                    listBox1.DataSource = OnlineUser;
                }
                customer.Type = "ListUser";
                customer.Message = JsonConvert.SerializeObject(OnlineUser);
                // client => chuyển ngược message => List OnlineUser của nó -> hiển thị
                await mServer.SendToAll(customer);
            }
            // Type = login
            // 
            if (customer.Type == "Login")
            {
                if (!OnlineUser.Contains(customer.Username))
                {
                    // Gọi hàm để check Username/password
                    bool check = await Result_Login(customer);
                    if (check == true)
                    {
                        // Kiểm tra, nếu user không có trong danh sách online
                        // Thì trả về đăng nhập thành công
                        if (!OnlineUser.Contains(customer.Username))
                        {
                            OnlineUser.Add(customer.Username);
                            listBox1.DataSource = null;
                            listBox1.DataSource = OnlineUser;
                        }
                        Customer t = new Customer
                        {
                            Type = "Login",
                            Message = "Successfull"
                        };
                        string Check_Message = JsonConvert.SerializeObject(t);
                        await mServer.SendToSpecialUser(e.customer, t);
                        e.customer.GetStream().Flush();
                        // Update danh sách online tới User
                        t.Type = "ListUser";
                        t.Message = JsonConvert.SerializeObject(OnlineUser);
                        await mServer.SendToAll(t);
                    }
                    else
                    {
                        // Tài khoản hoặc mật khẩu không chính xác
                        Customer t = new Customer
                        {
                            Type = "Login",
                            Message = "Fail"
                        };
                        await mServer.SendToSpecialUser(e.customer, t);
                        e.customer.GetStream().Flush();
                    }
                }
                else
                {
                    //User đang được sử dụng
                    Customer t = new Customer
                    {
                        Type = "Login",
                        Message = "Exist"
                    };
                    await mServer.SendToSpecialUser(e.customer, t);
                }
            }
            if (customer.Type == "Register")
            {
                bool check = await Result_Register(customer);
                if (check == true)
                {
                    Customer t = new Customer
                    {
                        Type = "Register",
                        Message = "Successfull"
                    };
                    await mServer.SendToSpecialUser(e.customer, t);
                    e.customer.GetStream().Flush();
                }
                else
                {
                    Customer t = new Customer
                    {
                        Type = "Register",
                        Message = "Fail"
                    };
                    await mServer.SendToSpecialUser(e.customer, t);
                }
            }
            if (customer.Type == "SendMessage")
            {
                //Forward bản tin này tới tất cả các user
                await mServer.SendToAll(customer);
            }
            if (customer.Type == "CheckOnline")
            {
                // Kiểm tra user đã tồn tại chưa
                if(!OnlineUser.Contains(customer.Username))
                {
                    //Nếu chưa thì add vào
                    OnlineUser.Add(customer.Username);
                }
                // Update list user tới tất cả client
                customer.Type = "ListUser";
                customer.Message = JsonConvert.SerializeObject(OnlineUser);
                await mServer.SendToAll(customer);
            }
        }
        private void Hienthi(string Text)
        {
            KhungChat.Items.Add(Text);
            KhungChat.SelectedIndex = KhungChat.Items.Count - 1;
        }
        // Login
        public async Task<bool> Result_Login(Customer t)
        {

            string login = "SELECT * FROM tbl_users WHERE username ='" + t.Username + "'and password= '" + t.Password + "'";
            cmd = new OleDbCommand(login, con);
            OleDbDataReader dr = cmd.ExecuteReader();
            bool x = dr.Read();
            //con.Close();
            return x;
        }
        public async Task<bool> Result_Register(Customer t)
        {
            string register = "SELECT * FROM tbl_users WHERE username ='" + t.Username + "'";
            cmd = new OleDbCommand(register, con);
            OleDbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return false;
            }
            else
            {
                register = "INSERT INTO tbl_users VALUES('" + t.Username + "','" + t.Password + "')";
                cmd = new OleDbCommand(register, con);
                cmd.ExecuteNonQuery();
                return true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Customer Check = new Customer();
            OnlineUser.Clear();
            Check.Type = "CheckOnline";
            mServer.SendToAll(Check);
            listBox1.DataSource = OnlineUser;
        }
    }

}
