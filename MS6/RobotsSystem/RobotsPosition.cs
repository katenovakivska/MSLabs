using System;
using System.Collections.Generic;
using System.Text;

namespace RobotsSystem
{
    public class RobotsPosition
    {
        //количество маркеров/фишек
        public int MarkersCount { get; set; }
        //выходящие дуги, входящие в переходы
        public List<RobotsArc> NextArcs { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int AmountOfPerformance { get; set; }
        public double TimeOfPerformance { get; set; }
        public RobotsPosition(string name, int markersCount, string type = "Not free condition")
        {
            Name = name;
            MarkersCount = markersCount;
            NextArcs = new List<RobotsArc>();
            Type = type;
            if (MarkersCount > 0)
            {
                AmountOfPerformance = 1;
            }
            else
            {
                AmountOfPerformance = 0;
            }
            TimeOfPerformance = 0;
        }

    }
}
