using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uSolidGServer
{
    public enum ClientPacktes
    {
        CHelloServer = 1,
    }
    class DataReciever
    {
        public static void HandleHelloServer(int connectionID, byte[] data)
        {
            ByteBuffer Buffer = new ByteBuffer();
            Buffer.WriteBytes(data);
            int PacketID = Buffer.ReadInteger();
            string msg = Buffer.ReadString();
            Buffer.Dispose();
            Console.WriteLine(msg);
        }
    }
}
