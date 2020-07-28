using EgorLucky.MathParser;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests
{
    public class FractionTests
    {
        private readonly MathParser _parser = new MathParser();

        [Fact]
        public void ParseFraction()
        {
            var expression = "1/2/3/4/x";
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
            var expectedResult = 1d/2d/3d/4d/parameter.Value;

            Assert.True(result.IsSuccessfulCreated);
            Assert.Equal(expectedResult, computedResult);
        }
    }
}
