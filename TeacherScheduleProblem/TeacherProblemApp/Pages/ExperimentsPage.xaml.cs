using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TeacherProblemApp.AppModel;

namespace TeacherProblemApp.Pages
{
    public partial class ExperimentsPage : Page, INotifyPropertyChanged
    {
        public PlotModel TimeModel { get; private set; }
        public PlotModel ValueModel { get; private set; }

        private object locker = new object();

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public ExperimentsPage()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            TimeModel = new PlotModel { Title = "Algorithms Time" };
            TimeModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = int.Parse(StartValue.Text), Maximum = int.Parse(FinishValue.Text) });

            ValueModel = new PlotModel { Title = "Algorithms Value" };
            TimeModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = int.Parse(StartValue.Text), Maximum = int.Parse(FinishValue.Text) });

            var expData = new Model.ExperimentsSettings
            {
                StartCount = int.Parse(StartValue.Text),
                FinishCount = int.Parse(FinishValue.Text),
                Step = int.Parse(Step.Text),
                Count = int.Parse(StepCount.Text)
            };
            var data = await GetProblems(expData);

            var task1 = Task.Run(async () =>
            {
               try
               {
                   var greedyResults = await GetExperimentResults(Config.GreedyAlgorithmExperiments, data);

                   var greedyTimeSeries = GetTimeSeries("Greedy", new TimeResultFunction(greedyResults), Colors.Green);
                   var greedyValueSeries = GetValueSeries("Greedy", new ValueResultFunction(greedyResults), Colors.Green);

                   Application.Current.Dispatcher.Invoke(() =>
                   {
                       lock (locker)
                       {
                           TimeModel.Series.Add(greedyTimeSeries);
                           ValueModel.Series.Add(greedyValueSeries);
                       }
                   });
               }
               catch (Exception ex)
               {
                   System.Diagnostics.Debug.WriteLine($"Can't get data with error : {ex.Message}");
               }
            });

            var task2 = Task.Run(async () =>
            {
                try
                {
                    var antColonyResults = await GetExperimentResults(Config.AntColonyAlgorithmExperiments, data);

                    var antColonyTimeSeries = GetTimeSeries("AntColony", new TimeResultFunction(antColonyResults), Colors.Red);
                    var antColonyValueSeries = GetValueSeries("AntColony", new ValueResultFunction(antColonyResults), Colors.Red);

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        lock (locker)
                        {
                            TimeModel.Series.Add(antColonyTimeSeries);
                            ValueModel.Series.Add(antColonyValueSeries);
                        }
                    });
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Can't get data with error : {ex.Message}");
                }
            });

            var task3 = Task.Run(async () =>
            {
                try
                {
                    var beesResults = await GetExperimentResults(Config.BeesAlgorithmExperiments, data);

                    var beesTimeSeries = GetTimeSeries("Bees", new TimeResultFunction(beesResults), Colors.Yellow);
                    var beesValueSeries = GetValueSeries("Bees", new ValueResultFunction(beesResults), Colors.Yellow);

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        lock (locker)
                        {
                            TimeModel.Series.Add(beesTimeSeries);
                            ValueModel.Series.Add(beesValueSeries);

                            TimePlot.Model = TimeModel;
                            ValuePlot.Model = ValueModel;
                        }
                    });
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Can't get data with error : {ex.Message}");
                }
            });

            await Task.WhenAll(new List<Task>() { task1, task2, task3 });

            TimePlot.Model = TimeModel;
            ValuePlot.Model = ValueModel;
        }

        private async Task<List<Model.Data>> GetProblems(Model.ExperimentsSettings settings)
        {
            var dataProxy = new DataProxy();
            return await dataProxy.GetProblems(Config.GenerateRequest, settings);
        }

        private async Task<List<Model.ExperimentResult>> GetExperimentResults(string apiUrl, List<Model.Data> expData)
        {
            var dataProxy = new DataProxy();
            return await dataProxy.GetExperimentResult(apiUrl, expData);
        }

        private LineSeries GetTimeSeries(string algorithmName, TimeResultFunction function, Color color)
        {
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

        private LineSeries GetValueSeries(string algorithmName, ValueResultFunction function, Color color)
        {
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
