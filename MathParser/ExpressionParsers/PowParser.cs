using EgorLucky.MathParser.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EgorLucky.MathParser.ExpressionParsers
{
    public class PowParser : IExpressionParser
    {
        private readonly MathParser _mathParser;

        public PowParser(MathParser mathParser)
        {
            _mathParser = mathParser;
        }

        public string Name => nameof(Pow);


        public MathTryParseResult TryParse(string expression, ICollection<Variable> variables = null)
        {
            var mathTryParseResult = new MathTryParseResult()
            {
                ErrorMessage = "This is not pow: " + expression,
                IsSuccessfulCreated = false
            };
            if (!expression.Contains("^") || expression.EndsWith("^"))
                return mathTryParseResult;

            var i = -1;
            foreach (var ch in expression)
            {
                i++;
                if (ch != '^')
                    continue;

                else if (i != 0 && i != expression.Length - 1)
                {
                    var @base = expression.Substring(0, i);
                    var log = expression.Substring(i + 1);
                    if (Validate.IsBracketsAreBalanced(@base) && Validate.IsBracketsAreBalanced(log))
                    {
                        var baseParseResult = _mathParser.TryParse(@base, variables);
                        if (!baseParseResult.IsSuccessfulCreated)
                            return baseParseResult;

                        var bracketTypes = new Type[] { typeof(Sum), typeof(Product)};

                        if (bracketTypes.Contains(baseParseResult.Expression.GetType()) &&
                            !Validate.IsExpressionInBrackets(@base))
                            return mathTryParseResult;

                        var logParseResult = _mathParser.TryParse(log, variables);
                        if (!logParseResult.IsSuccessfulCreated)
                            return mathTryParseResult;

                        if (bracketTypes.Contains(logParseResult.Expression.GetType()) &&
                            !Validate.IsExpressionInBrackets(log))
                            return mathTryParseResult;

                        mathTryParseResult.IsSuccessfulCreated = true;
                        mathTryParseResult.ErrorMessage = "";
                        mathTryParseResult.Expression = new Pow()
                        {
                            Base = baseParseResult.Expression,
                            Log = logParseResult.Expression,
                            Variables = variables
                        };

                        return mathTryParseResult;
                    }
                }
            }

            return mathTryParseResult;
        }
    }
}
