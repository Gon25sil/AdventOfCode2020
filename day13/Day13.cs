using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.day13
{
    public class Day13 : Day
    {
        private int timestamp { get; set; }
        private string[] busIds { get; set; }

        public Day13() : base("day13")
        {
            timestamp = Int32.Parse(input[0]);


        }

        public override void SolveA()
        {
            Console.WriteLine("-----Part1-----");
            int earlieastBusTime = -1;
            int earlieastBusid = -1;
            var watch = System.Diagnostics.Stopwatch.StartNew();

            string[] busIds = input[1].Split(',');

            for (int i = 0; i < busIds.Length; i++)
            {

                if (busIds[i] == "x")
                    continue;

                int busId = Int16.Parse(busIds[i]);
                int remainder = timestamp % busId;
                int busTime = remainder == 0 ? timestamp : timestamp - remainder + busId;

                if (earlieastBusTime == -1)
                {
                    earlieastBusTime = busTime;
                    earlieastBusid = busId;
                }
                else if (busTime < earlieastBusTime)
                {
                    earlieastBusTime = busTime;
                    earlieastBusid = busId;
                }


            }

            int result = (earlieastBusTime - timestamp) * earlieastBusid;
            watch.Stop();

            Console.WriteLine($"Value found: {result}");
            Console.WriteLine($"Elapsed time (ms): {watch.ElapsedMilliseconds}");
            Console.WriteLine($"Elapsed ticks: {watch.ElapsedTicks}");
        }

        public override void SolveB()
        {
            Console.WriteLine("-----Part2-----");
            
            var watch = System.Diagnostics.Stopwatch.StartNew();

            List<(int remainder, int num)> remainder2id = new List<(int, int)>();
            string[] busIds = input[1].Split(',');

            for (int i = 0; i < busIds.Length; i++)
            {
                if (busIds[i] == "x")
                    continue;

                int busid = Int32.Parse(busIds[i]);
                if(i==0)
                    remainder2id.Add((i, busid));
                else
                    remainder2id.Add((busid - i, busid));
            }
            long result = findMinX(remainder2id.Select(x=>x.num).ToArray(), remainder2id.Select(x => x.remainder).ToArray());

            watch.Stop();
            
            Console.WriteLine($"Value found: {result}");
            Console.WriteLine($"Elapsed time (ms): {watch.ElapsedMilliseconds}");
            Console.WriteLine($"Elapsed ticks: {watch.ElapsedTicks}");
        }


        // Returns modulo inverse of  
        // 'a' with respect to 'm'  
        // using extended Euclid Algorithm.  
        // Refer below post for details: 
        // https://www.geeksforgeeks.org/multiplicative-inverse-under-modulo-m/ 
        public long inv(long a, long m)
        {
            long m0 = m, t, q;
            long x0 = 0, x1 = 1;

            if (m == 1)
                return 0;

            // Apply extended  
            // Euclid Algorithm 
            while (a > 1)
            {
                // q is quotient 
                q = a / m;

                t = m;

                // m is remainder now,  
                // process same as  
                // euclid's algo 
                m = a % m; a = t;

                t = x0;

                x0 = x1 - q * x0;

                x1 = t;
            }

            // Make x1 positive 
            if (x1 < 0)
                x1 += m0;

            return x1;
        }

        // k is size of num[] and rem[]. 
        // Returns the smallest number 
        // x such that: 
        // x % num[0] = rem[0], 
        // x % num[1] = rem[1], 
        // .................. 
        // x % num[k-2] = rem[k-1] 
        // Assumption: Numbers in num[]  
        // are pairwise coprime (gcd  
        // for every pair is 1) 
        public  long findMinX(int[] num,
                            int[] rem)
        {
            // Compute product 
            // of all numbers 
            int k = num.Length;
            long prod = 1;
            for (int i = 0; i < k; i++)
                prod *= num[i];

            // Initialize result 
            long result = 0;

            // Apply above formula 
            for (int i = 0; i < k; i++)
            {
                long pp = prod / num[i];
                result += rem[i] *
                          inv(pp, num[i]) * pp;
            }

            return result % prod;
        }
    }
}
