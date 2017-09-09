using Mediator.Net.Contracts;
using System;

namespace AsyncSocket.Server.Mediator.Requests
{
    internal class AverageRequest : IRequest
    {
        public AverageRequest(Byte[] data)
        {
            Data = data;
        }


        // PROPERTIES /////////////////////////////////////////////////////////////////////////////
        public Byte[] Data { get; set; }
    }
}