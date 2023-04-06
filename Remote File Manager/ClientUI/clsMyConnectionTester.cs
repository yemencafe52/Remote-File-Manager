namespace ClientUI
{
    using System.Threading;
    public class MyConnectionTester
    {
        private SocketCommander sc;
        private bool isRunning;
        private Thread th0;
        private int ticker;
        private int ping;

        public MyConnectionTester(SocketCommander sc)
        {
            this.sc = sc;
            this.ping = -1;
        }

        public bool Start()
        {
            bool res = false;

            this.isRunning = true;
            try
            {
                this.th0 = new Thread(StartPing);
                {
                    this.th0.Start();
                    res = true;
                }
            }
            catch { }

            return res;
        }

        private void StartPing()
        {
            while(this.isRunning)
            {
                byte[] buffer = new byte[1];

                ticker = System.Environment.TickCount;
                if (sc.GetRemoteObject(new PacketLib.Packet(253),ref buffer))
                {
                    if (buffer[0] == 253)
                    {
                        ping = System.Environment.TickCount - ticker;
                    }
                    else
                    {
                        ticker = -1;
                        ping = -1;
                    }
                }
                else
                {
                    ticker = -1;
                    ping = -1;
                }

                Thread.Sleep(3000);
            }
        }

        public bool Stop()
        {
            bool res = false;

            try
            {
                this.isRunning = false;
                this.th0.Join();
                res = true;
            }
            catch { }
            return res;
        }

        public int Ping
        {
            get
            {
                return this.ping;
            }
        }
        ~MyConnectionTester()
        {
            if (!(this.th0 is null))
            {
                this.isRunning = false;
                th0.Join();
            }
        }
    }
}
