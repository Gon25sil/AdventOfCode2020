using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020.day14
{
    public class Day14 : Day
    {
        public Day14() : base("day14")
        {
        }

        public override void SolveA()
        {
            Console.WriteLine("-----Part1-----");
            var watch = System.Diagnostics.Stopwatch.StartNew();

            char[] currMask = new char[36];
            int currMemoryAddr = -1;
            long currValue = -1;
            Dictionary<int, long> adress2value = new Dictionary<int, long>();
            for (int i = 0; i < input.Length; i++)
            {

                //Validate if is a mask or not
                if (input[i].Substring(0, 4) == "mask")
                    currMask = input[i].Substring(7, 36).ToCharArray();
                else
                {
                    currMemoryAddr = Int32.Parse(Regex.Match(input[i], @"\[(.*)]").Groups[1].Value);
                    currValue = Int32.Parse(Regex.Match(input[i], @"\d+$").Value);
                    long newValue = 0;

                    for (int j = 0; j < currMask.Length; j++)
                    {
                        long vbit = currValue & (long)Math.Pow(2, currMask.Length - j - 1);

                        if (currMask[j] == 'X')
                            newValue += vbit;

                        if (currMask[j] == '1')
                        {
                            newValue += (long)Math.Pow(2, currMask.Length - j - 1);
                        }


                    }
                    if (adress2value.ContainsKey(currMemoryAddr))
                        adress2value[currMemoryAddr] = newValue;
                    else
                        adress2value.Add(currMemoryAddr, newValue);
                }


            }
            long result = adress2value.Sum(x => x.Value);
            watch.Stop();

            Console.WriteLine($"Value found: {result}");
            Console.WriteLine($"Elapsed time (ms): {watch.ElapsedMilliseconds}");
            Console.WriteLine($"Elapsed ticks: {watch.ElapsedTicks}");
        }

        public override void SolveB()
        {
            Console.WriteLine("-----Part2-----");
            var watch = System.Diagnostics.Stopwatch.StartNew();

            char[] currMask = new char[36];
            int currMemoryAddr = -1;
            long value = -1;
            Dictionary<long, long> adress2value = new Dictionary<long, long>();

            for (int i = 0; i < input.Length; i++)
            {

                //Validate if is a mask or not
                if (input[i].Substring(0, 4) == "mask")
                    currMask = input[i].Substring(7, 36).ToCharArray();
                else
                {
                    currMemoryAddr = Int32.Parse(Regex.Match(input[i], @"\[(.*)]").Groups[1].Value);
                    value = Int32.Parse(Regex.Match(input[i], @"\d+$").Value);

                    List<long> addresses = new List<long>() { 0 };
                    for (int j = 0; j < currMask.Length; j++)
                    {
                        long vbit = currMemoryAddr & (long)Math.Pow(2, currMask.Length - j - 1);

                        int sizeBeforeNewAddresses = addresses.Count;
                        for (int k = 0; k < sizeBeforeNewAddresses; k++)
                        {
                            switch (currMask[j])
                            {
                                case 'X':
                                    addresses.Add(addresses[k] + (long)Math.Pow(2, currMask.Length - j - 1));
                                    break;
                                case '1':
                                    addresses[k] += (long)Math.Pow(2, currMask.Length - j - 1);

                                    break;
                                case '0':
                                    addresses[k] += vbit;

                                    break;
                            }

                        }
                    }

                    for (int j = 0; j < addresses.Count; j++)
                    {
                        if (adress2value.ContainsKey(addresses[j]))
                            adress2value[addresses[j]] = value;
                        else
                            adress2value.Add(addresses[j], value);
                    }
                }


            }
            long result = adress2value.Sum(x => x.Value);
            watch.Stop();

            Console.WriteLine($"Value found: {result}");
            Console.WriteLine($"Elapsed time (ms): {watch.ElapsedMilliseconds}");
            Console.WriteLine($"Elapsed ticks: {watch.ElapsedTicks}");
        }

    }
}
