using System;
using System.Collections.Generic;
using System.Text;

namespace MS5
{
    public class NCheck
    {
        public int N { get; set; }
        public double Time { get; set; }
        public List<Process> Processes { get; set; }

        public List<Element> Elements { get; set; }
        Random random = new Random();
        public NCheck(int n, double time)
        {
            N = n;
            Time = time;
            Processes = new List<Process>();
            Elements = new List<Element>();
        }
        public void CreateNProcess()
        {
            for (int i = 0; i < N + 1; i++)
            {
                double delay = random.NextDouble();
                int queueLength = random.Next(0, 6);
                int maxParallel = random.Next(1, 5);
                Process p = new Process(delay, $"PROCESSOR{i + 1}", "Exponential", queueLength, maxParallel);
                Processes.Add(p);
            }
        }

        public void CreateNextProcesses()
        {
            double delay = random.NextDouble();
            int queueLength = random.Next(2, 10);
            int maxParallel = random.Next(2, 10);
            Process dispose = new Process(delay, "DISPOSE", "Exponential", queueLength, maxParallel);
            for (int i = 0; i < Processes.Count; i++)
            {
                int nextProcessesAmount = random.Next(1, N / 3);

                if (i == 0)
                {
                    nextProcessesAmount = N / 2;
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
            double delayCreate = random.NextDouble();
            Create create = new Create(delayCreate, "CREATOR", "Exponential");
            
            Elements.Add(create);
            CreateNProcess();
            CreateNextProcesses();
            create.NextElement = Processes[0];

            Model model = new Model(Elements, N);
            model.Simulate(Time);
            model.FindTheoraticalAndExperimentalEvaluation();
        }
    }
}
