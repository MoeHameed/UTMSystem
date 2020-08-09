using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MappingSystem
{
    public class MappingSystem
    {
        private const int Port = 7755;
        private readonly UdpClient _udpClient;

        public MappingSystem()
        {
            _udpClient = new UdpClient(Port);
            _udpClient.BeginReceive(ReceiveData, null);
            Console.WriteLine("Listening on port " + Port + " ...");
        }

        public void ReceiveData(IAsyncResult res)
        {
            var ipEndPoint = new IPEndPoint(IPAddress.Any, Port);
            var receivedBytes = _udpClient.EndReceive(res, ref ipEndPoint);

            Console.WriteLine("Received data from " + ipEndPoint.Address + ":" + ipEndPoint.Port + " ...");
            //Console.WriteLine(Encoding.UTF8.GetString(receivedBytes));

            _udpClient.BeginReceive(ReceiveData, null);
        }
    }
}
