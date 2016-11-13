using System;


namespace MyPhotoViewer.Core
{
    public interface IDateTimePeriod
    {
        DateTime From { get; }
        DateTime To { get; }
    }
}