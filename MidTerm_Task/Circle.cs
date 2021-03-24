using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MidTerm_Task
{
    class Circle
    {
        public Point Center = new Point();
        public int Radius;
        public SolidColorBrush outline;
        public int deltax = 1; // -10 to go left, +10 to go right
        public int deltay = 1;
        public Circle(Point p, int r, SolidColorBrush color)
        {
            Center = p;
            Radius = r;
            outline = color;
        }
    }
}
