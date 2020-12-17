using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.day15
{
    public class Day15 : Day
    {
        public Day15() : base("day15")
        {
        }

        public override void SolveA()
        {
            Console.WriteLine("-----Part1-----");
            var watch = System.Diagnostics.Stopwatch.StartNew();
            ConcurrentDictionary<int, int> number2PreviousSpokenTurn = new ConcurrentDictionary<int, int>();
            ConcurrentDictionary<int, int> number2count = new ConcurrentDictionary<int, int>();

            string[] numbers = File.ReadAllText($"day15/input.txt").Split(',');
            
            int i = 0;
            int spokenNumber = 0;
            int savePreviousTurn = 0;


            while (i<2020)
            {
                if (i < numbers.Length)
                {
                    spokenNumber = Int16.Parse((numbers[i]));
                }
                else
                {
                    if(number2count[spokenNumber]>1)
                        spokenNumber = i - savePreviousTurn;
                    else 
                        spokenNumber = 0;


                }

                number2PreviousSpokenTurn.TryGetValue(spokenNumber, out savePreviousTurn);
                number2PreviousSpokenTurn.AddOrUpdate(spokenNumber, i+1, (key, newValue) => i+1);
                
                int count = number2count.GetOrAdd(spokenNumber, (key) => 0);
                number2count[spokenNumber] = count+1;
                
                
                i++;
                
            }
            watch.Stop();

            Console.WriteLine($"Value found: {spokenNumber}");
            Console.WriteLine($"Elapsed time (ms): {watch.ElapsedMilliseconds}");
            Console.WriteLine($"Elapsed ticks: {watch.ElapsedTicks}");
        }

        public override void SolveB()
        {
            Console.WriteLine("-----Part2-----");
            var watch = System.Diagnostics.Stopwatch.StartNew();
            ConcurrentDictionary<int, int> number2PreviousSpokenTurn = new ConcurrentDictionary<int, int>();
            ConcurrentDictionary<int, int> number2count = new ConcurrentDictionary<int, int>();

            string[] numbers = File.ReadAllText($"day15/input.txt").Split(',');

            int i = 0;
            int spokenNumber = 0;
            int savePreviousTurn = 0;


            while (i < 30000000)
            {
                if (i < numbers.Length)
                {
                    spokenNumber = Int16.Parse((numbers[i]));
                }
                else
                {
                    if (number2count[spokenNumber] > 1)
                        spokenNumber = i - savePreviousTurn;
                    else
                        spokenNumber = 0;


                }

                number2PreviousSpokenTurn.TryGetValue(spokenNumber, out savePreviousTurn);
                number2PreviousSpokenTurn.AddOrUpdate(spokenNumber, i + 1, (key, newValue) => i + 1);

                int count = number2count.GetOrAdd(spokenNumber, (key) => 0);
                number2count[spokenNumber] = count + 1;


                i++;

            }
            watch.Stop();

            Console.WriteLine($"Value found: {spokenNumber}");
            Console.WriteLine($"Elapsed time (ms): {watch.ElapsedMilliseconds}");
            Console.WriteLine($"Elapsed ticks: {watch.ElapsedTicks}");
        }
    }
}
