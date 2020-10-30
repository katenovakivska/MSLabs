using System;

namespace RobotsSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            RobotsPosition p1 = new RobotsPosition("INCOMING", 1);
            RobotsPosition p2 = new RobotsPosition("FIRST ROBOT IS FREE", 0);
            RobotsPosition p3 = new RobotsPosition("FIRST MACHINE IS FREE", 0);
            RobotsPosition p4 = new RobotsPosition("FIRST MACHINE PROCESSING", 0);
            RobotsPosition p5 = new RobotsPosition("FIRST MACHINE FINISHED PROCESSING", 0);
            RobotsPosition p6 = new RobotsPosition("SECOND ROBOT IS FREE", 0);
            RobotsPosition p7 = new RobotsPosition("SECOND MACHINE IS FREE", 0);
            RobotsPosition p8 = new RobotsPosition("SECOND MACHINE PROCESSING", 0);
            RobotsPosition p9 = new RobotsPosition("SECOND MACHINE FINISHED PROCESSING", 0);
            RobotsPosition p10 = new RobotsPosition("THIRD ROBOT IS FREE", 0);
            RobotsPosition p11 = new RobotsPosition("CONDITION FOR MOVING", 0);
            RobotsPosition p12 = new RobotsPosition("AMOUNT OF DETAILS", 0);
            RobotsPosition p13 = new RobotsPosition("AMOUNT OF FREE PLACES IN FIRST MACHINE", 3, "Amount of places");
            RobotsPosition p14 = new RobotsPosition("MOUNT OF FREE PLACES IN SECOND MACHINE", 3, "Amount of places");
            RobotsPosition p15 = new RobotsPosition("STOP OF FIRST ROBOT", 1, "Free robot");
            RobotsPosition p16 = new RobotsPosition("STOP OF SECOND ROBOT", 1, "Free robot");
            RobotsPosition p17 = new RobotsPosition("STOP OF THIRD ROBOT", 1, "Free robot");
            RobotsPosition p18 = new RobotsPosition("FIRST ROBOT IS FREE", 0);

            RobotsTransition t1 = new RobotsTransition("INCOMING", 1);
            RobotsTransition t2 = new RobotsTransition("CAPTURE", 1);
            RobotsTransition t3 = new RobotsTransition("GO FIRST", 1);
            RobotsTransition t4 = new RobotsTransition("GO FORTH", 1);
            RobotsTransition t5 = new RobotsTransition("GO FIFTH", 1);
            RobotsTransition t6 = new RobotsTransition("CAPTURE", 5);
            RobotsTransition t7 = new RobotsTransition("PROCESS FIRST", 1);
            RobotsTransition t8 = new RobotsTransition("PROCESS SECOND", 1);
            RobotsTransition t9 = new RobotsTransition("PROCESS THIRD", 1);
            RobotsTransition t10 = new RobotsTransition("PROCESS FORTH", 1);
            RobotsTransition t11 = new RobotsTransition("PROCESS FIFTH", 1);
            RobotsTransition t12 = new RobotsTransition("EXIT FIRST", 0);
            RobotsTransition t13 = new RobotsTransition("EXIT SECOND", 0);
            RobotsTransition t14 = new RobotsTransition("EXIT THIRD", 0);
            RobotsTransition t15 = new RobotsTransition("EXIT FORTH", 0);
            RobotsTransition t16 = new RobotsTransition("EXIT FIFTH", 0);
        }
        
    }
    

}
