using EgorLucky.MathParser.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EgorLucky.MathParser.FunctionParsers
{
    public class SumParser : IFunctionParser
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

            if (!expression.Contains("+"))
                return mathTryParseResult;

            foreach (var ch in expression)
            {
                counter++;
                if (ch == '(')
                    balance--;
                if (ch == ')')
                    balance++;
                
                if (ch == '+' && balance == 0 && counter != 1)
                {
                    var parseResult = _mathParser.TryParse(term, variables);

                    if (!parseResult.IsSuccessfulCreated)
                        return parseResult;
                    sum.Terms.Add(parseResult.Function);
                    term = "";
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
                    sum.Terms.Add(parseResult.Function);
                    
                }
            }

            if(sum.Terms.Count == 1)
                return mathTryParseResult;

            mathTryParseResult.IsSuccessfulCreated = true;
            mathTryParseResult.ErrorMessage = "";
            mathTryParseResult.Function = sum;

            return mathTryParseResult;
        }
    }
}
