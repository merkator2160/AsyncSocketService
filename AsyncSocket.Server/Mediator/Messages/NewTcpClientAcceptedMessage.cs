using Mediator.Net.Contracts;
using System.Net.Sockets;

namespace AsyncSocket.Server.Mediator.Messages
{
    internal class NewTcpClientAcceptedMessage : IEvent
    {
        public NewTcpClientAcceptedMessage(TcpClient client)
        {
            Client = client;
        }


        // PROPERTIES /////////////////////////////////////////////////////////////////////////////
        public TcpClient Client { get; }
    }
}