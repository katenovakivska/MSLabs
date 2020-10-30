using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MS6
{
    //позиция, обозначает условие для выполнения перехода
    public class Position
    {
        //количество маркеров/фишек
        public int MarkersCount { get; set; }
        //выходящие дуги, входящие в переходы
        public List<Arc> NextArcs { get; set; }
        public string Name { get; set; }
        public int MaxCount { get; set; }
        public int MinCount { get; set; }
        public int AverageCount { get; set; }

        public Position(string name, int markersCount)
        {
            Name = name;
            MarkersCount = markersCount;
            NextArcs = new List<Arc>();

        }

       
    }
}
