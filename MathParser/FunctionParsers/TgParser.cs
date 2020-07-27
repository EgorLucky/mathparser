using EgorLucky.MathParser;
using EgorLucky.MathParser.Functions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EgorLucky.MathParser.FunctionParsers
{
    public class TgParser : IFunctionParser
    {
        private readonly MathParser _mathParser;

        public TgParser(MathParser mathParser)
        {
            _mathParser = mathParser;
        }

        public string Name => nameof(Tg);

        public MathTryParseResult TryParse(string expression, ICollection<Variable> variables = null)
        {
            var mathTryParseResult = new MathTryParseResult()
            {
                ErrorMessage = "This is not tg: " + expression,
                IsSuccessfulCreated = false
            };

            if (!expression.StartsWith("tg"))
                return mathTryParseResult;

            string argString = expression.Substring(2);

            var parseResult = _mathParser.TryParse(argString, variables);

            if (!parseResult.IsSuccessfulCreated)
                return parseResult;

            var result = new Tg() { Argument = parseResult.Function };

            mathTryParseResult.IsSuccessfulCreated = true;
            mathTryParseResult.ErrorMessage = "";
            mathTryParseResult.Function = result;

            return mathTryParseResult;
        }
    }
}
