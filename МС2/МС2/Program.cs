using System;

namespace МС2
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            
            Model model = new Model(2, 1, 2);
            model.Simulate(1000);
            Console.WriteLine("*****************VERIFICATION OF MODEL*******************");
            Console.WriteLine();
            //int num = 10;
            //for (int i = 0; i < num; i++)
            //{
            //    int delayCreate = random.Next(1, 5);
            //    int delayProcess = random.Next(1, 5);
            //    int maxQueue = random.Next(1, 4);

            //    Model model1 = new Model(delayCreate, delayProcess, maxQueue);
            //    model1.Simulate(1000);
            //    Console.WriteLine($"dCreate = {delayCreate} dProcess = {delayProcess} MaxLength = {maxQueue} " +
            //        $" NCreated = {model1.created} NServed = {model1.served} NUnserved = {model1.unserved}" +
            //        $" failureP = {model1.P} AvgLength = {model1.L} AvgWorkload = {model1.N}" +
            //        $" Intensivity = {model1.lambda} CoefWorkload = {model1.ro} AvgTProcessing = {model1.T}");
            //    Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            //}

            Console.ReadKey();
        }
    }
}
