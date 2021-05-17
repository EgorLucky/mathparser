using EgorLucky.MathParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgorLucky.MathParser.Expressions
{
    public class Sum : IExpression
    {
        public string Name => nameof(Sum);
        public Sum()
        {
            Terms = new List<IExpression>();
        }
        public List<IExpression> Terms { get; set; }
        public IEnumerable<Variable> Variables { get; set; }
        public double ComputeValue(ICollection<Parameter> variables)
        {
            return Terms.Sum(p => p.ComputeValue(variables));
        }
    }
}
