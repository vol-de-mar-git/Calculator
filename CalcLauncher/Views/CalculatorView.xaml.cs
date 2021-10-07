using System.Windows;
using CalcLauncher.Models;
using CalcLauncher.ViewModels;

namespace CalcLauncher.Views
{
    public partial class CalculatorView : Window
    {
        public CalculatorView()
        {
            InitializeComponent();
            DataContext = new CalculatorViewModel(new CalculatorModel());
        }
    }
}