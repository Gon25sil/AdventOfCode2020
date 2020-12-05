using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.day5
{
    public class Day5
    {
        string[] input;
        public Day5()
        {
            Console.WriteLine("\n ****** DAY 5 ******");

            input = File.ReadAllLines("day5/day5.txt");
        }

        /*
        Instead of zones or groups, this airline uses binary space partitioning to seat people. A seat might be specified like FBFBBFFRLR, where F means "front", B means "back", L means "left", and R means "right".
        The first 7 characters will either be F or B; these specify exactly one of the 128 rows on the plane (numbered 0 through 127). Each letter tells you which half of a region the given seat is in. Start with the whole list of rows; the first letter indicates whether the seat is in the front (0 through 63) or the back (64 through 127). The next letter indicates which half of that region the seat is in, and so on until you're left with exactly one row.
        For example, consider just the first seven characters of FBFBBFFRLR:
            Start by considering the whole range, rows 0 through 127.
            F means to take the lower half, keeping rows 0 through 63.
            B means to take the upper half, keeping rows 32 through 63.
            F means to take the lower half, keeping rows 32 through 47.
            B means to take the upper half, keeping rows 40 through 47.
            B keeps rows 44 through 47.
            F keeps rows 44 through 45.
        The final F keeps the lower of the two, row 44.
        The last three characters will be either L or R; these specify exactly one of the 8 columns of seats on the plane (numbered 0 through 7). The same process as above proceeds again, this time with only three steps. L means to keep the lower half, while R means to keep the upper half.

        For example, consider just the last 3 characters of FBFBBFFRLR:
            Start by considering the whole range, columns 0 through 7.
            R means to take the upper half, keeping columns 4 through 7.
            L means to take the lower half, keeping columns 4 through 5.
            The final R keeps the upper of the two, column 5.
        So, decoding FBFBBFFRLR reveals that it is the seat at row 44, column 5.

        Every seat also has a unique seat ID: multiply the row by 8, then add the column. In this example, the seat has ID 44 * 8 + 5 = 357.
        Here are some other boarding passes:
            BFFFBBFRRR: row 70, column 7, seat ID 567.
            FFFBBBFRRR: row 14, column 7, seat ID 119.
            BBFFBBFRLL: row 102, column 4, seat ID 820.
        As a sanity check, look through your list of boarding passes. What is the highest seat ID on a boarding pass?
         */
        public void SolveA()
        {
            Console.WriteLine("-----Part1-----");
            int result=0;
            
            string rowId;
            string columnId;
            int row;
            int column;
            
            var watch = System.Diagnostics.Stopwatch.StartNew();

            foreach (var item in input)
            {
                rowId = item.Substring(0, 7);
                columnId = item.Substring(7, 3);

                row = CalcPos(0, 127, rowId);
                column = CalcPos(0, 7, columnId);

                if((row * 8 + column) > result) 
                    result = row * 8 + column;
            }

            watch.Stop();

            Console.WriteLine($"Value found: {result}");
            Console.WriteLine($"Elapsed time (ms): {watch.ElapsedMilliseconds}");
            Console.WriteLine($"Elapsed ticks: {watch.ElapsedTicks}");
        }

        private int CalcPos(int startPos, int endPos, string id)
        {
            if (startPos == endPos)
                return startPos;

            var seatsToCut = 1 + (endPos - startPos) / 2;

            if (id[0] == 'B' || id[0] == 'R') //Upper half
            {
                startPos += seatsToCut;
            } else
            {
                endPos -= seatsToCut;
            }

            return CalcPos(startPos, endPos, id.Remove(0, 1));
        }

        /*
         Ding! The "fasten seat belt" signs have turned on. Time to find your seat.
        It's a completely full flight, so your seat should be the only missing boarding pass in your list. However, there's a catch: some of the seats at the very front and back of the plane don't exist on this aircraft, so they'll be missing from your list as well.
        Your seat wasn't at the very front or back, though; the seats with IDs +1 and -1 from yours will be in your list.
        What is the ID of your seat?
         */
        public void SolveB() 
        {
            //The IDs are between 8 and 1016
            // the ID is between to existent IDs
            Console.WriteLine("-----Part2-----");
            
            int result = 0;
            string rowId;
            string columnId;
            int row;
            int column;
            SortedSet<int> sortedIds = new SortedSet<int>();

            var watch = System.Diagnostics.Stopwatch.StartNew();

            foreach (var item in input)
            {
                rowId = item.Substring(0, 7);
                columnId = item.Substring(7, 3);

                row = CalcPos(0, 127, rowId);
                column = CalcPos(0, 7, columnId);
                result = row * 8 + column;

                sortedIds.Add(result);
            }

            int previousVerifiedId=-1; 
            foreach (int item in sortedIds)
            {
                if (previousVerifiedId == -1) // first iteration
                {
                    previousVerifiedId = item;
                    continue;
                }

                if (item - previousVerifiedId == 2)
                {
                    result = item-1;
                    break;
                }
                previousVerifiedId = item;
            }

            watch.Stop();

            Console.WriteLine($"Value found: {result}");
            Console.WriteLine($"Elapsed time (ms): {watch.ElapsedMilliseconds}");
            Console.WriteLine($"Elapsed ticks: {watch.ElapsedTicks}");

        }
    }
}
