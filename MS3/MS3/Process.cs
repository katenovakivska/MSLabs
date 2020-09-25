using System;
using System.Collections.Generic;
using System.Text;

namespace MS3
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

        public Process()
        {
            NextProcesses = new List<Process>();
            QueueLength = 0;
            AverageQueue = 0.0;
            MaxQueueObserved = 0;
            AverageWorkload = 0;
        }
        public Process(double delay, string name, string distribution, int maxQueueLength, int maxParallel) : base(delay, name, distribution)
        {
            NextProcesses = new List<Process>();
            QueueLength = 0;
            MaxQueueLength = maxQueueLength;
            AverageQueue = 0.0;
            MaxQueueObserved = 0;
            AverageWorkload = 0;
            MaxParallel = maxParallel;
        }
        public override void InAct(int numberOfTasks)
        {
            int numberOfFreeDevices = MaxParallel - State;
            if (numberOfTasks <= numberOfFreeDevices && numberOfTasks > 0)
            {
                State += numberOfTasks;
                numberOfTasks = 0;
            }
            else if(numberOfTasks > numberOfFreeDevices)
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
            }
            
            if (numberOfTasks > 0)
            {
                Failure += numberOfTasks;
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
            
            if (NextProcesses.Count != 0)
            {
                int index = random.Next(0, NextProcesses.Count);
                Process nextProcess = NextProcesses[index];
                nextProcess.InAct(1);
            }
        }
        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine($"failure = {Failure}");
        }
        public override void CountStatistics(double delta)
        {
            AverageQueue += QueueLength * delta;
            AverageProcessingTime += delta;
            AverageWorkload += delta * State;
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
