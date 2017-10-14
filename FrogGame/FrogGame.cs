using System.Collections.Generic;

namespace FrogGame
{
    internal class FrogGame
    {
        private int n;
        private StateBuilder builder;

        public FrogGame(int n)
        {
            this.n = n;
            this.builder = new StateBuilder(n);
        }

        internal IEnumerable<string> FindRoad()
        {
            List<string> states = new List<string>();

            var gameState = builder.GetRoot();
            states.Add(gameState.ToString());

            while (!gameState.IsFinal())
            {
                gameState = this.builder.Next();
                states.Add(gameState.ToString());
            }

            return states;
        }
    }
}