using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientUI
{
    public enum UploadFileState : byte
    {
        UPLOADING = 1,
        PAUSED
    }
    public class UploadFile
    {
        private string srcFPath;
        private string dstPath;
        private long fileSize;
        private int downloadRate;
        private UploadFileState state;

        public UploadFile(
         string srcFPath,
         string dstPath,
         long fileSize,
         int downloadRate,
         UploadFileState state
            )
        {
            this.srcFPath = srcFPath;
            this.dstPath = dstPath;
            this.fileSize = fileSize;
            this.downloadRate = downloadRate;
            this.state = state;
        }

        public string SourceFile
        {
            get
            {
                return this.srcFPath;
            }
        }

        public string DestnationPath
        {
            get
            {
                return this.dstPath;
            }
        }

        public long FileSize
        {
            get
            {
                return this.fileSize;
            }
        }

        public int Rate
        {
            get
            {
                return this.downloadRate;
            }
        }

        public UploadFileState State
        {
            get
            {
                return this.state;
            }

        }

        public bool Start()
        {
            bool res = false;
            return res;

        }

        public bool Pause()
        {
            bool res = false;
            return res;

        }

        public bool Cancel()
        {
            bool res = false;
            return res;
        }
    }
}
