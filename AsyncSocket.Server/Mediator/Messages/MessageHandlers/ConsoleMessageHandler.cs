using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System;
using System.Threading.Tasks;

namespace AsyncSocket.Server.Mediator.Messages.MessageHandlers
{
    internal class ConsoleMessageHandler : IEventHandler<ConsoleMessage>
    {
        public Task Handle(IReceiveContext<ConsoleMessage> context)
        {
            return Task.Run(() =>
            {
                Console.WriteLine(context.Message.Text);
            });
        }
    }
}