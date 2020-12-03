using AdventOfCode2020.day3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day3
    {
        private string[] input;
        private Map map;

        public Day3()
        {
            Console.WriteLine("\n ****** DAY 3 ******");

            input = File.ReadAllLines("day3/day3.txt");
            map = new Map(input[0].Length, input.Length, 3, 1);
        }

        public void SolveA()
        {
            Console.WriteLine("-----Part1-----");

            int result = 0;
            int posX = 0;
            int posY = 0;

            var watch = System.Diagnostics.Stopwatch.StartNew();

            while (posY != map.Height - 1)
            {
                map.NextPosition(ref posX, ref posY);

                if (input[posY][posX] == '#')
                {
                    result++;
                }
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

            int[][] slops =
            {
                new int[] { 1,1 },
                new int[] { 3,1 },
                new int[] { 5,1 },
                new int[] { 7,1 },
                new int[] { 1,2 }
            };

            int numberOfTrees = 0;
            long result = 1;
            int posX = 0;
            int posY = 0;

            var watch = System.Diagnostics.Stopwatch.StartNew();

            foreach (var currentSlop in slops)
            {
                posX = 0;
                posY = 0;
                numberOfTrees = 0;

                map.SlopX = currentSlop[0];
                map.SlopY = currentSlop[1];

                while (posY != map.Height - 1)
                {
                    map.NextPosition(ref posX, ref posY);

                    if (input[posY][posX] == '#')
                    {
                        numberOfTrees++;
                    }
                }

                result *= numberOfTrees;
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
