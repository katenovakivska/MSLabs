using MS4;
using System;
using System.Collections.Generic;

namespace AutoBank
{
    class Program
    {
        static void Main(string[] args)
        {
            Create c = new Create(0.1, "CREATOR", "Exponential");
            Process p1 = new Process(0.3, "CASHBOX1", "Exponential", 3, 1);
            Process p2 = new Process(0.3, "CASHBOX2", "Exponential", 3, 1);
            Process d = new Process(0.3, "GO OUT", "Exponential", 3, 1);
            p1.NextProcesses = new List<Process> { p2, d };
            p2.NextProcesses = new List<Process> { p1, d };
            p1.OtherProcess = p2;
            p2.OtherProcess = p1;
            p1.State = 1;
            p2.State = 1;
            c.TNext = 0.5;
            p1.QueueLength = 2;
            p2.QueueLength = 2;
            c.NextElementss = new List<Process> { p1, p2 };
            List<Element> list = new List<Element> { c, p1, p2, d};
            Model model = new Model(list);
            model.Simulate(100.0);
            model.ReturnResult();
        }
    }
}
