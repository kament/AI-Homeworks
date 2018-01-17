using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal class Transform
    {
        const int Size = 3;
        delegate Point TransformFunc(Point p);

        public static Point Rotate90Degree(Point p)
        {
            return new Point(Size - p.Y - 1, p.X);
        }
        public static Point MirrorX(Point p)
        {
            return new Point(Size - p.X - 1, p.Y);
        }
        public static Point MirrorY(Point p)
        {
            return new Point(p.X, Size - p.Y - 1);
        }

        List<TransformFunc> actions = new List<TransformFunc>();
        public Point ActOn(Point p)
        {
            foreach (TransformFunc f in actions)
            {
                if (f != null)
                    p = f(p);
            }

            return p;
        }

        Transform(TransformFunc op, TransformFunc[] ops)
        {
            if (op != null)
                actions.Add(op);
            if (ops != null && ops.Length > 0)
                actions.AddRange(ops);
        }
        public static List<Transform> s_transforms = new List<Transform>();
        static Transform()
        {
            for (int i = 0; i < 4; i++)
            {
                TransformFunc[] ops = Enumerable.Repeat<TransformFunc>(Rotate90Degree, i).ToArray();
                s_transforms.Add(new Transform(null, ops));
                s_transforms.Add(new Transform(MirrorX, ops));
                s_transforms.Add(new Transform(MirrorY, ops));
            }
        }
    }
}
