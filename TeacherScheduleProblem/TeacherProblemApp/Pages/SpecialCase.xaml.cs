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

namespace TeacherProblemApp.Pages
{
    public partial class SpecialCase : Page
    {
        public Frame Frame;

        public SpecialCase()
        {
            InitializeComponent();
        }

        private void Results_Click(object sender, RoutedEventArgs e)
        {
            var count = int.Parse(Students.Text);
            var data = new Data
            {
                Count = count,
                Matrix = Table.GetWeightMatrix(),
                Time = Table.GetTimeMatrix()
            };

            Frame.Navigate(new ResultPage { Data = data });
        }

        private void TableCreate_Click(object sender, RoutedEventArgs e)
        {
            var count = int.Parse(Students.Text);
            if (count > 10 || count < 1)
            {
                MessageBox.Show("Incorrect data: students count between 1 < count < 10");
            }
            else
            {
                Table.RowColumnNumber = count;

                Results.Opacity = 1f;
                Results.IsEnabled = true;
            }
        }

        private void Students_TextChanged(object sender, TextChangedEventArgs e)
        {
            Results.Opacity = 0.5f;
            Results.IsEnabled = false;
        }
    }
}
