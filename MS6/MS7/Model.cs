using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MS7
{
    class Model
    {
        //все позиции
        public List<Position> Positions { get; set; }
        //все переходы
        public List<Transition> Transitions { get; set; }
        public int NextTransition { get; set; }
        public int Count { get; set; }
        public Model(List<Position> positions, List<Transition> transitions)
        {
            Positions = new List<Position>();
            Transitions = new List<Transition>();
            Positions = positions;
            Transitions = transitions;
        }

        //симитировать шаги
        public void Simulate(int modelingSteps)
        {
            SetProbabilities();
            int prev = -1;

            List<int> available = new List<int>();
            int previous = 4;
            for (int i = 0; i < modelingSteps; i++)
            {
                if (Math.Abs(previous - i) >= 4)
                {
                    Positions[0].MarkersCount = 1;
                    NextTransition = 0;
                    previous = i;
                }
                else 
                {
                    foreach (var t in Transitions)
                    {
                        if (t.CheckConditionsOfTransitions(Positions) && available.Contains(Transitions.IndexOf(t)) == true)
                        {
                            NextTransition = Transitions.IndexOf(t);
                        }
                    }
                }
                available.Clear();
                if (!(prev == NextTransition))
                {
                    Console.WriteLine();
                }

                foreach (var p in Positions)
                {
                    p.CountStatistics();
                }

                if (Transitions[NextTransition].CheckConditionsOfTransitions(Positions) && !(prev == NextTransition))
                {
                    List<Position> pos = Transitions[NextTransition].OutArcs.Select(x => x.NextPosition).ToList();
                    Positions = Transitions[NextTransition].TakeMarkers(Positions);
                    available = FindNextTransition(pos);
                }
                prev = NextTransition;
                Count++;
            }
            PrintStatistics();
        }
        public void PrintStatistics()
        {
            Console.WriteLine("--------------------------Markers statistics--------------------------");
            foreach (var p in Positions)
            {
                p.AverageMarkersCount /= p.Count;
                Console.WriteLine($"{p.Name}:".PadRight(25) + $"start markers amount = {p.StartMarkersCount}   " +
                    $" max markers amount = {p.MaxMarkersCount}    min markers amount = {p.MinMarkersCount} " +
                    $"   avg markers amount = {p.AverageMarkersCount}");
                Console.WriteLine();
            }
        }
        public void SetProbabilities()
        {
            foreach (var p in Positions)
            {
                if (p.NextArcs.Count > 0)
                {
                    double probability = (double)1 / p.NextArcs.Count;
                    foreach (var a in p.NextArcs)
                    {
                        a.Probability = probability;
                    }
                }
            }
        }
        public List<int> FindNextTransition(List<Position> positions)
        {
            List<int> available = new List<int>();
            foreach (var p in positions)
            {
                int indexOfArc = GetNextElement(p.NextArcs);
                if (p.NextArcs.Count > 0)
                {
                    if (p.NextArcs[indexOfArc].NextTransition != null)
                    {
                        available.Add(Transitions.IndexOf(p.NextArcs[indexOfArc].NextTransition));
                    }
                }
            }
            return available;
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
