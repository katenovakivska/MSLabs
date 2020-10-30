using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MS8
{
    class Program
    {
        static void Main(string[] args)
        {
            Task1();
            //Task2();
            //Task3();
        }
        public static void Task1()
        {
            Place p1 = new Place("GENERATED MESSAGE IN A", 1);
            Place p2 = new Place("IS GENERATED IN A", 0);
            Place p3 = new Place("REQUEST A->B IS SENT", 0);
            Place p4 = new Place("SIGNAL B->A IS SENT", 0);
            Place p5 = new Place("MESSAGE A->B IS SENT", 0);
            Place p6 = new Place("B RECEIVED MESSAGE", 0);
            Place p7 = new Place("MESSAGE ABOUT SUCCESS IS SENT BY B", 0);
            Place p8 = new Place("SUCCESSFUL SENDING A", 0);
            Place p9 = new Place("WAITING OF CONFIRMATION IN B", 1);
            Place p10 = new Place("NODE FINISH SENDING", 1);
            Place p11 = new Place("GENERATED MESSAGE IN B", 1);
            Place p12 = new Place("IS GENERATED IN B", 0);
            Place p13 = new Place("REQUEST B->A IS SENT", 0);
            Place p14 = new Place("SIGNAL A->B IS SENT", 0);
            Place p15 = new Place("MESSAGE B->A IS SENT", 0);
            Place p16 = new Place("A RECEIVED MESSAGE", 0);
            Place p17 = new Place("MESSAGE ABOUT SUCCESS IS SENT BY A", 0);
            Place p18 = new Place("SUCCESSFUL SENDING B", 0);
            Place p19 = new Place("WAITING OF CONFIRMATION IN A", 1);

            Transition t1 = new Transition("GENERATE MESSAGE IN A");
            Transition t2 = new Transition("REQUEST A->B");
            Transition t3 = new Transition("SIGNAL B->A");
            Transition t4 = new Transition("SEND MESSAGE A->B");
            Transition t5 = new Transition("B RECEIVED MESSAGE");
            Transition t6 = new Transition("B SEND MESSAGE ABOUT SUCCESS");
            Transition t7 = new Transition("A RECEIVE MESSAGE ABOUT SUCCESS");
            Transition t8 = new Transition("GENERATE MESSAGE IN B");
            Transition t9 = new Transition("REQUEST B->A");
            Transition t10 = new Transition("SIGNAL A->B");
            Transition t11 = new Transition("SEND MESSAGE B->A");
            Transition t12 = new Transition("A RECEIVED MESSAGE");
            Transition t13 = new Transition("A SEND MESSAGE ABOUT SUCCESS");
            Transition t14 = new Transition("B RECEIVE MESSAGE ABOUT SUCCESS");

            Arc a0 = new Arc("arc1", 1, p1);
            Arc a1 = new Arc("arc1", 1, t1, p1);
            Arc a2 = new Arc("arc2", 1, p2);
            Arc a3 = new Arc("arc3", 1, t2, p2);
            Arc a4 = new Arc("arc4", 1, p3);
            Arc a5 = new Arc("arc5", 1, t3, p3);
            Arc a6 = new Arc("arc6", 1, p4);
            Arc a7 = new Arc("arc7", 1, t4, p4);
            Arc a8 = new Arc("arc8", 1, p5);
            Arc a9 = new Arc("arc9", 1, t5, p5);
            Arc a10 = new Arc("arc10", 1, p6);
            Arc a11 = new Arc("arc11", 1, t6, p6);
            Arc a12 = new Arc("arc12", 1, p7);
            Arc a13 = new Arc("arc13", 1, t7, p7);
            Arc a14 = new Arc("arc14", 1, p8);
            Arc a15 = new Arc("arc15", 1, p9);
            Arc a16 = new Arc("arc16", 1, t7, p9);
            Arc a17 = new Arc("arc17", 1, p10);
            Arc a18 = new Arc("arc18", 1, t7, p10);
            Arc a19 = new Arc("arc19", 1, p11);
            Arc a20 = new Arc("arc20", 1, t8, p11);
            Arc a21 = new Arc("arc21", 1, p12);
            Arc a22 = new Arc("arc22", 1, t9, p12);
            Arc a23 = new Arc("arc23", 1, p13);
            Arc a24 = new Arc("arc24", 1, t10, p13);
            Arc a25 = new Arc("arc25", 1, p14);
            Arc a26 = new Arc("arc26", 1, t11, p14);
            Arc a27 = new Arc("arc27", 1, p15);
            Arc a28 = new Arc("arc28", 1, t12, p15);
            Arc a29 = new Arc("arc29", 1, p16);
            Arc a30 = new Arc("arc30", 1, t13, p16);
            Arc a31 = new Arc("arc31", 1, p17);
            Arc a32 = new Arc("arc32", 1, t14, p17);
            Arc a33 = new Arc("arc33", 1, p18);
            Arc a34 = new Arc("arc34", 1, p19);
            Arc a35 = new Arc("arc35", 1, t11, p19);
            Arc a36 = new Arc("arc36", 1, p10);
            Arc a37 = new Arc("arc37", 1, t11, p10);

            t1.InArcs.Add(a1);
            t1.OutArcs.Add(a2);
            t1.OutArcs.Add(a0);
            t2.InArcs.Add(a3);
            t2.OutArcs.Add(a4);
            t3.InArcs.Add(a5);
            t3.OutArcs.Add(a6);
            t4.InArcs.Add(a7);
            t4.InArcs.Add(a16);
            t4.InArcs.Add(a18);
            t4.OutArcs.Add(a8);
            t5.InArcs.Add(a9);
            t5.OutArcs.Add(a10);
            t6.InArcs.Add(a11);
            t6.OutArcs.Add(a12);
            t7.InArcs.Add(a13);
            t7.OutArcs.Add(a14);
            t7.OutArcs.Add(a17);
            t7.OutArcs.Add(a15);
            t8.InArcs.Add(a20);
            t8.OutArcs.Add(a19);
            t8.OutArcs.Add(a21);
            t9.InArcs.Add(a22);
            t9.OutArcs.Add(a23);
            t10.InArcs.Add(a24);
            t10.OutArcs.Add(a25);
            t11.InArcs.Add(a26);
            t11.OutArcs.Add(a27);
            t11.InArcs.Add(a35);
            t11.InArcs.Add(a37);
            t12.InArcs.Add(a28);
            t12.OutArcs.Add(a29);
            t13.InArcs.Add(a30);
            t13.OutArcs.Add(a31);
            t14.InArcs.Add(a32);
            t14.OutArcs.Add(a33);
            t14.OutArcs.Add(a36);
            t14.OutArcs.Add(a34);


            List<Place> positions = new List<Place>() { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19 };
            List<Transition> transitions = new List<Transition>() { t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14 };

            Model model = new Model(positions, transitions);
            model.Simulate(1000);
        }
        public static void Task2()
        {
            int k = 6;
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
            model.Simulate(100);
        }

        public static void Task3()
        {
            int n = 6;
            Place p1 = new Place("INCOMING FIRST TYPE", 1);
            Place p2 = new Place("CAPTURE OCCURED IN FIRST", 0);
            Place p3 = new Place("INCOMING SECOND TYPE", 1);
            Place p4 = new Place("CAPTURE OCCURED IN SECOND", 0);
            Place p5 = new Place("INCOMING THIRD TYPE", 1);
            Place p6 = new Place("CAPTURE OCCURED IN THIRD", 0);
            Place p7 = new Place("AMOUNT OF PROCESSORS", n);
            Place p8 = new Place("AMOUNT OF PROCESSED FIRST TYPE TASKS", 0);
            Place p9 = new Place("AMOUNT OF PROCESSED SECOND TYPE TASKS", 0);
            Place p10 = new Place("AMOUNT OF PROCESSED THIRD TYPE TASKS", 0);

            Transition t1 = new Transition("CAPTURE PROCESSORS FIRST TYPE");
            Transition t2 = new Transition("FREE PROCESSORS FIRST TYPE");
            Transition t3 = new Transition("CAPTURE PROCESSORS SECOND TYPE");
            Transition t4 = new Transition("FREE PROCESSORS SECOND TYPE");
            Transition t5 = new Transition("CAPTURE PROCESSORS THIRD TYPE");
            Transition t6 = new Transition("FREE PROCESSORS THIRD TYPE");

            Arc a0 = new Arc("arc0", 1, p1);
            Arc a1 = new Arc("arc1", 1, t1, p1);
            Arc a2 = new Arc("arc2", 1, p3);
            Arc a3 = new Arc("arc3", 1, t3, p3);
            Arc a4 = new Arc("arc4", 1, p5);
            Arc a5 = new Arc("arc5", 1, t5, p5);
            Arc a6 = new Arc("arc6", 1, p2);
            Arc a7 = new Arc("arc7", 1, t2, p2);
            Arc a8 = new Arc("arc8", n, t1, p7);
            Arc a9 = new Arc("arc9", n, p7);
            Arc a10 = new Arc("arc10", 1, p4);
            Arc a11 = new Arc("arc11", 1, t4, p4);
            Arc a12 = new Arc("arc12", n / 3, t3, p7);
            Arc a13 = new Arc("arc13", n / 3, p7);
            Arc a14 = new Arc("arc14", 1, p6);
            Arc a15 = new Arc("arc15", 1, t6, p6);
            Arc a16 = new Arc("arc16", n / 2, p7);
            Arc a17 = new Arc("arc17", n / 2, t5, p7);
            Arc a18 = new Arc("arc18", 1, p8);
            Arc a19 = new Arc("arc19", 1, p9);
            Arc a20 = new Arc("arc20", 1, p10);

            t1.OutArcs.Add(a0);
            t1.InArcs.Add(a1);
            t1.OutArcs.Add(a6);
            t1.InArcs.Add(a8);
            t2.InArcs.Add(a7);
            t2.OutArcs.Add(a9);
            t3.OutArcs.Add(a2);
            t3.InArcs.Add(a3);
            t3.OutArcs.Add(a10);
            t3.InArcs.Add(a12);
            t4.InArcs.Add(a11);
            t4.OutArcs.Add(a13);
            t5.OutArcs.Add(a4);
            t5.InArcs.Add(a5);
            t5.OutArcs.Add(a14);
            t5.InArcs.Add(a17);
            t6.InArcs.Add(a15);
            t6.OutArcs.Add(a16);
            t2.OutArcs.Add(a18);
            t4.OutArcs.Add(a19);
            t6.OutArcs.Add(a20);

            List<Place> positions = new List<Place>() { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10 };
            List<Transition> transitions = new List<Transition>() { t1, t2, t3, t4, t5, t6 };

            Model model = new Model(positions, transitions);
            model.Simulate(100);
            int AllProcessed = p8.MarkersCount + p9.MarkersCount + p10.MarkersCount;
            Console.WriteLine($"Processed amount: {AllProcessed}");
            Console.WriteLine($"Ratio of one to all amount: {((double)p8.MarkersCount / AllProcessed) * 100} %");
            Console.WriteLine($"Ratio of second to all amount: {((double)p9.MarkersCount / AllProcessed) * 100} %");
            Console.WriteLine($"Ratio of third to all amount: {((double)p8.MarkersCount / AllProcessed) * 100} %");
        }
    }
}
