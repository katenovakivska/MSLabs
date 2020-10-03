using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital
{
    public class PatientType
    {
        public int Index { get; set; }
        public double Frequency { get; set; }
        public double AvgTimeOfRegistration { get; set; }
        public int Quantity { get; set; }
        public double WaitingTime { get; set; }

        public PatientType(int id, double frequency, double avgTimeOfRegistration)
        {
            Index = id;
            Frequency = frequency;
            AvgTimeOfRegistration = avgTimeOfRegistration;
            Quantity = 0;
        }
        public int ChooseProbability(List<PatientType> probabilities)
        {
            Random random = new Random();
            int x = 0;
            double a = random.NextDouble();
            probabilities = probabilities.OrderByDescending(x => x.Frequency).ToList();
            double sum = probabilities.Sum(x => x.Frequency);
            for (int i = 0; i < probabilities.Count; i++)
            {
                if (a < sum)
                {
                    x = probabilities[i].Index;
                }
                sum -= probabilities[i].Frequency;
            }
            return x;
        }
    }
}
