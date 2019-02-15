using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uSolidGServer
{
    public enum ServerPackets
    {
        SWelcomeMessage = 1,
    }
    static class DataSender
    {
        public static void SendWelcomeMessage(int connectionID)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInteger((int)ServerPackets.SWelcomeMessage);
            buffer.WriteString("Welcome to the Server");
            ClientManager.SendDataTo(connectionID, buffer.ToArray());
            buffer.Dispose();
        }
    }
}
