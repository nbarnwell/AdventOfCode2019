namespace OrbitSystem
{
    using System;
    using System.Collections.Generic;

    public static class EnumerableExtensions
    {
        public static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> input, Predicate<T> test)
        {
            foreach (var item in input)
            {
                yield return item;

                if (test(item))
                {
                    yield break;
                }
            }
        }
    }
}