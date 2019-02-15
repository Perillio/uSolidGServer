using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uSolidGServer
{
    static class ServerHandleData
    {
        public delegate void Packet(int ConnectionID, byte[] data);
        public static Dictionary<int, Packet> Packets = new Dictionary<int, Packet>();

        public static void initializePackets()
        {
            Packets.Add((int)ClientPacktes.CHelloServer, DataReciever.HandleHelloServer);
        }

        public static void HandleData(int ConnectionID, byte[] data)
        {
            byte[] buffer = (byte[])data.Clone();
            int pLength = 0;
            if(ClientManager.client[ConnectionID].buffer == null)
            {
                ClientManager.client[ConnectionID].buffer = new ByteBuffer();
            }
            ClientManager.client[ConnectionID].buffer.WriteBytes(buffer);
            if(ClientManager.client[ConnectionID].buffer.Count() == 0)
            {
                ClientManager.client[ConnectionID].buffer.Clear();
                return;
            }
            if(ClientManager.client[ConnectionID].buffer.Lenght() >= 4)
            {
                pLength = ClientManager.client[ConnectionID].buffer.ReadInteger(false);
                if(pLength <= 0)
                {
                    ClientManager.client[ConnectionID].buffer.Clear();
                    return;
                }
            }
            while(pLength > 0 & pLength <= ClientManager.client[ConnectionID].buffer.Lenght() -4)
            {
                if(pLength <= ClientManager.client[ConnectionID].buffer.Lenght() -4)
                {
                    ClientManager.client[ConnectionID].buffer.ReadInteger();
                    data = ClientManager.client[ConnectionID].buffer.ReadBytes(pLength);
                    HandleDataPackets(ConnectionID, data);

                }
                pLength = 0;
                if (ClientManager.client[ConnectionID].buffer.Lenght() >= 4)
                {
                    pLength = ClientManager.client[ConnectionID].buffer.ReadInteger(false);
                    if (pLength <= 0)
                    {
                        ClientManager.client[ConnectionID].buffer.Clear();
                        return;
                    }
                }
            }

            if(pLength <= 1)
            {
                ClientManager.client[ConnectionID].buffer.Clear();
            }
        }
        private static void HandleDataPackets(int connectionID, byte[] data)
        {
            ByteBuffer Buffer = new ByteBuffer();
            Buffer.WriteBytes(data);
            int PacketID = Buffer.ReadInteger();
            Buffer.Dispose();
            if(Packets.TryGetValue(PacketID,out Packet Packet))
            {
                Packet.Invoke(connectionID, data);
            }
        }
    }
}
