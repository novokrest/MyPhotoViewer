using System;
using System.Drawing;
using System.IO;


namespace MyPhotoViewer.Core
{
    public class ImageChecker
    {
        private readonly byte[] _data;

        public ImageChecker(byte[] data)
        {
            Verifiers.ArgNullVerify(data, nameof(data));
            _data = data;
        }

        public bool IsImage()
        {
            try
            {
                using (var memoryStream = new MemoryStream(_data))
                {
                    Image.FromStream(memoryStream);
                }
            }
            catch(ArgumentException)
            {
                return false;
            }

            return true;
        }

        public static bool IsImage(byte[] data)
        {
            return new ImageChecker(data).IsImage();
        }
    }
}
