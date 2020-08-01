using System;
using System.Collections.Generic;
using System.Text;

namespace EgorLucky.MathParser.ExpressionParsers
{
    public class FunctionParser<Function> : IExpressionParser where Function : IFunction, new()
    {
        /// <summary>
        /// Число аргументов
        /// </summary>
        private readonly int _argCount;
        private readonly MathParser _mathParser;
        public FunctionParser(string functionName, int argCount, MathParser mathParser)
        {
            if (string.IsNullOrEmpty(functionName) || argCount == 0)
                throw new ArgumentException();

            Name = functionName.ToLower();
            _mathParser = mathParser;
            _argCount = argCount;
        }

        public string Name { get; }

        public MathTryParseResult TryParse(string expression, ICollection<Variable> variables = null)
        {
            var mathTryParseResult = new MathTryParseResult()
            {
                ErrorMessage = $"This is not {Name}: " + expression,
                IsSuccessfulCreated = false
            };

            if (!expression.StartsWith(Name))
                return mathTryParseResult;

            var argsString = expression.Substring(Name.Length);

            if (_argCount > 1 && !Validate.IsExpressionInBrackets(argsString))
            {
                mathTryParseResult.ErrorMessage = $"Incorrect arguments in {Name}";
                return mathTryParseResult;
            }

            if(_argCount > 1)
            {
                argsString = argsString.Remove(0, 1);
                argsString = argsString.Remove(argsString.Length - 1, 1);
            }

            var arguments = new List<IExpression>();

            var balance = 0;
            var argumentString = "";
            var counter = 0;

            if (!expression.Contains(",") && _argCount != 1
                || expression.EndsWith(","))
                return mathTryParseResult;

            foreach (var ch in argsString)
            {
                counter++;
                if (ch == '(')
                    balance--;
                if (ch == ')')
                    balance++;

                if (ch == ',' && balance == 0 && counter != 1)
                {
                    var parseResult = _mathParser.TryParse(argumentString, variables);

                    if (!parseResult.IsSuccessfulCreated)
                        return parseResult;
                    arguments.Add(parseResult.Expression);
                    argumentString = "";
                    continue;
                }

                argumentString += ch;
                if (counter == argsString.Length)
                {
                    var parseResult = _mathParser.TryParse(argumentString, variables);

                    if (!parseResult.IsSuccessfulCreated)
                        return parseResult;
                    arguments.Add(parseResult.Expression);
                }
            }

            if (arguments.Count != _argCount)
                return mathTryParseResult;

            var result = new Function() { Arguments = arguments };

            mathTryParseResult.IsSuccessfulCreated = true;
            mathTryParseResult.ErrorMessage = "";
            mathTryParseResult.Expression = result;

            return mathTryParseResult;
        }
    }
}
