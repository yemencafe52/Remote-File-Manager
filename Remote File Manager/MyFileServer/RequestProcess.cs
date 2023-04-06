using PacketLib;
using System;
using System.Net.Sockets;
using System.Text;

namespace MyFileServer
{
    internal class RequestProcess
    {
        public RequestProcess(Socket s)
        {
            try
            {
                byte[] buffer = new byte[1024 * 8];

                int len = s.Receive(buffer);

                if (len > 0)
                {
                    byte[] temp = new byte[len];
                    Array.Copy(buffer, 0, temp, 0, len);

                    Packet p = new Packet(temp);

                    switch(p.Command)
                    {
                        case 1:
                            {
                                if (new HandCheek("admin","admin").Success)
                                {
                                    byte[] buffer2 = new FileManager().GetDrives();
                                    s.Send(new Packet(1, buffer2).ToBytes());
                                }
                                break;
                            }
                        case 2:
                            {
                                if (new HandCheek("admin", "admin").Success)
                                {
                                    string path = Encoding.UTF8.GetString(p.Buffer);
                                    byte[] buffer2 = new FileManager().GetDir(path);
                                    s.Send(new Packet(2, buffer2).ToBytes());
                                }

                                break;
                            }
                        case 3:
                            {
                                if (new HandCheek("admin", "admin").Success)
                                {
                                    string path = Encoding.UTF8.GetString(p.Buffer);
                                    byte[] buffer2 = new FileManager().GetFiles((path));
                                    s.Send(new Packet(2, buffer2).ToBytes());
                                }

                                break;
                            }

                        case 7:
                            {
                                if (new HandCheek("admin", "admin").Success)
                                {

                                    byte[] buffer2 = Encoding.UTF8.GetBytes(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                                    s.Send(new Packet(2, buffer2).ToBytes());
                                }

                                break;
                            }

                        case 8:
                            {
                                if (new HandCheek("admin", "admin").Success)
                                {
                                    string path = Encoding.UTF8.GetString(p.Buffer);
                                    byte[] buffer2 = Encoding.UTF8.GetBytes(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
                                    s.Send(new Packet(2, buffer2).ToBytes());
                                }

                                break;
                            }

                        case 253:
                            {
                                if (new HandCheek("admin", "admin").Success)
                                {
                                    byte[] buffer2 = new byte[] { 253 };
                                    s.Send(new Packet(253, buffer2).ToBytes());
                                }

                                break;
                            }
                    }
                }

                s.Close();
            }
            catch { }
        }
    }
}