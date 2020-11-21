using System;
using System.Collections.Generic;
using System.Linq;

namespace Intcode
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> values, Action<T> action)
        {
            foreach (var value in values)
            {
                action(value);
            }
        }

        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> values)
        {
            if (!values.Any())
            {
                yield return Enumerable.Empty<T>();
            }
            else
            {
                foreach (IEnumerable<T> permutations in GetPermutations(values.Skip(1)))
                {
                    yield return permutations;
                    yield return values.Take(1).Concat(permutations);
                }
            }
        }
    }
}