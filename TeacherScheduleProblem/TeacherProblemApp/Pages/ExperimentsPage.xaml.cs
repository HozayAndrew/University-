using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
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
using TeacherProblemApp.AppModel;

namespace TeacherProblemApp.Pages
{
    public partial class ExperimentsPage : Page
    {
        public PlotModel MyModel { get; private set; }

        public ExperimentsPage()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            this.MyModel = new PlotModel { Title = "Algorithms" };
            MyModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = int.Parse(StartValue.Text), Maximum = int.Parse(FinishValue.Text) });

            try
            {
                var greedySeries = await GetSeries("Greedy", Config.GreedyAlgorithmExperiments, Colors.Green);
                this.MyModel.Series.Add(greedySeries);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Can't get data with error : {ex.Message}");
            }

            try
            {
                var antColonySeries = await GetSeries("AntColony", Config.AntColonyAlgorithmExperiments, Colors.Red);
                this.MyModel.Series.Add(antColonySeries);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Can't get data with error : {ex.Message}");
            }

            try
            {
                var _Series = await GetSeries("_", Config._AlgorithmExperiments, Colors.Yellow);
                MyModel.Series.Add(_Series);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Can't get data with error : {ex.Message}");
            }

            Plot.Model = MyModel;
        }

        private async Task<ResultFunction> GetFunction(string apiUrl)
        {
            var dataProxy = new DataProxy();
            var expData = new Model.ExperimentsData
            {
                StartCount = int.Parse(StartValue.Text),
                FinishCount = int.Parse(FinishValue.Text),
                Step = int.Parse(Step.Text)
            };

            var result = await dataProxy.GetExperimentResult(apiUrl, expData);

            var functionModel = new ResultFunction(result);

            return functionModel;
        }

        private async Task<LineSeries> GetSeries(string algorithmName, string apiUrl, Color color)
        {
            var function = await GetFunction(apiUrl);
            var lineSeries = new LineSeries
            {
                Title = algorithmName,
                Color = OxyColor.FromArgb(color.A, color.R, color.G, color.B)
            };

            foreach (var point in function.Points)
            {
                lineSeries.Points.Add(point);
            }

            return lineSeries;
        }
    }
}
