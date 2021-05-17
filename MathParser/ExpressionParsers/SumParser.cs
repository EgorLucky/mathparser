using EgorLucky.MathParser.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EgorLucky.MathParser.ExpressionParsers
{
    public class SumParser : IExpressionParser
    {
        private readonly ProductParser _productFactory;
        private readonly MathParser _mathParser;

        public SumParser(ProductParser productFactory)
        {
            _productFactory = productFactory;
        }

        public SumParser(MathParser mathParser)
        {
            _mathParser = mathParser;
        }

        public string Name => nameof(Sum);

        public MathTryParseResult TryParse(string expression, ICollection<Variable> variables = null)
        {
            var sum = new Sum();

            var balance = 0;
            var term = "";
            var counter = 0;

            var mathTryParseResult = new MathTryParseResult()
            {
                ErrorMessage = "This is not sum: " + expression,
                IsSuccessfulCreated = false
            };

            if (!expression.Contains("+") && !expression.Contains("-") ||
                (expression.Last() == '+' || expression.Last() == '-'))
                return mathTryParseResult;

            foreach (var ch in expression)
            {
                counter++;
                if (ch == '(')
                    balance--;
                if (ch == ')')
                    balance++;
                
                if ((ch == '+' || ch == '-') && balance == 0 && counter != 1)
                {
                    var parseResult = _mathParser.TryParse(term, variables);

                    if (!parseResult.IsSuccessfulCreated)
                        return parseResult;
                    sum.Terms.Add(parseResult.Expression);
                    term = ch == '-'? "-":"";
                    continue;
                }

                term += ch;
                if (counter == expression.Length)
                {
                    if (sum.Terms.Count == 0)
                        return mathTryParseResult;

                    var parseResult = _mathParser.TryParse(term, variables);

                    if (!parseResult.IsSuccessfulCreated)
                        return parseResult;
                    sum.Terms.Add(parseResult.Expression);
                    
                }
            }

            if(sum.Terms.Count == 1)
                return mathTryParseResult;

            sum.Variables = variables;

            mathTryParseResult.IsSuccessfulCreated = true;
            mathTryParseResult.ErrorMessage = "";
            mathTryParseResult.Expression = sum;

            return mathTryParseResult;
        }
    }
}
