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
    /// <summary>
    /// Interaction logic for ChoosingPage.xaml
    /// </summary>
    public partial class ChoosingPage : Page
    {
        public Frame Frame;

        public ChoosingPage()
        {
            InitializeComponent();
        }

        private void SpecialCase_Click(object sender, RoutedEventArgs e)
        {
            var page = new SpecialCase { Frame = Frame };
            Frame.Navigate(page);
        }

        private void Experiments_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new ExperimentsPage());
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
