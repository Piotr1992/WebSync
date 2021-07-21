using System.Linq;
using Ionic.Zlib;


namespace BmpWebSyncLogic.helpers
{
    public class ZipCompress
    {

        public static byte[] Compress(byte[] data)
        {
            var output = ZlibStream.CompressBuffer(data);
            return output.ToArray();
        }


        public static byte[] DeCompress (byte[] data)
        {
            var output = ZlibStream.UncompressBuffer(data);
            return output.ToArray();
        }

    }
}
