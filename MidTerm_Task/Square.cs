using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MidTerm_Task
{
    class Square
    {
        public Point topleft = new Point();
        public int Height;
        public int Width;
        public SolidColorBrush outline;
        public int deltax = 1; // -10 to go left, +10 to go right
        public int deltay = 1;
        public Square(Point p, int h,int w, SolidColorBrush color)
        {
            topleft = p;
            Height = h;
            Width = w;
            outline = color;
        }
    }
}
