using System;
using System.Collections.Generic;
using System.Text;

namespace EgorLucky.MathParser
{
    public interface IFunctionParser :IMathParserEntity
    {
        MathTryParseResult TryParse(string expression, ICollection<Variable> variables = null);
    }
}
