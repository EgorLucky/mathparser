using EgorLucky.MathParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgorLucky.MathParser.Functions
{
    public class Product : IFunction
    {
        public string Name => nameof(Product);
        public Product()
        {
            Factors = new List<IFunction>();
        }
        public List<IFunction> Factors { get; set; }
        public double ComputeValue(ICollection<Parameter> variables)
        {
            var result = 1.0;
            Factors.ForEach(s => result *= s.ComputeValue(variables));
            return result;
        }
    }
}
