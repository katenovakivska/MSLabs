using System;
using System.Collections.Generic;
using System.Text;

namespace MS4
{
    public class Result
    {
        public string Name { get; set; }
        public double Average { get; set; }
        public double Workload { get; set; }

        public Result()
        { 
        }
        public Result(string name, double average, double workload)
        {
            Name = name;
            Average = average;
            Workload = workload;
        }
    }
}
