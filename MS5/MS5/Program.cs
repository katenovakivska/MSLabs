using System;
using System.Collections.Generic;

namespace MS5
{
    class Program
    {
        static void Main(string[] args)
        {
            ////------------------------------------Task 1----------------------------------------------
            //Create c = new Create(2.0, "CREATOR", "Exponential");
            //Process p1 = new Process(0.6, "PROCESSOR1", "Exponential", 2, 1);
            //Process p2 = new Process(0.3, "PROCESSOR2", "Exponential", 2, 1);
            //Process p3 = new Process(0.4, "PROCESSOR3", "Exponential", 2, 1);
            //Process p4 = new Process(0.1, "PROCESSOR4", "Exponential", 2, 2);
            //Process d = new Process(0, "DISPOSE", "Exponential", 0, 1);

            //c.NextElement = p1;
            //p1.NextProcesses = new List<Process> { p2, p3, p4 };
            //p2.NextProcesses = new List<Process> { p1 };
            //p3.NextProcesses = new List<Process> { p1 };
            //p4.NextProcesses = new List<Process> { p1 };
            //List<Element> elementsList = new List<Element> { c, p1, p2, p3, p4 };
            //Model model = new Model(elementsList);
            //model.Simulate(1000.0);
            //model.FindTheoraticalAndExperimentalEvaluation();
            //List<Result> res = model.ReturnResult();

            //double[] theoreticalAverageQueue = new double[] { 1.786, 0.003, 0.004, 0.00001 };
            //double[] theoreticalWorkload = new double[] { 0.714, 0.054, 0.062, 0.036 };
            //double[] queueAccuracy = new double[res.Count];
            //double[] workloadAccuracy = new double[res.Count];

            //for (int i = 0; i < theoreticalAverageQueue.Length; i++)
            //{
            //    queueAccuracy[i] = Math.Abs(res[i].Average - theoreticalAverageQueue[i]) / theoreticalAverageQueue[i];
            //    workloadAccuracy[i] = Math.Abs(res[i].Workload - theoreticalWorkload[i]) / theoreticalWorkload[i];
            //    Console.WriteLine($"queueAccuracy = {queueAccuracy[i]} workloadAccuracy = {workloadAccuracy[i]}");
            //}

            //////------------------------------------Task 2 and Task 4----------------------------------------------

            //int N = 50;
            //NCheck nCheck = new NCheck(N, 1000);
            //nCheck.StartCheck();

            //////------------------------------------Task 3----------------------------------------------
            //ExperimentalEvaluation evaluation = new ExperimentalEvaluation(1000, 10, 7);
            //evaluation.StartCheck();

            ////------------------------------------Task 5----------------------------------------------
            //Change condition of test example  in first task with adding new route
            Create c = new Create(2.0, "CREATOR", "Exponential");
            Process p1 = new Process(0.6, "PROCESSOR1", "Exponential", 2, 1);
            Process p2 = new Process(0.3, "PROCESSOR2", "Exponential", 2, 1);
            Process p3 = new Process(0.4, "PROCESSOR3", "Exponential", 2, 1);
            Process p4 = new Process(0.1, "PROCESSOR4", "Exponential", 2, 2);
            Process d = new Process(0, "DISPOSE", "Exponential", 0, 1);

            c.NextElement = p1;
            p1.NextProcesses = new List<Process> { p2, p3, p4 };
            p2.NextProcesses = new List<Process> { p1 };
            p3.NextProcesses = new List<Process> { p1, p4 };
            p4.NextProcesses = new List<Process> { p1 };
            List<Element> elementsList = new List<Element> { c, p1, p2, p3, p4 };
            Model model = new Model(elementsList);
            model.Simulate(1000.0);
            model.FindTheoraticalAndExperimentalEvaluation();
        }
    }
}
