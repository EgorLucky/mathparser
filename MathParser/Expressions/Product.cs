using EgorLucky.MathParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgorLucky.MathParser.Expressions
{
    public record Product : IExpression
    {
        public string Name => nameof(Product);
        public Product()
        {
            Factors = new List<IExpression>();
        }
        public List<IExpression> Factors { get; init; }
        public IEnumerable<Variable> Variables { get; init; }
        public double ComputeValue(ICollection<Parameter> variables)
        {
            var result = 1.0;
            Factors.ForEach(s => result *= s.ComputeValue(variables));
            return result;
        }
    }
}
