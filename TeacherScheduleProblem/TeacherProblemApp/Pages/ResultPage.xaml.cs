using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace TeacherProblemApp.Pages
{
    public partial class ResultPage : Page
    {
        public Data Data;

        private object locker = new object();

        public ResultPage()
        {
            InitializeComponent();
            Loaded += ResultPage_Loaded;
        }

        private void ResultPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (Data == null)
            {
                MessageBox.Show("Opps! Something go wrong. Please try again");
                return;
            }

            var dataProxy = new DataProxy();

            var currentDirectory = Directory.GetCurrentDirectory();
            var resultsDirectory = System.IO.Path.Combine(currentDirectory, "Results");
            Directory.CreateDirectory(resultsDirectory);

            _ = Task.Run(async () =>
            {
                try
                {
                    var greedyAlgorithm = await dataProxy.GetData(Config.GreedyAlgorithm, Data);

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        lock (locker)
                        {
                            Results.Children.Add(new Controls.Result()
                            {
                                AlgorithmName = "Greedy",
                                AlgorithmTime = greedyAlgorithm.AlgorithmTime,
                                SumTime = greedyAlgorithm.SumTime,
                                Sequence = ConvertToSequence(greedyAlgorithm.StudentSequence)
                            });
                        }
                    });

                    var json = JsonConvert.SerializeObject(new SaveFile() { InputData = Data, OutputData = greedyAlgorithm, Name = "Greedy algorithm" });
                    File.WriteAllText(System.IO.Path.Combine(resultsDirectory, $"{Guid.NewGuid().ToString()}.json"), json);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Can't get data from greedy with error : {ex.Message}");
                }
            });

            _ = Task.Run(async () =>
            {
                try
                {
                    var antColonyAlgorithm = await dataProxy.GetData(Config.AntColonyAlgorithm, Data);

                    Application.Current.Dispatcher.Invoke(() =>
                    { 
                        Results.Children.Add(new Controls.Result()
                        {
                            AlgorithmName = "Ant Colony",
                            AlgorithmTime = antColonyAlgorithm.AlgorithmTime,
                            SumTime = antColonyAlgorithm.SumTime,
                            Sequence = ConvertToSequence(antColonyAlgorithm.StudentSequence)
                        });
                    });

                    var json = JsonConvert.SerializeObject(new SaveFile() { InputData = Data, OutputData = antColonyAlgorithm, Name = "Ant colony algorithm" });
                    File.WriteAllText(System.IO.Path.Combine(resultsDirectory, $"{Guid.NewGuid().ToString()}.json"), json);

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Can't get data from ant colony with error : {ex.Message}");
                }
            });

            _ = Task.Run(async () =>
            {
                try
                {
                    var beesAlgorithm = await dataProxy.GetData(Config.BeesAlgorithm, Data);

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        lock (locker)
                        {
                            Results.Children.Add(new Controls.Result()
                            {
                                AlgorithmName = "Bees",
                                AlgorithmTime = beesAlgorithm.AlgorithmTime,
                                SumTime = beesAlgorithm.SumTime,
                                Sequence = ConvertToSequence(beesAlgorithm.StudentSequence)
                            });
                        }
                    });

                    var json = JsonConvert.SerializeObject(new SaveFile() { InputData = Data, OutputData = beesAlgorithm, Name = "Bees algorithm" });
                    File.WriteAllText(System.IO.Path.Combine(resultsDirectory, $"{Guid.NewGuid().ToString()}.json"), json);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Can't get data from bees with error : {ex.Message}");
                }
            });
        }

        private string ConvertToSequence(List<int> data)
        {
            var str = "";
            for (int i = 0; i < data.Count; i++)
            {
                if (i != data.Count - 1)
                {
                    str += $"{data[i]}-";
                }
                else
                {
                    str += $"{data[i]}";
                }
            }

            return str;
        }
    }
}
