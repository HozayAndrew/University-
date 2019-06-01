using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

    public partial class Result : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private string _algorithmName;
        public string AlgorithmName
        {
            get => _algorithmName;
            set
            {
                _algorithmName = value;
                OnPropertyChanged();
            }
        }

        private string _sequence;
        public string Sequence
        {
            get => _sequence;
            set
            {
                _sequence = value;
                OnPropertyChanged();
            }
        }

        private int _sumTime;
        public int SumTime
        {
            get => _sumTime;
            set
            {
                _sumTime = value;
                OnPropertyChanged();
            }
        }

        private double _algorithmTime;
        public double AlgorithmTime
        {
            get => _algorithmTime;
            set
            {
                _algorithmTime = value;
                OnPropertyChanged();
            }
        }

        public Result()
        {
            InitializeComponent();
        }
    }
}
