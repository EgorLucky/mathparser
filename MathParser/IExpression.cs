using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgorLucky.MathParser
{
    public interface IExpression
    {
        string Name { get; }
        IEnumerable<Variable> Variables { get; set; }

        double ComputeValue(ICollection<Parameter> variables);
    }
}
