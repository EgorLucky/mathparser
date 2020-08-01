using System;
using System.Collections.Generic;
using System.Text;

namespace EgorLucky.MathParser
{
    public interface IExpressionParser :IMathParserEntity
    {
        MathTryParseResult TryParse(string expression, ICollection<Variable> variables = null);
    }
}
