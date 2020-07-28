using EgorLucky.MathParser;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests
{
    public class PowTests
    {
        private readonly MathParser _parser = new MathParser();
        [Fact]
        public void ParsTgSquared()
        {
            var expression = "tg(x)^2";
            var parameter = new Parameter
            {
                VariableName = "x",
                Value = 0
            };

            var variables = new List<Variable>()
            {
                parameter.GetVariable()
            };

            var result = _parser.TryParse(expression, variables);

            var computedResult = 0d;
            if (result.IsSuccessfulCreated)
                computedResult = result.Function.ComputeValue(new List<Parameter>() { parameter });
            var expectedResult = Math.Pow(Math.Tan(parameter.Value), 2);

            Assert.True(result.IsSuccessfulCreated);
            Assert.Equal(expectedResult, computedResult);
        }
    }
}
