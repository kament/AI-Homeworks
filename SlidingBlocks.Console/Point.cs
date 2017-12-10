using System;

namespace SlidingBlocks.Console
{
    internal class Point : IEquatable<Point>
    {
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; }

        public int Y { get; }

        public bool Equals(Point other)
        {
            return this.X == other.X && this.Y == other.Y;
        }

        public override int GetHashCode()
        {
            return this.X >> this.Y;
        }
    }
}