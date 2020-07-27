using EgorLucky.MathParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgorLucky.MathParser.Functions
{
    public class Pow : IFunction
    {
        public string Name => nameof(Pow);
        public IFunction Log { get; set; }
        public IFunction Base { get; set; }
        public double ComputeValue(ICollection<Parameter> variables)
        {
            var @base = Base.ComputeValue(variables);
            var log = Log.ComputeValue(variables);

            return Math.Pow(@base, log);
        }
    }
}
