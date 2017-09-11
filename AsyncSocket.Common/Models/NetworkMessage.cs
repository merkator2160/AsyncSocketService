using AsyncSocket.Common.Models.Enums;
using System;

namespace AsyncSocket.Common.Models
{
    [Serializable]
    public class NetworkMessage
    {
        public MessageType Type { get; set; }
        public Byte[] Data { get; set; }
    }
}