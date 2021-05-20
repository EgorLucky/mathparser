using EgorLucky.MathParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgorLucky.MathParser.Expressions
{
    public record Exp : IFunction
    {
        public string Name => nameof(Exp);
        public IExpression Argument => Arguments.FirstOrDefault();
        public ICollection<IExpression> Arguments { get; init; }
        public IEnumerable<Variable> Variables { get; init; }
        public double ComputeValue(ICollection<Parameter> variables)
        {
            return Math.Exp(Argument.ComputeValue(variables));
        }
    }
}
