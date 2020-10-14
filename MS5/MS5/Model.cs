using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MS5
{
    public class Model
    {
        public List<Element> list = new List<Element>();
        public double TNext { get; set; }
        public double TCurrent { get; set; }
        public int AmountOfSMO { get; set; }
        public int AmountOfEvents { get; set; }
        int Event { get; set; }
        public double EventIntensivity { get; set; }
        public double ModelingTime { get; set; }

        public long AverageAmountOfTacts { get; set; }
        public double AverageQueue { get; set; }
        public double AverageQueueTime { get; set; }

        public List<Element> SortedList = new List<Element>();

        Stopwatch stopwatchEvent = new Stopwatch();

        Stopwatch stopwatchAlgorithm = new Stopwatch();

        public Model(List<Element> elements)
        {
            list = elements;
            TNext = 0.00;
            Event = 0;
            TCurrent = TNext;
        }
        public Model(List<Element> elements, int amount)
        {
            list = elements;
            TNext = 0.00;
            Event = 0;
            TCurrent = TNext;
            AmountOfSMO = amount;
            AmountOfEvents = AmountOfSMO + 1;
        }
        public void Simulate(double timeModeling)
        {
            stopwatchAlgorithm.Start();
            int prev = -1;
            ModelingTime = timeModeling;
            while (TCurrent < timeModeling)
            {
                stopwatchEvent.Start();
                TNext = Double.MaxValue;

                foreach (Element e in list)
                {
                    if (e.TNext < TNext)
                    {
                        TNext = e.TNext;
                        Event = list.IndexOf(e);
                    }
                }
                ManageChannels();
                //if (!(prev == 0 && Event == 0))
                //    Console.WriteLine($"Event in: {list[Event].Name} time = {TNext}");


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
                prev = Event;
                stopwatchEvent.Stop();
                list[Event].AmountOfTacts += stopwatchEvent.ElapsedMilliseconds;
            }
            stopwatchAlgorithm.Stop();
            PrintResult();
        }

        public void ManageChannels()
        {
            List<Channel> channels = new List<Channel>();
            List<Channel> outChannels = new List<Channel>();
            if (list[Event].GetType() == typeof(Process) && list[Event].Name != "DISPOSE")
            {
                Process p = (Process)list[Event];
                p.InChannel();
            }
            foreach (var e in list)
            {
                if (e.GetType() == typeof(Process) && e.Name != "DISPOSE")
                {
                    Process p = (Process)e;
                    outChannels = p.OutChannel(TNext);
                    foreach (var o in outChannels)
                    {
                        channels.Add(o);
                    }

                }
            }
            channels = channels.OrderBy(x => x.TimeOut).ToList();
            foreach (var c in channels)
            {
               // Console.WriteLine($"{c.Name} is free now t = {c.TimeOut}");
            }
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
            Console.WriteLine("\n-------------RESULTS-------------");
            list[0].PrintResult();
            int count = 0; 
            foreach (Element e in list)
            {
                
                
                if (e.GetType() == typeof(Process) && e.Name != "DISPOSE")
                {
                    Process p = (Process)e;
                    p.AverageQueueTime = p.AverageQueue / p.Quantity;
                    p.AverageQueue /= TCurrent;
                    p.AverageProcessingTime /= p.Quantity;
                    p.ProbabilityFailure = p.Failure / (double)(p.Quantity + p.Failure);
                    p.AverageWorkload /= TCurrent;

                    AverageQueue += p.AverageQueue;
                    AverageQueueTime += p.AverageQueueTime;
                    
                    count += p.QuantityQueue;

                    if (p.MaxWorkload > 0)
                    {

                        Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                        e.PrintResult();
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
            EventIntensivity = (AverageQueue / AverageQueueTime) / list.Where(e => list.IndexOf(e) > 0).Sum(e => e.Quantity);
        }
        public List<Result> ReturnResult()
        {
            List<Result> result = new List<Result>();
            double average, workload;
            foreach (var e in list)
            {
                //e.PrintResult();
                if (e.GetType() == typeof(Process))
                {
                    Process p = (Process)e;
                    average = p.AverageQueue;
                    workload = p.AverageWorkload;
                    result.Add(new Result(p.Name, average, workload));
                }
            }
            return result;
        }

        public void FindTheoraticalAndExperimentalEvaluation()
        {
            Console.WriteLine();
            Console.WriteLine("Comparing of theoratical and experimental evaluations in milliseconds");
            foreach (var e in list)
            {
                AverageAmountOfTacts += e.AmountOfTacts;
            }
            AverageAmountOfTacts /= list.Where(e => list.IndexOf(e) > 0).Sum(e => e.Quantity);
            double theoratical = Math.Round(EventIntensivity * ModelingTime * AverageAmountOfTacts);
            Console.WriteLine($"theoratical evaluation: O({theoratical})");
            Console.WriteLine($"experimental evaluation: O({stopwatchAlgorithm.ElapsedMilliseconds})");
            if (theoratical > stopwatchAlgorithm.ElapsedMilliseconds)
            {
                Console.WriteLine("Theoratical evaluation is bigger than experimental evaluation");
            }
            else 
            {
                Console.WriteLine("Experimental evaluation is bigger than theoratical evaluation");
            }
            
        }
    }
}
