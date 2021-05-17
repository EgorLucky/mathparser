using EgorLucky.MathParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgorLucky.MathParser.Expressions
{
    public class Ctg : IFunction
    {
        public string Name => nameof(Ctg);
        public IExpression Argument => Arguments.FirstOrDefault();
        public ICollection<IExpression> Arguments { get; set; }
        public IEnumerable<Variable> Variables { get; set; }

        public double ComputeValue(ICollection<Parameter> variables)
        {
            return 1.0 / Math.Tan(Argument.ComputeValue(variables));
        }
    }
}
