using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day1
    {
        private int[] array;

        public Day1()
        {
            Console.WriteLine("\n ****** DAY 1 ******");
            this.array = File.ReadAllLines("day1/day1.txt").Select(t => Convert.ToInt32(t)).ToArray();
        }

        /// <summary>
        /// Before you leave, the Elves in accounting just need you to fix your expense report (your puzzle input); apparently, something isn't quite adding up.
        /// Specifically, they need you to find the two entries that sum to 2020 and then multiply those two numbers together.
        /// For example, suppose your expense report contained the following:
        /// 1721 
        /// 979
        /// 366
        /// 299
        /// 675
        /// 1456
        /// In this list, the two entries that sum to 2020 are 1721 and 299. Multiplying them together produces 1721 * 299 = 514579, so the correct answer is 514579.
        /// Of course, your expense report is much larger. Find the two entries that sum to 2020; what do you get if you multiply them together?
        /// </summary>
        public void SolverA()
        {
            

            #region Part 1
            Console.WriteLine("-----Part1-----");

            HashSet<int> checkedValues = new HashSet<int>();
            int result = 0;
            int pair = 0;
            int item = 0;

            var watch = System.Diagnostics.Stopwatch.StartNew();


            for (int i = 0; i < array.Length; i++)
            {
                item = array[i];
                pair = 2020 - item;
                if (checkedValues.Contains(pair))
                {
                    result = item * pair;
                    break;
                }
                checkedValues.Add(item);
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            var elapsedTicks = watch.ElapsedTicks;

            Console.WriteLine($"Value found: {result}");

            Console.WriteLine($"Elapsed time (ms): {elapsedMs}");
            Console.WriteLine($"Elapsed ticks: {elapsedTicks}");
            #endregion


        }

        
         /// <summary>
         /// The Elves in accounting are thankful for your help; one of them even offers you a starfish coin they had left over from a past vacation.
         /// They offer you a second one if you can find three numbers in your expense report that meet the same criteria.
         /// Using the above example again, the three entries that sum to 2020 are 979, 366, and 675. Multiplying them together produces the answer, 241861950.
         /// In your expense report, what is the product of the three entries that sum to 2020?
         /// </summary>
        public void SolverB()
        {
            Console.WriteLine("-----Part2-----");
            
            HashSet<int> checkedValues = new HashSet<int>();
            int result = 0;
            int pair = 0;
            int elem1 = 0;
            int elem2 = 0;
            bool found = false;

            var watch = System.Diagnostics.Stopwatch.StartNew();

            for (int i = 0; i < array.Length-2; i++)
            {
                elem1 = array[i];
                for (int j = 1; j < array.Length; j++)
                {
                    elem2 = array[j];
                    pair = 2020 - elem1 - elem2;
                   
                    if (checkedValues.Contains(pair))
                    {
                        result = pair * elem1 * elem2;
                        found = true;
                        break;
                    }
                    checkedValues.Add(elem2);
                }

                if (found)
                    break;
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
