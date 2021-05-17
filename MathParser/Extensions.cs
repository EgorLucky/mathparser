using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgorLucky.MathParser
{
    public static class Extensions
    {
        public static MathTryParseResult TryParse(this MathParser parser, string mathExpression, params string[] variableNames)
        {
            var variablesObjects = variableNames
                .Select(v => new Variable { Name = v })
                .ToList();
            return parser.TryParse(mathExpression, variablesObjects);
        }


        public static double ComputeValue(this IExpression expression, params double[] parameterValues)
        {
            if (parameterValues.Length != expression.Variables.Count())
                throw new Exception("Parameter values count is not equal to expression variables count.");

            var parameters = new List<Parameter>();

            if (expression.Variables != null)
            {
                var index = 0;
                foreach (var variable in expression.Variables)
                {
                    var parameter = new Parameter()
                    {
                        VariableName = variable.Name,
                        Value = parameterValues[index]
                    };

                    parameters.Add(parameter);

                    index++;
                }
            }

            var result = expression.ComputeValue(parameters);

            return result;
        }
    }
}
