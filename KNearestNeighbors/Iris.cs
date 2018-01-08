using System;

namespace KNearestNeighbors
{
    public class Iris
    {
        private Iris(string value)
        {
            this.Value = value;
        }

        public static Iris Create(string value)
        {
            switch (value)
            {
                case "Iris-virginica":
                case "Iris-versicolor":
                case "Iris-setosa":
                    return new Iris(value);
                default:
                    throw new NotImplementedException(value);
            }
        }

        public string Value { get; set; }
    }
}