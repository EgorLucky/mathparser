using EgorLucky.MathParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgorLucky.MathParser.Expressions
{
    public record Pow : IExpression
    {
        public string Name => nameof(Pow);
        public IExpression Log { get; init; }
        public IExpression Base { get; init; }
        public IEnumerable<Variable> Variables { get; init; }
        public double ComputeValue(ICollection<Parameter> variables)
        {
            var @base = Base.ComputeValue(variables);
            var log = Log.ComputeValue(variables);

            return Math.Pow(@base, log);
        }
    }
}
