using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.day10
{
    public class Day10 : Day
    {
        SortedSet<int> sortedAdaptarsBag = new SortedSet<int>();

        //Stores the number of ways to complete the chain. Key -> id, Value -> number of ways
        Dictionary<int, long> waysToCompleteChainById = new Dictionary<int, long>();

        public Day10() : base("day10")
        {
        }

        public override void SolveA()
        {
            Console.WriteLine("-----Part1-----");

            int result = 0;
            var watch = System.Diagnostics.Stopwatch.StartNew();

            int _1joltCount = 0;
            int _3joltCount = 0;

            foreach (var item in input)
            {
                sortedAdaptarsBag.Add(Int16.Parse(item));
            }

            int pos1 = 0;
            for (int i = 0; i < sortedAdaptarsBag.Count; i++)
            {
                int pos2 = sortedAdaptarsBag.ElementAt(i);
                int joltDiff = pos2 - pos1;
                pos1 = pos2;

                switch (joltDiff)
                {
                    case 1:
                        _1joltCount++;
                        break;
                    case 3:
                        _3joltCount++;
                        break;
                    default:
                        break;
                }

            }

            result = _1joltCount * (_3joltCount + 1);

            watch.Stop();

            Console.WriteLine($"Value found: {result}");
            Console.WriteLine($"Elapsed time (ms): {watch.ElapsedMilliseconds}");
            Console.WriteLine($"Elapsed ticks: {watch.ElapsedTicks}");
        }

        public override void SolveB()
        {
            Console.WriteLine("-----Part1-----");

            long result = 0;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            sortedAdaptarsBag.Add(0);

            result = HowManyAvailableChargers(0);
            watch.Stop();

            Console.WriteLine($"Value found: {result}");
            Console.WriteLine($"Elapsed time (ms): {watch.ElapsedMilliseconds}");
            Console.WriteLine($"Elapsed ticks: {watch.ElapsedTicks}");
        }

        /// <summary>
        ///  Return the number of ways to complete the chain to a specific sortedAdaptarsBag[idx]
        /// </summary>
        private long HowManyAvailableChargers(int idx)
        {
            int currJolt = sortedAdaptarsBag.ElementAt(idx);
            long result = 0;

            if (idx == sortedAdaptarsBag.Count - 1)
                return 1;

            if (waysToCompleteChainById.ContainsKey(idx))
                return waysToCompleteChainById[idx];

            for (int i = 1; i <= 3 && idx + i < sortedAdaptarsBag.Count; i++)
            {
                if (sortedAdaptarsBag.ElementAt(idx + i) - currJolt <= 3)
                {
                    result += HowManyAvailableChargers(idx + i);
                }
            }

            waysToCompleteChainById.Add(idx, result);

            return result;
        }
    }
}
