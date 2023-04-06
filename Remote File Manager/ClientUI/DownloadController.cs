using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientUI
{
    public class DownloadController
    {
        private static List<DownloadFile> downList;
        public DownloadController()
        {
            if(downList is null)
            {
                downList = new List<DownloadFile>();
            }
        }
        public bool Add(DownloadFile df)
        {
            bool res = false;

            try
            {   
                downList.Add(df);
                df.Start();
            }
            catch { }

            return res;
        }

        public bool Remove()
        {
            bool res = false;

            //DownloadFile df;

            return res;
        }
    }
}
