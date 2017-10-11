using System;
using System.Collections.Generic;

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

            FrogGame game = new FrogGame(n);
            IEnumerable<string> configurations = game.FindRoad();

            string outPut = string.Join(Environment.NewLine, configurations);

            Console.WriteLine(outPut);
        }
    }
}
