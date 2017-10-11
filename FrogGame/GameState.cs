using System;
using System.Linq;

namespace FrogGame
{
    internal class GameState
    {
        public const char LeftFrogIndicator = '>';
        public const char RightFrogIndicator = '<';

        private int n;
        private char[] state;

        public GameState(int n)
        {
            this.n = n;
            int frogSize = 2 * n + 1;
            State = new char[frogSize];

            for (int i = 0; i < n; i++)
            {
                State[i] = LeftFrogIndicator;
            }

            State[n] = ' ';

            for (int i = n + 1; i < frogSize; i++)
            {
                State[i] = RightFrogIndicator;
            }
        }

        public GameState(char[] state)
        {
            State = state;
            this.n = (state.Length - 1) / 2;
        }

        public char[] State
        {
            get { return this.state; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(State));
                }

                int n = state.Length - 1;
                if (n % 2 != 0)
                {
                    throw new ArgumentException($"{nameof(state)} is with invalid length! {n}");
                }

                this.state = value;
            }
        }

        public bool IsFinal()
        {
            bool leftSideIsFullWithRightFrogs = State
                .Take(n)
                .All(c => c == RightFrogIndicator);

            bool rightSideIsFullWithLeftFrogs = State
                .Skip(n + 1)
                .Take(n)
                .All(c => c == LeftFrogIndicator);

            bool isFinal = leftSideIsFullWithRightFrogs && rightSideIsFullWithLeftFrogs;

            return isFinal;
        }

        public override string ToString()
        {
            return string.Join(string.Empty, State);
        }
    }
}
