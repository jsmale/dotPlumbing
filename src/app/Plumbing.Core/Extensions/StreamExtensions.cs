using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Plumbing.Extensions
{
    public static class StreamExtensions
    {
        public static byte[] ToBytes(this Stream stream)
        {
            return stream.ToBytes(stream.Length);
        }

        public static byte[] ToBytes(this Stream stream, Int64 length)
        {
            using (var binReader = new BinaryReader(stream))
            {
                if (length <= Int32.MaxValue)
                {
                    return binReader.ReadBytes((Int32)length);
                }

                var remainingLength = length;
                IEnumerable<byte> bytes = new byte[0];
                while (remainingLength > Int32.MaxValue)
                {
                    bytes = bytes.Concat(binReader.ReadBytes(Int32.MaxValue));
                    remainingLength -= Int32.MaxValue;
                }
                bytes = bytes.Concat(binReader.ReadBytes((Int32)remainingLength));
                return bytes.ToArray();
            }
        }
    }
}