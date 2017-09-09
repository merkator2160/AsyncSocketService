using AsyncSocket.Server.Models;
using Mediator.Net;
using Mediator.Net.Unity;
using Microsoft.Practices.Unity;
using System;

namespace AsyncSocket.Server
{
    class Program
    {
        private const Int32 Port = 50000;
        private const String Address = "127.0.0.1";


        static void Main(String[] args)
        {
            using (var container = ConfigureContainer())
            {
                var connectionListener = container.Resolve<ConnectionListener>();
                connectionListener.Start();

                Console.ReadKey();
            }
        }
        private static IUnityContainer ConfigureContainer()
        {
            var container = new UnityContainer();

            var mediaBuilder = new MediatorBuilder();
            mediaBuilder.RegisterHandlers(typeof(Program).Assembly);
            UnityExtensioins.Configure(mediaBuilder, container);

            container.RegisterType<ConnectionListener>();
            container.RegisterInstance(new RootConfig()
            {
                NetworkConfig = new NetworkConfig()
                {
                    Address = Address,
                    Port = Port
                }
            });

            return container;
        }
    }
}
