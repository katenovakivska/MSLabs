using MS4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Hospital
{
    public class HospitalModel
    {
        public List<Element> ElementsList { get; set; }
        public double TCurrent { get; set; }
        public double TNext { get; set; }
        public int Event { get; set; }
        public HospitalModel(List<Element> list)
        {
            ElementsList = list;
            TCurrent = 0.0;
            TNext = 0.0;
        }
      
        public void Simulate(double timeModeling)
        {
            int prev = -1;
            while (TCurrent < timeModeling)
            {
                TNext = Double.MaxValue;
                foreach (var e in ElementsList)
                {
                    if (e.TNext < TNext)
                    {
                        TNext = e.TNext;
                        Event = ElementsList.IndexOf(e);
                    }
                }
                ManageChannels();
                if (!(prev == 0 && Event == 0))
                Console.WriteLine($"It's time for event in {ElementsList[Event].Name} , time = {ElementsList[Event].TNext}");
                foreach (var e in ElementsList)
                {
                    if (e.GetType() == typeof(Doctor))
                    {
                        Doctor d = (Doctor)e;
                        if (d.States.Count > 0)
                        {
                            d.DoStatistics(TNext - TCurrent, d.States[0]);
                        }
                        else
                        {
                            d.DoStatistics(TNext - TCurrent, 0);
                        }
                    }
                    else
                    {
                        e.CountStatistics(TNext - TCurrent);
                    }
                }
                TCurrent = TNext;
                foreach (var e in ElementsList)
                {
                    e.TCurrent = TCurrent;
                }
                //Get(Event).OutAct();
                ElementsList[Event].OutAct();
                foreach (var e in ElementsList)
                {
                    if (e.TNext == TCurrent)
                    {
                        e.OutAct();
                    }
                }
                //PrintInfo();
            }
            PrintResult();
            
        }

        public void ManageChannels()
        {
            List<Channel> channels = new List<Channel>();
            List<Channel> outChannels = new List<Channel>();
            if ((ElementsList[Event].GetType() == typeof(HospitalProcess) || ElementsList[Event] is HospitalProcess == true) && ElementsList[Event].Name != "EXIT")
            {
                HospitalProcess p = (HospitalProcess)ElementsList[Event];
                p.InChannel();
            }
            foreach (var e in ElementsList)
            {
                if ((e.GetType() == typeof(HospitalProcess) || e is HospitalProcess == true) && e.Name != "EXIT" )
                {
                    HospitalProcess p = (HospitalProcess)e;
                    outChannels = p.OutChannel(TNext);
                    foreach (var o in outChannels)
                    {
                        channels.Add(o);
                    }

                }
            }
            channels = channels.OrderBy(x => x.TimeOut).ToList();
            foreach (var c in channels)
            {
                Console.WriteLine();
                Console.WriteLine($"{c.Name} is free now t = {c.TimeOut}");
                Console.WriteLine();
            }
        }
        public void PrintInfo()
        {
            foreach (var e in ElementsList)
            {
                e.PrintInfo();
            }
        }

        public void PrintResult()
        {
            Console.WriteLine("---------------------RESULTS-----------------------");
            int patients = 0;
            double tWaiting = 0;
            double timeBetweenLab = 0;
            List<double> types = new List<double> {0, 0, 0};
            List<int> quantities = new List<int> { 0, 0, 0 };
            foreach (var e in ElementsList)
            {
                patients += e.Quantity;
                //if (e.GetType() == typeof(HospitalCreate))
                //{
                //    patients += e.Quantity;
                //}
                e.PrintResult();
                if (e.GetType() == typeof(HospitalCreate))
                {
                    HospitalCreate c = (HospitalCreate)e;
                    for (int i = 0; i < c.PatientsTypes.Count; i++)
                    {
                        quantities[i] = c.PatientsTypes[i].Quantity;

                    }
                    
                }
                if (e.GetType() == typeof(HospitalProcess))
                {
                    HospitalProcess process = new HospitalProcess();
                    process = (HospitalProcess)e;
                    patients += process.Quantity;
                    //if (process.Name == "LABORATORY" || process.Name == "DOCTOR" || process.Name == "CHAMBER")
                    //{
                    tWaiting += process.WaitingTime;
                    //}
                    foreach (var t in process.Types)
                    {
                        types[t.Index - 1] += t.WaitingTime;
                        //quantities[t.Index - 1] += process.Quantity;
                        quantities[t.Index - 1] += t.Quantity;
                    }
                    double average = process.AverageQueue / process.TCurrent;
                    double workload = process.AverageWorkload / process.TCurrent;
                    Console.WriteLine($"name = {process.Name} max parallel = {process.MaxParallel} quantity = {process.Quantity} averageQ = {average} " +
                        $" workload = {workload}");
                }
                if (e.GetType() == typeof(Doctor))
                {
                    Doctor doctor = (Doctor)e;
                    //patients += doctor.Quantity;
                    tWaiting += doctor.WaitingTime;
                    foreach (var t in doctor.Types)
                    {
                        types[t.Index - 1] += t.WaitingTime;
                        //quantities[t.Index - 1] += doctor.Quantity;
                        quantities[t.Index - 1] += t.Quantity;
                    }
                    double average = doctor.AverageQueue / doctor.TCurrent;
                    double workload = doctor.AverageWorkload / doctor.TCurrent;
                    Console.WriteLine($"name = {doctor.Name} max parallel = {doctor.MaxParallel} quantity = {doctor.Quantity} averageQ = {average} " +
                        $" workload = {workload}");
                    timeBetweenLab = doctor.DelaySum / doctor.ToLabAmount;

                }
            }
            double AverageTime = tWaiting / patients;
            for (int i = 0; i < types.Count; i++)
            {
                types[i] /= quantities[i];
                Console.WriteLine($"Average time in the hospital of type {i + 1} is {types[i]}");
            }
            //Console.WriteLine($"Average time in the hospital is {AverageTime}");
            Console.WriteLine($"Avg trip from doctor to lab duration is {timeBetweenLab}");
        }
    }
}
