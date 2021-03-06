using EgorLucky.MathParser;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests
{
    public class SumTests
    {
        private readonly MathParser _parser = new MathParser();

        [Fact]
        public void ParseSum()
        {
            var expression = "2 + 0.5 + 2.5*cos(pi) - log(2, 8) + sin(x) + tg(x)^2";
            var parameter = new Parameter("x", 0);

            var variables = new List<Variable>()
            {
                parameter.GetVariable()
            };

            var result = _parser.TryParse(expression, variables);

            var computedResult = 0d;
            if(result.IsSuccessfulCreated)
                computedResult = result.Expression.ComputeValue(new List<Parameter>() { parameter });

            var expectedResult = 2 + 0.5 + 2.5 * Math.Cos(Math.PI) - Math.Log(8, 2) + Math.Sin(parameter.Value) + Math.Pow(Math.Tan(parameter.Value), 2);

            Assert.True(result.IsSuccessfulCreated);
            Assert.Equal("Sum", result.Expression.Name);
            Assert.Equal(expectedResult, computedResult);
        }

        [Fact]
        public void ParseSumWithManyPlusesInTheEnd()
        {
            var expression = "2 + 0.5 + 2.5*cos(pi) - log(2, 8) + sin(x) + tg(x)^2++++++";
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

        [Fact]
        public void ParseSumWithMany_F_LettersInTheEnd()
        {
            var expression = "2 + 0.5 + 2.5*cos(pi) - log(2, 8) + sin(x) + tg(x)^2ffffffff";
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

        [Fact]
        public void ParseSumWithUnexistExpression()
        {
            var expression = "2 + 0.5 + 2.5*cos(pi) - log(2, 8) + sin(x) + unexistng(x)^2";
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
