using Mediator.Net.Contracts;
using System;

namespace AsyncSocket.Server.Mediator.Requests
{
    internal class MinimumRequest : IRequest
    {
        public MinimumRequest(Byte[] data)
        {
            Data = data;
        }


        // PROPERTIES /////////////////////////////////////////////////////////////////////////////
        public Byte[] Data { get; set; }
    }
}