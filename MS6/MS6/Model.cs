using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace MS6
{
    class Model
    {
        //все позиции
        public List<Position> Positions { get; set; }
        //все переходы
        public List<Transition> Transitions { get; set; }
        public double TCurrent { get; set; }
        public double TNext { get; set; }
        public int NextTransition { get; set; }
        //public int Boundary1 { get; set; }
        //public int Boundary2 { get; set; }
        public double AverageAmountOfBusy { get; set; }
        public int Count { get; set; }
        public Model(List<Position> positions, List<Transition> transitions/*, int boundary1, int boundary2*/)
        {
            Positions = new List<Position>();
            Transitions = new List<Transition>();
            Positions = positions;
            Transitions = transitions;
            TCurrent = 0;
            TNext = 0;
            //Boundary1 = boundary1;
            //Boundary2 = boundary2;
            AverageAmountOfBusy = 0;
        }

        //симитировать шаги
        public void Simulate(double timeModeling)
        {
            if (!IsStartCondition())
            {
                throw new Exception("Start condition is not performed");
            }
            SetProbabilities();
            int prev = -1;
            //int transition = 0;
            while (TCurrent < timeModeling)
            {
                TNext = Double.MaxValue;

                foreach (var t in Transitions)
                {
                    if (t.TNext < TNext && t.CheckConditionsOfTransitions(Positions))
                    {
                        TNext = t.TNext;
                        NextTransition = Transitions.IndexOf(t);
                    }
                }
                if (!(prev == 0 && NextTransition == 0))
                {
                    Console.WriteLine($"Transition: {Transitions[NextTransition].Name} time = {TNext}");
                    Console.WriteLine();
                }

                foreach (var t in Transitions)
                {
                    t.CountStatistics(TNext - TCurrent, Positions);
                }
                TCurrent = TNext;
                foreach (var t in Transitions)
                {
                    t.TCurrent = TCurrent;
                }
                //CountBusy();
                if (Transitions[NextTransition].CheckConditionsOfTransitions(Positions))
                {
                    List<Position> pos = Transitions[NextTransition].OutArcs.Select(x => x.NextPosition).ToList();
                    Positions = Transitions[NextTransition].TakeMarkers(Positions);
                    FindTNext(pos);
                }
                prev = NextTransition;
                Count++;
            }
            //PrintStatistics();
        }

        //public void PrintStatistics()
        //{
        //    double count = 0, TimeInSystem = 0;
        //    foreach (var t in Transitions)
        //    {
        //        TimeInSystem += t.AverageTimeInSystem;
        //        if (Transitions.IndexOf(t) >= Boundary1 && Transitions.IndexOf(t) >= Boundary1)
        //        {
        //            count += t.Count;
        //        }
        //    }

        //    AverageAmountOfBusy /= Count;
        //    Console.WriteLine($"Average time of detail: {TimeInSystem / Transitions.Last().Count}");
        //    Console.WriteLine($"Average amount of busy devices: {AverageAmountOfBusy}");
        //    Console.WriteLine("Workloads of devices:");
        //    int k = 0;
        //    foreach (var p in Positions.Where(p => p.Type == "Free condition"))
        //    {
        //        k++;
        //        Console.WriteLine($"Average workload of device {k}: {p.AmountOfPerformance / p.TimeOfPerformance}");
        //    }
        //}
        //public void CountBusy()
        //{
        //    AverageAmountOfBusy += Positions.Where(p => p.Type == "Free condition" && p.MarkersCount == 0).Count();
        //}
        public void SetProbabilities()
        {
            foreach (var p in Positions)
            {
                if(p.NextArcs.Count > 0)
                {
                    double probability = (double) 1 / p.NextArcs.Count;
                    foreach (var a in p.NextArcs)
                    {
                        a.Probability = probability;
                    }
                }
            }
        }
        public bool IsStartCondition()
        {
            if (Transitions[0].InArcs.Count == Transitions[0].InArcs.Where(x => x.PrevPosition.MarkersCount > 0).Count())
            {
                Transitions[0].TNext = 0;
                Transitions[0].IncomingFrequency();
                return true;
            }
            else
            {
                return false;
            }
        }

        public int FindNext(int transition)
        {
            foreach (var t in Transitions)
            {
                if (t.CheckConditionsOfTransitions(Positions))
                {
                    transition = Transitions.IndexOf(t);
                    return transition;
                }
                else
                {
                    transition = -1;
                }
            }
            return transition;
        }

        public void FindTNext(List<Position> positions)
        {
            foreach (var p in positions)
            {
               int indexOfArc = GetNextElement(p.NextArcs);
                //foreach (var a in p.NextArcs)
                //{
                //    if (a.NextTransition != null)
                //    {
                //        int index = Transitions.IndexOf(a.NextTransition);
                //        if (Transitions[index].CheckConditionsOfTransitions(Positions))
                //        {
                //            Transitions[index].FindTNext();
                //        }
                //    }
                //}
                if (p.NextArcs.Count > 0)
                {
                    if (p.NextArcs[indexOfArc].NextTransition != null)
                    {
                        int index = Transitions.IndexOf(p.NextArcs[indexOfArc].NextTransition);
                        if (Transitions[index].CheckConditionsOfTransitions(Positions))
                        {
                            Transitions[index].FindTNext();
                        }
                    }
                }
            }
        }
        private int GetNextElement(List<Arc> arcs)
        {
            Random random = new Random();
            double rand = random.NextDouble();
            int index = 0;

            for (int i = 0; i < arcs.Count; i++)
            {
                if (rand < arcs[i].Probability)
                {
                    index = i;
                    break;
                }
                else
                {
                    rand -= arcs[i].Probability;
                }
            }
            return index;
        }
    }

}
