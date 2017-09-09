using Mediator.Net.Contracts;
using System;

namespace AsyncSocket.Server.Mediator.Messages
{
    internal class ConsoleMessage : IEvent
    {
        public ConsoleMessage(String text)
        {
            Text = text;
        }


        // PROPERTIES /////////////////////////////////////////////////////////////////////////////
        public String Text { get; }
    }
}