using MS8;
using System;
using System.Collections.Generic;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            int k = 10;
            Place p1 = new Place("TASK IN PRODUCER", 1);
            Place p2 = new Place("TASK PROCESSING", 0);
            Place p3 = new Place("PROCESSED TASKS", 0);
            Place p4 = new Place("FREE PLACES IN BUFFER", k);

            Transition t1 = new Transition("CREATE TASK");
            Transition t2 = new Transition("PROCESS TASK");

            Arc a0 = new Arc("arc0", 1, p1);
            Arc a1 = new Arc("arc1", 1, t1, p1);
            Arc a2 = new Arc("arc2", 1, p2);
            Arc a3 = new Arc("arc3", 1, t2, p2);
            Arc a4 = new Arc("arc4", 1, p3);
            Arc a5 = new Arc("arc5", 1, p4);
            Arc a6 = new Arc("arc6", 1, t1, p4);

            t1.InArcs.Add(a1);
            t1.InArcs.Add(a6);
            t1.OutArcs.Add(a0);
            t1.OutArcs.Add(a2);
            t2.InArcs.Add(a3);
            t2.OutArcs.Add(a4);
            t2.OutArcs.Add(a5);

            List<Place> positions = new List<Place>() { p1, p2, p3, p4 };
            List<Transition> transitions = new List<Transition>() { t1, t2 };

            Model model = new Model(positions, transitions);
            model.Simulate(30);
        }
    }
}
