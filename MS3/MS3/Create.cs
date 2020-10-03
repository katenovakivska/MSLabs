using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MS3
{
    public class Create : Element
    {   
        public Create(double delay, string name, string distribution) : base(delay, name, distribution)
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
