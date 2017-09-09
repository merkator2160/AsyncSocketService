using AsyncSocket.Common.Extensions;
using AsyncSocket.Common.Models;
using AsyncSocket.Common.Models.Enums;
using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncSocket.Client
{
    class Program
    {
        private const String Host = "127.0.0.1";
        private const Int32 Port = 50000;


        static void Main(String[] args)
        {
            try
            {
                Thread.Sleep(1000); // waiting for server starts
                CalculateAverageAsync(new[] { 1.1f, 2.2f, 3.3f });
                Console.WriteLine();
                CalculateMinimumAsync(new[] { 1.1f, 2.2f, 3.3f });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadKey();
            }
        }
        private static async void CalculateAverageAsync(Single[] values)
        {
            using (var requestMemoryStream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(requestMemoryStream, values);
                var response = await SendRequestAsync(Host, Port, new NetworkMessage()
                {
                    Type = MessageType.Average,
                    Data = requestMemoryStream.ToArray()
                });

                var result = BitConverter.ToSingle(response.Data, 0);
                Console.WriteLine($"Received response: {result:F2}");
            }
        }
        private static async void CalculateMinimumAsync(Single[] values)
        {
            using (var requestMemoryStream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(requestMemoryStream, values);
                var response = await SendRequestAsync(Host, Port, new NetworkMessage()
                {
                    Type = MessageType.Minimum,
                    Data = requestMemoryStream.ToArray()
                });

                var result = BitConverter.ToSingle(response.Data, 0);
                Console.WriteLine($"Received response: {result:F2}");
            }
        }
        private static async Task<NetworkMessage> SendRequestAsync(String address, Int32 port, NetworkMessage message)
        {
            using (var client = new TcpClient())
            {
                await client.ConnectAsync(address, port);
                using (var networkStream = client.GetStream())
                {
                    await networkStream.WriteObjectAsync(message);
                    return await networkStream.ReadObjectAsync<NetworkMessage>();
                }
            }
        }
    }
}
