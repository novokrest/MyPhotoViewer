using System;
using System.Collections.Generic;

namespace MyPhotoViewer.Core
{
    public static class EnumerableExtension
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach(T element in enumerable)
            {
                action(element);
            }
        }
    }
}