using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNearestNeighbors
{
    internal static class Startup
    {
        private const int K = 3;
        private const int TEST_SET_COUNT = 20;

        private static Random rand = new Random();
        
        public static void Main()
        {
            IEnumerable<Individual> data = LoadData();

            IEnumerable<Individual> testSet = GetTestData(data);
            IEnumerable<Individual> learningSet = data.Except(testSet);

            int matched = KNearestNeighbors(testSet, learningSet);

            Console.WriteLine($"Accuracy: { (double) matched / testSet.Count()}");
        }

        private static int KNearestNeighbors(IEnumerable<Individual> testSet, IEnumerable<Individual> learningSet)
        {
            return testSet
                .Select(i => Matching(i, learningSet))
                .Sum();
        }

        private static int Matching(Individual i, IEnumerable<Individual> learningSet)
        {
            var sortedByEuclid = learningSet
                .ToDictionary(x => x, x => EuclidFunction(i, x))
                .OrderBy(x => x.Value)
                .Take(K)
                .Select(x => x.Key);

            var definedClass = TakeCommonClass(sortedByEuclid);

            Console.WriteLine($"Test item class is: {definedClass} --- Actual: {i.Class.Value}");

            if (definedClass == i.Class.Value)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private static IEnumerable<Individual> GetTestData(IEnumerable<Individual> data)
        {
            int count = data.Count();

            return Enumerable
                .Range(0, TEST_SET_COUNT)
                .Select(x => data.ElementAt(rand.Next(0, count)));
        }

        private static IEnumerable<Individual> LoadData()
        {
            foreach (var line in File.ReadAllLines("IrisData.txt"))
            {
                string[] arr = line.Split(',');

                yield return new Individual
                {
                    P1 = double.Parse(arr[0]),
                    P2 = double.Parse(arr[1]),
                    P3 = double.Parse(arr[2]),
                    P4 = double.Parse(arr[3]),
                    Class = Iris.Create(arr[4])
                };
            }
        }

        private static string TakeCommonClass(IEnumerable<Individual> individuals)
        {
            string mostCommon = individuals
                .GroupBy(v => v.Class.Value)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            return mostCommon;
        }

        private static double EuclidFunction(Individual first, Individual second)
        {
            return Math.Sqrt(Math.Pow((first.P1 - second.P1), 2) + 
                Math.Pow((first.P2 - second.P2), 2) + 
                Math.Pow((first.P3 - second.P3), 2) + 
                Math.Pow((first.P4 - second.P4), 2));
        }
    }
}
