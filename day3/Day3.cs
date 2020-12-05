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
        public readonly int Width;
        public readonly int Height;


        public Day3()
        {
            Console.WriteLine("\n ****** DAY 3 ******");

            input = File.ReadAllLines("day3/day3.txt");
            Width = input[0].Length;
            Height = input.Length;

        }

        public void SolveA()
        {
            Console.WriteLine("-----Part1-----");

            int result = 0;
            int posX = 0;
            int posY = 0;
            int SlopX = 3;
            int SlopY = 1;

            var watch = System.Diagnostics.Stopwatch.StartNew();

            while (posY != Height - 1)
            {
                //map.NextPosition(ref posX, ref posY);
                posX = (posX + 3) % Width; // a % b -> a - (a / b) * b;
                posY += 1;

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
            int SlopX = 3;
            int SlopY = 1;

            var watch = System.Diagnostics.Stopwatch.StartNew();

            foreach (var currentSlop in slops)
            {
                posX = 0;
                posY = 0;
                numberOfTrees = 0;

                SlopX = currentSlop[0];
                SlopY = currentSlop[1];

                while (posY != Height - 1)
                {
                    posX = (posX + SlopX) % input[0].Length; // a % b -> a - (a / b) * b;
                    posY += SlopY;

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
