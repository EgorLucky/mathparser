using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EgorLucky.MathParser
{
    public record Variable(string Name = "") : IExpression
    {
        public IEnumerable<Variable> Variables { get; init; }

        public double ComputeValue(ICollection<Parameter> parameters)
        {
            return parameters.Where(p => p.VariableName == this.Name)
                            .Select(p => p.Value)
                            .FirstOrDefault();
        }
    }
}
