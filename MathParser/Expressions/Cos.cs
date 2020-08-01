using EgorLucky.MathParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgorLucky.MathParser.Expressions
{
    public class Cos : IFunction
    {
        public string Name => nameof(Cos);
        public IExpression Argument => Arguments.FirstOrDefault();
        public ICollection<IExpression> Arguments { get; set; }

        public double ComputeValue(ICollection<Parameter> variables)
        {
            return Math.Cos(Argument.ComputeValue(variables));
        }
    }
}
