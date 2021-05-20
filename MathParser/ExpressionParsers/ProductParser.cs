using EgorLucky.MathParser.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EgorLucky.MathParser.ExpressionParsers
{
    public class ProductParser: IExpressionParser
    {
        private readonly MathParser _mathParser;

        public ProductParser(MathParser mathParser)
        {
            _mathParser = mathParser;
        }

        public string Name => nameof(Product);

        public MathTryParseResult TryParse(string expression, ICollection<Variable> variables = null)
        {
            var result = new Product() 
            { 
                Variables = variables 
            };

            var balance = 0;
            var factor = "";
            var counter = 0;

            var mathTryParseResult = new MathTryParseResult()
            {
                ErrorMessage = "This is not product: " + expression,
                IsSuccessfulCreated = false
            };

            if (!expression.Contains("*") && !expression.StartsWith("-")
                || expression.Last() == '*')
                return mathTryParseResult;

            if (expression.StartsWith("-") && !char.IsDigit(expression[1]))
                expression = "-1*" + expression.Substring(1);

            foreach (var ch in expression)
            {
                counter++;
                if (ch == '(')
                    balance--;
                if (ch == ')')
                    balance++;

                if (ch == '*' && balance == 0)
                {
                    var parseResult = _mathParser.TryParse(factor, variables);
                    if (!parseResult.IsSuccessfulCreated)
                        return parseResult;

                    if(parseResult.Expression is Sum && !Validate.IsExpressionInBrackets(factor))
                        return mathTryParseResult;

                    result.Factors.Add(parseResult.Expression);
                    factor = "";
                    continue;
                }

                factor += ch;
                if (counter == expression.Length)
                {
                    if(result.Factors.Count == 0)
                        return mathTryParseResult;

                    var parseResult = _mathParser.TryParse(factor, variables);
                    if (!parseResult.IsSuccessfulCreated)
                        return parseResult;

                    if (parseResult.Expression is Sum && !Validate.IsExpressionInBrackets(factor))
                        return mathTryParseResult;

                    result.Factors.Add(parseResult.Expression);
                    continue;
                }
            }

            if (result.Factors.Count == 1)
                return mathTryParseResult;

            return new MathTryParseResult
            {
                IsSuccessfulCreated = true,
                Expression = result
            };
        }
    }
}