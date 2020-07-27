using EgorLucky.MathParser.Functions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EgorLucky.MathParser.FunctionParsers
{
    public class LogParser : IFunctionParser
    {
        private readonly MathParser _mathParser;
        public LogParser(MathParser mathParser)
        {
            _mathParser = mathParser;
        }

        public string Name => nameof(Log);

        public MathTryParseResult TryParse(string expression, ICollection<Variable> variables = null)
        {
            var mathTryParseResult = new MathTryParseResult()
            {
                ErrorMessage = "This is not log: " + expression,
                IsSuccessfulCreated = false
            };

            if (!expression.StartsWith("log"))
                return mathTryParseResult;

            var argsString = expression.Substring(3);

            if (!Validate.IsExpressionInBrackets(argsString))
            {
                mathTryParseResult.ErrorMessage = "Incorrect arguments in log";
                return mathTryParseResult; 
            }

            argsString = argsString.Remove(0, 1);
            argsString = argsString.Remove(argsString.Length - 1, 1);

            var argStrings = argsString.Split(",");

            if(argStrings.Length != 2)
            {
                mathTryParseResult.ErrorMessage = "Wrong number of arguments in log";
                return mathTryParseResult;
            }

            var baseParseResult = _mathParser.TryParse(argStrings[0], variables);

            if (!baseParseResult.IsSuccessfulCreated)
                return baseParseResult;

            var argumentParseResult = _mathParser.TryParse(argStrings[1], variables);

            if (!argumentParseResult.IsSuccessfulCreated)
                return argumentParseResult;

            var result = new Log() 
            { 
                Argument = argumentParseResult.Function,
                Base = baseParseResult.Function
            };

            mathTryParseResult.IsSuccessfulCreated = true;
            mathTryParseResult.ErrorMessage = "";
            mathTryParseResult.Function = result;

            return mathTryParseResult;
        }
    }
}
