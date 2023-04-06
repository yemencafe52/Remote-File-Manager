namespace ClientUI
{
    public class Station
    {
        private string pcName;
        private int port;
        private string userName;
        private string password;

        public Station(
         string pcName,
         int port,
         string userName,
         string password     
            )
        {
            this.pcName = pcName;
            this.port = port;
            this.userName = userName;
            this.password = password;
        }

        public string PCName
        {
            get
            {
                return this.pcName;
            }
        }

        public int Port
        {
            get
            {
                return this.port;
            }
        }

        public string UserName
        {
            get
            {
                return this.userName;
            }
        }

        public string Password
        {
            get
            {
                return this.password;
            }
        }
    }
}