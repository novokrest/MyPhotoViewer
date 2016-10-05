using System;

namespace MyPhotoViewer.Models
{
    internal interface IPhotoImage
    {
        string Path { get; }
        DateTime CreationDate { get; }
    }
}