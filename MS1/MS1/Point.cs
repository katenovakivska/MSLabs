using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS1
{
    //класс, що відображає логіку таблиць відповідності n до x
    public class Point
    {
        public double x { get; set; }
        public double n { get; set; }

        public Point()
        {
        }

        public Point(double x, double n)
        {
            this.x = x;
            this.n = n;
        }
    }
}
