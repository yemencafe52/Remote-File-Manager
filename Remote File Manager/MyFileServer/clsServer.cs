namespace MyFileServer
{
    using System;
    using System.Net.Sockets;
    using System.Net;

    public class Server
    {
        private Socket s;
        private int port;
        private bool isRunning;
        public Server(int port)
        {
            this.port = port;
        }

        public bool StartServer()
        {
            bool res = false;

            try
            {
                StopServer();
                this.s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.s.Bind(new IPEndPoint(IPAddress.Parse("0.0.0.0"), this.port));
                this.s.Listen(0);
                this.s.BeginAccept(new AsyncCallback(OnAccept), null);
                this.isRunning = true;
            }
            catch
            {
                StopServer();
            }
            return res;
        }

        private void OnAccept(IAsyncResult ar)
        {
            try
            {
                Socket c = this.s.EndAccept(ar);
                this.s.BeginAccept(new AsyncCallback(OnAccept), null);
                new RequestProcess(c);

            }
            catch
            {
                StopServer();
            }
        }

        public bool StopServer()
        {
            bool res = false;

            try
            {
                if(!(this.s is null))
                {
                    this.s.Close();
                    this.s.Dispose();
                }

                this.s = null;
                this.isRunning = false;
                res = true;
            }
            catch
            {

            }

            return res;
        }

        ~Server()
        {
            StopServer();
        }

        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }
        }
    }

}
