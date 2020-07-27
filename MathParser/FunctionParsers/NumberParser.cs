using EgorLucky.MathParser.Functions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EgorLucky.MathParser.FunctionParsers
{
    public class NumberParser : IFunctionParser
    {
        public string Name => nameof(Number);

        public IFunction Create(double value)
        {
            return new Number() { Value = value };
        }

        public MathTryParseResult TryParse(string expression, ICollection<Variable> variables = null)
        {
            if (!string.IsNullOrEmpty(expression)
                && double.TryParse(expression.Replace("+-1", "-1"), 
                                    NumberStyles.Number, 
                                    CultureInfo.CreateSpecificCulture("en-EN"), 
                                    out double result))
                return new MathTryParseResult
                {
                    IsSuccessfulCreated = true,
                    Function = new Number { Value = result }
                };
            else
                return new MathTryParseResult
                {
                    IsSuccessfulCreated = false,
                    ErrorMessage = "This is not number"
                };
        }
    }
}
