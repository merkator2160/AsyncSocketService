using AsyncSocket.Common.Models;
using Mediator.Net.Contracts;

namespace AsyncSocket.Server.Mediator.Requests.RequestHandlers
{
    internal class MinimumResponse : IResponse
    {
        public NetworkMessage Message { get; set; }
    }
}