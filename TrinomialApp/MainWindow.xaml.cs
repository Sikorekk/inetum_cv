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

namespace TrinomialApp
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private (string Name, string Value) oldValue;
        private Trinomial trinomial;
        private TrinomialGraph trinomialGraph;
        public MainWindow()
        {
            InitializeComponent();
            trinomialGraph = new TrinomialGraph(400, 20, new Trinomial(), GraphCanvas);
            FuncIncreasesLabel.Content = "f(x) " + char.ConvertFromUtf32(0x2197) + " :";
            FuncDecreasesLabel.Content = "f(x) " + char.ConvertFromUtf32(0x2198) + " :";
            trinomialGraph.CreateGraphLayout();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox senderTextBox = sender as TextBox;
            senderTextBox.SelectionStart = senderTextBox.Text.Length;
            senderTextBox.SelectionLength = 0;
            if (!senderTextBox.IsFocused)
                return;
            if (string.IsNullOrWhiteSpace(senderTextBox.Text) && senderTextBox.Text.Length > 0)
            {
                senderTextBox.Text = String.Empty;
                return;
            }
            else if (string.IsNullOrWhiteSpace(senderTextBox.Text))
                return;
            if (senderTextBox.Text[senderTextBox.Text.Length - 1] == ',' && !senderTextBox.Text.Substring(0, senderTextBox.Text.Length - 1).Contains(','))
                return;
            if (!string.IsNullOrWhiteSpace(senderTextBox.Text) && senderTextBox.Text.Length == 1 && senderTextBox.Text[0] == '-')
                return;
            if (senderTextBox.Text.Length == 1 && senderTextBox.Text[0] == '-')
                return;
            if (double.TryParse(senderTextBox.Text, out double parsed) && senderTextBox.Name == oldValue.Name && senderTextBox.Text != oldValue.Value)
                senderTextBox.Text = parsed.ToString();
            if (!string.IsNullOrWhiteSpace(senderTextBox.Text) && senderTextBox.Name == oldValue.Name)
            {
                if (Char.IsWhiteSpace(senderTextBox.Text[senderTextBox.Text.Length - 1]))
                {
                    senderTextBox.Text = oldValue.Value;
                    return;
                }
                if (senderTextBox.Text[0] != '-' && !double.TryParse(senderTextBox.Text, out double nothing1))
                {
                    senderTextBox.Text = oldValue.Value;
                    return;
                }
                if (senderTextBox.Text[0] == '-' && senderTextBox.Text.Length > 1 && !double.TryParse(senderTextBox.Text, out double nothing2))
                {
                    senderTextBox.Text = oldValue.Value;
                    return;
                }
            }
            double a, b, c, x1, x2, p, q;
            switch (senderTextBox.Name)
            {
                case "ATextBox":
                    {
                        if (!string.IsNullOrWhiteSpace(ATextBox.Text) && double.TryParse(ATextBox.Text, out a) && a == 0)
                        {
                            MessageBox.Show("Współrzędna A musi być różna od 0");
                            ATextBox.Text = string.Empty;
                            return;
                        }
                        if (double.TryParse(ATextBox.Text, out a) && double.TryParse(BTextBox.Text, out b) && double.TryParse(CTextBox.Text, out c))
                            trinomial = new Trinomial(a, b, c);
                        else if (double.TryParse(ATextBox.Text, out a) && double.TryParse(X1TextBox.Text, out x1) && double.TryParse(X2TextBox.Text, out x2))
                            trinomial = new Trinomial(a, -a * (x1 + x2), a * x1 * x2);
                        else if (double.TryParse(ATextBox.Text, out a) && double.TryParse(PTextBox.Text, out p) && double.TryParse(QTextBox.Text, out q))
                            trinomial = new Trinomial(a, -a * p * 2, a * Math.Pow(p, 2) + q);
                    } break;
                case "BTextBox":
                    {
                        if (double.TryParse(ATextBox.Text, out a) && double.TryParse(BTextBox.Text, out b) && double.TryParse(CTextBox.Text, out c))
                            trinomial = new Trinomial(a, b, c);
                    }
                    break;
                case "CTextBox":
                    {
                        if (double.TryParse(ATextBox.Text, out a) && double.TryParse(BTextBox.Text, out b) && double.TryParse(CTextBox.Text, out c))
                            trinomial = new Trinomial(a, b, c);
                    }
                    break;
                case "X1TextBox":
                    {
                        if (string.IsNullOrWhiteSpace(X2TextBox.Text))
                            return;
                        if (double.TryParse(ATextBox.Text, out a) && double.TryParse(X1TextBox.Text, out x1) && double.TryParse(X2TextBox.Text, out x2))
                            trinomial = new Trinomial(a, -a * (x1 + x2), a * x1 * x2);
                    } break;
                case "X2TextBox":
                    {
                        if (string.IsNullOrWhiteSpace(X1TextBox.Text))
                            return;
                        if (double.TryParse(ATextBox.Text, out a) && double.TryParse(X1TextBox.Text, out x1) && double.TryParse(X2TextBox.Text, out x2))
                            trinomial = new Trinomial(a, -a * (x1 + x2), a * x1 * x2);
                    }
                    break;
                case "YForXTextBox":
                    {
                        if (trinomial == null && !string.IsNullOrWhiteSpace(YForXTextBox.Text))
                        {
                            MessageBox.Show("Uzupełnij dane trójmianu");
                            YForXTextBox.Text = String.Empty;
                        }
                        else if (double.TryParse(YForXTextBox.Text, out double x))
                            YForXTextBlock.Text = trinomial.GetYForX(x).ToString();
                        else if (string.IsNullOrWhiteSpace(YForXTextBox.Text))
                        {
                            YForXTextBox.Text = String.Empty;
                            YForXTextBlock.Text = String.Empty;
                        }
                    }
                    break;
                case "XForYTextBox":
                    {
                        if (trinomial == null && !string.IsNullOrWhiteSpace(XForYTextBox.Text))
                        {
                            MessageBox.Show("Uzupełnij dane trójmianu");
                            XForYTextBox.Text = String.Empty;
                        }
                        else if (double.TryParse(XForYTextBox.Text, out double y))
                        {
                            if (trinomial.GetXForY(y).firstX == null && trinomial.GetXForY(y).secondX == null)
                            {
                                X1ForYTextBlock.Text = "Brak";
                                X2ForYTextBlock.Text = "Brak";
                            }
                            else
                            {
                                X1ForYTextBlock.Text = trinomial.GetXForY(y).firstX.ToString();
                                X2ForYTextBlock.Text = trinomial.GetXForY(y).secondX.ToString();
                            }
                        }
                        else if (string.IsNullOrWhiteSpace(XForYTextBox.Text))
                        {
                            X1ForYTextBlock.Text = String.Empty;
                            X2ForYTextBlock.Text = String.Empty;
                        }
                    } break;
                case "PTextBox":
                    {
                        if (string.IsNullOrWhiteSpace(QTextBox.Text))
                            return;
                        if (double.TryParse(ATextBox.Text, out a) && double.TryParse(PTextBox.Text, out p) && double.TryParse(QTextBox.Text, out q))
                            trinomial = new Trinomial(a, -a * p * 2, a * Math.Pow(p, 2) + q);
                    } break;
                case "QTextBox":
                    {
                        if (string.IsNullOrWhiteSpace(PTextBox.Text))
                            return;
                        if (double.TryParse(ATextBox.Text, out a) && double.TryParse(PTextBox.Text, out p) && double.TryParse(QTextBox.Text, out q))
                            trinomial = new Trinomial(a, -a * p * 2, a * Math.Pow(p, 2) + q);
                    } break;
                case "TangentTextBox":
                    {
                        if (trinomial == null && !string.IsNullOrWhiteSpace(TangentTextBox.Text))
                        {
                            MessageBox.Show("Uzupełnij dane trójmianu");
                            TangentTextBox.Text = String.Empty;
                        }
                        else if (double.TryParse(TangentTextBox.Text, out double x))
                            TangentTextBlock.Text = trinomial.GetTangent(x);
                        else if (string.IsNullOrWhiteSpace(TangentTextBox.Text))
                            TangentTextBlock.Text = String.Empty;
                    } break;
            }

            if (trinomial != null)
            {
                trinomialGraph.Trinomial = trinomial;
                ATextBox.Text = trinomial.A.ToString();
                BTextBox.Text = trinomial.B.ToString();
                CTextBox.Text = trinomial.C.ToString();
                DeltaTextBlock.Text = trinomial.Delta.ToString();
                if (trinomial.X1 != null && trinomial.X2 != null)
                    if (senderTextBox.Name == "X1TextBox")
                    {
                        X2TextBox.Text = trinomial.X2.ToString();
                        X1TextBox.Text = trinomial.X1.ToString();
                    }
                    else if (senderTextBox.Name == "X2TextBox")
                    {
                        X1TextBox.Text = trinomial.X1.ToString();
                        X2TextBox.Text = trinomial.X2.ToString();
                    }
                    else
                    {
                        X1TextBox.Text = trinomial.X1.ToString();
                        X2TextBox.Text = trinomial.X2.ToString();
                    }
                else
                {
                    X1TextBox.Text = "Brak";
                    X2TextBox.Text = "Brak";
                }
                PTextBox.Text = trinomial.P.ToString();
                QTextBox.Text = trinomial.Q.ToString();
                fx1TextBlock.Text = trinomial.ToString(1);
                fx2TextBlock.Text = trinomial.ToString(2);
                fx3TextBlock.Text = trinomial.ToString(3);
                SetOfValuesTextBlock.Text = trinomial.SetOfValues;
                FuncIncreasesTextBlock.Text = trinomial.FuncIncreases;
                FuncDecreasesTextBlock.Text = trinomial.FuncDecreases;
                YMaxTextBlock.Text = trinomial.YMax;
                YMinTextBlock.Text = trinomial.YMin;
                AxisTextBlock.Text = trinomial.Axis.ToString();
                DerivativeTextBlock.Text = trinomial.Derivative;
                trinomialGraph.CreateGraphLayout();
                trinomialGraph.CreateGraph();
                if (double.TryParse(TangentTextBox.Text, out double tx))
                {
                    TangentTextBlock.Text = trinomial.GetTangent(tx);
                    trinomialGraph.CreateGraphLayout();
                    trinomialGraph.CreateGraph();
                    trinomialGraph.CreateTangent(tx);
                }
                else
                {
                    trinomialGraph.CreateGraphLayout();
                    trinomialGraph.CreateGraph();
                    TangentTextBlock.Text = String.Empty;
                }
                if (double.TryParse(YForXTextBox.Text, out double x))
                    YForXTextBlock.Text = trinomial.GetYForX(x).ToString();
                else
                    YForXTextBlock.Text = String.Empty;
                if (double.TryParse(XForYTextBox.Text, out double y))
                {
                    if (trinomial.GetXForY(y).firstX == null && trinomial.GetXForY(y).secondX == null)
                    {
                        X1ForYTextBlock.Text = "Brak";
                        X2ForYTextBlock.Text = "Brak";
                    }
                    else
                    {
                        X1ForYTextBlock.Text = trinomial.GetXForY(y).firstX.ToString();
                        X2ForYTextBlock.Text = trinomial.GetXForY(y).secondX.ToString();
                    }
                }
                else
                {
                    X1ForYTextBlock.Text = String.Empty;
                    X2ForYTextBlock.Text = String.Empty;
                }
            }
            oldValue = (senderTextBox.Name, senderTextBox.Text);
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox senderTextBox = sender as TextBox;
            oldValue = (senderTextBox.Name, senderTextBox.Text);
            if (trinomial == null)
                return;
            if ((senderTextBox.Name == "X1TextBox" || senderTextBox.Name == "X2TextBox") && trinomial.X1 == null && trinomial.X2 == null && !string.IsNullOrWhiteSpace(X1TextBox.Text) && !string.IsNullOrWhiteSpace(X2TextBox.Text))
            {
                X1TextBox.Text = String.Empty;
                X2TextBox.Text = String.Empty;
                return;
            }
            if ((senderTextBox.Name == "X1TextBox" || senderTextBox.Name == "X2TextBox") && (string.IsNullOrWhiteSpace(X1TextBox.Text) || string.IsNullOrWhiteSpace(X2TextBox.Text)))
                return;
            ATextBox.Text = trinomial.A.ToString();
            BTextBox.Text = trinomial.B.ToString();
            CTextBox.Text = trinomial.C.ToString();
            DeltaTextBlock.Text = trinomial.Delta.ToString();
            if (trinomial.X1 != null && trinomial.X2 != null)
                if (senderTextBox.Name == "X1TextBox")
                {
                    X2TextBox.Text = trinomial.X2.ToString();
                    X1TextBox.Text = trinomial.X1.ToString();
                }
                else if (senderTextBox.Name == "X2TextBox")
                {
                    X1TextBox.Text = trinomial.X1.ToString();
                    X2TextBox.Text = trinomial.X2.ToString();
                }
                else
                {
                    X1TextBox.Text = trinomial.X1.ToString();
                    X2TextBox.Text = trinomial.X2.ToString();
                }
            else
            {
                X1TextBox.Text = "Brak";
                X2TextBox.Text = "Brak";
            }
            PTextBox.Text = trinomial.P.ToString();
            QTextBox.Text = trinomial.Q.ToString();
            fx1TextBlock.Text = trinomial.ToString(1);
            fx2TextBlock.Text = trinomial.ToString(2);
            fx3TextBlock.Text = trinomial.ToString(3);
            SetOfValuesTextBlock.Text = trinomial.SetOfValues;
            FuncIncreasesTextBlock.Text = trinomial.FuncIncreases;
            FuncDecreasesTextBlock.Text = trinomial.FuncDecreases;
            YMaxTextBlock.Text = trinomial.YMax;
            YMinTextBlock.Text = trinomial.YMin;
            AxisTextBlock.Text = trinomial.Axis.ToString();
            DerivativeTextBlock.Text = trinomial.Derivative;
            trinomialGraph.CreateGraphLayout();
            trinomialGraph.CreateGraph();
            if (double.TryParse(TangentTextBox.Text, out double tx))
            {
                TangentTextBlock.Text = trinomial.GetTangent(tx);
                trinomialGraph.CreateGraphLayout();
                trinomialGraph.CreateGraph();
                trinomialGraph.CreateTangent(tx);
            }
            else
            {
                trinomialGraph.CreateGraphLayout();
                trinomialGraph.CreateGraph();
                TangentTextBlock.Text = String.Empty;
            }
            if (double.TryParse(YForXTextBox.Text, out double x))
                YForXTextBlock.Text = trinomial.GetYForX(x).ToString();
            else
                YForXTextBlock.Text = String.Empty;
            if (double.TryParse(XForYTextBox.Text, out double y))
            {
                if (trinomial.GetXForY(y).firstX == null && trinomial.GetXForY(y).secondX == null)
                {
                    X1ForYTextBlock.Text = "Brak";
                    X2ForYTextBlock.Text = "Brak";
                }
                else
                {
                    X1ForYTextBlock.Text = trinomial.GetXForY(y).firstX.ToString();
                    X2ForYTextBlock.Text = trinomial.GetXForY(y).secondX.ToString();
                }
            }
            else
            {
                X1ForYTextBlock.Text = String.Empty;
                X2ForYTextBlock.Text = String.Empty;
            }
        }

        private void InequalityButton_Click(object sender, RoutedEventArgs e)
        {
            if (trinomial == null)
            {
                MessageBox.Show("Uzupełnij dane trójmianu");
                return;
            }
            Button senderButton = sender as Button;
            trinomialGraph.CreateGraphLayout();
            trinomialGraph.CreateGraph();
            if (double.TryParse(TangentTextBox.Text, out double tx))
                trinomialGraph.CreateTangent(tx);
            switch (senderButton.Name)
            {
                case "LessButton":
                    {
                        InequalitySymbolTextBlock.Text = "f(x) < 0 :";
                        InequalityRangeTextBlock.Text = trinomial.GetInequalityRange(1);
                        trinomialGraph.CreateSections(1);
                    } break;
                case "LessEqualButton":
                    {
                        InequalitySymbolTextBlock.Text = "f(x) <= 0 :";
                        InequalityRangeTextBlock.Text = trinomial.GetInequalityRange(2);
                        trinomialGraph.CreateSections(2);
                    }
                    break;
                case "GreaterEqualButton":
                    {
                        InequalitySymbolTextBlock.Text = "f(x) >= 0 :";
                        InequalityRangeTextBlock.Text = trinomial.GetInequalityRange(3);
                        trinomialGraph.CreateSections(3);
                    }
                    break;
                case "GreaterButton":
                    {
                        InequalitySymbolTextBlock.Text = "f(x) > 0 :";
                        InequalityRangeTextBlock.Text = trinomial.GetInequalityRange(4);
                        trinomialGraph.CreateSections(4);
                    }
                    break;
            }
        }
    }
}
