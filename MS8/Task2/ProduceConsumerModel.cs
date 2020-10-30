using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MS8;
namespace Task2
{
    class ProduceConsumerModel
    {
        //все позиции
        public List<Place> Places { get; set; }
        //все переходы
        public List<Transition> Transitions { get; set; }
        public List<Transition> NextTransitions { get; set; }

        Random Random = new Random();
        public ProduceConsumerModel(List<Place> positions, List<Transition> transitions)
        {
            Places = new List<Place>();
            Transitions = new List<Transition>();
            Places = positions;
            Transitions = transitions;
            NextTransitions = new List<Transition>();
        }

        //симитировать шаги
        public void Simulate(int modelingSteps)
        {
            int counter = 0;
            while (counter < modelingSteps)
            {
                Console.WriteLine();
                Console.WriteLine($"---------------------------iteration {counter + 1}-------------------------");

                foreach (var t in Transitions)
                {
                    if (t.CheckConditionsOfTransition(Places))
                    {
                        NextTransitions.Add(t);
                    }
                }
                foreach (var t in NextTransitions)
                {
                    t.Probability = (double)1 / NextTransitions.Count();
                }

                LaunchTransitions();

                NextTransitions.Clear();

                foreach (var p in Places)
                {
                    p.CountStatistics();
                }

                counter++;
            }
            PrintStatistics();
        }

        public void LaunchTransitions()
        {
            double r = Random.NextDouble();
            for (int i = 0; i < NextTransitions.Count; i++)
            {
                if (r < NextTransitions[i].Probability)
                {
                    Places = NextTransitions[i].TakeMarkers(Places);
                    break;
                }
                else
                    r -= NextTransitions[i].Probability;
            }
        }

        public void PrintStatistics()
        {
            Console.WriteLine("--------------------------Markers statistics--------------------------");
            foreach (var p in Places)
            {
                p.AverageMarkersCount /= p.Count;
                Console.WriteLine($"{p.Name}:".PadRight(25) + $"start markers amount = {p.StartMarkersCount}   " +
                    $" max markers amount = {p.MaxMarkersCount}    min markers amount = {p.MinMarkersCount} " +
                    $"   avg markers amount = {p.AverageMarkersCount}");
                Console.WriteLine();
            }
        }

    }
}
