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

        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> enumerable, int chunkLength)
        {
            IEnumerator<T> enumerator = enumerable.GetEnumerator();

            while(enumerator.MoveNext())
            {
                yield return GetElements(enumerator, chunkLength);
            }
        }

        private static IEnumerable<T> GetElements<T>(IEnumerator<T> enumerator, int elementsCount)
        {
            bool hasNextElement = true;
            for (int i = 0; i < elementsCount && hasNextElement; ++i)
            {
                yield return enumerator.Current;
                hasNextElement = enumerator.MoveNext();
            }
        }
    }
}