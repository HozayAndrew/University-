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
using TeacherProblemApp.Pages;

namespace TeacherProblemApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            NavigateChoosingPage();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            NavigateChoosingPage();
        }

        private void NavigateChoosingPage()
        {
            var page = new ChoosingPage
            {
                Frame = _frame
            };

            _frame.Navigate(page);
        }
    }
}
