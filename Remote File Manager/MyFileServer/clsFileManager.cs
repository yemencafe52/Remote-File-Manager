using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MyFileServer
{
    public class FileManager
    {

        public byte[] GetDrives()
        {
            byte[] res = new byte[] { 0 };

            try
            {
                res = new ObjectToBytes<DriveInfo[]>(DriveInfo.GetDrives()).GetBytes();
            }
            catch { }

            return res;
        }

        public byte[] GetDir(string path)
        {
            byte[] res = new byte[] { 0 };

            try
            {
                res = new ObjectToBytes<DirectoryInfo[]>(new DirectoryInfo(path).GetDirectories()).GetBytes();
            }
            catch { }

            return res;
        }

        public byte[] GetFiles(string path)
        {
            byte[] res = new byte[] { 0 };

            try
            {
                res = new ObjectToBytes<FileInfo[]>(new DirectoryInfo(path).GetFiles()).GetBytes();
            }
            catch { }

            return res;
        }

        public bool CreateFolder(string name)
        {
            bool res = false;

            try
            {
                Directory.CreateDirectory(name);
                res = true;
            }
            catch { }
            return res;
        }

        public bool DeleteFolder(string name)
        {
            bool res = false;
            try
            {
                Directory.Delete(name,true);
                res = true;
            }
            catch { }
            return res;
        }

        public bool ReNameFolder(string oldPath,string newPath)
        {
            bool res = false;
            try
            {
                Directory.Move(oldPath, newPath);
                res = true;
            }
            catch { }
            return res;
        }

        public bool CreateFile(string path,string fileName)
        {
            bool res = false;
            try
            {
                FileStream fs = new FileStream(path, FileMode.Create);
                fs.Close();
                res = true;
            }
            catch { }
            return res;
        }

        public bool ReNameFile(string srcPath, string dstPath)
        {
            bool res = false;
            try
            {
                File.Move(srcPath, dstPath);
                res = true;

            }
            catch { }

            return res;

        }

        public bool DeleteFile(string filePath)
        {
            bool res = false;
            try
            {
                File.Delete(filePath);
                res = true;
            }
            catch { }
            return res;
        }
    }
}
