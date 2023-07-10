using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> GenerateCombinations<T>(this IEnumerable<T> source, int k, IEqualityComparer<T> comparer = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (k <= 0)
                throw new ArgumentException("k should be greater than zero.");
            if (comparer == null)
                comparer = EqualityComparer<T>.Default;

            return GenerateCombinationsImpl(source.ToList(), k, comparer);
        }

        private static IEnumerable<IEnumerable<T>> GenerateCombinationsImpl<T>(List<T> source, int k, IEqualityComparer<T> comparer)
        {
            if (k == 0)
            {
                yield return Enumerable.Empty<T>();
                yield break;
            }
            if (source.Count == 0)
                yield break;

            var head = source[0];
            var tail = source.Skip(1).ToList();

            foreach (var subCombination in GenerateCombinationsImpl(tail, k - 1, comparer))
            {
                yield return new T[] { head }.Concat(subCombination);
            }

            foreach (var combination in GenerateCombinationsImpl(tail, k, comparer))
            {
                yield return combination;
            }
        }

        public static IEnumerable<IEnumerable<T>> GenerateSubsets<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (comparer == null)
                comparer = EqualityComparer<T>.Default;

            return GenerateSubsetsImpl(source.ToList(), comparer);
        }

        private static IEnumerable<IEnumerable<T>> GenerateSubsetsImpl<T>(List<T> source, IEqualityComparer<T> comparer)
        {
            if (source.Count == 0)
            {
                yield return Enumerable.Empty<T>();
                yield break;
            }

            var head = source[0];
            var tail = source.Skip(1).ToList();

            foreach (var subset in GenerateSubsetsImpl(tail, comparer))
            {
                yield return subset;

                if (!subset.Contains(head, comparer))
                {
                    yield return new T[] { head }.Concat(subset);
                }
            }
        }

        public static IEnumerable<IEnumerable<T>> GeneratePermutations<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (comparer == null)
                comparer = EqualityComparer<T>.Default;

            return GeneratePermutationsImpl(source.ToList(), comparer);
        }

        private static IEnumerable<IEnumerable<T>> GeneratePermutationsImpl<T>(List<T> source, IEqualityComparer<T> comparer)
        {
            if (source.Count == 0)
            {
                yield return Enumerable.Empty<T>();
                yield break;
            }

            for (int i = 0; i < source.Count; i++)
            {
                var element = source[i];
                var remaining = source.Take(i).Concat(source.Skip(i + 1));

                foreach (var permutation in GeneratePermutationsImpl(remaining.ToList(), comparer))
                {
                    yield return new T[] { element }.Concat(permutation);
                }
            }
        }
    }

}
