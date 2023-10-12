using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketAsync
{
    public class ReceiveMessageEventArgs : EventArgs
    {
        public string TextSent { get; set; }
        public ReceiveMessageEventArgs(string _TextSent)
        {
            TextSent = _TextSent;
        }
    }
}
