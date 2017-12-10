using System;
using System.Collections.Generic;
using System.Linq;

namespace SlidingBlocks.Console
{
    public class StatePriorityQueue
    {
        private SortedList<int, Queue<State>> queue;

        public StatePriorityQueue()
        {
            this.queue = new SortedList<int, Queue<State>>();
            this.Count = 0;
        }

        public int Count { get; private set; }

        public void Enqueue(State state)
        {
            if (!queue.ContainsKey(state.HScore))
            {
                queue.Add(state.HScore, new Queue<State>());
            }

            queue[state.HScore].Enqueue(state);
            Count++;
        }

        public State Dequeue()
        {
            if(Count != 0)
            {
                var state = this.queue.First();
                if(state.Value.Count == 1)
                {
                    this.queue.Remove(state.Key);
                }

                Count--;

                return state.Value.Dequeue();
            }

            throw new Exception("Dequeue");
        }
    }
}
