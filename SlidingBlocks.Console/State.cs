using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlidingBlocks.Console
{
    public class State : IEquatable<State>
    {
        private int hScore;
        private List<List<int>> playGround;

        public State(List<List<int>> playGround, Turn turn, int gScore)
        {
            this.playGround = new List<List<int>>();
            this.Turn = turn;
            this.hScore = -1;
            this.GScore = gScore;

            for (int i = 0; i < playGround.Count; i++)
            {
                this.playGround.Add(playGround[i].ToList());
            }
        }

        public int GScore { get; }

        public Turn Turn { get; }

        public List<List<int>> Playground
        {
            get
            {
                var pg = new List<List<int>>();
                for (int i = 0; i < this.playGround.Count; i++)
                {
                    pg.Add(this.playGround[i].ToList());
                }

                return pg;
            }
        }

        public int HScore
        {
            get
            {
                if (this.hScore == -1)
                {
                    this.hScore = CalculateHeuristic() + this.GScore;
                }

                return this.hScore;
            }
        }

        public bool IsGoal
        {
            get
            {
                int counter = 1;
                for (int row = 0; row < this.playGround.Count; row++)
                {
                    for (int col = 0; col < this.playGround.Count; col++)
                    {
                        if(row == this.playGround.Count - 1 && col == this.playGround.Count - 1)
                        {
                            if(this.playGround[row][col] != 0)
                            {
                                return false;
                            }
                        }
                        else if (this.playGround[row][col] != counter)
                        {
                            return false;
                        }

                        counter++;
                    }
                }

                return true;
            }
        }

        private int CalculateHeuristic()
        {
            var heuristic = 0;
            for (int row = 0; row < this.playGround.Count; row++)
            {
                for (int col = 0; col < this.playGround.Count; col++)
                {
                    int n = this.playGround[row][col];
                    int y = n / this.playGround.Count;
                    int x = n - this.playGround.Count * y;

                    heuristic += Math.Abs(col - x) + Math.Abs(row - y);
                }
            }

            return heuristic;
        }

        public override int GetHashCode()
        {
            int hashcode = 0;
            for (int row = 0; row < this.playGround.Count; row++)
            {
                for (int col = 0; col < this.playGround.Count; col++)
                {
                    hashcode >>= this.playGround[row][col];
                }
            }

            return hashcode;
        }

        public bool Equals(State other)
        {
            for (int row = 0; row < this.playGround.Count; row++)
            {
                for (int col = 0; col < this.playGround.Count; col++)
                {
                    if (this.playGround[row][col] != other.playGround[row][col])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
