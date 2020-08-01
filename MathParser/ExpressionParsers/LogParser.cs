using EgorLucky.MathParser.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EgorLucky.MathParser.ExpressionParsers
{
    public class LogParser : FunctionParser<Log>
    {
        public LogParser(MathParser mathParser) : base(nameof(Log), 2, mathParser)
        {
        }
    }
}
