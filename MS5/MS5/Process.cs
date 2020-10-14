using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MS5
{
    public class Process : Element
    {
        private int QueueLength { get; set; }
        public int MaxQueueLength { get; set; }
        public int Failure { get; set; }
        public double ProbabilityFailure { get; set; }
        public double AverageQueue { get; set; }
        public int MaxQueueObserved { get; set; }
        public double AverageQueueTime { get; set; }
        public double AverageProcessingTime { get; set; }
        public double AverageWorkload { get; set; }
        public double MaxWorkload { get; set; }
        public int MaxParallel { get; set; }
        public List<Process> NextProcesses { get; set; }

        public Random random = new Random();
        public List<Channel> Channels { get; set; }
        public int NumberOfTasks { get; set; }
        public List<double> Probabilities { get; set; }
        public int QuantityQueue { get; set; }
        public Process()
        {
            NextProcesses = new List<Process>();
            QueueLength = 0;
            AverageQueue = 0.0;
            MaxQueueObserved = 0;
            AverageWorkload = 0;
        }
        public Process(double delay, string name, string distribution, int maxQueueLength, int maxParallel, double devDelay = 0) : base(delay, name, distribution, devDelay)
        {
            NextProcesses = new List<Process>();
            QueueLength = 0;
            MaxQueueLength = maxQueueLength;
            QuantityQueue = 0;
            AverageQueue = 0.0;
            MaxQueueObserved = 0;
            AverageWorkload = 0;
            MaxParallel = maxParallel;
            Channels = new List<Channel>();
            Probabilities = new List<double>();
            for (int i = 0; i < MaxParallel; i++)
            {
                Channels.Add(new Channel($"{Name}->Channel{i + 1}", 0.0, true));
            }
        }
        public override void InAct(int numberOfTasks)
        {
            int numberOfFreeDevices = MaxParallel - State;
            NumberOfTasks = numberOfTasks;
            if (numberOfTasks <= numberOfFreeDevices && numberOfTasks > 0)
            {
                State += numberOfTasks;
                numberOfTasks = 0;
            }
            else if (numberOfTasks > numberOfFreeDevices)
            {
                numberOfTasks -= numberOfFreeDevices;
                State = MaxParallel;
            }

            TNext = TCurrent + GetDelay();
            if (numberOfTasks > 0)
            {
                int numberOfFreePlaces = MaxQueueLength - QueueLength;
                if (numberOfTasks < numberOfFreePlaces)
                {
                    QueueLength += numberOfTasks;
                    numberOfTasks = 0;
                }
                else
                {
                    numberOfTasks -= numberOfFreePlaces;
                    QueueLength = MaxQueueLength;
                }
                QuantityQueue++;
            }

            if (numberOfTasks > 0)
            {
                Failure += numberOfTasks;
            }
        }
        public List<Channel> OutChannel(double t)
        {
            List<Channel> outChannels = new List<Channel>();
            Channels = Channels.OrderBy(x => x.TimeOut).ToList();
            foreach (var c in Channels)
            {
                if (Channels.Count != 0)
                {
                    if (c.TimeOut < t && c.IsFree == false)
                    {
                        c.IsFree = true;
                        outChannels.Add(c);
                    }
                }
            }
            return outChannels;
        }
        public void InChannel()
        {
            int count = 0;
            int numberOfFreeDevices = MaxParallel - State;
            if (NumberOfTasks <= numberOfFreeDevices && NumberOfTasks > 0)
            {

                for (int i = 0; i < Channels.Count; i++)
                {
                    if (Channels[i].IsFree == true)
                    {
                        Channels[i].TimeOut = TCurrent + GetDelay();
                        Channels[i].IsFree = false;
                        //Console.WriteLine($"{Channels[i].Name} is busy and will be free in t = {Channels[i].TimeOut}");
                        count++;
                    }
                    if (count == NumberOfTasks)
                    {
                        break;
                    }
                }
                NumberOfTasks = 0;
            }
            else if (NumberOfTasks > numberOfFreeDevices)
            {
                for (int i = 0; i < Channels.Count; i++)
                {
                    if (Channels[i].IsFree == true)
                    {
                        Channels[i].TimeOut = TCurrent + GetDelay();
                        Channels[i].IsFree = false;
                        count++;
                    }
                }
                NumberOfTasks -= numberOfFreeDevices;
            }
        }
        public override void OutAct()
        {
            base.OutAct();

            TNext = Double.MaxValue;
            State -= 1;
            if (QueueLength > 0)
            {
                QueueLength--;
                State += 1;
                TNext = TCurrent + GetDelay();
            }

            if (NextProcesses.Count != 0 && State != -1)
            {
                //if (Probabilities.Count > 0)
                //{
                //    int index = RandomGenerator.RandomProbability(Probabilities);
                int index = random.Next(0, NextProcesses.Count);
                Process nextProcess = NextProcesses[index];
                    if (nextProcess != null)
                        nextProcess.InAct(1);
                   // Console.WriteLine($"IN FUTURE goes from {Name} to {nextProcess.Name} t = {nextProcess.TNext}");
                //}
            }
        }
        public override void PrintInfo()
        {
            base.PrintInfo();
            //Console.WriteLine($"failure = {Failure}");
        }
        public override void CountStatistics(double delta)
        {
            AverageQueue += QueueLength * delta;
            if (State > 0)
            {
                AverageWorkload += delta * State;
            }
            AverageProcessingTime += delta;
            if (QueueLength > MaxQueueObserved)
            {
                MaxQueueObserved = QueueLength;
            }
            if (MaxWorkload < State)
            {
                MaxWorkload = State;
            }
        }
    }
}
