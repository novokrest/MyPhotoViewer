using System;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using DrawingImage = System.Drawing.Image;

namespace MyPhotoViewer.Core
{
    public class ImageTypeRecognizer
    {
        private static readonly ImageTypeMatcher[] ImageTypeMatchers = new[]
        {
            new ImageTypeMatcher(ImageFormat.Png, ImageType.Png),
            new ImageTypeMatcher(ImageFormat.Gif, ImageType.Gif),
            new ImageTypeMatcher(ImageFormat.Jpeg, ImageType.Jpeg),
            new ImageTypeMatcher(ImageFormat.Tiff, ImageType.Tiff)
        };

        private readonly byte[] _data;

        public static ImageType Recognize(byte[] data)
        {
            return new ImageTypeRecognizer(data).Recognize();
        }

        public ImageTypeRecognizer(byte[] data)
        {
            _data = data;
        }

        public ImageType Recognize()
        {
            using (var memoryStream = new MemoryStream(_data))
            {
                var image = DrawingImage.FromStream(memoryStream);
                return ImageTypeMatchers.First(matcher => matcher.Match(image)).ImageType;
            }
        }

        private class ImageTypeMatcher
        {
            private ImageFormat _imageFormat;

            public ImageTypeMatcher(ImageFormat imageFormat, ImageType imageType)
            {
                _imageFormat = imageFormat;
                ImageType = imageType;
            }

            public ImageType ImageType { get; }

            public bool Match(DrawingImage image)
            {
                return _imageFormat.Equals(image.RawFormat);
            }
        }
    }
}
