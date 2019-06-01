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

namespace TeacherProblemApp.Controls
{
    public partial class DynamicTable : UserControl
    {
        private int _rowColumnNumber;
        public int RowColumnNumber
        {
            get => _rowColumnNumber;
            set
            {
                CreateTable(value);
                CreateTimeTable(value);
                _rowColumnNumber = value;
            }
        }

        private Random _randomizer = new Random();

        public DynamicTable()
        {
            InitializeComponent();
        }

        public List<int> GetTimeMatrix()
        {
            var time = new List<int>();

            var stackPanelTime = Time.Children[0] as StackPanel;
            foreach (var textBox in stackPanelTime.Children)
            {
                var value = int.Parse((textBox as TextBox).Text);
                time.Add(value);
            }

            return time;
        }

        public List<List<int>> GetWeightMatrix()
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

            return matrix;
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
                            Height = cellHeight,
                            Text = _randomizer.Next(10, 50).ToString()
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
                    Height = cellHeight,
                    Text = _randomizer.Next(10, 50).ToString()
                };

                stackPanel.Children.Add(textBox);
            }

            Time.Children.Clear();
            Time.Children.Add(stackPanel);
        }
    }
}
