using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace uSolidGServer
{
    static class ClientManager
    {
        public static Dictionary<int, client> client = new Dictionary<int, client>();

        public static void CreateNewConnection(TcpClient tempClient)
        {
            client newClient = new client();
            newClient.socket = tempClient;
            newClient.connectionID = ((IPEndPoint)tempClient.Client.RemoteEndPoint).Port;
            newClient.Start();
            client.Add(newClient.connectionID, newClient);
            DataSender.SendWelcomeMessage(newClient.connectionID);
        }
        public static void SendDataTo(int connectionID, byte[] data)
        {
            ByteBuffer Buffer = new ByteBuffer();
            Buffer.WriteInteger(data.GetUpperBound(0) - data.GetUpperBound(0) + 1);
            Buffer.WriteBytes(data);
            client[connectionID].stream.BeginWrite(Buffer.ToArray(), 0, Buffer.ToArray().Length, null, null);
            Buffer.Dispose();
        }
    }
}
