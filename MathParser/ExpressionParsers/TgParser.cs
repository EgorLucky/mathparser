using EgorLucky.MathParser;
using EgorLucky.MathParser.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EgorLucky.MathParser.ExpressionParsers
{
    public class TgParser : FunctionParser<Tg>
    {
        public TgParser(MathParser mathParser) : base(nameof(Tg), 1, mathParser)
        {
        }
    }
}
