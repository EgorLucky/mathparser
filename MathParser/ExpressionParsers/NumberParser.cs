using EgorLucky.MathParser.Expressions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EgorLucky.MathParser.ExpressionParsers
{
    public class NumberParser : IExpressionParser
    {
        public string Name => nameof(Number);

        public IExpression Create(double value)
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
                    Expression = new Number { Value = result }
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
