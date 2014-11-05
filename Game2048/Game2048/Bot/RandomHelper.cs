using System;
using System.Collections.Generic;

namespace Game2048.Bot
{
    public static class RandomHelper
    {
        private static readonly Random random = new Random();

        public static int GetRandomTileValue()
        {
            return random.Next(2);
        }

        public static int RandomInt(int exclusiveMax)
        {
            return random.Next(exclusiveMax);
        }

        public static T RandomElement<T>(IList<T> collection)
        {
            if (collection.Count == 0) return default(T);
            int index = random.Next(collection.Count);
            return collection[index];
        }

        public static T RandomElement<T>(T[] array)
        {
            if (array.Length == 0) return default(T);
            int index = random.Next(array.Length);
            return array[index];
        }
    }
}