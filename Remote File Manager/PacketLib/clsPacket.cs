namespace PacketLib
{
    using System;
    public class Packet
    {
        private byte cmd;
        private byte[] buffer;

        public Packet(byte cmd)
        {
            this.cmd = cmd;
            this.buffer = new byte[0];
        }

        public Packet(byte cmd,byte[] buffer)
        {
            this.cmd = cmd;
            this.buffer = new byte[buffer.Length];
            Array.Copy(buffer, 0, this.buffer,0 ,this.buffer.Length);
        }

        public Packet(byte[] ar)
        {
            this.cmd = ar[0];
            this.buffer = new byte[ar.Length - 1];
            Array.Copy(ar, 1, this.buffer, 0, this.buffer.Length);
        }

        public byte Command
        {
            get
            {
                return this.cmd;
            }
        }
        public byte[] Buffer
        {
            get
            {
                return this.buffer;
            }
        }

        public byte[] ToBytes()
        {
            byte[] res = new byte[this.buffer.Length + 1];
            res[0] = this.cmd;
            Array.Copy(this.buffer, 0, res, 1, this.buffer.Length);
            return res;
        }
    }
}
