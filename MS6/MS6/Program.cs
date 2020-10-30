using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace MS6
{
    class Program
    {
        public static void Main(string[] args)
        {


            //Position position1 = new Position("pos1", 1);
            //Position position2 = new Position("pos2", 0);
            //Transition transition = new Transition("trans", availableTransitions);

            //OutArc arc1 = new OutArc("arcOut1", 1);
            //arc1.NextPosition = position1;

            //OutArc arc2 = new OutArc("arcOut1", 1);
            //arc2.NextPosition = position2;

            //IntoArc arcIn = new IntoArc("arcIn", 1);
            //arcIn.NextTransition = transition;
            //arcIn.PrevPosition = position1;

            //transition.PrevArcs = new IntoArc[] { arcIn };
            //transition.NextArcs = new OutArc[] { arc1, arc2 };

            //position1.NextArcs = new IntoArc[] { arcIn };

            //Model model = new Model(availableTransitions, new Position[] { position1, position2 }, new Transition[] { transition });
            //model.Simulate(10);
            //model.PrintStats(new SimpleVisualization());

            //AvailableTransitions availableTransitions = new AvailableTransitions(new BaseUniform(0, 1, 1));
            Task1();

            Console.ReadKey();
        }

        static void Task1()
        {
            Position p1 = new Position("INCOMING", 1);
            Position p2 = new Position("FIRST IS FREE", 0);
            Position p3 = new Position("SECOND IS FREE", 0);
            //Position p4 = new Position("THIRD IS FREE", 0, "Free condition");
            //Position p5 = new Position("FORTH IS FREE", 0, "Free condition");
            //Position p6 = new Position("FIFTH IS FREE", 0, "Free condition");
            Position p7 = new Position("FIRST IS PROCESSING", 1);
            Position p8 = new Position("FIRST IS PROCESSED", 0);
            Position p9 = new Position("SECOND IS PROCESSING", 1);
            Position p10 = new Position("SECOND IS PROCESSED", 0);
            //Position p11 = new Position("THIRD IS PROCESSING", 1);
            //Position p12 = new Position("THIRD IS PROCESSED", 0);
            //Position p13 = new Position("FORTH IS PROCESSING", 1);
            //Position p14 = new Position("FORTH IS PROCESSED", 0);
            //Position p15 = new Position("FIFTH IS PROCESSING", 1);
            //Position p16 = new Position("FIFTH IS PROCESSED", 0);
            Position p17 = new Position("FINISH", 0);


            Transition t1 = new Transition("GO FIRST", 1);
            Transition t2 = new Transition("GO SECOND", 1);
            //Transition t3 = new Transition("GO THIRD", 1);
            //Transition t4 = new Transition("GO FORTH", 1);
            //Transition t5 = new Transition("GO FIFTH", 1);
            //Transition t6 = new Transition("RETURN TO FIRST", 5);
            Transition t7 = new Transition("PROCESS FIRST", 1);
            Transition t8 = new Transition("PROCESS SECOND", 1);
            //Transition t9 = new Transition("PROCESS THIRD", 1);
            //Transition t10 = new Transition("PROCESS FORTH", 1);
            //Transition t11 = new Transition("PROCESS FIFTH", 1);
            Transition t12 = new Transition("EXIT FIRST", 0);
            Transition t13 = new Transition("EXIT SECOND", 0);
            //Transition t14 = new Transition("EXIT THIRD", 0);
            //Transition t15 = new Transition("EXIT FORTH", 0);
            //Transition t16 = new Transition("EXIT FIFTH", 0);

            Arc arc1 = new Arc("arcIn1", 1, t1, p1);
            Arc arc3 = new Arc("arcOut3", 4, p2);
            Arc arc4 = new Arc("arcIn4", 1, t2, p2);
            Arc arc5 = new Arc("arcOut5", 1, p3);
            //Arc arc6 = new Arc("arcIn6", 1, t3, p3);
            //Arc arc7 = new Arc("arcOut7", 1, p4);
            //Arc arc8 = new Arc("arcIn8", 1, t4, p4);
            //Arc arc9 = new Arc("arcOut9", 1, p5);
            //Arc arc10 = new Arc("arcIn10", 1, t5, p5);
            //Arc arc11 = new Arc("arcOut11", 1, p6);
            //Arc arc12 = new Arc("arcIn12", 1, t6, p6);
            Arc arc13 = new Arc("arcOut13", 1, p2);
            Arc arc14 = new Arc("arcIn14", 1, t7, p2);
            Arc arc40 = new Arc("arcIn40", 1, t8, p3);
            //Arc arc41 = new Arc("arcIn41", 1, t9, p4);
            //Arc arc42 = new Arc("arcIn42", 1, t10, p5);
            //Arc arc43 = new Arc("arcIn43", 1, t11, p6);
            Arc arc15 = new Arc("arcIn15", 1, t7, p7);
            Arc arc20 = new Arc("arcIn20", 1, t8, p9);
            //Arc arc24 = new Arc("arcIn24", 1, t9, p11);
            //Arc arc28 = new Arc("arcIn28", 1, t10, p13);
            //Arc arc32 = new Arc("arcIn32", 1, t11, p15);
            Arc arc16 = new Arc("arcOut16", 1, p7);
            Arc arc19 = new Arc("arcOut19", 1, p9);
            //Arc arc23 = new Arc("arcOut23", 1, p11);
            //Arc arc27 = new Arc("arcOut27", 1, p13);
            //Arc arc31 = new Arc("arcOut31", 1, p15);
            Arc arc17 = new Arc("arcOut17", 1, p8);
            Arc arc21 = new Arc("arcOut21", 1, p10);
            //Arc arc25 = new Arc("arcOut25", 1, p12);
            //Arc arc29 = new Arc("arcOut29", 1, p14);
            //Arc arc33 = new Arc("arcOut33", 1, p16);
            Arc arc18 = new Arc("arcIn18", 1, t12, p8);
            Arc arc22 = new Arc("arcIn22", 1, t13, p10);
            //Arc arc26 = new Arc("arcIn26", 1, t14, p12);
            //Arc arc30 = new Arc("arcIn30", 1, t15, p14);
            //Arc arc34 = new Arc("arcIn34", 1, t16, p16);
            Arc arc35 = new Arc("arcOut35", 1, p17);
            Arc arc36 = new Arc("arcOut36", 1, p17);
            Arc arc37 = new Arc("arcOut37", 1, p17);
            Arc arc38 = new Arc("arcOut38", 1, p17);
            Arc arc39 = new Arc("arcOut39", 1, p17);


            t1.InArcs.Add(arc1);
            t1.OutArcs.Add(arc3);
            t2.InArcs.Add(arc4);
            t2.OutArcs.Add(arc5);
            //t3.InArcs.Add(arc6);
            //t3.OutArcs.Add(arc7);
            //t4.InArcs.Add(arc8);
            //t4.OutArcs.Add(arc9);
            //t5.InArcs.Add(arc10);
            //t5.OutArcs.Add(arc11);
            //t6.InArcs.Add(arc12);
            //t6.OutArcs.Add(arc13);
            t7.InArcs.Add(arc14);
            t8.InArcs.Add(arc40);
            //t9.InArcs.Add(arc41);
            //t10.InArcs.Add(arc42);
            //t11.InArcs.Add(arc43);
            t7.InArcs.Add(arc15);
            t8.InArcs.Add(arc20);
            //t9.InArcs.Add(arc24);
            //t10.InArcs.Add(arc28);
            //t11.InArcs.Add(arc32);
            t7.OutArcs.Add(arc16);
            t8.OutArcs.Add(arc19);
            //t9.OutArcs.Add(arc23);
            //t10.OutArcs.Add(arc27);
            //t11.OutArcs.Add(arc31);
            t7.OutArcs.Add(arc17);
            t8.OutArcs.Add(arc21);
            //t9.OutArcs.Add(arc25);
            //t10.OutArcs.Add(arc29);
            //t11.OutArcs.Add(arc33);
            t12.InArcs.Add(arc18);
            t13.InArcs.Add(arc22);
            //t14.InArcs.Add(arc26);
            //t15.InArcs.Add(arc30);
            //t16.InArcs.Add(arc34);
            t12.OutArcs.Add(arc35);
            t13.OutArcs.Add(arc36);
            //t14.OutArcs.Add(arc37);
            //t15.OutArcs.Add(arc38);
            //t16.OutArcs.Add(arc39);

            p1.NextArcs = new List<Arc>() { arc1 };
            p2.NextArcs = new List<Arc>() { arc4, arc14 };
            p3.NextArcs = new List<Arc>() { arc40/*, arc6 */};
            //p4.NextArcs = new List<Arc>() { arc8, arc41 };
            //p5.NextArcs = new List<Arc>() { arc10, arc42 };
            //p6.NextArcs = new List<Arc>() { arc43 };
            p7.NextArcs = new List<Arc>() { arc15 };
            p8.NextArcs = new List<Arc>() { arc18 };
            p9.NextArcs = new List<Arc>() { arc20 };
            p10.NextArcs = new List<Arc>() { arc22 };
            //p11.NextArcs = new List<Arc>() { arc24 };
            //p12.NextArcs = new List<Arc>() { arc26 };
            //p13.NextArcs = new List<Arc>() { arc28 };
            //p14.NextArcs = new List<Arc>() { arc30 };
            //p15.NextArcs = new List<Arc>() { arc32 };
            //p16.NextArcs = new List<Arc>() { arc16 };



            List<Position> positions = new List<Position>() { p1, p2, p3/*, p4, p5, p6,*/, p7, p8, p9, p10/*, p11, p12, p13, p14, p15, p16, */,p17 };
            List<Transition> transitions = new List<Transition>() { t1, t2, /*t3, t4, t5, t6,*/ t7, t8,/* t9, t10, t11,*/ t12, t13/*, t14, t15, t16*/ };
            //List<Arc> arcs = new List<Arc>() { arc1, /*arc2,*/ arc3, arc4, arc5, arc6, arc7, arc8, arc9, arc10, arc11, arc12, arc13, arc14, arc15, arc16,
            // arc17, arc18, arc19, arc20, arc21, arc22, arc23, arc24, arc25, arc26, arc27, arc28, arc29, arc30, arc31, arc32, arc33, arc34, arc35,
            // arc36, arc37, arc38, arc39, arc40, arc41, arc42, arc43};
            Model model = new Model(positions, transitions);
            model.Simulate(100);
        }
    }
}

