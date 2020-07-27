using System;

namespace EgorLucky.MathParser.Constants
{
    public class PI : IConst
    {
        public string Name => "pi";

        public double Value => Math.PI;
    }
}
