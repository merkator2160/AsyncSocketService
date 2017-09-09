using AsyncSocket.Common.Models;
using AsyncSocket.Common.Models.Enums;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace AsyncSocket.Server.Mediator.Requests.RequestHandlers
{
    internal class AverageRequestHandler : IRequestHandler<AverageRequest, AverageResponse>
    {
        public Task<AverageResponse> Handle(ReceiveContext<AverageRequest> context)
        {
            return Task.Run(() =>
            {
                using (var memoryStream = new MemoryStream(context.Message.Data))
                {
                    var values = (Single[])new BinaryFormatter().Deserialize(memoryStream);

                    var sum = values.Sum();
                    var result = sum / values.Length;

                    return new AverageResponse
                    {
                        Message = new NetworkMessage()
                        {
                            Type = MessageType.Average,
                            Data = BitConverter.GetBytes(result)
                        }
                    };
                }
            });
        }
    }
}