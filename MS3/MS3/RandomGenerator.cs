using System;
using System.Collections.Generic;
using System.Text;

namespace MS3
{
    public class RandomGenerator
    {
        public static Random random = new Random();
        public static double Exponential(double timeMean)
        {
            double a = 0;
            while (a == 0)
            {
                a = random.NextDouble();
            }
            a = -timeMean * Math.Log(a);

            return a;
        }

        public static double Uniform(double timeMin, double timeMax)
        {
            double a = 0;
            while (a == 0)
            {
                a = random.NextDouble();
            }
            a = timeMin + a * (timeMax - timeMin);

            return a;
        }

        public static double Normal(double timeMean, double timeDeviation)
        {
            double a;
            Random random = new Random();
            double u1 = 1.0 - random.NextDouble(); 
            double u2 = 1.0 - random.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            a = timeMean + timeDeviation * randStdNormal;

            return a;
        }

    }
}
