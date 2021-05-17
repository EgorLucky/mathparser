using EgorLucky.MathParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgorLucky.MathParser.Expressions
{
    class Fraction: IExpression
    {
        public string Name => nameof(Fraction);
        public IExpression Numerator { get; set; }
        public IExpression Denominator { get; set; }
        public IEnumerable<Variable> Variables { get ; set; }

        public double ComputeValue(ICollection<Parameter> variables)
        {
            return Numerator.ComputeValue(variables) / Denominator.ComputeValue(variables);
        }

    }
}
