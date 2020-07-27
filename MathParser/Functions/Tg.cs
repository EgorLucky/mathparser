using EgorLucky.MathParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgorLucky.MathParser.Functions
{
    public class Tg : IFunction
    {
        public string Name => nameof(Tg);
        public IFunction Argument { get; set; }
        public double ComputeValue(ICollection<Parameter> variables)
        {
            return Math.Tan(Argument.ComputeValue(variables));
        }
    }
}
