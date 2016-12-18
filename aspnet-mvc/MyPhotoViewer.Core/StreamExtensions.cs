using System.IO;

namespace MyPhotoViewer.Core
{
    public static class StreamExtensions
    {
        public static byte[] ReadData(this Stream stream, int length)
        {
            byte[] data = new byte[length];
            int total = 0;
            while(total < length)
            {
                int read = stream.Read(data, total, length - total);
                total += read;
            }
            return data;
        }
    }
}
