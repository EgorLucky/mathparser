using EgorLucky.MathParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgorLucky.MathParser.Expressions
{
    public record Number : IExpression
    {
        public string Name => nameof(Number);
        public double Value { get; init; }
        public IEnumerable<Variable> Variables { get; init; }
        public double ComputeValue(ICollection<Parameter> variables)
        {
            return Value;
        }
    }
}
