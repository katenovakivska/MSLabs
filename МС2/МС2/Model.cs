using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace МС2
{
    public class Model
    {
        private double tNext { get; set; } //час наступної події
        private double tCurrent { get; set; } //час поточної події
        private double t0 { get; set; } //момент надходження вимоги у систему
        private double t1 { get; set; } //момент звільнення пристрою обслуговування від вимоги
        private double delayCreate { get; set; } //перебування в черзі
        private double delayProcess { get; set; } //час перебування у каналі
        public int created { get; set; } //кількість обслуговуваних та необслуговуваних вимог
        public int served { get; set; } //кількість обслуговуваних вимог
        public int unserved { get; set; } //кількість необслуговуваних
        private int state { get; set; } //стан пристрою вільний/зайнятий
        private int maxQueue { get; set; } //максимальна довжина черги
        private int queueLength { get; set; } //поточна кількість вимог у черзі
        private bool isFree { get; set; } //канал може бути вільний
        private Generator generator = new Generator(); //генератор тривалості знаходження вимоги у каналі
        public double Q { get; set; } //середній час перебування у черзі
        public double T { get; set; } //середній час обробки
        public double N { get; set; } //середнє навантаження пристрою
        public double P { get; set; } //ймовірність відмови
        public double L { get; set; } //ймовірність відмови
        public double lambda { get; set; } //інтенсивність надходження заявок
        public double ro { get; set; } //коеффіцієнт завантаження приладу
        public Model(double delayCreate, double delayProcess, int maxQueue)
        {
            this.delayCreate = delayCreate;
            this.delayProcess = delayProcess;
            tNext = 0.0;
            tCurrent = tNext;
            t0 = tCurrent; 
            t1 = Double.MaxValue;
            this.maxQueue = maxQueue;
            Q = 0.00; N = 0.00;
            T = 0.00; P = 0.00;
            L = 0.00; lambda = 0.00;
            ro = 0.00;
        }

        public void Simulate(double tModeling)
        {
            while (tCurrent < tModeling)
            {
                tNext = t0;
                isFree = false;

                MoveModelingTime();


                if (isFree == false)
                {
                    EventIncoming();
                }
                else
                {
                    EventEndOfWaiting();
                }
                
                PrintStates();
            }

            Q /= served;
            lambda = served / tModeling;
            L = Q * lambda;
            T /= served;
            ro = lambda * T;
            N = (ro - Math.Pow(ro, served + 1)) / (1 - Math.Pow(ro, served + 1));
            P = (double) unserved / served;

            //PrintStatistic();
        }
        public void MoveModelingTime()
        {
            if (t1 < tNext)
            {
                tNext = t1;
                isFree = true;
            }
            tCurrent = tNext;
        }
        public void EventIncoming()
        {
            t0 = tCurrent + GetDurationBeforeLaunch();
            created++;
            if (state == 0)
            {
                state = 1;
                t1 = tCurrent + GetDurationOfProcess();
            }
            else
            {
                if (queueLength < maxQueue)
                    queueLength++;
                else
                    unserved++;
            }
        }
        public void EventEndOfWaiting()
        {
            t1 = Double.MaxValue;
            state = 0;
            if (queueLength > 0)
            {
                state = 1;
                t1 = tCurrent + GetDurationOfProcess();
                Q += (t1 - t0) * queueLength;
                T += (t1 - t0);
                queueLength--;
            }
            served++;
        }
        private double GetDurationOfProcess()
        {
            return generator.GenerateSteady(delayProcess);
        }
        private double GetDurationBeforeLaunch()
        {
            return generator.GenerateSteady(delayCreate);
        }
       
        public void PrintStatistic()
        {
            Console.WriteLine("STATISTICS");
            Console.WriteLine($"number of processes = {created}");
            Console.WriteLine($"number of served processes = {served}");
            Console.WriteLine($"failure = {unserved}");
            Console.WriteLine($"average time in turn = {Q}");
            Console.WriteLine($"average workload = {N}");
        }
        public void PrintStates()
        {
            Console.WriteLine($"t = {tCurrent} state = {state} queue = {queueLength}");  
        }

    }
}
