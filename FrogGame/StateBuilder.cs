using System;
using System.Collections.Generic;

namespace FrogGame
{
    internal class StateBuilder
    {
        private int n;
        private Stack<GameState> states;
        private HashSet<GameState> visited;

        public StateBuilder(int n)
        {
            this.n = n;
            this.states = new Stack<GameState>();
            this.visited = new HashSet<GameState>();
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
            if (states.Count == 0)
            {
                throw new Exception("States stack contains no elements, game cannot continue!");
            }

            GameState lastState = states.Peek();
            GameState neighbour = NotVisitedNeighbour(lastState);

            if (neighbour != null)
            {
                this.states.Push(neighbour);
                this.visited.Add(neighbour);

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
            GameState neighbour = GenerateNeighbour(lastState);
            if (neighbour == null)
            {
                return null;
            }
            else if (!visited.Contains(neighbour))
            {
                return neighbour;
            }
            else
            {
                return NotVisitedNeighbour(lastState);
            }
        }

        private GameState GenerateNeighbour(GameState lastState)
        {
            if (!lastState.IsFinal())
            {
                for (int i = 0; i < lastState.State.Length; i++)
                {
                    if (DerectRightMovePossible(i, lastState.State))
                    {
                        var newState = lastState.Clone();
                        newState.State[i + 1] = newState.State[i];
                        newState.State[i] = Frog.Empty();

                        return newState;
                    }
                    else if (JumnRightMovePossible(i, lastState.State))
                    {
                        var newState = lastState.Clone();
                        var temp = newState.State[i + 2];
                        newState.State[i + 2] = newState.State[i];
                        newState.State[i] = Frog.Empty();

                        return newState;
                    }
                    else if (DirectLeftMovePossible(i, lastState.State))
                    {
                        var newState = lastState.Clone();
                        newState.State[i - 1] = newState.State[i];
                        newState.State[i] = Frog.Empty();

                        return newState;
                    }
                    else if (JumpLeftMovePossible(i, lastState.State))
                    {
                        var newState = lastState.Clone();
                        var temp = newState.State[i - 2];
                        newState.State[i - 2] = newState.State[i];
                        newState.State[i] = Frog.Empty();

                        return newState;
                    }
                }

                return null;
            }
            else
            {
                throw new Exception("Cannot get neighbour for final node!");
            }
        }

        private bool JumnRightMovePossible(int index, Frog[] state)
        {
            return state[index].IsLeft() && state.Length - index > 3 && state[index + 2].IsEmpty();
        }

        private bool DerectRightMovePossible(int index, Frog[] state)
        {
            return state[index].IsLeft() && state.Length - index > 2 && state[index + 1].IsEmpty();
        }

        private bool JumpLeftMovePossible(int index, Frog[] state)
        {
            return state[index].IsRight() && index > 1 && state[index - 2].IsEmpty();
        }

        private bool DirectLeftMovePossible(int index, Frog[] state)
        {
            return state[index].IsRight() && index > 0 && state[index - 1].IsEmpty();
        }
    }
}