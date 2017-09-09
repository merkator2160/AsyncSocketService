using AsyncSocket.Server.Mediator.Messages;
using AsyncSocket.Server.Models;
using Mediator.Net;
using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;

namespace AsyncSocket.Server
{
    internal class ConnectionListener : IDisposable
    {
        private readonly IMediator _mediator;
        private readonly NetworkConfig _config;
        private Boolean _disposed;
        private readonly TcpListener _tcpListener;


        public ConnectionListener(RootConfig config, IMediator mediator)
        {
            _mediator = mediator;
            _config = config.NetworkConfig;
            _tcpListener = TcpListener.Create(_config.Port);

            ThreadPool.QueueUserWorkItem(AcceptNewClientThread);
        }


        // FUNCTONS ///////////////////////////////////////////////////////////////////////////////
        public void Start()
        {
            _tcpListener.Start();

            _mediator.PublishAsync(new ConsoleMessage($"Array Min and Avg service is now running on port {_config.Port}"));
            _mediator.PublishAsync(new ConsoleMessage("Hit <enter> to stop service\n"));
        }
        public void Stop()
        {
            _tcpListener.Stop();
        }


        // THREADS ////////////////////////////////////////////////////////////////////////////////
        private void AcceptNewClientThread(Object state)
        {
            while (!_disposed)
            {
                try
                {
                    var tcpClient = _tcpListener.AcceptTcpClient();
                    var clientEndPoint = tcpClient.Client.RemoteEndPoint.ToString();
                    _mediator.PublishAsync(new ConsoleMessage($"Received connection request from {clientEndPoint}"));
                    _mediator.PublishAsync(new NewTcpClientAcceptedMessage(tcpClient));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            }
        }


        // IDisposable ////////////////////////////////////////////////////////////////////////////
        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;

                _tcpListener?.Stop();
            }
        }
    }
}