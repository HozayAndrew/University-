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
using TeacherProblem.Model;

namespace TeacherProblemApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var matrix = new List<List<int>>();

            var mainStackPanel = Table.Children[0] as StackPanel;
            for (int i = 0; i < mainStackPanel.Children.Count; i++)
            {
                matrix.Add(new List<int>());
                var tableStackPanel = mainStackPanel.Children[i] as StackPanel;
                foreach (var control in tableStackPanel.Children)
                {
                    var textBox = control as TextBox;
                    if (textBox != null)
                    {
                        var value = int.Parse(textBox.Text);
                        matrix[i].Add(value);
                    }
                    else
                    {
                        matrix[i].Add(1000000);
                    }
                }
            }

            var time = new List<int>();

            var stackPanelTime = Time.Children[0] as StackPanel;
            foreach (var textBox in stackPanelTime.Children)
            {
                var value = int.Parse((textBox as TextBox).Text);
                time.Add(value);
            }

            var data = new Data
            {
                Count = time.Count,
                Matrix = matrix,
                Time = time,
            };

            await PostAsync(data);
        }

        private void FormTable_Click(object sender, RoutedEventArgs e)
        {
            int numberOfStudents = 0;
            try
            {
                numberOfStudents = int.Parse(StudentCount.Text);

                if (numberOfStudents <= 1 || numberOfStudents > 10)
                {
                    throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show("Недопустимое количество студентов 1 < n <= 10");
            }

            if (numberOfStudents != 0)
            {
                CreateTable(numberOfStudents);
                CreateTimeTable(numberOfStudents);
            }
        }

        private void CreateTable(int numberOfCells)
        {
            var tableHeight = Table.ActualHeight;
            var tableWidth = Table.ActualWidth;
            var cellHeight = tableHeight / numberOfCells;
            var cellWidth = tableWidth / numberOfCells;

            var stackPanel = new StackPanel
            {
                Width = tableWidth,
                Height = tableHeight
            };

            for (int i = 0; i < numberOfCells; i++)
            {
                var rowStackPanel = new StackPanel
                {
                    Width = tableWidth,
                    Height = cellHeight,
                    Orientation = Orientation.Horizontal
                };

                for (int j = 0; j < numberOfCells; j++)
                {
                    if (i != j)
                    {
                        var textBox = new TextBox
                        {
                            Width = cellWidth,
                            Height = cellHeight
                        };

                        rowStackPanel.Children.Add(textBox);
                    }
                    else
                    {
                        var textBlock = new TextBlock
                        {
                            Width = cellWidth,
                            Height = cellHeight,
                        };

                        rowStackPanel.Children.Add(textBlock);
                    }
                }

                stackPanel.Children.Add(rowStackPanel);
            }

            Table.Children.Clear();
            Table.Children.Add(stackPanel);
        }

        private void CreateTimeTable(int numberOfCells)
        {
            var tableHeight = Time.ActualHeight;
            var tableWidth = Time.ActualWidth;
            var cellHeight = tableHeight / numberOfCells;

            var stackPanel = new StackPanel
            {
                Width = tableWidth,
                Height = tableHeight
            };

            for (int i = 0; i < numberOfCells; i++)
            {
                var textBox = new TextBox
                {
                    Width = tableWidth,
                    Height = cellHeight
                };

                stackPanel.Children.Add(textBox);
            }

            Time.Children.Clear();
            Time.Children.Add(stackPanel);
        }

        private async void GenerateTable_Click(object sender, RoutedEventArgs e)
        {
            int numberOfStudents = 0;
            try
            {
                numberOfStudents = int.Parse(StudentCount.Text);

                if (numberOfStudents <= 1)
                {
                    throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show("Недопустимое количество студентов 1 < n");
            }

            if (numberOfStudents != 0)
            {
                var helper = new DataHelper();
                var data = helper.GetData(numberOfStudents);
                await PostAsync(data);
            }
        }

        private async Task PostAsync(Data data)
        {
            var dataProxy = new DataProxy();
            var result = await dataProxy.GetData(data);
        }
    }
}
