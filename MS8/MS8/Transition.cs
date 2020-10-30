using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MS8
{
    //переход, он же событие 
    public class Transition
    {
        //входящие дуги
        public List<Arc> InArcs { get; set; }
        //выходящие дуги
        public List<Arc> OutArcs { get; set; }
        public string Name { get; set; }
        public double Probability { get; set; }

        public Transition(string name)
        {
            Name = name;
            InArcs = new List<Arc>();
            OutArcs = new List<Arc>();
        }

        public List<Place> TakeMarkers(List<Place> positions)
        {
            Console.WriteLine($"Transition {Name}: ");
            Console.Write("markers take from ");
            foreach (var a in InArcs)
            {
                Console.Write($"{a.PrevPosition.Name} - {positions.Where(x => x.Name == a.PrevPosition.Name).FirstOrDefault().MarkersCount} ");
                positions.Where(x => x.Name == a.PrevPosition.Name).FirstOrDefault().MarkersCount -= a.Multiplicity;
            }
            Console.WriteLine();
            return GiveMarkers(positions);
        }

        public List<Place> GiveMarkers(List<Place> places)
        {
            Console.Write("to ");
            foreach (var a in OutArcs)
            {
                Console.Write($"{a.NextPosition.Name} - {places.Where(x => x.Name == a.NextPosition.Name).FirstOrDefault().MarkersCount} ");
                places.Where(x => x.Name == a.NextPosition.Name).FirstOrDefault().MarkersCount += a.Multiplicity;
            }
            Console.WriteLine();

            return places;
        }

        public bool CheckConditionsOfTransition(List<Place> places)
        {
            bool flag = true;

            List<string> placesNames = InArcs.Select(x => x.PrevPosition.Name).ToList();
            List<Place> placesForCheck = places.Where(x => placesNames.Contains(x.Name)).ToList();

            for (int i = 0; i < placesForCheck.Count; i++)
            {
                if (placesForCheck[i].MarkersCount < InArcs[i].Multiplicity)
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }
    }
}
