using AsyncSocket.Common.Models;
using Mediator.Net.Contracts;

namespace AsyncSocket.Server.Mediator.Requests
{
    internal class AverageResponse : IResponse
    {
        public NetworkMessage Message { get; set; }
    }
}