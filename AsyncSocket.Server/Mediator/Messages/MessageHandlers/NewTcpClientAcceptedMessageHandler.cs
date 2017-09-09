using AsyncSocket.Common.Extensions;
using AsyncSocket.Common.Models;
using AsyncSocket.Common.Models.Enums;
using AsyncSocket.Server.Mediator.Requests;
using AsyncSocket.Server.Mediator.Requests.RequestHandlers;
using Mediator.Net;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncSocket.Server.Mediator.Messages.MessageHandlers
{
    internal class NewTcpClientAcceptedMessageHandler : IEventHandler<NewTcpClientAcceptedMessage>
    {
        private readonly IMediator _mediator;


        public NewTcpClientAcceptedMessageHandler(IMediator mediator)
        {
            _mediator = mediator;
        }


        // IEventHandler<NewTcpClientAcceptedMessage> /////////////////////////////////////////////
        public Task Handle(IReceiveContext<NewTcpClientAcceptedMessage> context)
        {
            return Task.Run(() =>
            {
                HandleSingleRequestAsync(context.Message.Client);
            });
        }


        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        private async void HandleSingleRequestAsync(TcpClient tcpClient)
        {
            try
            {
                using (tcpClient)
                {
                    using (var networkStream = tcpClient.GetStream())
                    {
                        while (true)
                        {
                            if (!networkStream.DataAvailable)
                            {
                                Thread.Sleep(10);
                                continue;
                            }

                            var request = await networkStream.ReadObjectAsync<NetworkMessage>();
                            var response = await HandleResponseAsync(request);
                            await networkStream.WriteObjectAsync(response);

                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private async Task<NetworkMessage> HandleResponseAsync(NetworkMessage request)
        {
            switch (request.Type)
            {
                case MessageType.Minimum:
                    var minimumResult = await _mediator.RequestAsync<MinimumRequest, MinimumResponse>(new MinimumRequest(request.Data));
                    return minimumResult.Message;

                case MessageType.Average:
                    var averageResult = await _mediator.RequestAsync<AverageRequest, AverageResponse>(new AverageRequest(request.Data));
                    return averageResult.Message;

                default:
                    throw new NotSupportedException($"Operation type \"{request.Type}\" is not supported!");
            }
        }
    }
}