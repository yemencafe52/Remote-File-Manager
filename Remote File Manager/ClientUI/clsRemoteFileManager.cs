using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ClientUI
{
    public class RemoteFileManager
    {
        public bool GetDrives(Station s,ref List<DriveInfo> drives)
        {
            bool res = false;

            try
            {
                SocketCommander sc = new SocketCommander(s.PCName, s.Port);
                byte[] objBytes = null;

                if (sc.GetRemoteObject(new PacketLib.Packet(1), ref objBytes)) 
                {
                    DriveInfo[] obj = null;

                    if(new BytesToObject<DriveInfo[]>(objBytes).GetObject(ref obj))
                    {
                        drives.AddRange(obj);
                        res = true;
                    }
                }
            }
            catch { }

            return res;
        }

        public bool GetDir(Station s, string path, ref List<DirectoryInfo> dirs)
        {
            bool res = false;

            try
            {
                SocketCommander sc = new SocketCommander(s.PCName, s.Port);
                byte[] objBytes = null;

                if (sc.GetRemoteObject(new PacketLib.Packet(2,Encoding.UTF8.GetBytes(path)), ref objBytes))
                {
                    DirectoryInfo[] obj = null;

                    if (new BytesToObject<DirectoryInfo[]>(objBytes).GetObject(ref obj))
                    {
                        dirs.AddRange(obj);
                        res = true;
                    }
                }
            }
            catch { }

            return res;
        }

        public bool GetFiles(Station s,string path,ref List<FileInfo> files)
        {
            bool res = false;

            try
            {
                SocketCommander sc = new SocketCommander(s.PCName, s.Port);
                byte[] objBytes = null;

                if (sc.GetRemoteObject(new PacketLib.Packet(3,Encoding.UTF8.GetBytes(path)), ref objBytes))
                {
                    FileInfo[] obj = null;

                    if (new BytesToObject<FileInfo[]>(objBytes).GetObject(ref obj))
                    {
                        files.AddRange(obj);
                        res = true;
                    }
                }
            }
            catch { }

            return res;
        }

        public bool CreateFolder(Station s, string path)
        {
            bool res = false;

            try
            {
                SocketCommander sc = new SocketCommander(s.PCName, s.Port);
                byte[] objBytes = null;

                if (sc.GetRemoteObject(new PacketLib.Packet(4, Encoding.UTF8.GetBytes(path)), ref objBytes))
                {
                    FileInfo[] obj = null;

                    if (new BytesToObject<FileInfo[]>(objBytes).GetObject(ref obj))
                    {
                        res = true;
                    }
                }
            }
            catch { }
            return res;
        }

        public bool DeleteFolder(Station s, string path)
        {
            bool res = false;
            try
            {
                SocketCommander sc = new SocketCommander(s.PCName, s.Port);
                byte[] objBytes = null;

                if (sc.GetRemoteObject(new PacketLib.Packet(5, Encoding.UTF8.GetBytes(path)), ref objBytes))
                {
                    FileInfo[] obj = null;

                    if (new BytesToObject<FileInfo[]>(objBytes).GetObject(ref obj))
                    {
                        res = true;
                    }
                }
            }
            catch { }
            return res;
        }

        public bool ReNameFolder(Station s, string oldPath,string newPath)
        {
            bool res = false;
            try
            {
                SocketCommander sc = new SocketCommander(s.PCName, s.Port);
                byte[] objBytes = null;

                if (sc.GetRemoteObject(new PacketLib.Packet(6, Encoding.UTF8.GetBytes("")), ref objBytes))
                {
                    FileInfo[] obj = null;

                    if (new BytesToObject<FileInfo[]>(objBytes).GetObject(ref obj))
                    {
                        res = true;
                    }
                }
            }
            catch { }
            return res;
        }

        public bool CreateFile(Station s, string path)
        {
            bool res = false;
            try
            {
                SocketCommander sc = new SocketCommander(s.PCName, s.Port);
                byte[] objBytes = null;

                if (sc.GetRemoteObject(new PacketLib.Packet(7, Encoding.UTF8.GetBytes(path)), ref objBytes))
                {
                    FileInfo[] obj = null;

                    if (new BytesToObject<FileInfo[]>(objBytes).GetObject(ref obj))
                    {
                        res = true;
                    }
                }
            }
            catch { }
            return res;
        }

        public bool ReNameFile(Station s, string srcPath, string dstPath)
        {
            bool res = false;
            try
            {
                SocketCommander sc = new SocketCommander(s.PCName, s.Port);
                byte[] objBytes = null;

                if (sc.GetRemoteObject(new PacketLib.Packet(4, Encoding.UTF8.GetBytes("")), ref objBytes))
                {
                    FileInfo[] obj = null;

                    if (new BytesToObject<FileInfo[]>(objBytes).GetObject(ref obj))
                    {
                        res = true;
                    }
                }
            }
            catch { }

            return res;

        }

        public bool DeleteFile(Station s, string filePath)
        {
            bool res = false;
            try
            {
                SocketCommander sc = new SocketCommander(s.PCName, s.Port);
                byte[] objBytes = null;

                if (sc.GetRemoteObject(new PacketLib.Packet(4, Encoding.UTF8.GetBytes(filePath)), ref objBytes))
                {
                    FileInfo[] obj = null;

                    if (new BytesToObject<FileInfo[]>(objBytes).GetObject(ref obj))
                    {
                        res = true;
                    }
                }
            }
            catch { }
            return res;
        }
    }
}
