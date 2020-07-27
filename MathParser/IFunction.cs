using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgorLucky.MathParser
{
    public interface IFunction
    {
        string Name { get; }
        double ComputeValue(ICollection<Parameter> variables);
    }
}
