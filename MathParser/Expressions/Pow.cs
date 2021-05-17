using EgorLucky.MathParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgorLucky.MathParser.Expressions
{
    public class Pow : IExpression
    {
        public string Name => nameof(Pow);
        public IExpression Log { get; set; }
        public IExpression Base { get; set; }
        public IEnumerable<Variable> Variables { get; set; }
        public double ComputeValue(ICollection<Parameter> variables)
        {
            var @base = Base.ComputeValue(variables);
            var log = Log.ComputeValue(variables);

            return Math.Pow(@base, log);
        }
    }
}
