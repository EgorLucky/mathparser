using EgorLucky.MathParser.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EgorLucky.MathParser.ExpressionParsers
{
    public class CtgParser : FunctionParser<Ctg>
    {
        public CtgParser(MathParser mathParser) : base(nameof(Ctg), 1, mathParser)
        {
        }
    }
}
