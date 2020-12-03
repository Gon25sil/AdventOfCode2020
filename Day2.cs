using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day2
    {
        private string[] input;
        public Day2()
        {
            input = File.ReadAllLines("day2.txt");
        }

        public void SolveA()
        {
            Console.WriteLine("-----Part1-----");

            int result = 0;
            int occurrences = 0;
            char character;

            var watch = System.Diagnostics.Stopwatch.StartNew();

            foreach (var entry in input)
            {
                string[] words = entry.Split(' ');

                int[] nums = words[0].Split('-').Select(i => Convert.ToInt32(i)).ToArray();

                character = words[1].FirstOrDefault();
                occurrences = words[2].Split(character).Length - 1;

                if (occurrences >= nums[0] && occurrences <= nums[1])
                    result++;
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            var elapsedTicks = watch.ElapsedTicks;

            Console.WriteLine($"Value found: {result}");

            Console.WriteLine($"Elapsed time (ms): {elapsedMs}");
            Console.WriteLine($"Elapsed ticks: {elapsedTicks}");
        }

        public void SolveB()
        {
            Console.WriteLine("-----Part2-----");

            int result = 0;
            char character;
            char c1;
            char c2;
            var watch = System.Diagnostics.Stopwatch.StartNew();

            foreach (var entry in input)
            {
                string[] words = entry.Split(' ');

                int[] nums = words[0].Split('-').Select(i => Convert.ToInt32(i)).ToArray();

                character = words[1].FirstOrDefault();
                c1 = words[2][nums[0]-1];
                c2 = words[2][nums[1]-1];

                if (c1 == character ^ c2 == character) //XOR
                    result++;
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            var elapsedTicks = watch.ElapsedTicks;

            Console.WriteLine($"Value found: {result}");

            Console.WriteLine($"Elapsed time (ms): {elapsedMs}");
            Console.WriteLine($"Elapsed ticks: {elapsedTicks}");
        }
    }
}
