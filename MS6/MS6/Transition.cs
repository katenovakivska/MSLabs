using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MS6
{
    //переход, он же событие 
    public class Transition
    {
        //доступен ли переход
        public bool IsAvailable { get; set; }
        //входящие дуги
        public List<Arc> InArcs { get; set; }
        //выходящие дуги
        public List<Arc> OutArcs { get; set; }
        public double AverageDelay { get; set; }
        public double DevDelay { get; set; }
        public double TNext { get; set; }
        public double TCurrent { get; set; }
        public string Name { get; set; }
        public string Distribution { get; set; }
        public double AverageTimeInSystem { get; set; }
        public int Count { get; set; }
        
        public Transition(string name, double delay, string distribution = "Exponential", double devDelay = 0)
        {
            Name = name;
            InArcs = new List<Arc>();
            OutArcs = new List<Arc>();
            AverageDelay = delay;
            TNext = Double.MaxValue;
            Distribution = distribution;
            DevDelay = devDelay;
            Count = 0;
            IsAvailable = false;
        }

        public bool CheckConditionsOfTransitions(List<Position> positions)
        {
            List<string> names = InArcs.Select(x => x.PrevPosition.Name).ToList();
            if (positions.Where(x => names.Contains(x.Name) == true).Sum(x => x.MarkersCount) >= InArcs.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Position> TakeMarkers(List<Position> positions)
        {
            //Console.Write($"Transition {Name}: ");
            //Console.Write("markers take from ");
            foreach (var a in InArcs)
            {
                if (a.PrevPosition.Name != "INCOMING")
                {
                    positions.Where(x => x.Name == a.PrevPosition.Name).FirstOrDefault().MarkersCount -= 1;
                }

                //Console.Write($"{a.PrevPosition.Name} ");
            }
            
            Console.WriteLine();
            return LaunchTransition(positions);
        }

        public List<Position> LaunchTransition(List<Position> positions)
        {
            //Console.Write("to ");
            foreach (var a in OutArcs)
            {
                positions.Where(x => x.Name == a.NextPosition.Name).FirstOrDefault().MarkersCount += 1;
                positions.Where(x => x.Name == a.NextPosition.Name).FirstOrDefault().AmountOfPerformance += 1;
                //Console.Write($"{a.NextPosition.Name} ");
            }
            //Console.Write($"time = {TNext}");
            //Console.WriteLine();
            //Console.WriteLine();
            TNext = TCurrent + GetDelay();
            if(Name == "GO FIRST")
            {
                IncomingFrequency();
            }
            return positions;
        }

        public void FindTNext()
        {
            TNext = TCurrent + GetDelay();
        }

        public void IncomingFrequency()
        {
            TNext +=InArcs.FirstOrDefault().PrevPosition.MarkersCount / OutArcs.FirstOrDefault().Multiplicity;
        }
        public double GetDelay()
        {
            double delay = 0.00;
            switch (Distribution)
            {
                case "Exponential":
                    delay = RandomGenerator.Exponential(AverageDelay);
                    break;
                case "Uniform":
                    delay = RandomGenerator.Uniform(AverageDelay, DevDelay);
                    break;
                case "Normal":
                    delay = RandomGenerator.Normal(AverageDelay, DevDelay);
                    break;
                case "":
                    delay = AverageDelay;
                    break;
            }

            return delay;
        }

        public void CountStatistics(double delta, List<Position> positions)
        {
            AverageTimeInSystem += delta;
            Count++;
            foreach (var a in OutArcs)
            {
                positions.Where(x => x.Name == a.NextPosition.Name).FirstOrDefault().TimeOfPerformance += delta;
            }
        }
    }
}
