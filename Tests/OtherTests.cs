﻿using EgorLucky.MathParser;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests
{
    public class OtherTests
    {
        private readonly MathParser _parser = new MathParser();

        [Fact]
        public void ParseManyBrackets()
        {
            var expression = "(((tg(x)^2)))";
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
                computedResult = result.Expression.ComputeValue(new List<Parameter>() { parameter });
            var expectedResult = Math.Pow(Math.Tan(parameter.Value), 2);

            Assert.True(result.IsSuccessfulCreated);
            Assert.Equal("Pow", result.Expression.Name);
            Assert.Equal(expectedResult, computedResult);
        }

        [Fact]
        public void ParseEmptyWithManyBrackets()
        {
            var expression = "((()))";
            
            var result = _parser.TryParse(expression);

            Assert.False(result.IsSuccessfulCreated);
        }
    }
}
