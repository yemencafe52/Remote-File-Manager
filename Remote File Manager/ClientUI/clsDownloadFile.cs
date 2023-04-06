using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientUI
{
    public enum DownloadFileState : byte
    {
        DOWNLOADING=1,
        PAUSED,
        COMPLETE
    }
    public class DownloadFile
    {
        private int number;
        private string srcFPath;
        private string dstPath;
        private long fileSize;
        private int downloadRate;
        private float precent;
        private DownloadFileState state;
        //==============================
        private SocketCommander sc;

        public DownloadFile(
         int number,
         string srcFPath,
         string dstPath,
         long fileSize,
         int downloadRate,
         float precent,
         DownloadFileState state,
         SocketCommander sc
            )
        {
            this.number = number;
            this.srcFPath = srcFPath;
            this.dstPath = dstPath;
            this.fileSize = fileSize;
            this.downloadRate = downloadRate;
            this.precent = precent;
            this.state = state;
            this.sc = sc;
        }


        public float Precent
        {
            get
            {
                return this.precent;
            }
        }

        public int Number
        {
            get
            {
                return this.number;
            }
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

        public DownloadFileState State
        {
            get
            {
                return this.state;
            }

        }

        public async void Start()
        {
            //bool res = false;

            try
            {
                this.state = DownloadFileState.PAUSED;
                await Task.Run(() =>
                {
                    var p = new Progress<long>(i => {
                        this.precent = ((i / this.fileSize) * 100);
                        this.state = DownloadFileState.DOWNLOADING;

                        if (i >= this.fileSize)
                        {
                            this.state = DownloadFileState.COMPLETE;
                        }

                    });

                    sc.GetRemoteFile(this.srcFPath,this.dstPath,p);

                });  
            }
            catch { }

           // return res;

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
