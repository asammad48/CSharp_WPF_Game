using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MidTerm_Task
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ShapeAnimationEngine engine = new ShapeAnimationEngine();
        public MainWindow()
        {
            InitializeComponent();
        }
        // Global Variable for All to Use for all Function  
        SolidColorBrush brush_selected = Brushes.Red;
        Point circlep, circler, squaretop,squarewidth = default;
        int circle_selected=0;
        int square_selected = 0;
        int circle_radius = 0;
        int red_selected=0;
        int blue_selected = 0;
        int green_selected = 0;
        private void circle_Click(object sender, RoutedEventArgs e)
        {
            circle_selected = 1;
            square_selected = 0;
        }

        private void square_Click(object sender, RoutedEventArgs e)
        {
            circle_selected = 0;
            square_selected = 1;
        }

        private void red_Click(object sender, RoutedEventArgs e)
        {
            red_selected = 1;
            blue_selected = 0;
            green_selected = 0;
        }

        private void green_Click(object sender, RoutedEventArgs e)
        {
            red_selected = 0;
            blue_selected = 0;
            green_selected = 1;
        }

        private void blue_Click(object sender, RoutedEventArgs e)
        {
            red_selected = 0;
            blue_selected = 1;
            green_selected = 0;
        }

        private void MyCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            if(red_selected==1)
            {
                brush_selected = new SolidColorBrush(Colors.Red);  // Red Color for Red Button
            }
            else if(blue_selected==1)
            {
                brush_selected = new SolidColorBrush(Colors.Blue); // Blue Color for Blue Button
            }
            else if(green_selected==1)
            {
                brush_selected = new SolidColorBrush(Colors.Green); // Green color for Green Button
            }
             if(circle_selected==1) // If Circle button is selected 
            {
              if (circlep == default)
                {
                    circlep = e.GetPosition(MyCanvas);
                }
                else
                {
                    circler= e.GetPosition(MyCanvas);
                    circle_radius =Convert.ToInt32(Math.Sqrt(((Convert.ToInt32(circler.X) - Convert.ToInt32(circlep.X)) * (Convert.ToInt32(circler.X) - Convert.ToInt32(circlep.X))) + ((Convert.ToInt32(circler.Y) - Convert.ToInt32(circlep.Y)) * (Convert.ToInt32(circler.Y) - Convert.ToInt32(circlep.Y)))));  // To Calculate Radius using Distance Formula
                    engine.AddCircle(circlep, circle_radius, brush_selected);
                    Repaint();
                    circlep = default;
                    circler = default;
                    circle_radius = 0;
                }
            }
             else if(square_selected==1) // If Square Button is Selected
            {
                if(squaretop==default) // On First Click
                {
                    squaretop = e.GetPosition(MyCanvas);
                }
                else // On Second Click
                {
                    squarewidth = e.GetPosition(MyCanvas);
                    int height =Convert.ToInt32(squarewidth.Y - squaretop.Y);// Get Height
                    int width =Convert.ToInt32(squarewidth.X - squaretop.X); // Get Width
                    int height1 = height > 0 ? height : -height; // If height is negative convert it into Positive
                    int width1 = width > 0 ? width : -width; // If width is negative Convert it into Postive
                    engine.AddSquare(squaretop, height1, width1, brush_selected); // Add Square to ArrayList
                    
                    Repaint();
                    squaretop = default;
                    squarewidth = default;
                }
            }
        }
        private void Repaint()
        {
            MyCanvas.Children.Clear();

            foreach (Circle c in engine.circles) // To draw Circle
            {
                DrawCircle(c);
            }
            foreach (Square s in engine.squares) // To draw Square after All Circle
            {
                DrawSquare(s);
            }
        }
        private void DrawCircle(Circle c)
        {

            

            Ellipse e = new Ellipse()
            {
                Width = c.Radius,
                Height = c.Radius,
                Stroke = c.outline,
                StrokeThickness = 1
            };
            e.SetValue(Canvas.LeftProperty, c.Center.X);
            e.SetValue(Canvas.TopProperty, c.Center.Y);

            MyCanvas.Children.Add(e);
        }
        private void DrawSquare(Square s)
        {
            Rectangle newsquare = new Rectangle
            {
                Width = s.Width,
                Height = s.Height,
                StrokeThickness = 1,
                Stroke = s.outline
            };

            Canvas.SetLeft(newsquare, s.topleft.X); // set the left position of rectangle to  X
            Canvas.SetTop(newsquare, s.topleft.Y); // set the top position of rectangle to  Y

            MyCanvas.Children.Add(newsquare);
        }


        private void Play_Click(object sender, RoutedEventArgs e)
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Step();
        }

        private void Step()
        {
            List<int> removeObject = new List<int>();
            int i = -1;
            foreach (Circle c in engine.circles) // For updating Step for Circle
            {
               
                if (c.Center.X + c.Radius > MyCanvas.Width-10)  // Right wall
                {
                    c.deltax = -c.deltax;
                }
                if (c.Center.Y + c.Radius > MyCanvas.Height) //Bottom Wall
                {
                    c.deltay = -c.deltay;
                }
                if (c.Center.Y<0)// Top Wall
                {
                    c.deltay = -c.deltay;
                }
                if (c.Center.X <0) // Left Wall
                {
                    c.deltax = -c.deltax;
                }
                i = i + 1;// To get index number
                double xdistance = c.Center.X - 300;// Distance between Black hole/Point w.r.t to X position
                double ydistance = c.Center.Y - 300;// Distance between Black hole/Point w.r.t to Y position
                xdistance *= xdistance;
                ydistance *= ydistance;
                double totaldistance = Math.Sqrt(xdistance + ydistance);// Distance Formula for distance calculation
                if(Convert.ToInt32(totaldistance)<=c.Radius) //if distance is less than Radius
                {
                    removeObject.Add(i); // Add ircle in removing list
                   // MessageBox.Show(c.Center + "  " + c.Radius);
                }
                    c.Center.X += c.deltax;
                    c.Center.Y += c.deltay; // To run Diagonally add Both X any Y position
                
            }
            if(removeObject!=null)
            {
                foreach(int j in removeObject)
                {
                    engine.circles.RemoveAt(j);// removing circle from screen Forever
                }
            }
            removeObject.Clear();
            i = -1;
            foreach (Square s in engine.squares)   // For Updating Steps for Squares
            {
                if (s.topleft.X + s.Width > MyCanvas.Width - 10) //Right Wall
                {
                    s.deltax = -s.deltax;
                }
                if (s.topleft.Y + s.Height > MyCanvas.Height) //Bottom Wall
                {
                    s.deltay = -s.deltay;
                }
                if (s.topleft.Y< 0)  //Top Wall
                {
                    s.deltay = -s.deltay;
                }
                if (s.topleft.X < 0) //Left Wall
                {
                    s.deltax = -s.deltax;
                }
                i = i + 1;
                if(s.topleft.Y<=300 && s.topleft.Y + s.Height>=300)
                {
                    if(s.topleft.X<=300 && s.topleft.X + s.Width>=300)
                    {
                        removeObject.Add(i);
                        //MessageBox.Show(s.topleft + "  " + s.Height + "  " + s.Width);
                    }
                }
                s.topleft.X += s.deltax; // Add Value in X Position
                s.topleft.Y += s.deltay;// To run Diagonally Also Add Value in Y position



            }
            if (removeObject != null)
            {
                foreach (int j in removeObject)
                {
                    engine.squares.RemoveAt(j);// removing circle from screen Forever
                }
            }
            removeObject.Clear();
            i = -1;
            Repaint();
        }

    }
}
