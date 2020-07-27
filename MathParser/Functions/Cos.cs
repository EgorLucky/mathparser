using EgorLucky.MathParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgorLucky.MathParser.Functions
{
    public class Cos : IFunction
    {
        public string Name => nameof(Cos);
        public IFunction Argument { get; set; }
        public double ComputeValue(ICollection<Parameter> variables)
        {
            return Math.Cos(Argument.ComputeValue(variables));
        }
    }
}
