using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System.Text;

namespace MS4
{
    public class Element
    {
        public string Name { get; set; }
        public double TNext { get; set; }
        public double AverageDelay { get; set; }
        protected double DevDelay { get; set; }
        public string Distribution { get; set; }
        public int Quantity { get; set; }
        public double TCurrent { get; set; }
        public int State { get; set; }
        public Element NextElement { get; set; }
        public List<Element> NextElements { get; set; }

        public RandomGenerator RandomGenerator = new RandomGenerator();
        public bool IsNext { get; set; }
        private static int NextId;
        public int Id { get; set; }
        public Element()
        {
            TNext = 0.0;
            AverageDelay = 1.0;
            Distribution = "Exponential";
            TCurrent = TNext;
            State = 0;
            Id = NextId;
            NextId++;
            Name = "element " + Name;
        }
        public Element(double delay, string name, string distribution, double devDelay)
        {
            Name = name;
            TNext = 0.0;
            AverageDelay = delay;
            DevDelay = devDelay;
            Distribution = distribution;
            TCurrent = TNext;
            State = 0;
            Id = NextId;
            NextId++;
            IsNext = true;
            NextElements = new List<Element>();
            //NextProcesses = new List<Process>();
            Name = "element " + Name;
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
                case "Erlang":
                    delay = RandomGenerator.Erlang(AverageDelay, DevDelay);
                    break;
                case "":
                    delay = AverageDelay;
                    break;
            }

            return delay;
        }
        public virtual void InAct(int i)
        {

        }
        public virtual void OutAct()
        {
            Quantity++;
        }
        public void PrintResult()
        {
            Console.WriteLine($"{Name}: quantity = {Quantity}");
        }
        public virtual void PrintInfo()
        {
            Console.WriteLine($"{Name}: state = {State} quantity = {Quantity} tnext = {TNext}");
        }
        public virtual void CountStatistics(double delta)
        {

        }
    }
}
