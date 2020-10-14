using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MS5
{
    public class ExperimentalEvaluation
    {
        public int AmountOfProcess { get; set; }
        public int AmountOfExperimental { get; set; }
        public double Time { get; set; }
        public int QueueLength { get; set; }
        public int MaxParallel { get; set; }
        public double DelayCreate { get; set; }
        public double DelayDispose { get; set; }
        public double DelayProcess { get; set; }
        public List<Process> Processes { get; set; }
        public List<Element> Elements { get; set; }
        Random random = new Random();
        public ExperimentalEvaluation(double time, int AmountOfProcesses, int amountOfExperiments)
        {
            Time = time;
            AmountOfProcess = AmountOfProcesses;
            AmountOfExperimental = amountOfExperiments;
            QueueLength = random.Next(2, 10);
            MaxParallel = random.Next(2, 10);
            DelayCreate = random.NextDouble();
            DelayDispose = random.NextDouble();
            DelayProcess = random.NextDouble();
            Processes = new List<Process>();
            Elements = new List<Element>();
        }
        public void CreateNProcess()
        {
            for (int i = 0; i < AmountOfProcess + 1; i++)
            {
                Process p = new Process(DelayProcess, $"PROCESSOR{i + 1}", "Exponential", QueueLength, MaxParallel);
                Processes.Add(p);
            }
        }

        public void CreateNextProcesses()
        {
            Process dispose = new Process(DelayDispose, "DISPOSE", "Exponential", QueueLength, MaxParallel);
            for (int i = 0; i < Processes.Count; i++)
            {
                int nextProcessesAmount = AmountOfProcess / 4;

                if (i == 0)
                {
                    nextProcessesAmount = AmountOfProcess / 2;
                }

                for (int j = 0; j < nextProcessesAmount; j++)
                {
                    int processIndex = random.Next(0, Processes.Count);
                    while (processIndex == i)
                    {
                        processIndex = random.Next(0, Processes.Count);
                    }
                    Process nextProcess = Processes[processIndex];
                    Processes[i].NextProcesses.Add(nextProcess);
                }
                Processes[i].NextProcesses.Add(dispose);
                Elements.Add(Processes[i]);
                
            }

        }

        public void StartCheck()
        {
            Console.WriteLine("----------Experimental evaluation----------");
            for (int i = 0; i < AmountOfExperimental; i++)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                Create create = new Create(DelayCreate, "CREATOR", "Exponential");

                Elements.Add(create);
                CreateNProcess();
                CreateNextProcesses();
                create.NextElement = Processes[0];

                Model model = new Model(Elements, AmountOfProcess);
                model.Simulate(Time);
                stopwatch.Stop();
                Console.WriteLine($"Experiment {i + 1}: amount of events = {AmountOfProcess} time of work in milliseconds = {stopwatch.ElapsedMilliseconds}");
                AmountOfProcess += random.Next(20, 50);
                Elements = new List<Element>();
                Processes = new List<Process>();
            }
            
        }
    }
}
