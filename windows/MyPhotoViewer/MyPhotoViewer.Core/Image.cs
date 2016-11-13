namespace MyPhotoViewer.Core
{
    public class Image
    {
        public static Image Create(byte[] data, string mimeType)
        {
            ImageType type = ImageMimeTypeConverter.ToImageType(mimeType);
            return new Image(data, type);
        }

        public Image(byte[] data, ImageType type)
        {
            Data = data;
            Type = type;
        }

        public byte[] Data { get; }
        public ImageType Type { get; }
    }
}
