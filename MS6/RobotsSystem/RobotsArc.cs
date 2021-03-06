﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RobotsSystem
{
    public class RobotsArc
    {
        //кратность
        public int Multiplicity { get; set; }
        //следующая позиция 
        public RobotsPosition NextPosition { get; set; }
        //или следующий переход
        public Transition NextTransition { get; set; }
        public Position PrevPosition { get; set; }

        public string Name { get; set; }

        public Arc(string name, int multiplicity, Transition next, Position prev)
        {
            Multiplicity = multiplicity;
            Name = name;
            NextTransition = next;
            PrevPosition = prev;
        }

        public Arc(string name, int multiplicity, Position next)
        {
            Multiplicity = multiplicity;
            Name = name;
            NextPosition = next;
        }
    }
}
