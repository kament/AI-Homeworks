using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    internal class Startup
    {
        static void Main(string[] args)
        {
            TicTacToeGame game = new TicTacToeGame();

            bool stop = false;
            while (!stop)
            {
                bool userFirst = false;
                game = new TicTacToeGame();
                Console.WriteLine("Play first?[y/n]");

                if (Console.ReadLine().StartsWith("y", StringComparison.InvariantCultureIgnoreCase))
                {
                    userFirst = true;
                }

                int depth = 8;


                Console.WriteLine("{0} play first", userFirst ? "User" : "Computer");

                while (!game.Current.IsTerminalNode())
                {
                    if (userFirst)
                    {
                        game.GetNextMoveFromUser();
                        game.ComputerMakeMove(depth);
                    }
                    else
                    {
                        game.ComputerMakeMove(depth);
                        game.GetNextMoveFromUser();
                    }
                }
                Console.WriteLine("The final result is \n" + game.Current);
                if (game.Current.RecursiveScore < -200)
                {
                    Console.WriteLine("O has won.");
                }
                else if (game.Current.RecursiveScore > 200)
                {
                    Console.WriteLine("X has won.");
                }
                else
                {
                    Console.WriteLine("Tie.");
                }
            }
        }
    }
}

