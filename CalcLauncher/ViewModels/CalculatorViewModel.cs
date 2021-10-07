using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using CalcHost;
using CalcLauncher.Annotations;
using CalcLauncher.Models;
using Grpc.Net.Client;

namespace CalcLauncher.ViewModels
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        
        
        #region PropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Fields

        private string _expression;

        private CalculatorModel _calculatorModel;

        #endregion

        #region Commands

        private ICommand _keyButtonCommand;
        private ICommand _clearCommand;
        private ICommand _enterCommand;
        
        public ICommand KeyButtonCommand =>
            _keyButtonCommand ??= new RelayCommand(obj => Append(obj as string));
        
        public ICommand ClearButtonCommand =>
            _clearCommand ??= new RelayCommand(obj => Clear());
        
        public ICommand EnterButtonCommand =>
            _enterCommand ??= new RelayCommand(obj => Enter());

        #endregion

        #region Constructor

        public CalculatorViewModel(CalculatorModel calculatorModel)
        {
            _calculatorModel = calculatorModel;
        }

        #endregion

        #region Properties

        public string Expression
        {
            get => _expression;
            set
            {
                _expression = value;
                OnPropertyChanged(nameof(Expression));
            }
        }

        #endregion
        
        #region Methods

        private void Append(string symbol)
        {
            
            try
            {
                if ((_expressionIsResult && IsDigit(symbol)) || Expression == "ERR")
                {
                    Expression = String.Empty;
                }
                
                _expressionIsResult = false;
                
                if (NewAndLastSymbolIsOperand(symbol))
                {
                    Expression = Expression.Remove(Expression.Length - 1) + symbol;
                }
                else
                {
                    Expression += symbol;
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
        
        private void Clear()
        {
            Expression = String.Empty;
        }
        
        private async void Enter()
        {
            if (NotNullAndCorrect)
            {
                Expression =
                    (await _calculatorModel.CalcClient.CalculateAsync(new CalculateRequest {Expr = Expression}))
                    .Response;
                _expressionIsResult = true;
            }
        }

        private string LastSymbol(string expr)
        {
            return expr?.Substring(expr.Length - 1);
        }

        #endregion

        #region Boolean

        private bool NotNullAndCorrect =>
            !String.IsNullOrEmpty(Expression) && !"+-*/(".Contains(LastSymbol(Expression));

        private bool NewAndLastSymbolIsOperand(string symbol) =>
            "*/+-".Contains(symbol) && "*/+-".Contains(LastSymbol(Expression));

        private bool IsDigit(string expr) =>
            Double.TryParse(expr, out double result);

        private bool _expressionIsResult;

        #endregion

    }
}