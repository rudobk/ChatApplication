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

    public class SocketClientAsync
    {
        IPAddress mServerIPAddress;
        int mServerPort;
        TcpClient mClient;
        public EventHandler ConnectedToServer;

        public EventHandler CannotConnectToServer;

        public EventHandler ServerDisconnected;

        public EventHandler<ReceiveMessageEventArgs> ReceiveMessageFromServer;
        public SocketClientAsync()
        {
            mClient = null;
            mServerPort = -1;
            mClient = null;
        }
        public IPAddress ServerIPAddress
        {
            get
            {
                return mServerIPAddress;
            }
        }
        public int ServerPort
        {
            get
            {
                return mServerPort;
            }
        }
        public bool SetServerIPAddress(string _IPAddressServer)
        {
            IPAddress ipaddr = null;
            if (!IPAddress.TryParse(_IPAddressServer, out ipaddr))
            {
                return false;
            }
            mServerIPAddress = ipaddr;
            return true;
        }

        public async Task SendToServer(string strInputUser)
        {

            if (string.IsNullOrEmpty(strInputUser))
            {
                return;
            }
            if (mClient != null)
            {
                byte[] Text = Encoding.Unicode.GetBytes(strInputUser);
                //byte[] buffMessage = Encoding.Unicode.GetBytes(strInputUser);
                await mClient.GetStream().WriteAsync(Text, 0, Text.Length);
                mClient.GetStream().Flush();
            }
        }

        public void CloseAndDisconnect()
        {
            if (mClient != null)
            {
                if (mClient.Connected)
                    mClient.Close();
            }
        }

        public bool SetServerPort(int _PortNumber)
        {
            if (_PortNumber >= 1 && _PortNumber <= 65535)
            {
                mServerPort = _PortNumber;
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task ConnectToServer()
        {
            if (mClient == null)
            {
                mClient = new TcpClient();
            }
            try
            {
                await mClient.ConnectAsync(mServerIPAddress, mServerPort);
                ConnectedToServer(this, new EventArgs());
                ReadDataFromServer(mClient);
            }
            catch (Exception ecxp)
            {
                Debug.WriteLine(ecxp.ToString());
                CannotConnectToServer(this, new EventArgs());
                throw;
            }
        }

        private async void ReadDataFromServer(TcpClient mClient)
        {
            try
            {
                NetworkStream stream = null;
                //StreamReader clientStreamReader = new StreamReader(mClient.GetStream(),Encoding.Unicode);
                byte[] buffer = new byte[256];
                int readByteCount = 0;
                while (true)
                {
                    stream = mClient.GetStream();
                    readByteCount = await stream.ReadAsync(buffer, 0, buffer.Length);
                    //readByteCount = await clientStreamReader.ReadAsync(buffer, 0, buffer.Length);
                    if (readByteCount != 0)
                    {
                        if (buffer[0] != '\0')
                        {
                            ReceiveMessageEventArgs eReceiveMessage;
                            string Text = Encoding.Unicode.GetString(buffer);
                            //string Text = new string(buffer);

                            eReceiveMessage = new ReceiveMessageEventArgs(Text);
                            OnReceiveMessageEvent(eReceiveMessage);
                            Debug.WriteLine("Read data from server: ");
                            Debug.WriteLine(Text);
                        }
                        Array.Clear(buffer, 0, buffer.Length);
                        mClient.GetStream().Flush();
                    }
                    else
                    {
                        ServerDisconnected(this, new EventArgs());
                        Debug.WriteLine("Server is disconnected");
                        DisconnectedToServer();
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }
        public void OnReceiveMessageEvent(ReceiveMessageEventArgs e)
        {
            EventHandler<ReceiveMessageEventArgs> handler = ReceiveMessageFromServer;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        public bool DisconnectedToServer()
        {
            if (mClient != null)
            {
                if (mClient.Connected)
                {
                    mClient.Close();
                    return true;
                }
            }
            return false;
        }

    }
}
