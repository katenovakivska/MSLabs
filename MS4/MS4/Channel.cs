using System;
using System.Collections.Generic;
using System.Text;

namespace MS4
{
    public class Channel
    {
        public string Name { get; set; }
        public double TimeOut { get; set; }
        public bool IsFree { get; set; }
        public Channel(string name, double timeOut, bool isFree)
        {
            Name = name;
            TimeOut = timeOut;
            IsFree = isFree;
        }
    }
}
