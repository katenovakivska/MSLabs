using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MS5
{
    public class RandomGenerator
    {
        public static Random random = new Random();
        public double Exponential(double timeMean)
        {
            double a = 0;
            while (a == 0)
            {
                a = random.NextDouble();
            }
            a = -timeMean * Math.Log(a);

            return a;
        }

        public double Uniform(double timeMin, double timeMax)
        {
            double a = 0;
            while (a == 0)
            {
                a = random.NextDouble();
            }
            a = timeMin + a * (timeMax - timeMin);

            return a;
        }

        public double Normal(double timeMean, double timeDeviation)
        {
            double a;
            Random random = new Random();
            double u1 = 1.0 - random.NextDouble();
            double u2 = 1.0 - random.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            a = timeMean + timeDeviation * randStdNormal;

            return a;
        }

        public double Erlang(double timeMean, double timeDeviation)
        {
            double a = -1 / timeDeviation;
            double[] R = new double[] { 0.43, 0.80, 0.29, 0.67, 0.19, 0.96, 0.02, 0.73, 0.50, 0.33, 0.14, 0.71 };
            double r = 1;
            for (int i = 0; i < (int)timeMean; i++)
            {
                r *= R[i];
            }
            a *= Math.Log(r);
            return a;
        }

        public int RandomProbability(List<double> probabilities)
        {
            Random random = new Random();
 
            var vers = new double[probabilities.Count];
            double sum = probabilities.Sum(x => x);
            vers[0] = probabilities[0] / sum;
            for (int i = 1; i < probabilities.Count - 1; i++)
            {
                vers[i] = probabilities[i] / sum + vers[i - 1];
            }
            vers[vers.Length - 1] = 1.0;
            double rnd = random.NextDouble();
            for (int i = 0; i < probabilities.Count; i++)
            {
                if (vers[i] > rnd)
                    return i;
            }
            return probabilities.Count - 1;
        }
    }
}
