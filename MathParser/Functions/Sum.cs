using EgorLucky.MathParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgorLucky.MathParser.Functions
{
    public class Sum : IFunction
    {
        public string Name => nameof(Sum);
        public Sum()
        {
            Terms = new List<IFunction>();
        }
        public List<IFunction> Terms { get; set; }
        public double ComputeValue(ICollection<Parameter> variables)
        {
            return Terms.Sum(p => p.ComputeValue(variables));
        }
    }
}
