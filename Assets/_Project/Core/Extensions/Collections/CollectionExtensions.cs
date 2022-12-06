using System;
using System.Collections.Generic;

namespace _Project.Core.Extensions.Collections {
    public static class CollectionExtensions {
        static readonly Random Random = new(Guid.NewGuid().GetHashCode());

        public static void Shuffle<T>(this T[] array) {
            var n = array.Length;
            while (n > 1) {
                var k = Random.Next(n--);
                (array[n], array[k]) = (array[k], array[n]);
            }
        }

        public static void Shuffle<T>(this IList<T> array) {
            var n = array.Count;
            while (n > 1) {
                var k = Random.Next(n--);
                (array[n], array[k]) = (array[k], array[n]);
            }
        }

        public static T GetRandomItem<T>(this T[] array) {
            return array[Random.Next(array.Length)];
        }

        public static T GetRandomItem<T>(this IList<T> list) {
            return list[Random.Next(list.Count)];
        }

        public static T[] MergeArrays<T>(this T[] array, T[] otherArray) {
            Array.Resize(ref array, array.Length + otherArray.Length);
            Array.Copy(otherArray, 0, array, array.Length, otherArray.Length);

            return array;
        }

        public static IList<T> MergeLists<T>(this IList<T> list, IList<T> otherList) {
            var mergedList = new List<T>(list.Count + otherList.Count);
            if (mergedList == null) 
                throw new ArgumentNullException(nameof(mergedList));
            mergedList.AddRange(list);
            mergedList.AddRange(otherList);

            return mergedList;
        }
    }
}