using System;
using System.Collections.Generic;

namespace MS7
{
    class Program
    {
        static void Main(string[] args)
        {
            Position p1 = new Position("INCOMING", 1);
            Position p2 = new Position("FIRST IS FREE", 0);
            Position p3 = new Position("SECOND IS FREE", 0);
            Position p4 = new Position("FIRST IS PROCESSING", 1);
            Position p5 = new Position("FIRST IS PROCESSED", 0);
            Position p6 = new Position("SECOND IS PROCESSING", 1);
            Position p7 = new Position("SECOND IS PROCESSED", 0);
            Position p8 = new Position("FINISH", 0);


            Transition t1 = new Transition("GO FIRST");
            Transition t2 = new Transition("GO SECOND");
            Transition t3 = new Transition("PROCESS FIRST");
            Transition t4 = new Transition("PROCESS SECOND");
            Transition t5 = new Transition("EXIT FIRST");
            Transition t6 = new Transition("EXIT SECOND");

            Arc arc1 = new Arc("arcIn1", 1, t1, p1);
            Arc arc2 = new Arc("arcOut2", 4, p2);
            Arc arc3 = new Arc("arcIn3", 1, t2, p2);
            Arc arc4 = new Arc("arcOut4", 1, p3);
            Arc arc5 = new Arc("arcIn5", 1, t3, p2);
            Arc arc6 = new Arc("arcIn6", 1, t4, p3);
            Arc arc7 = new Arc("arcIn7", 1, t3, p4);
            Arc arc8 = new Arc("arcIn8", 1, t4, p6);
            Arc arc9 = new Arc("arcOut9", 1, p4);
            Arc arc10 = new Arc("arcOut10", 1, p6);
            Arc arc11 = new Arc("arcOut11", 1, p5);
            Arc arc12 = new Arc("arcOut12", 1, p7);
            Arc arc13 = new Arc("arcIn13", 1, t5, p5);
            Arc arc14 = new Arc("arcIn14", 1, t6, p7);
            Arc arc15 = new Arc("arcOut15", 1, p8);
            Arc arc16 = new Arc("arcOut16", 1, p8);


            t1.InArcs.Add(arc1);
            t1.OutArcs.Add(arc2);
            t2.InArcs.Add(arc3);
            t2.OutArcs.Add(arc4);
            t3.InArcs.Add(arc5);
            t4.InArcs.Add(arc6);
            t3.InArcs.Add(arc7);
            t4.InArcs.Add(arc8);
            t3.OutArcs.Add(arc9);
            t4.OutArcs.Add(arc10);
            t3.OutArcs.Add(arc11);
            t4.OutArcs.Add(arc12);
            t5.InArcs.Add(arc13);
            t6.InArcs.Add(arc14);
            t5.OutArcs.Add(arc15);
            t6.OutArcs.Add(arc16);

            p1.NextArcs = new List<Arc>() { arc1 };
            p2.NextArcs = new List<Arc>() { arc3, arc5 };
            p3.NextArcs = new List<Arc>() { arc6 };
            p4.NextArcs = new List<Arc>() { arc7 };
            p5.NextArcs = new List<Arc>() { arc13 };
            p6.NextArcs = new List<Arc>() { arc8 };
            p7.NextArcs = new List<Arc>() { arc14 };

            List<Position> positions = new List<Position>() { p1, p2, p3, p4, p5, p6, p7, p8 };
            List<Transition> transitions = new List<Transition>() { t1, t2, t3, t4, t5, t6 };

            Model model = new Model(positions, transitions);
            model.Simulate(1000);
            //int[,] verification = new int[,] {
            //    { 2, 1, 1, 1, 0, 1, 0, 0 },
            //    { 1, 2, 0, 2, 0, 1, 2, 0 },
            //    { 1, 2, 2, 3, 0, 1, 0, 0 },
            //    { 2, 0, 10, 1, 0, 1, 0, 0 },
            //    { 10, 0, 1, 1, 0, 1, 0, 0 },
            //};
            //for (int i = 0; i < 5; i++)
            //{
            //    Console.WriteLine($"----------------------------------Statistics for verification case {i + 1}----------------------------------");
            //    Position p1 = new Position("INCOMING", verification[i, 0]);
            //    Position p2 = new Position("FIRST IS FREE", verification[i, 1]);
            //    Position p3 = new Position("SECOND IS FREE", verification[i, 2]);
            //    Position p4 = new Position("FIRST IS PROCESSING", verification[i, 3]);
            //    Position p5 = new Position("FIRST IS PROCESSED", verification[i, 4]);
            //    Position p6 = new Position("SECOND IS PROCESSING", verification[i, 5]);
            //    Position p7 = new Position("SECOND IS PROCESSED", verification[i, 6]);
            //    Position p8 = new Position("FINISH", verification[i, 7]);


            //    Transition t1 = new Transition("GO FIRST");
            //    Transition t2 = new Transition("GO SECOND");
            //    Transition t3 = new Transition("PROCESS FIRST");
            //    Transition t4 = new Transition("PROCESS SECOND");
            //    Transition t5 = new Transition("EXIT FIRST");
            //    Transition t6 = new Transition("EXIT SECOND");

            //    Arc arc1 = new Arc("arcIn1", 1, t1, p1);
            //    Arc arc2 = new Arc("arcOut2", 4, p2);
            //    Arc arc3 = new Arc("arcIn3", 1, t2, p2);
            //    Arc arc4 = new Arc("arcOut4", 1, p3);
            //    Arc arc5 = new Arc("arcIn5", 1, t3, p2);
            //    Arc arc6 = new Arc("arcIn6", 1, t4, p3);
            //    Arc arc7 = new Arc("arcIn7", 1, t3, p4);
            //    Arc arc8 = new Arc("arcIn8", 1, t4, p6);
            //    Arc arc9 = new Arc("arcOut9", 1, p4);
            //    Arc arc10 = new Arc("arcOut10", 1, p6);
            //    Arc arc11 = new Arc("arcOut11", 1, p5);
            //    Arc arc12 = new Arc("arcOut12", 1, p7);
            //    Arc arc13 = new Arc("arcIn13", 1, t5, p5);
            //    Arc arc14 = new Arc("arcIn14", 1, t6, p7);
            //    Arc arc15 = new Arc("arcOut15", 1, p8);
            //    Arc arc16 = new Arc("arcOut16", 1, p8);


            //    t1.InArcs.Add(arc1);
            //    t1.OutArcs.Add(arc2);
            //    t2.InArcs.Add(arc3);
            //    t2.OutArcs.Add(arc4);
            //    t3.InArcs.Add(arc5);
            //    t4.InArcs.Add(arc6);
            //    t3.InArcs.Add(arc7);
            //    t4.InArcs.Add(arc8);
            //    t3.OutArcs.Add(arc9);
            //    t4.OutArcs.Add(arc10);
            //    t3.OutArcs.Add(arc11);
            //    t4.OutArcs.Add(arc12);
            //    t5.InArcs.Add(arc13);
            //    t6.InArcs.Add(arc14);
            //    t5.OutArcs.Add(arc15);
            //    t6.OutArcs.Add(arc16);

            //    p1.NextArcs = new List<Arc>() { arc1 };
            //    p2.NextArcs = new List<Arc>() { arc3, arc5 };
            //    p3.NextArcs = new List<Arc>() { arc6 };
            //    p4.NextArcs = new List<Arc>() { arc7 };
            //    p5.NextArcs = new List<Arc>() { arc13 };
            //    p6.NextArcs = new List<Arc>() { arc8 };
            //    p7.NextArcs = new List<Arc>() { arc14 };

            //    List<Position> positions = new List<Position>() { p1, p2, p3, p4, p5, p6, p7, p8 };
            //    List<Transition> transitions = new List<Transition>() { t1, t2, t3, t4, t5, t6 };

            //    Model model = new Model(positions, transitions);
            //    model.Simulate(1000);
            //}
        }
    }
}
