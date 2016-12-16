using System;
using System.Collections.Generic;
using System.Linq;

namespace MyPhotoViewer.Core
{
    public enum ImageType
    {
        Png,
        Jpeg,
        Gif,
        Tiff
    }

    public static class ImageTypeFileExtensions
    {
        public static readonly IDictionary<ImageType, string[]> ImageTypeToFileExtensionsMap = new Dictionary<ImageType, string[]>
        {
            { ImageType.Png, new[] { "png" } },
            { ImageType.Jpeg, new[] { "jpeg", "jpg" } },
            { ImageType.Gif, new[] { "gif" } },
            { ImageType.Tiff, new[] { "tiff" } }
        };

        public static readonly IReadOnlyCollection<string> Extensions = ImageTypeToFileExtensionsMap.Values.SelectMany(values => values).ToList();
    }

    public static class ImageMimeTypeConverter
    {
        private static readonly Dictionary<ImageType, string[]> ImageToMimeTypeMap = new Dictionary<ImageType, string[]>
        {
            { ImageType.Png, new[] { "image/png" } },
            { ImageType.Jpeg, new[] { "image/jpg", "image/jpeg" } },
            { ImageType.Gif, new[] { "image/gif" } },
            { ImageType.Tiff, new[] { "image/tiff" } },
        };

        public static ImageType ToImageType(string mimeType)
        {
            foreach (var keyValuePair in ImageToMimeTypeMap)
            {
                ImageType imageType = keyValuePair.Key;
                string[] mimeTypes = keyValuePair.Value;

                if (mimeTypes.Contains(mimeType, StringComparer.OrdinalIgnoreCase))
                {
                    return imageType;
                }
            }

            Verifiers.Verify(false, "Unknown mime type name: {0}", mimeType);
            return default(ImageType);
        }

        public static string ToMimeType(ImageType imageType)
        {
            string[] mimeTypes = ImageToMimeTypeMap[imageType];
            return mimeTypes[0];
        }
    }
}
