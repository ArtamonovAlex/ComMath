using CommathLab3;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Wpf;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CommathL4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<string, Func<double, double, double>> Functions = new Dictionary<string, Func<double, double, double>>
        {
            { "y' = x^3 + x + 3y/x", (a,b) => a*a*a + a + 3*b/a},
            { "y' = cos(x)", (a,b) => Math.Cos(a) },
            { "y' = -2y", (a,b) => -2 * b }
        };

        public MainWindow()
        {
            InitializeComponent();
            FunctionBox.ItemsSource = Functions.Keys;
            FunctionBox.SelectedIndex = 0;
            Plot.Model = new PlotModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (GetInput(out double x0, out double y0, out double xN, out double accuracy, out string function))
            {


                double n = (xN - x0 + 1) * 10;
                double step = (xN - x0) / n;

                double[] y = MilneCalculator.Calculate(Functions[function], x0, y0, step, n, accuracy, out double[] x);
                LagrangePolynomial pol = new LagrangePolynomial(x, y);

                if (Plot.Model.Series.Count != 0)
                {
                    Plot.Model.Series.Clear();
                }

                //Supposed option
                Plot.Model.Series.Add(new FunctionSeries(pol.Compute, x0, xN, step));

                //-------------------------------------------------------
                //Alternative option if something went wrong
                //-------------------------------------------------------
                //OxyPlot.Series.LineSeries lineSeries = new OxyPlot.Series.LineSeries();
                //for (int counter = 0; counter < y.Length; counter++)
                //{
                //    lineSeries.Points.Add(new DataPoint(x[counter], y[counter]));
                //}
                //Plot.Model.Series.Add(lineSeries);


                Plot.Model.InvalidatePlot(true);
            }
        }

        private bool GetInput(out double x0, out double y0, out double xN, out double accuracy, out string function)
        {
            x0 = 0;
            y0 = 0;
            xN = 0;
            accuracy = 0;
            function = FunctionBox.Text;

            if (XInputBox.Text == "" || YInputBox.Text == "" || XNInputBox.Text == "" || AccInputBox.Text == "")
            {
                ErrorBox.Text = "Все поля должны быть \nзаполнены";
                return false;
            } else if (!double.TryParse(XInputBox.Text, out x0) || !double.TryParse(YInputBox.Text, out y0) || !double.TryParse(XNInputBox.Text, out xN) || !double.TryParse(AccInputBox.Text, out accuracy))
            {
                ErrorBox.Text = "Все введённые значения \nдолжны соответствовать \nтипу double";
                return false;
            } else
            {
                ErrorBox.Text = "";
                return true;
            }
        }
    }
}
