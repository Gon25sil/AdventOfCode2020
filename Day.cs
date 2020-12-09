using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public abstract class Day
    {
        public string[] input;

        protected Day(string name)
        {
            Console.WriteLine($"\n ****** {name.ToUpper()} ******");

            input = File.ReadAllLines($"{name}/input.txt");
        }

        public abstract void SolveA();
        public abstract void SolveB();
    }
}
