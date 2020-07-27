using EgorLucky.MathParser.Functions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EgorLucky.MathParser.FunctionParsers
{
    public class ProductParser: IFunctionParser
    {
        private readonly MathParser _mathParser;

        public ProductParser(MathParser mathParser)
        {
            _mathParser = mathParser;
        }

        public string Name => nameof(Product);

        public MathTryParseResult TryParse(string expression, ICollection<Variable> variables = null)
        {
            var result = new Product();
            var balance = 0;
            var factor = "";
            var counter = 0;

            var mathTryParseResult = new MathTryParseResult()
            {
                ErrorMessage = "This is not product: " + expression,
                IsSuccessfulCreated = false
            };

            if (!expression.Contains("*"))
                return mathTryParseResult;

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

                    if(parseResult.Function.GetType() == typeof(Sum) && !Validate.IsExpressionInBrackets(factor))
                        return mathTryParseResult;

                    result.Factors.Add(parseResult.Function);
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

                    if (parseResult.Function.GetType() == typeof(Sum) && !Validate.IsExpressionInBrackets(factor))
                        return mathTryParseResult;

                    result.Factors.Add(parseResult.Function);
                    continue;
                }
            }

            if (result.Factors.Count == 1)
                return mathTryParseResult;

            mathTryParseResult.IsSuccessfulCreated = true;
            mathTryParseResult.ErrorMessage = "";
            mathTryParseResult.Function = result;

            return mathTryParseResult;
        }
    }
}