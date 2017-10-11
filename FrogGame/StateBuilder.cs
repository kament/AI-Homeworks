using System;
using System.Collections.Generic;
using System.Linq;

namespace FrogGame
{
    internal class StateBuilder
    {
        private int n;

        private Stack<GameState> states;
        private HashSet<GameState> visited;
        private Dictionary<GameState, List<GameState>> neighbours;

        public StateBuilder(int n)
        {
            this.n = n;
            this.states = new Stack<GameState>();
        }

        public GameState GetRoot()
        {
            var root = new GameState(n);

            this.states.Push(root);
            this.visited.Add(root);

            return root;
        }

        public GameState Next()
        {
            GameState lastState = states.Peek();
            GameState neighbour = NotVisitedNeighbour(lastState);

            if(neighbour != null)
            {
                return neighbour;
            }
            else
            {
                states.Pop();
                return Next();
            }
        }

        private GameState NotVisitedNeighbour(GameState lastState)
        {
            if (!neighbours.ContainsKey(lastState))
            {
                List<GameState> neighbours = GenerateNeighbours(lastState);
                this.neighbours.Add(lastState, neighbours);
            }

            return neighbours[lastState].FirstOrDefault(x => !visited.Contains(x));
        }

        private List<GameState> GenerateNeighbours(GameState lastState)
        {
            throw new NotImplementedException();
        }
    }
}