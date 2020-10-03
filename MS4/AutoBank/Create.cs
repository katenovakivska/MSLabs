using MS4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoBank
{
    public class Create : Element
    {
        public List<Process> NextElementss { get; set; }

        public Create(double delay, string name, string distribution, double devDelay = 0) : base(delay, name, distribution, devDelay)
        {
            NextElementss = new List<Process>();
        }
        public override void OutAct()
        {
            base.OutAct();
            TNext = TCurrent + GetDelay();
            GetNextElement().InAct(1);
        }
        public Process GetNextElement()
        {
            List<int> queue = new List<int>();
            foreach (var e in NextElementss)
            {
                queue.Add(e.QueueLength);
            }
            int count = 0, minIndex = 0, min = 10000;
            for (int i = 0; i < queue.Count - 1; i++)
            {
                if (queue[i] == queue[0])
                {
                    count++;
                }
            }
            for (int i = 1; i < queue.Count - 1; i++)
            {
                if (queue[i] < min)
                {
                    minIndex = 0;
                }
            }
            if (queue.Sum(x => x) == 0 || count == queue.Count)
            {
                return NextElementss[0];
            }
            else
            {
                return NextElementss[minIndex];
            }
        }

    }
}
