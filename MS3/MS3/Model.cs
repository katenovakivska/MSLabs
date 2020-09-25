using System;
using System.Collections.Generic;
using System.Text;

namespace MS3
{
    public class Model
    {
        private List<Element> list = new List<Element>();
        public double TNext { get; set; }
        public double TCurrent { get; set; }
        int Event { get; set; }
        public Model(List<Element> elements)
        {
            list = elements;
            TNext = 0.00;
            Event = 0;
            TCurrent = TNext;
        }
        public void Simulate(double timeModeling, Create c)
        {
            Element prev = new Element();
            prev.NextElements = new List<Element>();
            prev.NextElements.Add(c);
            while (TCurrent < timeModeling)
            {
                TNext = Double.MaxValue;
                double minTNext = TNext;
                foreach (Element e in list)
                {
                    if (e.TNext < TNext && e.TNext < minTNext && prev.NextElements.Contains(e))
                    {
                        minTNext = e.TNext;
                        TNext = e.TNext;
                        Event = list.IndexOf(e);
                        
                    }
                }
                prev = (Element)list[Event];
                Console.WriteLine($"\nEvent in: {list[Event].Name} time = {TNext}");
                foreach (Element e in list)
                {
                    e.CountStatistics(TNext - TCurrent);
                }
                TCurrent = TNext;
                foreach (Element e in list)
                {
                    e.TCurrent = TCurrent;
                }
                list[Event].OutAct();
                foreach (Element e in list)
                {
                    if (e.TNext == TCurrent)
                    {
                        e.OutAct();
                    }
                }
               //PrintInfo();
             }
             PrintResult();
        }


        public void PrintInfo()
        {
            foreach (Element e in list)
            {
                e.PrintInfo();
            }
        }

        public void PrintResult()
        {
           // Console.WriteLine("\n-------------RESULTS-------------");
            foreach (Element e in list)
            {
                e.printResult();
                if (e.GetType() == typeof(Process)) 
                {
                    Process p = (Process)e;
                    p.AverageQueueTime = p.AverageQueue / p.Quantity;
                    p.AverageQueue /= TCurrent;
                    p.AverageProcessingTime /= p.Quantity;
                    p.ProbabilityFailure = p.Failure / (double)(p.Quantity + p.Failure);
                    p.AverageWorkload /= TCurrent;
                    //Console.WriteLine($"Delay = {p.AverageDelay} QLength = {p.MaxQueueObserved} MaxParallel = {p.MaxParallel} AvgQLength = {p.AverageQueue} " +
                    //$"MaxQLength = {p.MaxQueueObserved} AvgWorkload = {p.AverageWorkload} MaxWorkload = {p.MaxWorkload} AvgProcTime = {p.AverageProcessingTime}" +
                    //$" Failure = {p.Failure} PFailure = {p.ProbabilityFailure}    AvgQTime = {p.AverageQueueTime}");
                    //Console.WriteLine($"DisposeQuantity = {p.dispose.ExitQuantity}  Quantity = {p.Quantity}");
                    Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine($"mean length of queue = {p.AverageQueue}");
                    Console.WriteLine($"max observed queue length = {p.MaxQueueObserved}");
                    Console.WriteLine($"failure probability = {p.ProbabilityFailure}");
                    Console.WriteLine($"average time in queue = {p.AverageQueueTime}");
                    Console.WriteLine($"average processing time = {p.AverageProcessingTime}");
                    Console.WriteLine($"max workload = {p.MaxWorkload}");
                    Console.WriteLine($"average workload = {p.AverageWorkload}");
                }
            }
        }
    }
}

