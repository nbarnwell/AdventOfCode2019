using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Sources;

namespace Intcode
{
    public static class EnumerableExtensions
    {
        public static int Largest(this IEnumerable<int> values)
        {
            return values.OrderBy(x => x).Last();
        }

        public static void ForEach<T>(this IEnumerable<T> values, Action<T> action)
        {
            foreach (var value in values)
            {
                action(value);
            }
        }

        public static IEnumerable<T> Rotate<T>(this IEnumerable<T> values)
        {
            if (!values.Any())
            {
                return Enumerable.Empty<T>();
            }
            else if (values.Count() == 1)
            {
                return values;
            }
            else
            {
                return values.Skip(1).Concat(new[] {values.First()});
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