using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.day11
{
    public class Day11 : Day
    {
        private const char floor = '.';
        private const char occuppied = '#';
        private const char empty = 'L';
        private int mapWidth;
        private int mapHeight;


        private List<char[]> seatsSolveA = new List<char[]>();
        private List<char[]> seatsSolveB = new List<char[]>();
        private List<char[]> auxSeats = new List<char[]>();

        public Day11() : base("day11")
        {
            foreach (var item in input)
            {
                auxSeats.Add(item.ToCharArray());
                seatsSolveA.Add(item.ToCharArray());
                seatsSolveB.Add(item.ToCharArray());
            }
            mapWidth = seatsSolveA[0].Length;
            mapHeight = seatsSolveA.Count;
        }

        public override void SolveA()
        {
            Console.WriteLine("-----Part1-----");

            int result = 0;
            bool seatsHasChanges = false;

            var watch = System.Diagnostics.Stopwatch.StartNew();

            do
            {
                seatsHasChanges = false;
                for (int j = 0; j < seatsSolveA.Count; j++)
                {
                    for (int i = 0; i < seatsSolveA[j].Length; i++)
                    {
                        int adjOccupiedSeats = AmmountOfOccupiedAdjSeats(j, i);
                        switch (seatsSolveA[j][i])
                        {
                            case occuppied:
                                if (adjOccupiedSeats >= 4)
                                {
                                    auxSeats[j][i] = empty;
                                    result--;
                                    seatsHasChanges = true;
                                }
                                break;
                            case empty:
                                if (adjOccupiedSeats == 0)
                                {
                                    auxSeats[j][i] = occuppied;
                                    result++;
                                    seatsHasChanges = true;
                                }
                                break;
                            default:
                                auxSeats[j][i] = seatsSolveA[j][i];
                                break;
                        }
                    }
                }
                CopyAuxToOriginal(ref seatsSolveA);

            } while (seatsHasChanges);


            watch.Stop();

            Console.WriteLine($"Value found: {result}");
            Console.WriteLine($"Elapsed time (ms): {watch.ElapsedMilliseconds}");
            Console.WriteLine($"Elapsed ticks: {watch.ElapsedTicks}");
        }

        public void CopyAuxToOriginal(ref List<char[]> orig)
        {
            for (int i = 0; i < auxSeats.Count; i++)
            {
                for (int j = 0; j < auxSeats[i].Length; j++)
                {
                    orig[i][j] = auxSeats[i][j];
                }
            }
        }

        private int AmmountOfOccupiedAdjSeats(int row, int column)
        {
            int adjOccupiedSeats = 0;
            for (int i = -1; i <= 1; i++)
            {
                if (row + i < 0 || row + i > mapHeight - 1)
                    continue;

                for (int j = -1; j <= 1; j++)
                {
                    if ((j == 0 && i == 0) || column + j < 0 || column + j > mapWidth - 1)
                        continue;

                    if (seatsSolveA[row + i][column + j] == occuppied)
                        adjOccupiedSeats++;
                }
            }

            return adjOccupiedSeats;
        }

        private int AmmountOfOccupiedSeatsUsingFalconEye(int row, int column)
        {
            int adjOccupiedSeats = 0;

            for (int i = -1; i <= 1; i++)
            {
                if (row + i < 0 || row + i > mapHeight - 1)
                    continue;


                for (int j = -1; j <= 1; j++)
                {
                    int auxY = j;
                    int auxX = i;

                    bool sitFound = false;
                    do
                    {
                        if ((auxX == 0 && auxY == 0) || column + auxY < 0 || column + auxY > mapWidth - 1 || row + auxX < 0 || row + auxX > mapHeight - 1)
                            break;

                        if (seatsSolveB[row + auxX][column + auxY] == occuppied)
                        {
                            adjOccupiedSeats++;
                            sitFound = true;
                        }
                        else if (seatsSolveB[row + auxX][column + auxY] == empty)
                            break;
                        else
                        {
                            auxY += j;
                            auxX += i;
                        }
                    } while (!sitFound);
                }
            }

            return adjOccupiedSeats;
        }

        public override void SolveB()
        {

            Console.WriteLine("-----Part1-----");

            int result = 0;
            var watch = System.Diagnostics.Stopwatch.StartNew();

            bool seatsHasChanges;
            do
            {
                seatsHasChanges = false;
                for (int j = 0; j < seatsSolveB.Count; j++)
                {
                    for (int i = 0; i < seatsSolveB[j].Length; i++)
                    {
                        int adjOccupiedSeats = AmmountOfOccupiedSeatsUsingFalconEye(j, i);
                        switch (seatsSolveB[j][i])
                        {
                            case occuppied:
                                if (adjOccupiedSeats >= 5)
                                {
                                    auxSeats[j][i] = empty;
                                    result--;
                                    seatsHasChanges = true;
                                }
                                break;
                            case empty:
                                if (adjOccupiedSeats == 0)
                                {
                                    auxSeats[j][i] = occuppied;
                                    result++;
                                    seatsHasChanges = true;
                                }
                                break;
                            default:
                                auxSeats[j][i] = seatsSolveB[j][i];
                                break;
                        }
                    }
                }
                CopyAuxToOriginal(ref seatsSolveB);

            } while (seatsHasChanges);


            watch.Stop();

            Console.WriteLine($"Value found: {result}");
            Console.WriteLine($"Elapsed time (ms): {watch.ElapsedMilliseconds}");
            Console.WriteLine($"Elapsed ticks: {watch.ElapsedTicks}");

        }
    }
}
