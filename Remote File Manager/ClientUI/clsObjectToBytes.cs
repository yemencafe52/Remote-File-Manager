namespace ClientUI
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    public class BytesToObject<T>
    {
        private readonly byte[] obj;
        public BytesToObject(byte[] ob)
        {
            this.obj = new byte[ob.Length];
            Array.Copy(ob, 0, this.obj, 0, ob.Length);
        }

        public bool GetObject(ref T ob)
        {
            bool res = false;

            try
            {
                using(MemoryStream ms = new MemoryStream(this.obj))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    ob = (T)bf.Deserialize(ms);
                    res = true;
                    
                }
            }
            catch
            { }

            return res;
        }
    }
}
