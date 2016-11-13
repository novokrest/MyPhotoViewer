using System;
using System.Collections.Generic;
using System.Linq;

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

        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> enumerable, int chunkLength)
        {
            return enumerable.Select((e, i) => new { Value = e, Index = i / chunkLength })
                             .GroupBy(pair => pair.Index, pair => pair.Value)
                             .Cast<IEnumerable<T>>();
        }

        public static T GetRandom<T>(this IEnumerable<T> enumerable)
        {
            int id = RandomEx.Next(enumerable.Count());
            return enumerable.ElementAt(id);
        }
    }
}