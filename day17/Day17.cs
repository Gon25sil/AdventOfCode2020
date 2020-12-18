using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.day17
{
    public class Day17 : Day
    {
        //first list is x (row), second is y(column)
        private const char active = '#';
        private const char inactive = '.';
        private const char toBeInactive = 'i'; //pass active to inactive
        private const char toBeActive = 'a'; //pass inactive to active

        public Day17() : base("day17")
        {
        }

        /*
        
         
         */
        public override void SolveA()
        {
            Console.WriteLine("-----Part1-----");
            var watch = System.Diagnostics.Stopwatch.StartNew();
            int result = 0;
            List<List<List<char>>> maze = new List<List<List<char>>>();

            foreach (string t in input)
            {
                List<List<char>> row = new List<List<char>>();
                foreach (char character in t.ToCharArray())
                {
                    row.Add(new List<char> { character });
                }
                maze.Add(row);
            }


            int iterator = 0;
            while (iterator++ < 6)
            {
                IncreaseSizeOfMaze(ref maze);
                for (int i = 0; i < maze.Count; i++)
                {
                    for (int j = 0; j < maze[i].Count; j++)
                    {
                        for (int k = 0; k < maze[i][j].Count; k++)
                        {
                            int neighbors = NumberOfNeighbours(i, j, k, maze);
                            if (maze[i][j][k] == active && (neighbors < 2 || neighbors > 3)) //active to inactive
                            {
                                maze[i][j][k] = toBeInactive;
                            }

                            else if (maze[i][j][k] == inactive && neighbors == 3) // inactive to active
                            {
                                maze[i][j][k] = toBeActive;
                            }
                        }
                    }
                }

                CleanAuxVariables(ref maze);
                int aa = NumberOfActiveCubes(maze);
            }

            result = NumberOfActiveCubes(maze);

            watch.Stop();

            Console.WriteLine($"Value found: {result}");
            Console.WriteLine($"Elapsed time (ms): {watch.ElapsedMilliseconds}");
            Console.WriteLine($"Elapsed ticks: {watch.ElapsedTicks}");
        }

        public int NumberOfActiveCubes(List<List<List<char>>> maze)
        {
            int result = 0;
            foreach (var row in maze)
            {
                foreach (var column in row)
                {
                    foreach (char c in column)
                    {
                        result += c == active ? 1 : 0;
                    }
                }
            }

            return result;
        }

        public int NumberOfActiveCubes(List<List<List<List<char>>>> maze)
        {
            int result = 0;
            foreach (var row in maze)
            {
                foreach (var column in row)
                {
                    foreach (var c in column)
                    {
                        foreach (char c1 in c)
                        {
                            result += c1 == active ? 1 : 0;
                        }
                    }
                }
            }

            return result;
        }
        public void CleanAuxVariables(ref List<List<List<char>>> maze)
        {
            for (int i = 0; i < maze.Count; i++)
            {
                for (int j = 0; j < maze[i].Count; j++)
                {
                    for (int k = 0; k < maze[i][j].Count; k++)
                    {
                        maze[i][j][k] = maze[i][j][k] == toBeActive ? active : maze[i][j][k];
                        maze[i][j][k] = maze[i][j][k] == toBeInactive ? inactive : maze[i][j][k];
                    }
                }
            }
        }
        public void CleanAuxVariables(ref List<List<List<List<char>>>> maze)
        {
            for (int i = 0; i < maze.Count; i++)
            {
                for (int j = 0; j < maze[i].Count; j++)
                {
                    for (int k = 0; k < maze[i][j].Count; k++)
                    {
                        for (int w = 0; w < maze[i][j][k].Count; w++)
                        {
                            maze[i][j][k][w] = maze[i][j][k][w] == toBeActive ? active : maze[i][j][k][w];
                            maze[i][j][k][w] = maze[i][j][k][w] == toBeInactive ? inactive : maze[i][j][k][w];
                        }
                    }
                }
            }
        }
        public void IncreaseSizeOfMaze(ref List<List<List<List<char>>>> maze)
        {
            //add 4th dimension
            for (int i = 0; i < maze.Count; i++)
            {
                for (int j = 0; j < maze[i].Count; j++)
                {
                    for (int k = 0; k < maze[i][j].Count; k++)
                    {
                        maze[i][j][k].Add(inactive);
                        maze[i][j][k].Insert(0, inactive);
                    }
                }
            }
            //add Height to the existing columns
            for (int i = 0; i < maze.Count; i++)
            {
                for (int j = 0; j < maze[i].Count; j++)
                {
                    for (int l = 0; l < 2; l++)
                    {
                        List<char> forthDimension = new List<char>();
                        for (int k = 0; k < maze[0][0][0].Count; k++)
                        {
                            forthDimension.Add(inactive);
                        }
                        if (l == 0)
                            maze[i][j].Add(forthDimension);
                        else
                            maze[i][j].Insert(0, forthDimension);
                    }
                }
            }
            //add Columns to the existing rows
            for (int i = 0; i < maze.Count; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    List<List<char>> height = new List<List<char>>();
                    for (int k = 0; k < maze[0][0].Count; k++)
                    {
                        List<char> forthDimension = new List<char>();
                        for (int l = 0; l < maze[0][0][0].Count; l++)
                        {
                            forthDimension.Add(inactive);
                        }
                        height.Add(forthDimension);
                    }
                    if (j == 0)
                        maze[i].Insert(0, height);
                    else
                        maze[i].Add(height);
                }
            }

            //Add new rows to the maze
            for (int i = 0; i < 2; i++)
            {
                List<List<List<char>>> row = new List<List<List<char>>>();

                for (int j = 0; j < maze[0].Count; j++)
                {
                    List<List<char>> column = new List<List<char>>();

                    for (int k = 0; k < maze[0][0].Count; k++)
                    {
                        List<char> forthDimension = new List<char>();
                        for (int l = 0; l < maze[0][0][0].Count; l++)
                        {
                            forthDimension.Add(inactive);
                        }
                        column.Add(forthDimension);
                    }

                    row.Add(column);
                }
                if (i == 0)
                    maze.Insert(0, row);
                else
                    maze.Add(row);
            }
        }
        public void IncreaseSizeOfMaze(ref List<List<List<char>>> maze)
        {
            //add Height to the existing columns
            for (int i = 0; i < maze.Count; i++)
            {
                for (int j = 0; j < maze[i].Count; j++)
                {
                    maze[i][j].Add(inactive);
                    maze[i][j].Insert(0, inactive);
                }
            }
            //add Columns to the existing rows
            for (int i = 0; i < maze.Count; i++)
            {

                for (int j = 0; j < 2; j++)
                {
                    List<char> height = new List<char>();
                    for (int k = 0; k < maze[0][0].Count; k++)
                    {
                        height.Add(inactive);
                    }
                    if (j == 0)
                        maze[i].Insert(0, height);
                    else
                        maze[i].Add(height);
                }
            }

            //Add new rows to the maze
            for (int i = 0; i < 2; i++)
            {
                List<List<char>> row = new List<List<char>>();

                for (int j = 0; j < maze[0].Count; j++)
                {
                    List<char> column = new List<char>();

                    for (int k = 0; k < maze[0][0].Count; k++)
                    {
                        column.Add(inactive);
                    }

                    row.Add(column);
                }
                if (i == 0)
                    maze.Insert(0, row);
                else
                    maze.Add(row);
            }
        }
        public int NumberOfNeighbours(int x, int y, int z, List<List<List<char>>> maze)
        {
            int result = 0;

            for (int difX = -1; difX <= 1; difX++)
            {
                if (x + difX < 0 || x + difX >= maze.Count)
                    continue;
                for (int difY = -1; difY <= 1; difY++)
                {
                    if (y + difY < 0 || y + difY >= maze[x + difX].Count)
                        continue;
                    for (int difZ = -1; difZ <= 1; difZ++)
                    {
                        if (z + difZ < 0 || z + difZ >= maze[x + difX][y + difY].Count || (difX == 0 && difY == 0 && difZ == 0))
                            continue;
                        result += (maze[x + difX][y + difY][z + difZ] == active || maze[x + difX][y + difY][z + difZ] == toBeInactive) ? 1 : 0;
                    }
                }
            }

            return result;
        }

        public int NumberOfNeighbours(int x, int y, int z, int w, List<List<List<List<char>>>> maze)
        {
            int result = 0;
            for (int difX = -1; difX <= 1; difX++)
            {
                if (x + difX < 0 || x + difX >= maze.Count)
                    continue;
                for (int difY = -1; difY <= 1; difY++)
                {
                    if (y + difY < 0 || y + difY >= maze[x + difX].Count)
                        continue;
                    for (int difZ = -1; difZ <= 1; difZ++)
                    {
                        if (z + difZ < 0 || z + difZ >= maze[x + difX][y + difY].Count)
                            continue;
                        for (int difW = -1; difW <= 1; difW++)
                        {
                            if (w + difW < 0 || w + difW >= maze[x + difX][y + difY][z + difZ].Count || ((difX == 0 && difY == 0 && difZ == 0 && difW == 0)))
                                continue;
                            result += (maze[x + difX][y + difY][z + difZ][w + difW] == active || maze[x + difX][y + difY][z + difZ][w + difW] == toBeInactive) ? 1 : 0;
                        }
                    }
                }
            }

            return result;
        }

        public override void SolveB()
        {
            Console.WriteLine("-----Part2-----");
            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<List<List<List<char>>>> maze = new List<List<List<List<char>>>>();

            foreach (string t in input)
            {
                List<List<List<char>>> row = new List<List<List<char>>>();
                foreach (char character in t.ToCharArray())
                {
                    row.Add(new List<List<char>>() { new List<char> { character } });
                }
                maze.Add(row);
            }


            int iterator = 0;
            while (iterator++ < 6)
            {
                IncreaseSizeOfMaze(ref maze);
                for (int i = 0; i < maze.Count; i++)
                {
                    for (int j = 0; j < maze[i].Count; j++)
                    {
                        for (int k = 0; k < maze[i][j].Count; k++)
                        {
                            for (int w = 0; w < maze[i][j][k].Count; w++)
                            {
                                int neighbors = NumberOfNeighbours(i, j, k, w, maze);
                                if (maze[i][j][k][w] == active && (neighbors < 2 || neighbors > 3)) //active to inactive
                                {
                                    maze[i][j][k][w] = toBeInactive;
                                }

                                else if (maze[i][j][k][w] == inactive && neighbors == 3) // inactive to active
                                {
                                    maze[i][j][k][w] = toBeActive;
                                }
                            }
                        }
                    }
                }

                CleanAuxVariables(ref maze);
            }

            int result = NumberOfActiveCubes(maze);
            watch.Stop();

            Console.WriteLine($"Value found: {result}");
            Console.WriteLine($"Elapsed time (ms): {watch.ElapsedMilliseconds}");
            Console.WriteLine($"Elapsed ticks: {watch.ElapsedTicks}");
        }
        void printMaze(List<List<List<char>>> maze)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.WriteLine($"z:{j - 1}");

                for (int k = 0; k < maze.Count; k++)
                {
                    for (int l = 0; l < maze[k].Count; l++)
                    {
                        Console.Write(maze[k][l][j]);
                    }
                    Console.WriteLine();
                }
            }
        }
        void printMaze(List<List<List<List<char>>>> maze)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.WriteLine($"z:{j - 1}, w:{i - 1}");

                    for (int k = 0; k < maze.Count; k++)
                    {
                        for (int l = 0; l < maze[k].Count; l++)
                        {
                            Console.Write(maze[k][l][j][i]);
                        }
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}
