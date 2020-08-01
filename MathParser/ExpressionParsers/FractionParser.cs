using EgorLucky.MathParser.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EgorLucky.MathParser.ExpressionParsers
{
    public class FractionParser : IExpressionParser
    {
        private readonly MathParser _mathParser;

        public FractionParser(MathParser mathParser)
        {
            _mathParser = mathParser;
        }

        public string Name => nameof(Fraction);

        public MathTryParseResult TryParse(string expression, ICollection<Variable> variables = null)
        {
            var mathTryParseResult = new MathTryParseResult()
            {
                ErrorMessage = "This is not fraction: " + expression,
                IsSuccessfulCreated = false
            };

            if (!expression.Contains("/") || expression.EndsWith("/"))
                return mathTryParseResult;

            for (var i = expression.Length - 1; i >= 0; i--)
            {
                var ch = expression[i];
                if (ch != '/')
                    continue;

                else if (i != 0 && i != expression.Length - 1)
                {
                    var numerator = expression.Substring(0, i);
                    var denominator = expression.Substring(i + 1);

                    if (Validate.IsBracketsAreBalanced(numerator) &&
                        Validate.IsBracketsAreBalanced(denominator))
                    {
                        var numeratorParseResult = _mathParser.TryParse(numerator, variables);
                        if(!numeratorParseResult.IsSuccessfulCreated)
                            return numeratorParseResult;

                        var denominatorParseResult = _mathParser.TryParse(denominator, variables);
                        if (!denominatorParseResult.IsSuccessfulCreated)
                            return denominatorParseResult;

                        var types = new Type[] { typeof(Sum), typeof(Product) };
                        if (types.Contains(numeratorParseResult.Expression.GetType()) &&
                            !Validate.IsExpressionInBrackets(numerator))
                            return mathTryParseResult;

                        if (types.Contains(denominatorParseResult.Expression.GetType()) &&
                            !Validate.IsExpressionInBrackets(denominator))
                            return mathTryParseResult;


                        mathTryParseResult.IsSuccessfulCreated = true;
                        mathTryParseResult.ErrorMessage = "";
                        mathTryParseResult.Expression = new Fraction()
                        {
                            Numerator = numeratorParseResult.Expression,
                            Denominator = denominatorParseResult.Expression
                        };

                        return mathTryParseResult;
                    }
                }
            }

            return mathTryParseResult;
        }
    }
}
