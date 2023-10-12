using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
namespace SocketAsync
{

    public class SocketServerAsync
    {

        IPAddress mIP;
        int mPort;
        TcpListener mTCPListener;

        List<TcpClient> mClients;

        List<string> Online_User;

        public EventHandler<ClientConnectedEventArgs> RaiseClientConnectedEvent;

        public EventHandler<ClientDisconnectedEventArgs> RaiseClientDisconnectedEvent;

        public EventHandler<ReceiveMessageEventArgs> RaiseReceiveMessageEvent;
        public bool KeepRunning { get; set; }

        public SocketServerAsync()
        {
            mClients = new List<TcpClient>();
        }
        // Mở port cho phép client kết nối
        public async void StartListeningForIncomingConnection(IPAddress ipaddr = null, int port = 12345)
        {
            if (ipaddr == null)
            {
                ipaddr = IPAddress.Any; // Lắng nghe trên mọi giao diện mạng
            }
            if (port <= 0)
            {
                port = 12345; // Thông tin port
            }
            mIP = ipaddr;
            mPort = port;
            mTCPListener = new TcpListener(mIP, mPort);
            try
            {
                mTCPListener.Start(); // Bắt đầu lắng nghe
                KeepRunning = true;
                while (KeepRunning)
                {
                    var returnedByAccept = await mTCPListener.AcceptTcpClientAsync(); // Chấp nhận kết nối
                    mClients.Add(returnedByAccept); // Thêm vào list Client
                    ClientConnectedEventArgs eClientConnected;
                    eClientConnected = new ClientConnectedEventArgs(
                        returnedByAccept.Client.RemoteEndPoint.ToString()
                        );
                    OnraiseClientConnectedEvent(eClientConnected);// Tạo Event có client kết nối
                    TakeCareOfTCPClient(returnedByAccept); // Gọi chương trình quản lý client
                }

            }
            catch (Exception excp)
            {
                System.Diagnostics.Debug.WriteLine(excp.ToString());
            }
        }
        public void OnraiseClientConnectedEvent(ClientConnectedEventArgs e)
        {
            EventHandler<ClientConnectedEventArgs> handler = RaiseClientConnectedEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        public void OnraiseClientDisconnectedEvent(ClientDisconnectedEventArgs e)
        {
            EventHandler<ClientDisconnectedEventArgs> handler = RaiseClientDisconnectedEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        public void OnReceiveMessageEvent(ReceiveMessageEventArgs e)
        {
            EventHandler<ReceiveMessageEventArgs> handler = RaiseReceiveMessageEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        public void StopServer()
        {
            try
            {
                if (mTCPListener != null)
                {
                    mTCPListener.Stop();
                }

                foreach (TcpClient c in mClients)
                {
                    c.GetStream().Close();
                    c.Close();
                }
                mClients.Clear();
            }
            catch (Exception excp)
            {

                Debug.WriteLine(excp.ToString());
            }
        }
        // Xử lý từng client
        private async void TakeCareOfTCPClient(TcpClient paramClient)
        {
            NetworkStream stream = null;
            try
            {
                // Get stream từ TcpClient
                stream = paramClient.GetStream();
                byte[] buff = new byte[1024];
                while (KeepRunning)
                {
                    // Đọc dữ liệu từ Client
                    int nRet = await stream.ReadAsync(buff, 0, buff.Length);
                    if (nRet == 0)
                    {
                        // Nếu đọc được null => Xóa client ngắt kết nối
                        RemoveClient(paramClient);
                        break;
                    }
                    // Chuyển sang kiểu chuỗi và giải mã
                    string receivedText = EncryptAndDecrypt(Encoding.Unicode.GetString(buff));
                    //string receivedText = Encoding.Unicode.GetString(buff);
                    ReceiveMessageEventArgs eReceiveMessage = new ReceiveMessageEventArgs(
                        paramClient,
                        paramClient.Client.RemoteEndPoint.ToString(),
                        receivedText);
                    // Event nhận tin nhắn từ Server
                    OnReceiveMessageEvent(eReceiveMessage);
                    Array.Clear(buff, 0, buff.Length);
                }

            }
            catch (Exception excp)
            {
                RemoveClient(paramClient);
            }

        }
        // Xử lý client ngắt kết nối
        // xóa client khỏi list user kết nối
        private void RemoveClient(TcpClient paramClient)
        {
            if (mClients.Contains(paramClient))
            {
                ClientDisconnectedEventArgs eDisconnected;
                eDisconnected = new ClientDisconnectedEventArgs(paramClient.Client.RemoteEndPoint.ToString());
                OnraiseClientDisconnectedEvent(eDisconnected);
                mClients.Remove(paramClient);
                Debug.WriteLine(String.Format("Client removed, count: {0}", mClients.Count));
            }
        }

        public async Task SendToAll(Customer customer)
        {
            // Chuyển thành chuỗi Json
            string Message = JsonConvert.SerializeObject(customer);
            if (string.IsNullOrEmpty(Message))
            {
                return;
            }
            try
            {
                // Mã hóa và chuyển thành mã Unicode
                byte[] buffMessage = Encoding.Unicode.GetBytes(EncryptAndDecrypt(Message));
                //byte[] buffMessage = Encoding.Unicode.GetBytes(Message);
                // Gửi tới tất cả các client
                foreach (TcpClient c in mClients)
                {
                    c.GetStream().WriteAsync(buffMessage, 0, buffMessage.Length);
                    c.GetStream().Flush();
                }
                Array.Clear(buffMessage, 0, buffMessage.Length);
            }
            catch (Exception excp)
            {
                Debug.WriteLine(excp.ToString());
            }

        }
        public async Task SendToSpecialUser(TcpClient client,Customer customer)
        {
            string Check_Message = JsonConvert.SerializeObject(customer);
            //byte[] buffMessage = Encoding.Unicode.GetBytes(Check_Message);
            byte[] buffMessage = Encoding.Unicode.GetBytes(EncryptAndDecrypt(Check_Message));

            await client.GetStream().WriteAsync(buffMessage, 0, buffMessage.Length);
            client.GetStream().Flush();
        }
        private static string EncryptAndDecrypt(string s)
        {
            char[] kytu = new char[s.Length];
            kytu = s.ToCharArray();
            for (int i = 0; i < kytu.Length; i++)
            {
                kytu[i] = (char)((int)kytu[i] ^ 11);
            }
            return new string(kytu);
        }
    }
    [Serializable]
    public class Customer
    {
        //4 thuộc tính dữ liệu
        public string Type { set; get; }
        public string Username { set; get; }
        public string Password { set; get; }
        public string Message { set; get; }
        // NỐi thành 1 chuỗi json => gửi đi. Đến khi bên nhận => chuyển chuỗi json này thành dữ liệu. => 

    }
}
