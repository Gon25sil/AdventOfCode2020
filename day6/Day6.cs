using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.day6
{
    public class Day6 : Day
    {
        public Day6() : base("day6")
        {
        }

        public override void SolveA()
        {
            Console.WriteLine("-----Part1-----");
            int result = 0;

            HashSet<char> questions = new HashSet<char>();

            var watch = System.Diagnostics.Stopwatch.StartNew();

            for (int i = 0; i < input.Length; i++)
            {

                for (int j = 0; j < input[i].Length; j++)
                {
                    questions.Add(input[i][j]);
                }

                if ((i + 1 == input.Length) || (input[i + 1].Length == 0)) // new group
                {
                    result += questions.Count;
                    questions.Clear();
                }
            }

            watch.Stop();

            Console.WriteLine($"Value found: {result}");
            Console.WriteLine($"Elapsed time (ms): {watch.ElapsedMilliseconds}");
            Console.WriteLine($"Elapsed ticks: {watch.ElapsedTicks}");
        }

        public override void SolveB()
        {
            Console.WriteLine("-----Part2-----");
            int result = 0;
            List<char> commonQuestions = new List<char>();
            HashSet<char> personQuestions = new HashSet<char>();
            bool skipToNextGroup = false;

            var watch = System.Diagnostics.Stopwatch.StartNew();

            for (int i = 0; i < input.Length; i++)
            {
                if (skipToNextGroup == false)
                {
                    personQuestions.Clear();

                    for (int j = 0; j < input[i].Length; j++) //read line
                    {
                        personQuestions.Add(input[i][j]);
                    }

                    if (commonQuestions.Count == 0 && !skipToNextGroup) //first line of the group
                    {
                        foreach (var item in personQuestions)
                        {
                            commonQuestions.Add(item);
                        }

                    }
                    else //verify the common characters
                    {
                        int iterator = 0;

                        while (iterator < commonQuestions.Count && commonQuestions.Count > 0)
                        {
                            if (!personQuestions.Contains(commonQuestions[iterator]))
                                commonQuestions.Remove(commonQuestions[iterator]);
                            else
                                iterator++;
                        }
                    }

                    if (commonQuestions.Count == 0)
                        skipToNextGroup = true;
                }
                if ((i + 1 == input.Length) || (input[i + 1].Length == 0)) // new group
                {
                    result += commonQuestions.Count ;
                    i++;
                    skipToNextGroup = false;
                    commonQuestions.Clear();
                }
            }
            watch.Stop();

            Console.WriteLine($"Value found: {result}");
            Console.WriteLine($"Elapsed time (ms): {watch.ElapsedMilliseconds}");
            Console.WriteLine($"Elapsed ticks: {watch.ElapsedTicks}");
        }
    }
}
