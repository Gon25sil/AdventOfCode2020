using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.day3
{
    public class Map
    {
        public readonly int With;
        public readonly int Height;
        public int SlopX { get; set; }
        public int SlopY { get; set; }

        public Map(int mapWith, int mapHeight, int slopX, int slopY)
        {
            this.With = mapWith;
            this.Height = mapHeight;
            this.SlopX = slopX;
            this.SlopY = slopY;
        }

        public void NextPosition(ref int currentX, ref int currentY)
        {
            currentX = (currentX + SlopX ) % With; // a % b -> a - (a / b) * b;
            currentY += SlopY;
        }
    }
}
