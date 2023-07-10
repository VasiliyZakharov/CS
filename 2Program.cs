using System;
using System.Collections.Generic;
using System.Linq;

namespace cs2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var input1 = new[] { 1, 2, 3 };
            var result1 = input1.GenerateCombinations(2);
            Console.WriteLine(string.Join("; ", result1.Select(x => $"[{string.Join(", ", x)}]")));

            var input2 = new[] { 1, 2, 3 };
            var result2 = input2.GenerateSubsets();
            Console.WriteLine(string.Join("; ", result2.Select(x => $"[{string.Join(", ", x)}]")));

            var input3 = new[] { 1, 2, 3 };
            var result3 = input3.GeneratePermutations();
            Console.WriteLine(string.Join("; ", result3.Select(x => $"[{string.Join(", ", x)}]")));
        }
    }
}
