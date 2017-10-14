using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FrogGame
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Enter N:");

            int n = 0;
            while (!int.TryParse(Console.ReadLine(), out n))
            {
                Console.WriteLine("Value is invalid number, please try again!");
            }

            Stopwatch sw = new Stopwatch();
            sw.Start();

            FrogGame game = new FrogGame(n);
            IEnumerable<string> configurations = game.FindRoad();

            sw.Stop();

            string outPut = string.Join(Environment.NewLine, configurations);
            Console.WriteLine(outPut);

            Console.WriteLine();
            Console.WriteLine($"Elapsed time in miliseconds {sw.ElapsedMilliseconds}");
            Console.WriteLine($"Elapsed time in minutes {sw.ElapsedMilliseconds / (1000M * 60M)}");
            Console.WriteLine($"Node visited {configurations.Count()}");
        }
    }
}
