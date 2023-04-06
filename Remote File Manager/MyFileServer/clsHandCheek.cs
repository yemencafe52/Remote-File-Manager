namespace MyFileServer
{
    public class HandCheek
    {
        private bool success;

        public bool Success
        {
            get
            {
                return this.success;
            }
        }
        public HandCheek(string user,string password)
        {
            success = true;
        }
    }
}
