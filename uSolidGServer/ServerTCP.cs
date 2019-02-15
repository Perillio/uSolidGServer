using System;
using System.Net;
using System.Net.Sockets;

namespace uSolidGServer
{
   
    static class ServerTCP
    {
        static TcpListener ServerSocket = new TcpListener(IPAddress.Any, 5557);
        public static void initializeNetwork()
        {
            Console.WriteLine("Packet listener starting...");
            ServerHandleData.initializePackets();
            ServerSocket.Start();
            ServerSocket.BeginAcceptTcpClient(new AsyncCallback(OnClientConnect), null);
        }

        private static void OnClientConnect(IAsyncResult result)
        {
            TcpClient client = ServerSocket.EndAcceptTcpClient(result);
            ServerSocket.BeginAcceptTcpClient(new AsyncCallback(OnClientConnect), null);
            ClientManager.CreateNewConnection(client);

        }
    }
}
