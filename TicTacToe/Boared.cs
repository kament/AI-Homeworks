using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{

    internal sealed class Board
    {
        GridEntry[] values;
        int score;
        bool turnForPlayerX;
        public int RecursiveScore { get; private set; }
        public bool GameOver { get; private set; }

        public Board(GridEntry[] values, bool turnForPlayerX)
        {
            this.turnForPlayerX = turnForPlayerX;
            this.values = values;
            ComputeScore();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    GridEntry v = values[i * 3 + j];
                    char c = '-';
                    if (v == GridEntry.PlayerX)
                    {
                        c = 'X';
                    }
                    else if (v == GridEntry.PlayerO)
                    {
                        c = 'O';
                    }
                    sb.Append(c);
                }
                sb.Append('\n');
            }
            return sb.ToString();
        }

        public Board GetChildAtPosition(int x, int y)
        {
            int i = x + y * 3;
            GridEntry[] newValues = (GridEntry[])values.Clone();

            if (values[i] != GridEntry.Empty)
            {
                throw new Exception(string.Format("invalid index [{0},{1}] is taken by {2}", x, y, values[i]));
            }

            newValues[i] = turnForPlayerX ? GridEntry.PlayerX : GridEntry.PlayerO;

            return new Board(newValues, !turnForPlayerX);
        }

        public bool IsTerminalNode()
        {
            if (GameOver)
            {
                return true;
            }

            foreach (GridEntry v in values)
            {
                if (v == GridEntry.Empty)
                {
                    return false;
                }
            }

            return true;
        }

        public IEnumerable<Board> GetChildren()
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] == GridEntry.Empty)
                {
                    GridEntry[] newValues = (GridEntry[])values.Clone();
                    newValues[i] = turnForPlayerX ? GridEntry.PlayerX : GridEntry.PlayerO;
                    yield return new Board(newValues, !turnForPlayerX);
                }
            }
        }

        public int MiniMax(int depth, bool needMax, int alpha, int beta, out Board childWithMax)
        {
            childWithMax = null;
            System.Diagnostics.Debug.Assert(turnForPlayerX == needMax);
            if (depth == 0 || IsTerminalNode())
            {
                RecursiveScore = score;
                return score;
            }

            foreach (Board cur in GetChildren())
            {
                Board dummy;
                int score = cur.MiniMax(depth - 1, !needMax, alpha, beta, out dummy);
                if (!needMax)
                {
                    if (beta > score)
                    {
                        beta = score;
                        childWithMax = cur;
                        if (alpha >= beta)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    if (alpha < score)
                    {
                        alpha = score;
                        childWithMax = cur;
                        if (alpha >= beta)
                        {
                            break;
                        }
                    }
                }
            }

            RecursiveScore = needMax ? alpha : beta;
            return RecursiveScore;
        }

        public Board FindNextMove(int depth)
        {
            Board ret = null;
            MiniMax(depth, turnForPlayerX, int.MinValue + 1, int.MaxValue - 1, out ret);
            return ret;
        }

        int GetScoreForOneLine(GridEntry[] values)
        {
            int countX = 0, countO = 0;
            foreach (GridEntry v in values)
            {
                if (v == GridEntry.PlayerX)
                {
                    countX++;
                }
                else if (v == GridEntry.PlayerO)
                {
                    countO++;
                }
            }

            if (countO == 3 || countX == 3)
            {
                GameOver = true;
            }


            int advantage = 1;
            if (countO == 0)
            {
                if (turnForPlayerX)
                {
                    advantage = 3;
                }

                return (int)System.Math.Pow(10, countX) * advantage;
            }
            else if (countX == 0)
            {
                if (!turnForPlayerX)
                {
                    advantage = 3;
                }

                return -(int)System.Math.Pow(10, countO) * advantage;
            }

            return 0;
        }

        void ComputeScore()
        {
            int ret = 0;
            int[,] lines =
            {
                { 0, 1, 2 },
                { 3, 4, 5 },
                { 6, 7, 8 },
                { 0, 3, 6 },
                { 1, 4, 7 },
                { 2, 5, 8 },
                { 0, 4, 8 },
                { 2, 4, 6 }
             };

            for (int i = lines.GetLowerBound(0); i <= lines.GetUpperBound(0); i++)
            {
                ret += GetScoreForOneLine(new GridEntry[] { values[lines[i, 0]], values[lines[i, 1]], values[lines[i, 2]] });
            }

            score = ret;
        }

        public Board TransformBoard(Transform t)
        {
            GridEntry[] values = Enumerable.Repeat(GridEntry.Empty, 9).ToArray();
            for (int i = 0; i < 9; i++)
            {
                Point p = new Point(i % 3, i / 3);
                p = t.ActOn(p);
                int j = p.X + p.Y * 3;
                System.Diagnostics.Debug.Assert(values[j] == GridEntry.Empty);
                values[j] = this.values[i];
            }
            return new Board(values, turnForPlayerX);
        }
    }

}
