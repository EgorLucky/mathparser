using System;
using System.Collections.Generic;
using System.Text;

namespace EgorLucky.MathParser
{
    public interface IFunction: IExpression
    {
        ICollection<IExpression> Arguments { get; set; }
    }
}
