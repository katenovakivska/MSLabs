using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital
{
    public class Doctor : HospitalProcess
    {
        public int ToLabAmount { get; set; }
        public List<PatientType> DTypes { get; set; }
        public int DPatientType { get; set; }
        public Doctor(double delay, double startDelay, string name, string distribution, int maxParallel) : base(delay, startDelay, name, distribution, maxParallel)
        {
            ToLabAmount = 0;
            DTypes = new List<PatientType>();
            DTypes = new List<PatientType>{
                new PatientType(1, 0.5, 1/15),
                new PatientType(2, 0.1, 1/40),
                new PatientType(3, 0.4, 1/30)
            };
        }

        public override void OutAct()
        {
            Quantity += 1;
            TNext = Double.MaxValue;
            int patientType = 0;
            if (States.Count > 0)
            {
                for (int i = 0; i < States.Count; i++)
                {
                    if (States[i] == 1)
                    {
                        patientType = States[i];
                        States.RemoveAt(i);
                        break;
                    }
                }
                if (patientType == 0)
                {
                    patientType = States.First();
                    States.RemoveAt(0);
                }
            }
            if (Queue.Count != 0)
            {
                patientType = Queue[0];
                Queue.RemoveAt(0);
                States.Add(patientType);
            }
            
                DPatientType = patientType;
            if (patientType != 0)
                DTypes[patientType - 1].Quantity++;
            if (patientType != 0)
            {
                HospitalProcess nextProcess;
                if (patientType == 1)
                {
                    nextProcess = NextProcesses[0];
                }
                else
                {
                    nextProcess = NextProcesses[1];
                    ToLabAmount += 1;
                }
                nextProcess.InAct(patientType);
                TNext = TCurrent + GetDelay();
                Console.WriteLine($"IN FUTURE from {Name} to {nextProcess.Name} t = {nextProcess.TNext}");
            }
        }

        public void DoStatistics(double delta, int State)
        {
            AverageQueue += Queue.Count * delta;
            AverageProcessingTime += delta;
            AverageWorkload += delta * States.Count;
            if (DPatientType != 0)
            Types[DPatientType - 1].WaitingTime += (Queue.Count + States.Count) * delta;
            WaitingTime += (Queue.Count + States.Count) * delta;

            InWaiting += Queue.Count + States.Count;
            if (Queue.Count > MaxQueueObserved)
            {
                MaxQueueObserved = Queue.Count;
            }
            if (MaxWorkload < States.Count)
            {
                MaxWorkload = States.Count;
            }
            if (State != 0 && (State == 2 || State == 3))
            {
                DelaySum += delta;
            }
        }
    }
}
