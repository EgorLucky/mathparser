using EgorLucky.MathParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgorLucky.MathParser.Functions
{
    public class Exp : IFunction
    {
        public string Name => nameof(Exp);
        public IFunction Argument { get; set; }
        public double ComputeValue(ICollection<Parameter> variables)
        {
            return Math.Exp(Argument.ComputeValue(variables));
        }
    }
}
