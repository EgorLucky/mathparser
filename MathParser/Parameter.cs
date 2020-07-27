namespace EgorLucky.MathParser
{
    public class Parameter
    {
        public string VariableName { get; set; }

        public double Value { get; set; }

        public Variable GetVariable()
        { 
            return new Variable { Name = VariableName };
        }
    }
}