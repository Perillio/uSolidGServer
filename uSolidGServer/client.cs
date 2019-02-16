using System;
using System.Net.Sockets;

namespace uSolidGServer
{
    public class client
    {
        public int connectionID;
        public TcpClient socket;
        public NetworkStream stream;
        private byte[] recBuffer;
        public ByteBuffer buffer;

        public void Start()
        {
            socket.SendBufferSize = 4096;
            socket.ReceiveBufferSize = 4096;
            stream = socket.GetStream();
            recBuffer = new byte[4096];
            stream.BeginRead(recBuffer, 0, socket.ReceiveBufferSize, OnRecieveData, null);
            Console.WriteLine("Icoming connection from ", socket.Client.RemoteEndPoint.ToString());
        }

        private void OnRecieveData(IAsyncResult result)
        {
            try
            {
                int length = stream.EndRead(result);
                if(length <= 0)
                {
                    CloseConnection();
                    return;
                }
                byte[] newbytes = new byte[length];
                Array.Copy(recBuffer, newbytes, length);


                ServerHandleData.HandleData(connectionID, newbytes);

                stream.BeginRead(recBuffer, 0, socket.ReceiveBufferSize, OnRecieveData, null);
            }
            catch (Exception)
            {
                CloseConnection();
                return;
            }
        }

        private void CloseConnection()
        {
            Console.WriteLine("Connection from '{0}' " + socket.Client.RemoteEndPoint.ToString() + "terminated.");
            socket.Close();
        }
    }
}
