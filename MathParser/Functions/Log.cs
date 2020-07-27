using System;
using System.Collections.Generic;
using System.Text;

namespace EgorLucky.MathParser.Functions
{
    public class Log : IFunction
    {
        public string Name => nameof(Log);

        public IFunction Base { get; set; }

        public IFunction Argument { get; set; }

        public double ComputeValue(ICollection<Parameter> variables)
        {
            var @base = Base.ComputeValue(variables);

            var argument = Argument.ComputeValue(variables);

            var result = Math.Log(argument, @base);

            return result;
        }
    }
}
