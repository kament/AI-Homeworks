using System;
using System.Linq;

namespace FrogGame
{
    internal class GameState : IEquatable<GameState>
    {
        private int n;
        private Frog[] state;

        public GameState(int n)
        {
            this.n = n;
            int frogSize = 2 * n + 1;
            State = new Frog[frogSize];

            for (int i = 0; i < n; i++)
            {
                State[i] = Frog.Left();
            }

            State[n] = Frog.Empty();

            for (int i = n + 1; i < frogSize; i++)
            {
                State[i] = Frog.Right();
            }
        }

        public GameState(Frog[] state)
        {
            State = state;
            this.n = (state.Length - 1) / 2;
        }

        public Frog[] State
        {
            get { return this.state; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(State));
                }

                int n = value.Length - 1;
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
                .All(f => f.IsRight());

            bool rightSideIsFullWithLeftFrogs = State
                .Skip(n + 1)
                .Take(n)
                .All(f => f.IsLeft());

            bool isFinal = leftSideIsFullWithRightFrogs && rightSideIsFullWithLeftFrogs;

            return isFinal;
        }

        public GameState Clone()
        {
            return new GameState(this.state.Clone() as Frog[]);
        }

        public override string ToString()
        {
            return string.Join(string.Empty, State.Select(f => f.ToString()));
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public bool Equals(GameState other)
        {
            return this.ToString() == other.ToString();
        }
    }
}
