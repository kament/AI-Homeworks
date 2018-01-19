using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NQueensProblem
{
    class Program
    {
        private static int maxConflicts = 0;
        private static int allSteps = 0;

        public static void Main(String[] args)
        {
            Console.WriteLine("Enter number of queens: ");
            int numberQueens = int.Parse(Console.ReadLine());
            int[] board = Solve(numberQueens);
            Print(board);
            Console.WriteLine($"All steps: {allSteps}");
            }

        private static int[] Solve(int numberQueens)
        {
            int[] queensArray = new int[numberQueens];
            Random rand = new Random();
            InitializeBoard(queensArray, numberQueens, rand);
            int steps = 0;
            while (steps <= 3 * numberQueens)
            {
                int col = GetColWithMaxConflict(queensArray, rand);
                if (!FindConflicts())
                {
                    break;
                }
                queensArray[col] = GetRowWithMinConflict(col, queensArray, rand);
                steps++;
            }
            if (FindConflicts())
            {
                Solve(numberQueens);
            }
            allSteps += steps;
            return queensArray;
        }

        private static void Print(int[] queensArray)
        {
            for (int i = 0; i < queensArray.Length; i++)
            {
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < queensArray.Length; j++)
                {
                    if (j == queensArray[i])
                    {
                        sb.Append("* ");
                    }
                    else
                    {
                        sb.Append("_ ");
                    }
                }
                Console.WriteLine(sb.ToString());
            }
        }

        private static void InitializeBoard(int[] queensArray, int numberQueens, Random rand)
        {
            for (int i = 0; i < numberQueens; i++)
            {
                queensArray[i] = rand.Next(numberQueens);
            }
        }

        private static int GetColWithMaxConflict(int[] queensArray, Random rand)
        {
            List<int> conflictQueens = new List<int>();
            maxConflicts = 0;
            for (int currCol = 0; currCol < queensArray.Length; currCol++)
            {
                int conflicts = 0;
                int currRow = queensArray[currCol];
                for (int otherCol = 0; otherCol < queensArray.Length; otherCol++)
                {
                    if (currCol == otherCol)
                    {
                        continue;
                    }
                    int otherRow = queensArray[otherCol];
                    if ((currRow == otherRow) || (currCol - otherCol == currRow - otherRow) || (currCol - otherCol == otherRow - currRow))
                    {
                        conflicts++;
                    }
                }
                if (conflicts == maxConflicts)
                {
                    conflictQueens.Add(currCol);
                }
                else if (conflicts > maxConflicts)
                {
                    conflictQueens.Clear();
                    conflictQueens.Add(currCol);
                    maxConflicts = conflicts;
                }
            }
            return conflictQueens[rand.Next(conflictQueens.Count())];
        }

        private static int GetRowWithMinConflict(int col, int[] queens, Random r)
        {
            List<int> conflictQueens = new List<int>();
            int minConflicts = queens.Length;
            for (int currRow = 0; currRow < queens.Length; currRow++)
            {
                int conflicts = 0;
                for (int otherCol = 0; otherCol < queens.Length; otherCol++)
                {
                    if (col == otherCol)
                    {
                        continue;
                    }
                    int otherRow = queens[otherCol];
                    if ((currRow == otherRow) || (col - otherCol == currRow - otherRow) || (col - otherCol == otherRow - currRow))
                    {
                        conflicts++;
                    }
                }
                if (conflicts == minConflicts)
                {
                    conflictQueens.Add(currRow);
                }
                else if (conflicts < minConflicts)
                {
                    conflictQueens = new List<int>();
                    conflictQueens.Add(currRow);
                    minConflicts = conflicts;
                }
            }
            return conflictQueens[r.Next(conflictQueens.Count())];
        }

        private static bool FindConflicts()
        {
            return maxConflicts != 0;
        }
    }
}
