using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CalcLauncher.Controls
{
    public partial class Keyboard : UserControl
    {
        public Keyboard()
        {
            InitializeComponent();
        }
        
        public ICommand HandleButtonCommand
                {
                    get =>
                        (ICommand)GetValue(HandleButtonCommandProperty);
        
                    set =>
                        SetValue(HandleButtonCommandProperty, value);
                }
                public static readonly DependencyProperty HandleButtonCommandProperty = DependencyProperty.Register(
                    nameof(HandleButtonCommand), typeof(ICommand), typeof(Keyboard));
        
                public ICommand RemoveButtonCommand
                {
                    get =>
                        (ICommand)GetValue(RemoveButtonCommandProperty);
        
                    set =>
                        SetValue(RemoveButtonCommandProperty, value);
                }
                public static readonly DependencyProperty RemoveButtonCommandProperty = DependencyProperty.Register(
                    nameof(RemoveButtonCommand), typeof(ICommand), typeof(Keyboard));
                
                public ICommand EnterButtonCommand
                {
                    get =>
                        (ICommand)GetValue(EnterButtonCommandProperty);
        
                    set =>
                        SetValue(EnterButtonCommandProperty, value);
                }
                public static readonly DependencyProperty EnterButtonCommandProperty = DependencyProperty.Register(
                    nameof(EnterButtonCommand), typeof(ICommand), typeof(Keyboard));
    }
}