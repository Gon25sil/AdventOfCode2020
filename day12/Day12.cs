using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.day12
{
    public class Day12 : Day
    {
        private const char North = 'N';
        private const char South = 'S';
        private const char East = 'E';
        private const char West = 'W';
        private const char Left = 'L';
        private const char Right = 'R';
        private const char Foward = 'F';

        private readonly List<char> orientation = new List<char> { North, East, South, West };

        public Day12() : base("day12")
        {
        }

        public override void SolveA()
        {
            Console.WriteLine("-----Part1-----");

            char facingPosition = East;
            int posX = 0;
            int posY = 0;
            int result = 0;

            var watch = System.Diagnostics.Stopwatch.StartNew();

            foreach (var item in input)
            {
                char moveAction = item[0];
                int moveValue = Int16.Parse(item.Remove(0, 1));

                if (moveAction == North || (moveAction == Foward && facingPosition == North))
                    posX += moveValue;
                else if (moveAction == South || (moveAction == Foward && facingPosition == South))
                    posX -= moveValue;
                else if (moveAction == East || (moveAction == Foward && facingPosition == East))
                    posY += moveValue;
                else if (moveAction == West || (moveAction == Foward && facingPosition == West))
                    posY -= moveValue;
                else if (moveAction == Left || moveAction == Right)
                    facingPosition = RotateShip(facingPosition, moveAction, moveValue);

            }
            result = Math.Abs(posX) + Math.Abs(posY);
            watch.Stop();

            Console.WriteLine($"Value found: {result}");
            Console.WriteLine($"Elapsed time (ms): {watch.ElapsedMilliseconds}");
            Console.WriteLine($"Elapsed ticks: {watch.ElapsedTicks}");
        }

        private char RotateShip(char currOrientation, char action, int value)
        {
            int index = orientation.IndexOf(currOrientation);

            if (action == Right)
            {
                index = (index + (value / 90)) % orientation.Count;

            }
            else
            {
                int jump = (360 - value) / 90;
                index = (index + jump) % orientation.Count;
            }

            return orientation[index];
        }
        public override void SolveB()
        {
            Console.WriteLine("-----Part2-----");

            int result = 0;
            int shipX = 0;
            int shipY = 0;
            int waypointX = 10;
            int waypointY = 1;

            var watch = System.Diagnostics.Stopwatch.StartNew();

            foreach (var item in input)
            {
                char moveAction = item[0];
                int moveValue = Int16.Parse(item.Remove(0, 1));

                if (moveAction == North)
                    waypointY += moveValue;
                else if (moveAction == South)
                    waypointY -= moveValue;
                else if (moveAction == East)
                    waypointX += moveValue;
                else if (moveAction == West)
                    waypointX -= moveValue;
                else if (moveAction == Left || moveAction == Right)
                    RotateWaypoint(moveAction, moveValue, ref waypointX, ref waypointY);
                else if (moveAction == Foward)
                {
                    shipX += moveValue * waypointX;
                    shipY += moveValue * waypointY;
                }
            }
            result = Math.Abs(shipX) + Math.Abs(shipY);

            watch.Stop();

            Console.WriteLine($"Value found: {result}");
            Console.WriteLine($"Elapsed time (ms): {watch.ElapsedMilliseconds}");
            Console.WriteLine($"Elapsed ticks: {watch.ElapsedTicks}");
        }

        private void RotateWaypoint(char moveAction, int moveValue, ref int waypointX, ref int waypointY)
        {
            double angleRadians = (Math.PI / 180) * moveValue * (moveAction == Left ? 1 : -1);
            double x = waypointX * Math.Cos(angleRadians) - waypointY * Math.Sin(angleRadians);
            double y = waypointX * Math.Sin(angleRadians) + waypointY * Math.Cos(angleRadians);

            waypointX = Convert.ToInt32(x);
            waypointY = Convert.ToInt32(y);

        }
    }
}
