using System;
using System.Collections.Generic;
using System.Linq;

namespace MS4
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create c = new Create(3.0, "CREATOR", "Exponential");
            //Process p1 = new Process(2.0, "PROCESSOR1", "Exponential", 2, 3);
            //Process p3 = new Process(2.0, "PROCESSOR3", "Exponential", 2, 3);
            //Process p4 = new Process(2.0, "PROCESSOR4", "Exponential", 2, 3);
            //Process p2 = new Process(2.0, "PROCESSOR2", "Exponential", 2, 3);
            //Process d = new Process(1.0, "DISPOSE", "", 0, 0);

            //c.NextElement = p1;

            //Process[] processes = new Process[] { p1, p2, p3, p4 };
            //List<Element> list = new List<Element>() { c, p1, p2, p3, p4, d };

            //p1.NextProcesses.Add(p2);
            //p1.NextProcesses.Add(p3);
            //p2.NextProcesses.Add(d);
            //p3.NextProcesses.Add(p4);
            //p4.NextProcesses.Add(p1);
            //p4.NextProcesses.Add(d);
            //p1.Probabilities = new List<Probability> { { new Probability(1, 0.4) }, { new Probability(2, 0.6) } };
            //p4.Probabilities = new List<Probability> { { new Probability(1, 0.2) }, { new Probability(2, 0.8) } };
            //Model model = new Model(list);
            //model.Simulate(1000.0);

            Create c = new Create(2.0, "CREATOR", "Exponential");
            Process p1 = new Process(0.6, "PROCESSOR1", "Exponential", 2, 1);
            Process p2 = new Process(0.3, "PROCESSOR2", "Exponential", 2, 1);
            Process p3 = new Process(0.4, "PROCESSOR3", "Exponential", 2, 1);
            Process p4 = new Process(0.1, "PROCESSOR4", "Exponential", 2, 2);
            Process d = new Process(0, "DISPOSE", "Exponential", 0, 1);

            c.NextElement = p1;
            p1.NextProcesses = new List<Process> { p2, p3, p4 };
            p1.Probabilities = new List<Probability> { new Probability(0.15), new Probability(0.13), new Probability(0.3) };
            p2.NextProcesses = new List<Process> { p1 };
            p3.NextProcesses = new List<Process> { p1 };
            p4.NextProcesses = new List<Process> { p1 };
            List<Element> elementsList = new List<Element> { c, p1, p2, p3, p4 };
            Model model = new Model(elementsList);
            model.Simulate(1000.0);
            List<Result> res = model.ReturnResult();

            double[] theoreticalAverageQueue = new double[] { 1.786, 0.003, 0.004, 0.00001 };
            double[] theoreticalWorkload = new double[] { 0.714, 0.054, 0.062, 0.036 };
            double[] queueAccuracy = new double[res.Count];
            double[] workloadAccuracy = new double[res.Count];

            for (int i = 0; i < theoreticalAverageQueue.Length; i++)
            {
                queueAccuracy[i] = Math.Abs(res[i].Average - theoreticalAverageQueue[i]) / theoreticalAverageQueue[i];
                workloadAccuracy[i] = Math.Abs(res[i].Workload - theoreticalWorkload[i]) / theoreticalWorkload[i];
                Console.WriteLine($"queueAccuracy = {queueAccuracy[i]} workloadAccuracy = {workloadAccuracy[i]}");
            }

        }
    }
}
