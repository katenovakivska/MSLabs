using System;
using System.Collections.Generic;
using System.Text;

namespace MS4
{
    public class Create : Element
    {

        public Create(double delay, string name, string distribution, double devDelay = 0) : base(delay, name, distribution, devDelay)
        {
        }
        public override void OutAct()
        {
            base.OutAct();
            TNext = TCurrent + GetDelay();
            NextElement.InAct(1);
        }


    }
}
