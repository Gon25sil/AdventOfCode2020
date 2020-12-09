using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day7 : Day
    {
        public Day7() : base("day7")
        {
        }

        public override void SolveA()
        {
            Dictionary<string, HashSet<string>> whichColorsContainsTheKey = new Dictionary<string, HashSet<string>>(); //Key -> color, value -> List of colors who contains this color
            HashSet<string> result = new HashSet<string>();

            var watch = System.Diagnostics.Stopwatch.StartNew();

            foreach (var item in input)
            {
                string whoContains = Regex.Match(item, @"^(.*?)\sbags").Groups[1].Value;
                MatchCollection colorsContained = Regex.Matches(item, @"\d\s(.*?)\sbag");

                foreach (Match match in colorsContained)
                {
                    string newColor = match.Groups[1].Value;

                    if (!whichColorsContainsTheKey.ContainsKey(newColor))
                        whichColorsContainsTheKey.Add(newColor, new HashSet<string>());

                    whichColorsContainsTheKey[newColor].Add(whoContains);

                }
            }

            GetWhoContainsColor("shiny gold", ref result, ref whichColorsContainsTheKey);

            watch.Stop();

            Console.WriteLine($"Value found: {result.Count}");
            Console.WriteLine($"Elapsed time (ms): {watch.ElapsedMilliseconds}");
            Console.WriteLine($"Elapsed ticks: {watch.ElapsedTicks}");
        }

        public void GetWhoContainsColor(string color, ref HashSet<string> result, ref Dictionary<string, HashSet<string>> whichColorsContainsTheKey)
        {
            if (whichColorsContainsTheKey.TryGetValue(color, out HashSet<string> whoContainShinyGold))
            {
                foreach (var item in whoContainShinyGold)
                {
                    result.Add(item);
                    GetWhoContainsColor(item, ref result, ref whichColorsContainsTheKey);
                }
            }
        }

        public override void SolveB()
        {
            int result = 0;
            Dictionary<string, List<(int quantity, string color)>> colorsContainedByKey = new Dictionary<string, List<(int, string)>>(); //Key -> color, value -> List of colors contained by this color

            var watch = System.Diagnostics.Stopwatch.StartNew();

            foreach (var item in input)
            {
                string whoContains = Regex.Match(item, @"^(.*?)\sbags").Groups[1].Value;
                MatchCollection colorsContained = Regex.Matches(item, @"(\d)\s(.*?)\sbag");

                if (!colorsContainedByKey.ContainsKey(whoContains))
                    colorsContainedByKey.Add(whoContains, new List<(int, string)>());

                foreach (Match match in colorsContained)
                {
                    string newColor = match.Groups[2].Value;
                    string quantity = match.Groups[1].Value;

                    (int, string) newPair = (int.Parse(quantity), newColor);
                    colorsContainedByKey[whoContains].Add(newPair);
                }
            }

            result = numberOfBagsRequiredByColor("shiny gold", ref colorsContainedByKey);

            watch.Stop();

            Console.WriteLine($"Value found: {result}");
            Console.WriteLine($"Elapsed time (ms): {watch.ElapsedMilliseconds}");
            Console.WriteLine($"Elapsed ticks: {watch.ElapsedTicks}");
        }

        public int numberOfBagsRequiredByColor(string color, ref Dictionary<string, List<(int quantity, string color)>> colorsContainedByKey)
        {
            int result = 0;
            if (colorsContainedByKey.TryGetValue(color, out List<(int quantity, string color)> colorsAndQuantities))
            {
                if (colorsAndQuantities.Count == 0)
                    return 0;

                foreach (var item in colorsAndQuantities)
                {
                    result += item.quantity + item.quantity * numberOfBagsRequiredByColor(item.color, ref colorsContainedByKey);
                }
            }
            else
                return 1;

            return result;
        }
    }
}
