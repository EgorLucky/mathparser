using EgorLucky.MathParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class ExtensionsTests
    {
        private readonly MathParser _parser = new MathParser();
        [Fact]
        public void ExtensionTest()
        {
            var expression = "x+y+2*z";
            var x = 0;
            var y = 20;
            var z = 1.1;

            var result = _parser.TryParse(expression, "x","y", "z");

            var computedResult = 0d;
            if (result.IsSuccessfulCreated)
                computedResult = result.Expression.ComputeValue(x, y, z);

            var expectedResult = x + y + 2 * z;

            Assert.True(result.IsSuccessfulCreated);
            Assert.Equal("Sum", result.Expression.Name);
            Assert.Equal(expectedResult, computedResult);
        }
    }
}
