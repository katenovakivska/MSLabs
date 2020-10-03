using MS4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoBank
{
    public class Model
    {
        public List<Element> list = new List<Element>();
        public double TNext { get; set; }
        public double TCurrent { get; set; }
        int Event { get; set; }

        public List<Element> sortedList = new List<Element>();
        public Model(List<Element> elements)
        {
            list = elements;
            TNext = 0.00;
            Event = 0;
            TCurrent = TNext;
        }
        public void Simulate(double timeModeling)
        {
            int prev = -1;

            while (TCurrent < timeModeling)
            {
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
                if (!(prev == 0 && Event == 0))
                    Console.WriteLine($"Event in: {list[Event].Name} time = {TNext}");


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
            }
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
                Console.WriteLine($"{c.Name} is free now t = {c.TimeOut}");
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
            foreach (Element e in list)
            {
                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                e.PrintResult();
                if (e.GetType() == typeof(Process) && e.Name != "DISPOSE")
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

        public void ReturnResult()
        {
            double average, workload, waiting, outWindow, avgBank = 0, actAndQueueQuantity = 0;
            int totalQuantity = 0, failure = 0;
            foreach (var e in list)
            {
                e.PrintResult();
                if (e.GetType() == typeof(Process))
                {
                    Process p = (Process)e;
                    average = p.AverageQueue;
                    workload = p.AverageWorkload;
                    waiting = p.AverageQueue / (p.Quantity + p.Failure);
                    outWindow = TCurrent / p.Quantity;
                    totalQuantity += p.Quantity;
                    actAndQueueQuantity += p.AverageAmount;
                    avgBank += average;
                    failure += p.Failure;
                    if (e.Name != "element GO OUT")
                    {
                        Console.WriteLine($"avgLength = {average} transfers = {p.TransferedCount} failure = {failure}" +
                        $" avgTimeInBank = {waiting} workload = {workload} inProgress = {p.State} " +
                        $" inQueue = {p.QueueLength} avgInWindow = {outWindow} ");
                        Console.WriteLine($"In action and in queue on process {p.AverageAmount / TCurrent}");
                    }
                }
            }
            Console.WriteLine($"In action and in queue on system {actAndQueueQuantity / TCurrent}");
            Console.WriteLine($"avgInBank = {avgBank}");
            //failure /= (failure + totalQuantity);
            //Console.WriteLine($"failure = {failure}");
        }

        
    }
}
