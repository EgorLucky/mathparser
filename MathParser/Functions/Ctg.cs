using EgorLucky.MathParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgorLucky.MathParser.Functions
{
    public class Ctg : IFunction
    {
        public string Name => nameof(Ctg);
        public IFunction Argument { get; set; }
        public double ComputeValue(ICollection<Parameter> variables)
        {
            return 1.0 / Math.Tan(Argument.ComputeValue(variables));
        }
    }
}
