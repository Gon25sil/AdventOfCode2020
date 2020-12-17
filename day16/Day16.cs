using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020.day16
{
    public class Day16 : Day
    {
        private List<List<int>> nearbyTickets = new List<List<int>>();
        private List<int> myTicket = new List<int>();
        private int numberOfFields = 0;
        private Dictionary<string, List<(int ll, int up)>> limits = new Dictionary<string, List<(int ll, int up)>>();

        public Day16() : base("day16")
        {
        }

        public override void SolveA()
        {
            Console.WriteLine("-----Part1-----");
            var watch = System.Diagnostics.Stopwatch.StartNew();
            bool nearbyTicketsFound = false;
            bool myTicketFound = false;
            int result = 0;

            foreach (var item in input)
            {
                if (item == string.Empty)
                    continue;

                if (!myTicketFound)
                {
                    if (item == "your ticket:")
                    {
                        myTicketFound = true;
                        continue;
                    }
                    var fields = Regex.Matches(item, @"(\d+)-(\d+)");
                    string fieldName = Regex.Match(item, @"^(.*):").Groups[1].Value;

                    if (!limits.ContainsKey(fieldName))
                        limits.Add(fieldName, new List<(int ll, int up)>());

                    foreach (Match field in fields)
                    {
                        limits[fieldName].Add((Int16.Parse(field.Groups[1].Value), Int16.Parse(field.Groups[2].Value)));
                    }

                }
                else if (!nearbyTicketsFound)
                {
                    if (item == "nearby tickets:")
                    {
                        nearbyTicketsFound = true;
                        continue;
                    }
                    foreach (string s in item.Split(','))
                    {
                        myTicket.Add(Int32.Parse(s));
                    }
                }
                else
                {
                    List<int> rowTickets = new List<int>();
                    string[] values = item.Split(',');
                    bool ticketInvalid = false;
                    foreach (var value in values)
                    {
                        int aux = Int32.Parse(value);
                        bool auxIsInvalid = true;
                        foreach (var limitByField in limits)
                        {
                            foreach (var limit in limitByField.Value)
                            {
                                auxIsInvalid &= aux > limit.up || aux < limit.ll;
                            }
                        }

                        if (auxIsInvalid)
                        {
                            result += aux;
                            ticketInvalid = true;
                        }
                        else
                            rowTickets.Add(aux);
                    }
                    if (!ticketInvalid)
                        nearbyTickets.Add(rowTickets);
                }

            }

            numberOfFields = limits.Count;
            watch.Stop();

            Console.WriteLine($"Value found: {result}");
            Console.WriteLine($"Elapsed time (ms): {watch.ElapsedMilliseconds}");
            Console.WriteLine($"Elapsed ticks: {watch.ElapsedTicks}");
        }

        public override void SolveB()
        {
            Console.WriteLine("-----Part2-----");
            var watch = System.Diagnostics.Stopwatch.StartNew();
            long result = 1;
            //nearbyTickets.Add(myTicket);
            List<List<string>> possibleFieldsByPosition = new List<List<string>>(numberOfFields);

            //Go to every field -> 20 fields
            int i = 0;
            while (i < numberOfFields)
            {
                possibleFieldsByPosition.Add(new List<string>());
                foreach (var pairsOfLimits in limits)
                {
                    bool isValid = false;

                    foreach (var nearbyTicket in nearbyTickets)
                    {
                        isValid = false;
                        foreach (var limit in pairsOfLimits.Value)
                        {
                            isValid |= nearbyTicket[i] <= limit.up && nearbyTicket[i] >= limit.ll;
                            if (isValid)
                                break;
                        }

                        if (!isValid)
                            break;
                    }

                    if (isValid)
                    {
                        possibleFieldsByPosition[i].Add(pairsOfLimits.Key);
                    }


                }
                i++;

            }

            //look for the fields that have only one option and remove this options from other fields
            i = 0;
            HashSet<string> fieldsFound = new HashSet<string>();
            while (i < possibleFieldsByPosition.Count)
            {
                if (possibleFieldsByPosition[i].Count == 1 && !fieldsFound.Contains(possibleFieldsByPosition[i][0]))
                {
                    fieldsFound.Add(possibleFieldsByPosition[i][0]);
                    for (int ind = 0; ind < possibleFieldsByPosition.Count; ind++)
                    {
                        if (i == ind)
                            continue;
                        possibleFieldsByPosition[ind].Remove(possibleFieldsByPosition[i][0]);
                    }
                }

                i++;
                if (i == numberOfFields && fieldsFound.Count < numberOfFields)
                    i = 0;
            }

            for (int g = 0; g < possibleFieldsByPosition.Count; g++)
            {
                if (possibleFieldsByPosition[g][0].StartsWith("departure"))
                {
                    result *= myTicket[g];
                }


            }

            watch.Stop();

            Console.WriteLine($"Value found: {result}");
            Console.WriteLine($"Elapsed time (ms): {watch.ElapsedMilliseconds}");
            Console.WriteLine($"Elapsed ticks: {watch.ElapsedTicks}");
        }
    }
}
