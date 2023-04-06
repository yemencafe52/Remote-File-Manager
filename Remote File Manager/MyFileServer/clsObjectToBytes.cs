namespace MyFileServer
{
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    public class ObjectToBytes<T>
    {
        private readonly T obj;
        public ObjectToBytes(T ob)
        {
            this.obj = ob;
        }

        public byte[] GetBytes()
        {
            byte[] res = null;

            try
            {
                using(MemoryStream ms = new MemoryStream())
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(ms, this.obj);
                    res = ms.ToArray();
                }
            }
            catch
            { }

            return res;
        }
    }
}
