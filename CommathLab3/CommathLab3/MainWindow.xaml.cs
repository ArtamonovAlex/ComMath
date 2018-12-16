using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Wpf;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
namespace CommathLab3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<string, Func<double, double>> Functions;

        private LagrangePolynomial Polynom;

        private double[] defaultParams = new double[] { 0, 10};

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Functions = new Dictionary<string, Func<double, double>> { { "Sin(x)", Math.Sin }, { "Ln(x)", Math.Log }, { "1/x", x => 1 / x } };
            FunctionBox.ItemsSource = Functions.Keys;
            FunctionBox.SelectedIndex = 0;
            PointsBox.Watermark = "Введите узлы \nинтерполяции через ;";
        }


        private void BuildButtonClick(object sender, RoutedEventArgs e)
        {
            //Input-------------------------------------------------------
            double[] x = GetInputForBuild(out string mistake);
            Func<double, double> func = Functions[FunctionBox.Text];
            double[] graphParams = GetGraphParams(out string graphMistake);
            //------------------------------------------------------------
            GraphErrorMessage.Text = graphMistake;
            ErrorMessage.Text = mistake;
            if (mistake == null)
            {
                //Computing---------------------------------------------------
                double[] y = new double[x.Length];
                for (int counter = 0; counter < y.Length; counter++)
                {
                    y[counter] = func(x[counter]);
                }
                Polynom = new LagrangePolynomial(x, y);
                //------------------------------------------------------------

                //Graphics----------------------------------------------------
                PlotModel model = new PlotModel();
                model.PlotType = PlotType.XY;
                model.Series.Add(new FunctionSeries(func, graphParams[0], graphParams[1], 0.01, FunctionBox.Text));
                model.Series.Add(new FunctionSeries(Polynom.Compute, graphParams[0], graphParams[1], 0.01, "Polynom"));
                foreach (double value in x)
                {
                    OxyPlot.Annotations.PointAnnotation point = new OxyPlot.Annotations.PointAnnotation
                    {
                        X = value,
                        Y = func(value),
                        Shape = MarkerType.Square
                };
                    model.Annotations.Add(point);
                }
                PlotView view = new PlotView
                {
                    Model = model
                };
                Container.Children.Add(view);
                Grid.SetColumn(view, 1);
                Grid.SetRowSpan(view, 2);
                //------------------------------------------------------------

                ComputeButton.IsEnabled = true;
            }
        }

        private double[] GetGraphParams(out string graphMistake)
        {
            graphMistake = null;
            if (StartBox.Text == "" || EndBox.Text == "")
            {
                graphMistake = "Оба поля должны быть заполнены, в противном случае берутся стандартные значения";
            } else
            {
                if(!double.TryParse(StartBox.Text,out double start))
                {
                    graphMistake = "Введите ЧИСЛО в поле От";
                } else
                {
                    if (!double.TryParse(EndBox.Text, out double end))
                    {
                        graphMistake = "Введите ЧИСЛО в поле До";
                    } else
                    {
                        if (end < start)
                        {
                            graphMistake = "Число До должно быть меньше числа От";
                        } else
                        {
                            return new double[] { start, end};
                        }
                    }
                }
            }
            return defaultParams;
        }

        private double[] GetInputForBuild(out string mistake)
        {
            mistake = null;
            List<double> xValues = new List<double>();
            string[] strValues = PointsBox.Text.Split(';');
            foreach(string strValue in strValues)
            {
                if (!double.TryParse(strValue, out double value))
                {
                    mistake = "Все значения должны быть числами";
                    break;
                }
                xValues.Add(value); 
                
            }
            if (strValues[0] == "")
            {
                mistake = "Введите хотя бы одно число";
            }
            foreach(double x in xValues)
            {
                if(!IsComputable(x, Functions[FunctionBox.Text]))
                {
                    mistake = "Не все числа удовлетворяют области определения функции";
                }
            }
            return xValues.ToArray();
        }

        private bool IsComputable(double x, Func<double,double> func)
        {
            if (func(x) == double.NaN || func(x) == double.NegativeInfinity || func(x) == double.PositiveInfinity) return false;
            else return true;
        }

        private void ComputeButtonClick(object sender, RoutedEventArgs e)
        {
            double xValue = GetInputForCompute(out string mistake);
            if (mistake != null)
            {
                ResultBox.Foreground = Brushes.Red;
                ResultBox.Text = mistake;
            } else
            {
                //Computing---------------------------------------------------
                double result = Polynom.Compute(xValue);
                //------------------------------------------------------------

                //Output------------------------------------------------------
                ResultBox.Foreground = Brushes.Black;
                ResultBox.Text = result.ToString();
                //------------------------------------------------------------
            }
        }

        private double GetInputForCompute(out string mistake)
        {
            mistake = null;
            string valueStr = ValueBox.Text;
            if (!double.TryParse(valueStr, out double value)) mistake = "Значение должно быть числом";
            return value;
        }

        private void ComputeButton_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((sender as Button).IsEnabled) (sender as Button).ToolTip = null;
        }
        
    }
}
