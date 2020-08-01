using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EgorLucky.MathParser
{
    public class Variable : IExpression
    {
        public string Name { get; set; }

        public double ComputeValue(ICollection<Parameter> parameters)
        {
            return parameters.Where(p => p.VariableName == this.Name)
                            .Select(p => p.Value)
                            .FirstOrDefault();
        }
    }
}
