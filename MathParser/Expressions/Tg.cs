using EgorLucky.MathParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgorLucky.MathParser.Expressions
{
    public class Tg : IFunction
    {
        public string Name => nameof(Tg);
        public IExpression Argument => Arguments.FirstOrDefault();
        public ICollection<IExpression> Arguments { get; set; }
        public IEnumerable<Variable> Variables { get; set; }
        public double ComputeValue(ICollection<Parameter> variables)
        {
            return Math.Tan(Argument.ComputeValue(variables));
        }
    }
}
