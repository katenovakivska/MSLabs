using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital
{
    public class Laboratory: HospitalProcess
    {
        public Laboratory(double delay, double startDelay, string name, string distribution, int maxParallel) : base(delay, startDelay, name, distribution, maxParallel)
        {
        }
        public override void OutAct()
        {
            Quantity += 1;
            TNext = Double.MaxValue;
            int patientType = 0;
            if (States.Count != 0)
            {
                patientType = States[0];
                States.RemoveAt(0);
            }
            if (Queue.Count != 0)
            {
                patientType = Queue[0];
                Queue.RemoveAt(0);
                States.Add(patientType);
            }
            if(patientType != 0)
            {
                HospitalProcess nextProcess;
                if (patientType == 2)
                {
                    nextProcess = NextProcesses[0];
                    nextProcess.InAct(patientType);
                    Console.WriteLine($"IN FUTURE from {Name} to {nextProcess.Name} t = {nextProcess.TNext}");
                }
                else if (patientType == 3)
                {
                    nextProcess = NextProcesses[1];
                    nextProcess.InAct(patientType);
                    Console.WriteLine($"IN FUTURE from {Name} to {nextProcess.Name} t = {nextProcess.TNext}");
                }
                TNext = TCurrent + GetDelay();
            }
        }

    }
}
