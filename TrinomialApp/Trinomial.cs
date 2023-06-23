using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrinomialApp
{
    class Trinomial
    {
        private double a, b, c;
        public Trinomial() { A = 1; B = 0; C = 0; }
        public Trinomial(double a, double b, double c) : this() { A = a; B = b; C = c; }
        public double A
        {
            get => Math.Round(a, 2);
            set
            {
                if (value == 0)
                    return;
                a = value;
            }
        }
        public double B { get => Math.Round(b, 2); set { b = value; } }
        public double C { get => Math.Round(c, 2); set { c = value; } }
        public double Delta
        {
            get => Math.Round(Math.Pow(B, 2) - 4 * A * C, 2);
        }
        public double? X1
        {
            get
            {
                if (Delta < 0)
                    return null;
                return Math.Round((-B - Math.Sqrt(Delta)) / (2 * A), 2);
            }
        }
        public double? X2
        {
            get
            {
                if (Delta < 0)
                    return null;
                return Math.Round((-B + Math.Sqrt(Delta)) / (2 * A), 2);
            }
        }
        public double P
        {
            get => Math.Round(-B / (2 * A), 2);
        }
        public double Q
        {
            get => Math.Round(-Delta / (4 * A), 2);
        }
        public string SetOfValues
        {
            get
            {
                if (A > 0)
                    return "<" + Q + " ; ∞)";
                else
                    return "(-∞ ; " + Q + ">";
            }
        }
        public string FuncIncreases
        {
            get
            {
                if (A > 0)
                    return "< " + P + " ; ∞)";
                else
                    return "(-∞ ; " + P + ">";
            }
        }
        public string FuncDecreases
        {
            get
            {
                if (A > 0)
                    return "(-∞ ; " + P + ">";
                else
                    return "<" + P + " ; ∞)";
            }
        }
        public string YMax
        {
            get
            {
                if (A < 0)
                    return Q + " dla x = " + P;
                else
                    return "Brak";
            }
        }
        public string YMin
        {
            get
            {
                if (A > 0)
                    return Q + " dla x = " + P;
                else
                    return "Brak";
            }
        }
        public double Axis
        {
            get
            {
                return P;
            }
        }
        public string Derivative
        {
            get
            {
                string derivative = A * 2 + "x";
                if (B != 0)
                    if (B > 0)
                        derivative += " + " + B;
                    else
                        derivative += " - " + Math.Abs(B);
                return derivative;
            }
        }
        public override string ToString()
        {

            string cont = string.Empty;
            if (A == 1)
                cont += "x^2";
            else
                cont += A + "x^2";
            if (B == 1)
                cont += " + x";
            else if (B < 0)
                cont += " - " + Math.Abs(B) + "x";
            else if (B > 0)
                cont += " + " + B + "x";
            if (C < 0)
                cont += " - " + Math.Abs(C);
            else if (C > 0)
                cont += " + " + C;
            return cont;
        }
        public string ToString(int form)
        {
            string cont = string.Empty;
            if (form == 1)
            {
                if (A == 1)
                    cont += "x²";
                else if (A == -1)
                    cont += "-x²";
                else
                    cont += A + "x²";
                if (B == 1)
                    cont += " + x";
                else if (B < 0)
                    cont += " - " + Math.Abs(B) + "x";
                else if (B > 0)
                    cont += " + " + B + "x";
                if (C < 0)
                    cont += " - " + Math.Abs(C);
                else if (C > 0)
                    cont += " + " + C;
            }
            else if (form == 2)
            {
                if (A == 1)
                    if (P != 0)
                        cont += "(x";
                    else
                        cont += "x²";
                else if (A == -1)
                    if (P != 0)
                        cont += "-(x";
                    else
                        cont += "-x²";
                else
                    if (P != 0)
                        cont += A + "(x";
                    else
                        cont += A + "x²";
                if (P > 0)
                    cont += " - " + P + ")²";
                else if (P < 0)
                    cont += " + " + Math.Abs(P) + ")²";
                if (Q > 0)
                    cont += " + " + Q;
                else if (Q < 0)
                    cont += " - " + Math.Abs(Q);
            }
            else if (form == 3)
            {
                if (Delta < 0)
                    cont += "Brak";
                else if (Delta == 0)
                {
                    if (A == 1)
                        if (X1 != 0)
                            cont += "(x";
                        else
                            cont += "x²";
                    else if (A == -1)
                        if (X1 != 0)
                            cont += "-(x";
                        else
                            cont += "-x²";
                    else
                        if (X1 != 0)
                            cont += A + "(x";
                        else
                            cont += A + "x²";
                    if (X1 > 0)
                        cont += " - " + X1 + ")²";
                    else if (X1 < 0)
                        cont += " + " + Math.Abs((double)X1) + ")²";
                }
                else
                {
                    if (A == 1)
                        if (X1 == 0 || X2 == 0)
                            cont += "x(x";
                        else
                            cont += "(x";
                    else if (A == -1)
                        if (X1 == 0 || X2 == 0)
                            cont += "-x(x";
                        else
                            cont += "-(x";
                    else
                        if (X1 == 0 || X2 == 0)
                            cont += A + "x(x";
                        else
                            cont += A + "(x";
                    if (X1 > 0 && X2 != 0)
                        cont += " - " + X1 + ")(x";
                    else if (X1 < 0 && X2 != 0)
                        cont += " + " + Math.Abs((double)X1) + ")(x";
                    else if (X1 > 0 && X2 == 0)
                        cont += " - " + X1 + ")";
                    else if (X1 < 0 && X2 == 0)
                        cont += " + " + Math.Abs((double)X1) + ")";
                    if (X2 > 0)
                        cont += " - " + X2 + ")";
                    else if (X2 < 0)
                        cont += " + " + Math.Abs((double)X2) + ")";
                }
            }
            return cont;
        }
        public double GetYForX(double x)
        {
            return A * Math.Pow(x, 2) + B * x + C;
        }
        public (double? firstX, double? secondX) GetXForY(double y)
        {
            Trinomial resultTrinomial = new Trinomial(A, B, C - y);
            return (firstX: resultTrinomial.X1, secondX: resultTrinomial.X2);
        }
        public string GetTangent(double x)
        {
            string tangent = string.Empty;
            double a = A * 2 * x + B;
            double b = GetYForX(x) - a * x;
            if (a != 0)
                tangent += a + "x";
            if (a != 0)
            {
                if (b > 0)
                    tangent += " + " + b;
                else if (b < 0)
                    tangent += " - " + Math.Abs(b);
            }
            else
                tangent += b;
            return tangent;
        }
        public string GetInequalityRange(int type)
        {
            string range = string.Empty;
            List<string> rangeValues = new List<string>();
            switch (type)
            {
                case 1:
                    {
                        if (A > 0)
                            if (X1 == null && X2 == null)
                                range = "Ø";
                            else if (X1 == X2)
                                range = "Ø";
                            else
                                range = "(" + X1 + " ; " + X2 + ")";
                        else
                            if (X1 == null && X2 == null)
                                range = "R";
                            else
                                range = "(-∞ ; " + X1 + ") ∪ (" + X2 + " ; ∞)";
                    } break;
                case 2:
                    {
                        if (A > 0)
                            if (X1 == null && X2 == null)
                                range = "Ø";
                            else if (X1 == X2)
                                range = "{ " + X1 + " }";
                            else
                                range = "<" + X1 + " ; " + X2 + ">";
                        else
                            if (X1 == null && X2 == null)
                                range = "R";
                            else if (X1 == X2)
                                range = "R";
                            else
                                range = "(-∞ ; " + X1 + "> ∪ <" + X2 + " ; ∞)";
                    }
                    break;
                case 3:
                    {
                        if (A > 0)
                            if (X1 == null && X2 == null)
                                range = "R";
                            else if (X1 == X2)
                                range = "R";
                            else
                                range = "(-∞ ; " + X1 + "> ∪ <" + X2 + " ; ∞)";
                        else
                            if (X1 == null && X2 == null)
                                range = "Ø";
                            else if (X1 == X2)
                                range = "{ " + X1 + " }";
                            else
                                range = "<" + X1 + " ; " + X2 + ">";
                    }
                    break;
                case 4:
                    {
                        if (A > 0)
                            if (X1 == null && X2 == null)
                                range = "R";
                            else
                                range = "(-∞ ; " + X1 + ") ∪ (" + X2 + " ; ∞)";
                        else
                            if (X1 == null && X2 == null)
                                range = "Ø";
                            else if (X1 == X2)
                                range = "Ø";
                            else
                                range = "(" + X1 + " ; " + X2 + ")";
                    }
                    break;
                default: break;
            }
            return range;

        }
    }
}
