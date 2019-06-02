using Model;
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

namespace TeacherProblemApp.Pages
{
    public partial class ResultPage : Page
    {
        public Data Data;

        public ResultPage()
        {
            InitializeComponent();
            Loaded += ResultPage_Loaded;
        }

        private async void ResultPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (Data != null)
            {
                var dataProxy = new DataProxy();
                try
                {
                    var greedyAlgorithm = await dataProxy.GetData(Config.GreedyAlgorithm, Data);
                    Results.Children.Add(new Controls.Result()
                    {
                        AlgorithmName = "Greedy",
                        AlgorithmTime = greedyAlgorithm.AlgorithmTime,
                        SumTime = greedyAlgorithm.SumTime,
                        Sequence = ConvertToSequence(greedyAlgorithm.StudentSequence)
                    });
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Can't get data with error : {ex.Message}");
                }

                try
                {
                    var antColonyAlgorithm = await dataProxy.GetData(Config.AntColonyAlgorithm, Data);
                    Results.Children.Add(new Controls.Result()
                    {
                        AlgorithmName = "Ant Colony",
                        AlgorithmTime = antColonyAlgorithm.AlgorithmTime,
                        SumTime = antColonyAlgorithm.SumTime,
                        Sequence = ConvertToSequence(antColonyAlgorithm.StudentSequence)
                    });

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Can't get data with error : {ex.Message}");
                }

                try
                {
                    var _Algorithm = await dataProxy.GetData(Config.BeesAlgorithm, Data);

                    Results.Children.Add(new Controls.Result()
                    {
                        AlgorithmName = "_",
                        AlgorithmTime = _Algorithm.AlgorithmTime,
                        SumTime = _Algorithm.SumTime,
                        Sequence = _Algorithm.StudentSequence.ToString()
                    });
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Can't get data with error : {ex.Message}");
                }
            }
            else
            {
                Results.Children.Add(new Controls.Result()
                {
                    AlgorithmName = "Test",
                    AlgorithmTime = 30.33,
                    SumTime = 345,
                    Sequence = "3 - 3 - 3 - 3"
                });
            }
        }

        private string ConvertToSequence(List<int> data)
        {
            var str = "";
            for (int i = 0; i < data.Count; i++)
            {
                if(i != data.Count - 1)
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
