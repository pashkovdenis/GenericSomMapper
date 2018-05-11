using System;
using System.Collections.Generic;
using System.Text;

namespace SomTests.Extensions
{
    public static class  MathExtensions
    {
        public static  double Distance(this double[] vector1, double[] vector2)
        {
            double value = 0;
            for (int i = 0; i < vector1.Length; i++)
                value += Math.Pow((vector1[i] - vector2[i]), 2);
            return Math.Sqrt(value);
        }

        public static void NormilizePatterns(this List<double[]> patterns, int dimensions)
        {
            for (int j = 0; j < dimensions; j++)
            {
                double sum = 0;
                for (int i = 0; i < patterns.Count; i++)
                    sum += patterns[i][j];
                double average = sum / patterns.Count;
                for (int i = 0; i < patterns.Count; i++)
                    patterns[i][j] = patterns[i][j] / average;
            }

        }

    }
}
