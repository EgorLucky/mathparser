namespace EgorLucky.MathParser
{
    public class MathTryParseResult
    {
        public bool IsSuccessfulCreated { get; set;}

        public IFunction Function { get; set; }

        public string ErrorMessage { get; set; }
    }
}