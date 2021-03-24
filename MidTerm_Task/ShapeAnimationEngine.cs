using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MidTerm_Task
{
    class ShapeAnimationEngine
    {
        public List<Circle> circles = new List<Circle>();
        public List<Square> squares = new List<Square>();
        internal void AddCircle(Point p, int r, SolidColorBrush color)
        {
            Circle c = new Circle(p, r, color);
            circles.Add(c);
        }
        
        internal void AddSquare(Point p, int h, int w, SolidColorBrush color)
        {
            Square s = new Square(p, h, w, color);
            squares.Add(s);
        }
    }
}
