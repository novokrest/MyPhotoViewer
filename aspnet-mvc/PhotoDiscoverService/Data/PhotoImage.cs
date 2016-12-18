using System;

namespace PhotoDiscoverService.Data
{
    internal interface IPhotoImage
    {
        string Path { get; }
        DateTime CreationDate { get; }
        byte[] Image { get; }
    }
}