using EgorLucky.MathParser;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests
{
    public class ProductTests
    {
        private readonly MathParser _parser = new MathParser();

        [Fact]
        public void ParseProduct()
        {
            var expression = "2 * 0.5 * -2.5*cos(pi)* tg(x)^2 * log(-2, -8) * sin(x)*(2+3)*(cos(0)+sin(0))*(2*(x+1))";
            var parameter = new Parameter
            {
                VariableName = "x",
                Value = 1
            };

            var variables = new List<Variable>()
            {
                parameter.GetVariable()
            };

            var result = _parser.TryParse(expression, variables);

            var computedResult = 0d;
            if(result.IsSuccessfulCreated)
                computedResult = result.Expression.ComputeValue(new List<Parameter>() { parameter });

            var expectedResult = 2 * 0.5 * -2.5 
                                    * Math.Cos(Math.PI) 
                                    * Math.Pow(Math.Tan(parameter.Value), 2) 
                                    * Math.Log(-8, -2) 
                                    * Math.Sin(parameter.Value)
                                    * (2+3)
                                    * (Math.Cos(0) + Math.Sin(0))
                                    * (2 * (parameter.Value + 1));

            
            Assert.True(result.IsSuccessfulCreated);
            Assert.Equal("Product", result.Expression.Name);
            Assert.Equal(expectedResult, computedResult);
        }

        [Fact]
        public void ParseProductWithUnexistFunction()
        {
            var expression = "2 * 0.5 * 2.5*kek(pi)* tg(x)^2 * log(2, 8) * sin(x) ";
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

            Assert.False(result.IsSuccessfulCreated);
        }

    }
}
