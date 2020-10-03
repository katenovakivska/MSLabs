using System;
using System.Collections.Generic;

namespace MS3
{
    class Program
    {
        static void Main(string[] args)
        {
            Create c = new Create(2.7, "CREATOR", "Exponential");
            Process p1 = new Process(2.0, "PROCESSOR1", "Exponential", 2, 3);
            Process p3 = new Process(2.0, "PROCESSOR3", "Exponential", 2, 3);
            Process p4 = new Process(2.0, "PROCESSOR4", "Exponential", 2, 3);
            Process p2 = new Process(2.0, "PROCESSOR2", "Exponential", 2, 3);
            Process d = new Process(2.0, "DISPOSE", "", 0, 3);
            
            c.NextElement = p1;
            Process[] processes = new Process[] { p1, p2, p3, p4 };
            List<Element> list = new List<Element>() { c, p1, p2, p3, p4, d };

            p1.NextProcesses.Add(p2);
            p1.NextProcesses.Add(p3);
            p2.NextProcesses.Add(d);
            p3.NextProcesses.Add(p4);
            p4.NextProcesses.Add(p1);
            p4.NextProcesses.Add(d);

            Model model = new Model(list);
            model.Simulate(1000.0);

            //Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            //double[,] verification = new double[10, 13] {
            //    { 2.2, 2.0, 2.3, 2.4, 3.0, 3.0, 3.0, 3.0, 2.0, 3.0, 2.0, 1.0, 2.0 },
            //    { 3.2, 3.7, 3.8, 3.8, 3.0, 3.0, 3.0, 3.0, 2.0, 3.0, 2.0, 1.0, 2.0 },
            //    { 4.2, 4.7, 4.8, 4.8, 3.0, 3.0, 3.0, 3.0, 2.0, 3.0, 2.0, 1.0, 2.0 },
            //    { 2.2, 2.0, 2.3, 2.4, 3.0, 3.0, 3.0, 3.0, 2.0, 3.0, 2.0, 1.0, 3.0 },
            //    { 2.2, 2.0, 2.3, 2.4, 5.0, 5.0, 5.0, 5.0, 2.0, 3.0, 2.0, 1.0, 4.0 },
            //    { 2.2, 2.0, 2.3, 2.4, 4.0, 4.0, 4.0, 4.0, 2.0, 3.0, 2.0, 1.0, 2.0 },
            //    { 2.2, 2.0, 2.3, 2.4, 6.0, 6.0, 6.0, 6.0, 2.0, 3.0, 2.0, 1.0, 2.0 },
            //    { 2.2, 2.0, 2.3, 2.4, 3.0, 3.0, 3.0, 3.0, 4.0, 6.0, 4.0, 2.0, 2.0 },
            //    { 2.2, 2.0, 2.3, 2.4, 3.0, 3.0, 3.0, 3.0, 2.0, 3.0, 2.0, 1.0, 2.0 },
            //    { 3.2, 4.0, 4.3, 4.4, 3.0, 3.0, 3.0, 3.0, 2.0, 3.0, 2.0, 1.0, 3.0 }
            //};
            //Console.WriteLine("--------------------------------------VERIFICATION TABLE----------------------------------------");

            //Random random = new Random();
            //for (int i = 0; i < 10; i++)
            //{
            //    Console.WriteLine($"****************************VERIFICATION CASE {i + 1}************************************");
            //    Create c1 = new Create(verification[i, 12], "CREATOR", "Exponential");

            //    Process p1_1 = new Process(verification[i, 0], "PROCESSOR1", "Exponential", (int)verification[i, 4], (int)verification[i, 8]);
            //    Process p1_3 = new Process(verification[i, 1], "PROCESSOR3", "Exponential", (int)verification[i, 5], (int)verification[i, 9]);
            //    Process p1_4 = new Process(verification[i, 2], "PROCESSOR4", "Exponential", (int)verification[i, 6], (int)verification[i, 10]);
            //    Process p1_2 = new Process(verification[i, 3], "PROCESSOR2", "Exponential", (int)verification[i, 7], (int)verification[i, 11]);
            //    Process d1 = new Process(verification[i, 0], "DISPOSE", "", 0, 0);

            //    c1.NextElement = p1_1;
            //    List<Element> list1 = new List<Element>() { c1, p1_1, p1_2, p1_3, p1_4, d1 };

            //    p1_1.NextProcesses.Add(p1_2);
            //    p1_1.NextProcesses.Add(p1_3);
            //    p1_2.NextProcesses.Add(d1);
            //    p1_3.NextProcesses.Add(p1_4);
            //    p1_4.NextProcesses.Add(p1_1);
            //    p1_4.NextProcesses.Add(d1);


            //    Model model1 = new Model(list1);
            //    model1.Simulate(1000.0);
            //    Console.WriteLine("");
            //}

            Console.ReadKey();
        }
    }
}
