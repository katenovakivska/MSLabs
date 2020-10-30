using System;
using System.Collections.Generic;
using System.Text;

namespace MS8
{
    //дуга, выходящая из перехода и входящаяв позицию
    public class Arc
    {
        //кратность
        public int Multiplicity { get; set; }
        //следующая позиция 
        public Place NextPosition { get; set; }
        //или следующий переход
        public Transition NextTransition { get; set; }
        public Place PrevPosition { get; set; }
        public string Name { get; set; }

        public Arc(string name, int multiplicity, Transition next, Place prev)
        {
            Multiplicity = multiplicity;
            Name = name;
            NextTransition = next;
            PrevPosition = prev;
        }

        public Arc(string name, int multiplicity, Place next)
        {
            Multiplicity = multiplicity;
            Name = name;
            NextPosition = next;
        }
    }
}
