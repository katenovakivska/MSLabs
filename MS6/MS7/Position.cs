using System;
using System.Collections.Generic;
using System.Text;

namespace MS7
{
    //позиция, обозначает условие для выполнения перехода
    public class Position
    {
        //количество маркеров/фишек
        public int MarkersCount { get; set; }
        //выходящие дуги, входящие в переходы
        public List<Arc> NextArcs { get; set; }
        public string Name { get; set; }
        public int StartMarkersCount { get; set; }
        public int MaxMarkersCount { get; set; }
        public int MinMarkersCount { get; set; }
        public double AverageMarkersCount { get; set; }
        public int Count { get; set; }

        public Position(string name, int markersCount)
        {
            Name = name;
            MarkersCount = markersCount;
            StartMarkersCount = markersCount;
            NextArcs = new List<Arc>();
            MaxMarkersCount = 0;
            MinMarkersCount = 0;
            AverageMarkersCount = 0;
            Count = 0;
        }

        public void CountStatistics()
        {
            if (MarkersCount > MaxMarkersCount)
            {
                MaxMarkersCount = MarkersCount;
            }
            if (MarkersCount < MinMarkersCount)
            {
                MinMarkersCount = MarkersCount;
            }
            AverageMarkersCount += MarkersCount;
            Count += 1;
        }
    }
}
