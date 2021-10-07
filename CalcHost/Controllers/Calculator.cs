using System;

namespace CalcHost.Controllers
{
    public class Calculator
    {
        #region Fields

        private readonly CalculationService _calculationService;

        #endregion

        #region Constructor

        public Calculator(CalculationService service)
        {
            _calculationService = service;
        }

        #endregion

        #region Methods

        public string Calculate(string expression)
        {
            if (expression.Contains("/0"))
            {
                return "ERR";
            }

            if (_calculationService.IsNum(expression))
                return expression;
            try
            {
                return _calculationService.Calculate(
                    _calculationService.ToPostfix(
                        _calculationService.Parsing(expression))).ToString();
            }
            catch (Exception e)
            {
                return "ERR";
            }

        }

        #endregion
        
    }
}