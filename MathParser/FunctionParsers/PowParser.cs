﻿using MathParserClasses;
using MathParserClasses.Functions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.FunctionParsers
{
    public class PowParser : IFunctionParser
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
            if (!expression.Contains("^"))
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

                        var logParseResult = _mathParser.TryParse(log, variables);
                        if (!logParseResult.IsSuccessfulCreated)
                            return logParseResult;

                        mathTryParseResult.IsSuccessfulCreated = true;
                        mathTryParseResult.ErrorMessage = "";
                        mathTryParseResult.Function = new Pow()
                        {
                            Base = baseParseResult.Function,
                            Log = logParseResult.Function
                        };

                        return mathTryParseResult;
                    }
                }
            }

            return mathTryParseResult;
        }
    }
}
