namespace SlidingBlocks.Console
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    public static class Startup
    {
        private static List<Turn> turns;
        private static HashSet<State> visited;
        private static ConcurrentDictionary<State, State> cameFrom;

        public static void Main()
        {
            turns = new List<Turn>();
            visited = new HashSet<State>();
            cameFrom = new ConcurrentDictionary<State, State>();

            var length = int.Parse(Console.ReadLine());
            var playGround = new List<List<int>>();

            for (int i = 0; i < Math.Sqrt(length + 1); i++)
            {
                var row = Console.ReadLine().Split(' ').Select(int.Parse).ToList();
                playGround.Add(row);
            }

            var path = FindSteps(playGround);
            Console.WriteLine(path.Count());
            path.Select(x => x.Turn).ForEach(e => Console.WriteLine(e));
        }
        
        private static IEnumerable<State> FindSteps(List<List<int>> playGround)
        {
            StatePriorityQueue queue = new StatePriorityQueue();

            State startState = new State(playGround, Turn.None, 0);
            queue.Enqueue(startState);

            while (queue.Count != 0)
            {
                State state = queue.Dequeue();
                AddTurn(state);
                visited.Add(state);

                if (state.IsGoal)
                {
                    return ReconstructPath(state).Reverse();
                }

                IEnumerable<State> childStates = GetChildStates(state);
                foreach (var childState in childStates)
                {
                    if (!visited.Contains(childState))
                    {
                        queue.Enqueue(childState);
                        cameFrom.AddOrUpdate(childState, state, (k, v) => v);
                    }
                }
            }

            throw new Exception();
        }

        private static IEnumerable<State> ReconstructPath(State state)
        {
            while (true)
            {
                if (cameFrom.ContainsKey(state))
                {
                    yield return state;
                    state = cameFrom[state];
                }
                else
                {
                    break;
                }
            }
        }

        private static IEnumerable<State> GetChildStates(State state)
        {
            List<List<int>> playground = state.Playground;
            Point startPoint = FindStart(playground);
            int gScore = state.GScore + 1;

            if (startPoint.X > 0)
            {
                List<List<int>> downPlayground = Move(playground, startPoint, new Point(startPoint.X - 1, startPoint.Y));
                yield return new State(downPlayground, Turn.Up, gScore);
            }
            
            if(startPoint.X < playground.Count - 1)
            {
                List<List<int>> upPlayground = Move(playground, startPoint, new Point(startPoint.X + 1, startPoint.Y));
                yield return new State(upPlayground, Turn.Down, gScore);
            }

            if(startPoint.Y > 0)
            {
                List<List<int>> leftPlayground = Move(playground, startPoint, new Point(startPoint.X, startPoint.Y - 1));
                yield return new State(leftPlayground, Turn.Left, gScore);
            }

            if (startPoint.Y < playground.Count - 1)
            {
                List<List<int>> rightPlayground = Move(playground, startPoint, new Point(startPoint.X, startPoint.Y + 1));
                yield return new State(rightPlayground, Turn.Right, gScore);
            }
        }

        private static List<List<int>> Move(List<List<int>> playground, Point source, Point destination)
        {
            var movedPlayGround = Copy(playground);
            var temp = movedPlayGround[destination.X][destination.Y];
            movedPlayGround[destination.X][destination.Y] = 0;
            movedPlayGround[source.X][source.Y] = temp;

            return movedPlayGround;
        }

        private static List<List<int>> Copy(List<List<int>> playground)
        {
            var pg = new List<List<int>>();
            for (int i = 0; i < playground.Count; i++)
            {
                pg.Add(playground[i].ToList());
            }

            return pg;
        }

        private static void AddTurn(State state)
        {
            if (state.Turn != Turn.None)
            {
                turns.Add(state.Turn);
            }
        }

        private static Point FindStart(List<List<int>> playGround)
        {
            for (int row = 0; row < playGround.Count; row++)
            {
                for (int col = 0; col < playGround.Count; col++)
                {
                    if (playGround[row][col] == 0)
                    {
                        return new Point(row, col);
                    }
                }
            }

            throw new Exception("Cannot find starting point");
        }

        public static void ForEach<T>(this IEnumerable<T> elements, Action<T> action)
        {
            foreach (var element in elements)
            {
                action(element);
            }
        }
    }
}
