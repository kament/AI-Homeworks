using System;
using System.Collections.Generic;

namespace FrogGame
{
    internal class StateBuilder
    {
        private const int RightJumpMinEdgePositionsDiff = 2;
        private const int DirectRightMoveMinEdgePositionsDiff = 1;
        private const int LeftJumpMinEdgePosition = 1;
        private const int DirectLeftMoveMinEdgePosition = 0;
        private const int DirectRightMovePosition = 1;
        private const int JumpRightMovePosition = 2;
        private const int DirectLeftMovePosition = -1;
        private const int JumpLeftMovePosition = -2;

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
            if (lastState.IsFinal())
            {
                throw new Exception("Cannot get neighbour for final node!");
            }

            for (int i = 0; i < lastState.State.Length; i++)
            {
                GameState newState = CreateNewState(lastState, i);
                if (newState != null && !visited.Contains(newState))
                {
                    return newState;
                }
            }

            return null;
        }

        private GameState CreateNewState(GameState lastState, int index)
        {
            if (DirectRightMovePossible(index, lastState.State))
            {
                return CreateNewStateFromLastState(lastState, index, DirectRightMovePosition);
            }
            else if (JumnRightMovePossible(index, lastState.State))
            {
                return CreateNewStateFromLastState(lastState, index, JumpRightMovePosition);
            }
            else if (DirectLeftMovePossible(index, lastState.State))
            {
                return CreateNewStateFromLastState(lastState, index, DirectLeftMovePosition);

            }
            else if (JumpLeftMovePossible(index, lastState.State))
            {
                return CreateNewStateFromLastState(lastState, index, JumpLeftMovePosition);
            }

            return null;
        }

        private static GameState CreateNewStateFromLastState(GameState lastState, int fromIndex, int positions)
        {
            var newState = lastState.Clone();
            newState.State[fromIndex + positions] = newState.State[fromIndex];
            newState.State[fromIndex] = Frog.Empty();

            return newState;
        }

        private bool JumnRightMovePossible(int index, Frog[] state)
        {
            return state[index].IsRight() && state.Length - index > RightJumpMinEdgePositionsDiff && state[index + JumpRightMovePosition].IsEmpty();
        }

        private bool DirectRightMovePossible(int index, Frog[] state)
        {
            return state[index].IsRight() && state.Length - index > DirectRightMoveMinEdgePositionsDiff && state[index + DirectRightMovePosition].IsEmpty();
        }

        private bool JumpLeftMovePossible(int index, Frog[] state)
        {
            return state[index].IsLeft() && index > LeftJumpMinEdgePosition && state[index + JumpLeftMovePosition].IsEmpty();
        }

        private bool DirectLeftMovePossible(int index, Frog[] state)
        {
            return state[index].IsLeft() && index > DirectLeftMoveMinEdgePosition && state[index + DirectLeftMovePosition].IsEmpty();
        }
    }
}