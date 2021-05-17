using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EgorLucky.MathParser.Expressions
{
    public class Log : IFunction
    {
        public string Name => nameof(Log);

        public IExpression Base => Arguments.FirstOrDefault();
        public IExpression Argument => Arguments.LastOrDefault();
        public ICollection<IExpression> Arguments { get; set; }
        public IEnumerable<Variable> Variables { get; set; }

        public double ComputeValue(ICollection<Parameter> variables)
        {
            var @base = Base.ComputeValue(variables);

            var argument = Argument.ComputeValue(variables);

            var result = Math.Log(argument, @base);

            return result;
        }
    }
}
