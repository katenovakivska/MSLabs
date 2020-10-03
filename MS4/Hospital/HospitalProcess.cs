using MS4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital
{
    public class HospitalProcess: Element
    {
        public List<int> Queue { get; set; }
        public List<int> States { get; set; }
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
        public List<HospitalProcess> NextProcesses { get; set; }
        public double WaitingTime { get; set; }
        public double InWaiting{ get; set; }
        public double DelaySum { get; set; }
        public double StartDelay { get; set; }

        public Random random = new Random();
        public List<Channel> Channels { get; set; }
        public int NumberOfTasks { get; set; }

        public List<PatientType> Types { get; set; }
        public int PatientType { get; set; }
        public HospitalProcess()
        {
            NextProcesses = new List<HospitalProcess>();
            AverageQueue = 0.0;
            MaxQueueObserved = 0;
            AverageWorkload = 0;
            States = new List<int>();
            Queue = new List<int>();
        }
        public HospitalProcess(double delay, double devDelay, string name, string distribution, int maxParallel) : base(delay, name, distribution, devDelay)
        {
            NextProcesses = new List<HospitalProcess>();
            MaxQueueLength = 10000;
            AverageQueue = 0.0;
            MaxQueueObserved = 0;
            AverageWorkload = 0;
            MaxParallel = maxParallel;
            DevDelay = devDelay; 
            States = new List<int>();
            Queue = new List<int>();
            NumberOfTasks = 1;
            Channels = new List<Channel>();
            for (int i = 0; i < MaxParallel; i++)
            {
                Channels.Add(new Channel($"{Name}->Channel{i + 1}", 0.0, true));
            }
            Types = new List<PatientType>{
                new PatientType(1, 0.5, 1/15),
                new PatientType(2, 0.1, 1/40),
                new PatientType(3, 0.4, 1/30)
            };
        }
        public override void InAct(int patientType)
        {
            if (States.Count < MaxParallel)
            {
                States.Add(patientType);
            }
            else if (Queue.Count < MaxQueueLength)
            {
                Queue.Add(patientType);
            }
            TNext = TCurrent + GetDelay();
        }
        public override void OutAct()
        {
            Quantity += 1;
            TNext = Double.MaxValue;
            int patientType = 0;
            if (States.Count > 0)
            {
                patientType = States[0];
                States.RemoveAt(0);
            }

            if (Queue.Count > 0)
            {
                int patientTypeQueue = Queue[0];
                Queue.RemoveAt(0);
                States.Add(patientTypeQueue);
            }
            PatientType = patientType;
            if(patientType != 0)
            Types[patientType - 1].Quantity++;
            if (NextProcesses.Count != 0 && patientType != 0)
            {
                int index = random.Next(0, NextProcesses.Count);
                HospitalProcess nextProcess = NextProcesses[index];
                nextProcess.InAct(patientType);
                TNext = TCurrent + GetDelay();
                Console.WriteLine($"IN FUTURE from {Name} to {nextProcess.Name} t = {nextProcess.TNext}");
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
                        Console.WriteLine();
                        Console.WriteLine($"{Channels[i].Name} is busy and will be free in t = {Channels[i].TimeOut}");
                        Console.WriteLine();
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
                        Console.WriteLine();
                        Console.WriteLine($"{Channels[i].Name} is busy and will be free in t = {Channels[i].TimeOut}");
                        Console.WriteLine();
                        count++;
                    }
                }
                NumberOfTasks -= numberOfFreeDevices;
            }
        }
        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine($"failure = {Failure}");
        }
        public override void CountStatistics(double delta)
        {
            AverageQueue += (Queue.Count * delta);
            AverageProcessingTime += delta;
            AverageWorkload += delta * States.Count;
            if(PatientType != 0)
            Types[PatientType - 1].WaitingTime += (Queue.Count + States.Count) * delta;
            WaitingTime += (Queue.Count + States.Count) * delta;
            DelaySum += delta;
            InWaiting += Queue.Count + States.Count;
            if (Queue.Count > MaxQueueObserved)
            {
                MaxQueueObserved = Queue.Count;
            }
            if (MaxWorkload < States.Count)
            {
                MaxWorkload = States.Count;
            }
        }
    }
}
