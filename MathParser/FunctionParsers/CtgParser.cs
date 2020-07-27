using EgorLucky.MathParser.Functions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EgorLucky.MathParser.FunctionParsers
{
    public class CtgParser : IFunctionParser
    {
        private readonly MathParser _mathParser;

        public CtgParser(MathParser mathParser)
        {
            _mathParser = mathParser;
        }
        public string Name => nameof(Ctg);

        public MathTryParseResult TryParse(string expression, ICollection<Variable> variables = null)
        {
            var mathTryParseResult = new MathTryParseResult()
            {
                ErrorMessage = "This is not ctg: " + expression,
                IsSuccessfulCreated = false
            };

            if (!expression.StartsWith("ctg"))
                return mathTryParseResult;

            string argString = expression.Substring(3);

            var parseResult = _mathParser.TryParse(argString, variables);

            if (!parseResult.IsSuccessfulCreated)
                return parseResult;

            var result = new Ctg() { Argument = parseResult.Function };

            mathTryParseResult.IsSuccessfulCreated = true;
            mathTryParseResult.ErrorMessage = "";
            mathTryParseResult.Function = result;

            return mathTryParseResult;
        }
    }
}
