using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace AsyncSocket.Common
{
    public static class NetworkHelpers
    {
        public static Boolean TryGetAddress(out IPAddress ipAddress, String hostName, AddressFamily targetFamily)
        {
            var ipHostInfo = Dns.GetHostEntry(hostName);
            ipAddress = ipHostInfo.AddressList.FirstOrDefault(x => x.AddressFamily == targetFamily);
            return ipAddress != null;
        }
        public static Boolean TryGetAddress(out IPAddress ipAddress, AddressFamily targetFamily)
        {
            var hostName = Dns.GetHostName();
            return TryGetAddress(out ipAddress, hostName, targetFamily);
        }
    }
}