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
    public class ClientConnectedEventArgs : EventArgs
    {
        public string Newclient { get; set; }
        public ClientConnectedEventArgs(string _newClient)
        {
            Newclient = _newClient;
        }
    }
    public class ClientDisconnectedEventArgs : EventArgs
    {
        public string DisconnectClient { get; set; }
        public ClientDisconnectedEventArgs(string _DisconnectClient)
        {
            DisconnectClient = _DisconnectClient;
        }
    }
    public class ReceiveMessageEventArgs : EventArgs
    {
        public TcpClient customer { set; get; }
        public string ClientSent { get; set; }
        public string TextSent { get; set; }
        public ReceiveMessageEventArgs(TcpClient _customer, string _ClientSent, string _TextSent)
        {
            customer = _customer;
            ClientSent = _ClientSent;
            TextSent = _TextSent;
        }
    }
}