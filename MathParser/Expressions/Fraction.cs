using EgorLucky.MathParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgorLucky.MathParser.Expressions
{
    record Fraction: IExpression
    {
        public string Name => nameof(Fraction);
        public IExpression Numerator { get; init; }
        public IExpression Denominator { get; init; }
        public IEnumerable<Variable> Variables { get ; init; }

        public double ComputeValue(ICollection<Parameter> variables)
        {
            return Numerator.ComputeValue(variables) / Denominator.ComputeValue(variables);
        }

    }
}
