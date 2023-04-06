using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using PacketLib;
using System.IO;

namespace ClientUI
{
    public class SocketCommander
    {
        private string ip;
        private int port;

        public SocketCommander(string ip,int port)
        {
            this.ip = ip;
            this.port = port;
        }
        public bool GetRemoteObject(Packet packet,ref byte[] data)
        {
            bool res = false;

            try
            {
                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                s.Connect(this.ip, this.port);
                s.Send(packet.ToBytes());

                byte[] buffer = new byte[10];
                List<byte> t = new List<byte>();

                int len = s.Receive(buffer);
                byte[] data2 = new byte[len-1];

                Array.Copy(buffer, 1, data2, 0, data2.Length);
                t.AddRange(data2);

                while ((len = s.Receive(buffer)) > 0)
                {
                    data2 = new byte[len];
                    Array.Copy(buffer, 0, data2, 0, data2.Length);
                    t.AddRange(data2);
                }

                if (t.Count > 0)
                {
                    data = t.ToArray();
                    res = true;
                }

            }
            catch { }

            return res;
        }
        public bool GetRemoteFile(string filePath ,string dstPath,IProgress<long> p)
        {
            bool res = false;

            try
            {
                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                s.Connect(this.ip, this.port);
                s.Send(new Packet(0).ToBytes());

                byte[] buffer = new byte[1024 * 8];

                while (s.Connected)
                {
                    int len = s.Receive(buffer);

                    if (len > 0)
                    {
                        byte[] data = new byte[len];
                        Array.Copy(buffer, 0, data, 0, data.Length);

                        FileStream fs = new FileStream(filePath, FileMode.Append);
                        fs.Write(data, 0, data.Length);

                        p.Report(fs.Length);
                        fs.Close();
                    }
                }
            }

            catch { }

            return res;
        }
    }

 
}
