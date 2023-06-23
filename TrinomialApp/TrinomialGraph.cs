using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TrinomialApp
{
    class TrinomialGraph
    {
        private int size, gap;
        public TrinomialGraph() { Size = 400; Gap = 20; Trinomial = new Trinomial(); Canvas = new Canvas(); }
        public TrinomialGraph(int size, int gap, Trinomial trinomial, Canvas canvas) : this() { Size = size; Gap = gap; Trinomial = trinomial; Canvas = canvas; }
        public int Size
        {
            get => size;
            set
            {
                if (value < 0)
                    return;
                size = value;
            }
        }
        public int Gap
        {
            get => gap;
            set
            {
                if (value < 0)
                    return;
                gap = value;
            }
        }
        public Trinomial Trinomial { get; set; }
        public Canvas Canvas { get; set; }
        public override string ToString() { return "TrinomialGraph[" + Size + " ; " + Gap + "]"; }
        private void CreateLine(double x1, double x2, double y1, double y2, Color color)
        {
            Line line = new Line();
            line.Stroke = new SolidColorBrush(color);
            line.X1 = x1;
            line.X2 = x2;
            line.Y1 = y1;
            line.Y2 = y2;
            Canvas.Children.Add(line);
        }
        private void CreateCircle(int size, int posX, int posY, bool isFilled, Brush stroke)
        {
            if (posX >= 0 && posX < Size + 1 && posY > 0 && posY < Size + 1)
            {
                Ellipse circle = new Ellipse();
                circle.Width = size;
                circle.Height = size;
                circle.Stroke = stroke;
                circle.StrokeThickness = 1;
                if (isFilled)
                    circle.Fill = stroke;
                circle.SetValue(Canvas.LeftProperty, (double)posX - size / 2);
                circle.SetValue(Canvas.TopProperty, (double)posY - size / 2);
                Canvas.Children.Add(circle);
            }
        }
        public void CreateGraphLayout()
        {
            Canvas.Children.Clear();
            CreateLine(0, Size, Size / 2, Size / 2, Colors.Black);
            CreateLine(Size / 2, Size / 2, 0, Size, Colors.Black);
            for (int i = 0; i < Size + 1; i += Size / (Size / Gap))
            {
                CreateLine(i, i, Size / 2 - 5, Size / 2 + 5, Colors.Black);
                CreateLine(Size / 2 - 5, Size / 2 + 5, i, i, Colors.Black);
            }
        }
        public void CreateGraph()
        {
            double x, y;
            for (int i = 0; i < Size + 1; i++)
            {
                x = (-Size / 2 + (double)i) / Gap;
                y = Size / 2 - Trinomial.GetYForX(x) * 20;
                if (i > 0 && i < Size + 1 && y > 0 && y < Size + 1)
                    CreateLine(i, i + 1, y, y + 1, Colors.Red);
                if (x == Trinomial.Axis)
                    for (int j = 0; j < Size + 1; j += 5)
                        CreateLine(i, i, j, j + 3, Colors.Green);
            }
        }
        public void CreateTangent(double usersX)
        {
            double a = Trinomial.A * 2 * usersX + Trinomial.B;
            double b = Trinomial.GetYForX(usersX) - a * usersX;
            double x, y;
            for (int i = 0; i < Size + 1; i++)
            {
                x = (-Size / 2 + (double)i) / Gap;
                y = Size / 2 - ((a * x + b) * Gap);
                if (i > 0 && i < Size + 1 && y > 0 && y < Size + 1)
                    CreateLine(i, i, y, y + 1, Colors.Magenta);
            }
            CreateCircle(10, (int)(Size / 2 + (usersX * Gap)), (int)(Size / 2 - (Trinomial.GetYForX(usersX) * 20)), true, Brushes.Purple);
        }
        public void CreateSections(int type)
        {
            switch (type)
            {
                case 1:
                    {
                        if (Trinomial.A > 0)
                            if (Trinomial.X1 == null && Trinomial.X2 == null)
                                return;
                            else if (Trinomial.X1 == Trinomial.X2)
                                return;
                            else
                            {
                                for (int i = (int)(Size / 2 + Trinomial.X1 * Gap); i < Size / 2 + Trinomial.X2 * Gap + 1; i++)
                                {
                                    if (Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) > 0 && Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) < Size + 1 && i > 0 && i < Size + 1)
                                        CreateLine(i, i + 1, Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap), Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(i + 1)) / Gap) * Gap) + 1, Colors.Blue);
                                }
                                CreateCircle(10, (int)(Size / 2 + Trinomial.X1 * Gap), (int)(Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(Size / 2 + Trinomial.X1 * Gap)) / Gap) * Gap)), false, Brushes.Blue);
                                CreateCircle(10, (int)(Size / 2 + Trinomial.X2 * Gap), (int)(Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(Size / 2 + Trinomial.X2 * Gap)) / Gap) * Gap)), false, Brushes.Blue);
                            }
                        else
                            if (Trinomial.X1 == null && Trinomial.X2 == null)
                                for (int i = 0; i < Size + 1; i++)
                                {
                                    if (Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) > 0 && Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) < Size + 1 && i > 0 && i < Size + 1)
                                        CreateLine(i, i + 1, Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap), Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(i + 1)) / Gap) * Gap) + 1, Colors.Blue);
                                }
                            else
                            {
                                for (int i = 0; i < Size / 2 + Trinomial.X2 * Gap + 1; i++)
                                {
                                    if (Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) > 0 && Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) < Size + 1 && i > 0 && i < Size + 1)
                                        CreateLine(i, i + 1, Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap), Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(i + 1)) / Gap) * Gap) + 1, Colors.Blue);
                                }
                                CreateCircle(10, (int)(Size / 2 + Trinomial.X2 * Gap), (int)(Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(Size / 2 + Trinomial.X2 * Gap)) / Gap) * Gap)), false, Brushes.Blue);
                                for (int i = (int)(Size / 2 + Trinomial.X1 * Gap); i < Size + 1; i++)
                                {
                                    if (Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) > 0 && Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) < Size + 1 && i > 0 && i < Size + 1)
                                        CreateLine(i, i + 1, Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap), Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(i + 1)) / Gap) * Gap) + 1, Colors.Blue);
                                }
                                CreateCircle(10, (int)(Size / 2 + Trinomial.X1 * Gap), (int)(Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(Size / 2 + Trinomial.X1 * Gap)) / Gap) * Gap)), false, Brushes.Blue);
                            }
                    } break;
                case 2:
                    {
                        if (Trinomial.A > 0)
                            if (Trinomial.X1 == null && Trinomial.X2 == null)
                                return;
                            else if (Trinomial.X1 == Trinomial.X2)
                            {
                                int i = (int)(Size / 2 + Trinomial.X1 * Gap);
                                if (Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) > 0 && Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) < Size + 1 && i > 0 && i < Size + 1)
                                        CreateLine(i, i + 1, Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap), Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(i + 1)) / Gap) * Gap) + 1, Colors.Blue);
                                CreateCircle(10, i, (int)(Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap)), true, Brushes.Blue);
                            }
                            else
                            {
                                for (int i = (int)(Size / 2 + Trinomial.X1 * Gap); i < Size / 2 + Trinomial.X2 * Gap + 1; i++)
                                {
                                    if (Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) > 0 && Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) < Size + 1 && i > 0 && i < Size + 1)
                                        CreateLine(i, i + 1, Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap), Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(i + 1)) / Gap) * Gap) + 1, Colors.Blue);
                                }
                                CreateCircle(10, (int)(Size / 2 + Trinomial.X1 * Gap), (int)(Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(Size / 2 + Trinomial.X1 * Gap)) / Gap) * Gap)), true, Brushes.Blue);
                                CreateCircle(10, (int)(Size / 2 + Trinomial.X2 * Gap), (int)(Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(Size / 2 + Trinomial.X2 * Gap)) / Gap) * Gap)), true, Brushes.Blue);
                            }
                        else
                            if (Trinomial.X1 != null && Trinomial.X2 != null)
                            {
                                for (int i = 0; i < (Size / 2) + Trinomial.X2 * Gap; i++)
                                {
                                    if ((Size / 2) - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) > 0 && (Size / 2) - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) < Size + 1 && i > 0 && i < Size + 1)
                                        CreateLine(i, i + 1, Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap), Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(i + 1)) / Gap) * Gap) + 1, Colors.Blue);
                                }
                                CreateCircle(10, (int)(Size / 2 + Trinomial.X2 * Gap), (int)(Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(Size / 2 + Trinomial.X2 * Gap)) / Gap) * Gap)), true, Brushes.Blue);
                                for (int i = (int)((Size / 2) + Trinomial.X1 * Gap); i < Size + 1; i++)
                                {
                                    if ((Size / 2) - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) > 0 && (Size / 2) - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) < Size + 1 && i > 0 && i < Size + 1)
                                        CreateLine(i, i + 1, Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap), Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(i + 1)) / Gap) * Gap) + 1, Colors.Blue);
                                }
                                CreateCircle(10, (int)(Size / 2 + Trinomial.X1 * Gap), (int)(Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(Size / 2 + Trinomial.X1 * Gap)) / Gap) * Gap)), true, Brushes.Blue);
                            }
                            else if (Trinomial.X1 != Trinomial.X2)
                            {
                                for (int i = 0; i < Size + 1; i++)
                                    if ((Size / 2) - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) > 0 && (Size / 2) - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) < Size + 1 && i > 0 && i < Size + 1)
                                        CreateLine(i, i + 1, Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap), Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(i + 1)) / Gap) * Gap) + 1, Colors.Blue);
                                CreateCircle(10, (int)(Size / 2 + Trinomial.X1 * Gap), (int)(Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(Size / 2 + Trinomial.X1 * Gap)) / Gap) * Gap)), true, Brushes.Blue);
                            }
                            else
                            {
                                for (int i = 0; i < Size + 1; i++)
                                    if ((Size / 2) - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) > 0 && (Size / 2) - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) < Size + 1 && i > 0 && i < Size + 1)
                                        CreateLine(i, i + 1, Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap), Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(i + 1)) / Gap) * Gap) + 1, Colors.Blue);
                            }
                    }
                    break;
                case 3:
                    {
                        if (Trinomial.A > 0)
                            if (Trinomial.X1 != null && Trinomial.X2 != null)
                            {
                                for (int i = 0; i < (Size / 2) + Trinomial.X1 * Gap; i++)
                                {
                                    if ((Size / 2) - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) > 0 && (Size / 2) - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) < Size + 1 && i > 0 && i < Size + 1)
                                        CreateLine(i, i + 1, Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap), Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(i + 1)) / Gap) * Gap) + 1, Colors.Blue);
                                }
                                CreateCircle(10, (int)(Size / 2 + Trinomial.X1 * Gap), (int)(Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(Size / 2 + Trinomial.X1 * Gap)) / Gap) * Gap)), true, Brushes.Blue);
                                for (int i = (int)((Size / 2) + Trinomial.X2 * Gap); i < Size + 1; i++)
                                {
                                    if ((Size / 2) - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) > 0 && (Size / 2) - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) < Size + 1 && i > 0 && i < Size + 1)
                                        CreateLine(i, i + 1, Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap), Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(i + 1)) / Gap) * Gap) + 1, Colors.Blue);
                                }
                                CreateCircle(10, (int)(Size / 2 + Trinomial.X2 * Gap), (int)(Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(Size / 2 + Trinomial.X2 * Gap)) / Gap) * Gap)), true, Brushes.Blue);
                            }
                            else if (Trinomial.X1 != Trinomial.X2)
                            {
                                for (int i = 0; i < Size + 1; i++)
                                {
                                    if ((Size / 2) - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) > 0 && (Size / 2) - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) < Size + 1 && i > 0 && i < Size + 1)
                                        CreateLine(i, i + 1, Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap), Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(i + 1)) / Gap) * Gap) + 1, Colors.Blue);
                                }
                                CreateCircle(10, (int)(Size / 2 + Trinomial.X1 * Gap), (int)(Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(Size / 2 + Trinomial.X1 * Gap)) / Gap) * Gap)), true, Brushes.Blue);
                            }
                            else
                            {
                                for (int i = 0; i < Size + 1; i++)
                                {
                                    if ((Size / 2) - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) > 0 && (Size / 2) - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) < Size + 1 && i > 0 && i < Size + 1)
                                        CreateLine(i, i + 1, Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap), Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(i + 1)) / Gap) * Gap) + 1, Colors.Blue);
                                }
                            }
                        else
                            if (Trinomial.X1 == null && Trinomial.X2 == null)
                                return;
                            else if (Trinomial.X1 == Trinomial.X2)
                            {
                                int i = (int)(Size / 2 + Trinomial.X1 * Gap);
                                if (Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) > 0 && Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) < Size + 1 && i > 0 && i < Size + 1)
                                        CreateLine(i, i + 1, Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap), Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(i + 1)) / Gap) * Gap) + 1, Colors.Blue);
                                CreateCircle(10, i, (int)(Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap)), true, Brushes.Blue);
                            }
                            else
                            {
                                for (int i = (int)(Size / 2 + Trinomial.X2 * Gap); i < Size / 2 + Trinomial.X1 * Gap + 1; i++)
                                {
                                    if (Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) > 0 && Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) < Size + 1 && i > 0 && i < Size + 1)
                                        CreateLine(i, i + 1, Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap), Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(i + 1)) / Gap) * Gap) + 1, Colors.Blue);
                                }
                                CreateCircle(10, (int)(Size / 2 + Trinomial.X1 * Gap), (int)(Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(Size / 2 + Trinomial.X1 * Gap)) / Gap) * Gap)), true, Brushes.Blue);
                                CreateCircle(10, (int)(Size / 2 + Trinomial.X2 * Gap), (int)(Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(Size / 2 + Trinomial.X2 * Gap)) / Gap) * Gap)), true, Brushes.Blue);
                            }
                    }
                    break;
                case 4:
                    {
                        if (Trinomial.A > 0)
                            if (Trinomial.X1 == null && Trinomial.X2 == null)
                                for (int i = 0; i < Size + 1; i++)
                                {
                                    if ((Size / 2) - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) > 0 && (Size / 2) - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) < Size + 1 && i > 0 && i < Size + 1)
                                        CreateLine(i, i + 1, Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap), Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(i + 1)) / Gap) * Gap) + 1, Colors.Blue);
                                }
                            else
                            {
                                for (int i = 0; i < (Size / 2) + Trinomial.X1 * Gap; i++)
                                {
                                    if ((Size / 2) - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) > 0 && (Size / 2) - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) < Size + 1 && i > 0 && i < Size + 1)
                                        CreateLine(i, i + 1, Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap), Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(i + 1)) / Gap) * Gap) + 1, Colors.Blue);
                                }
                                CreateCircle(10, (int)(Size / 2 + Trinomial.X1 * Gap), (int)(Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(Size / 2 + Trinomial.X1 * Gap)) / Gap) * Gap)), false, Brushes.Blue);
                                for (int i = (int)((Size / 2) + Trinomial.X2 * Gap); i < Size + 1; i++)
                                {
                                    if ((Size / 2) - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) > 0 && (Size / 2) - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) < Size + 1 && i > 0 && i < Size + 1)
                                        CreateLine(i, i + 1, Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap), Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(i + 1)) / Gap) * Gap) + 1, Colors.Blue);
                                }
                                CreateCircle(10, (int)(Size / 2 + Trinomial.X2 * Gap), (int)(Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(Size / 2 + Trinomial.X2 * Gap)) / Gap) * Gap)), false, Brushes.Blue);
                            }
                        else
                            if (Trinomial.X1 == null && Trinomial.X2 == null)
                                return;
                            else if (Trinomial.X1 == Trinomial.X2)
                                return;
                            else
                            {
                                for (int i = (int)(Size / 2 + Trinomial.X2 * Gap); i < Size / 2 + Trinomial.X1 * Gap + 1; i++)
                                    if (Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) > 0 && Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap) < Size + 1 && i > 0 && i < Size + 1)
                                        CreateLine(i, i + 1, Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)i) / Gap) * Gap), Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(i + 1)) / Gap) * Gap) + 1, Colors.Blue);
                                CreateCircle(10, (int)(Size / 2 + Trinomial.X1 * Gap), (int)(Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(Size / 2 + Trinomial.X1 * Gap)) / Gap) * Gap)), false, Brushes.Blue);
                                CreateCircle(10, (int)(Size / 2 + Trinomial.X2 * Gap), (int)(Size / 2 - (Trinomial.GetYForX((-Size / 2 + (double)(Size / 2 + Trinomial.X2 * Gap)) / Gap) * Gap)), false, Brushes.Blue);
                            }
                    }
                    break;
            }
        }
    }
}
