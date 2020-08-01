using EgorLucky.MathParser.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EgorLucky.MathParser.ExpressionParsers
{
    public class CosParser : FunctionParser<Cos>
    {
        public CosParser(MathParser mathParser):base(nameof(Cos), 1, mathParser)
        {
        }
    }
}
