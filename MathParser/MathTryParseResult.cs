namespace EgorLucky.MathParser
{
    public record MathTryParseResult
    {
        public bool IsSuccessfulCreated { get; init;}
        public IExpression Expression { get; init; }
        public string ErrorMessage { get; init; }
        public string InputString { get; init; }
    }
}