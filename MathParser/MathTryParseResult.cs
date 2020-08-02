namespace EgorLucky.MathParser
{
    public class MathTryParseResult
    {
        public bool IsSuccessfulCreated { get; set;}
        public IExpression Expression { get; set; }
        public string ErrorMessage { get; set; }
    }
}