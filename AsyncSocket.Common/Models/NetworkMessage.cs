using AsyncSocket.Common.Models.Enums;
using System;

namespace AsyncSocket.Common.Models
{
    [Serializable]
    public class NetworkMessage
    {
        public SerializableGuid SessionId { get; set; }
        public MessageType Type { get; set; }
        public Byte[] Data { get; set; }
    }
}