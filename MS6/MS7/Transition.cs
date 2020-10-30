using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MS7
{
    //переход, он же событие 
    public class Transition
    {
        //доступен ли переход
        public bool IsAvailable { get; set; }
        //входящие дуги
        public List<Arc> InArcs { get; set; }
        //выходящие дуги
        public List<Arc> OutArcs { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }

        public Transition(string name)
        {
            Name = name;
            InArcs = new List<Arc>();
            OutArcs = new List<Arc>();
            Count = 0;
            IsAvailable = false;
        }

        public bool CheckConditionsOfTransitions(List<Position> positions)
        {
            List<string> names = InArcs.Select(x => x.PrevPosition.Name).ToList();
            if (positions.Where(x => names.Contains(x.Name) == true).Sum(x => x.MarkersCount) >= InArcs.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Position> TakeMarkers(List<Position> positions)
        {
            Console.Write($"Transition {Name}: ");
            Console.Write("markers take from ");
            foreach (var a in InArcs)
            {
                 positions.Where(x => x.Name == a.PrevPosition.Name).FirstOrDefault().MarkersCount -= 1;

                Console.Write($"{a.PrevPosition.Name} ");
            }

            Console.WriteLine();
            return LaunchTransition(positions);
        }

        public List<Position> LaunchTransition(List<Position> positions)
        {
            Console.Write("to ");
            foreach (var a in OutArcs)
            {
                positions.Where(x => x.Name == a.NextPosition.Name).FirstOrDefault().MarkersCount += 1;
                Console.Write($"{a.NextPosition.Name} ");
            }
            Console.WriteLine();

            return positions;
        }
    }
}
