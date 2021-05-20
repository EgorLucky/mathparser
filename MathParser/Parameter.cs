namespace EgorLucky.MathParser
{
    public record Parameter(string VariableName = "", double Value = 0)
    {
        public Variable GetVariable()
        { 
            return new Variable(VariableName);
        }
    }
}