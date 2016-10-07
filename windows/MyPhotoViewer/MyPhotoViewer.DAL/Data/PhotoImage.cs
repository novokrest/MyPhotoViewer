using System;

namespace MyPhotoViewer.DAL.Data
{
    internal interface IPhotoImage
    {
        string Path { get; }
        DateTime CreationDate { get; }
    }
}