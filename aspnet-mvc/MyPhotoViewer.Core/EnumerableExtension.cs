﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace MyPhotoViewer.Core
{
    public static class EnumerableExtension
    {
        public static ISet<T> ToSet<T>(this IEnumerable<T> enumerable)
        {
            var set = new HashSet<T>();
            enumerable.ForEach(e => set.Add(e));

            return set;
        }

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
            if (enumerable.Count() == 0) return default(T);

            int id = RandomEx.Next(enumerable.Count());
            return enumerable.ElementAt(id);
        }

        public static string ToJoinString<T>(this IEnumerable<T> enumerable)
        {
            return String.Join(",", enumerable);
        }
    }

    public static class SetExtensions
    {
        public static void AddRange<T>(this ISet<T> set, IEnumerable<T> added)
        {
            added.ForEach(e => set.Add(e));
        }
    }
}