using System;
using System.Collections.Generic;
using System.Text;

namespace МС2
{
    public class Generator
    {
        Random random = new Random();
        public Generator()
        {

        }
        public double GenerateSteady(double duration)
        {
            double r = 0, b = random.Next(5, 10);

            while (r == 0)
            {
                r = random.NextDouble();
            }

            return duration + (b - duration) * r;

        }

        public double GenerateExponential(double duration)
        {
            double a = 0;
            while (a == 0)
            {
                a = random.NextDouble();
            }

            return -duration * Math.Log(a);
        }
    }
}
