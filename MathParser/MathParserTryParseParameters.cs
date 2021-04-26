using System.Collections.Generic;

namespace EgorLucky.MathParser
{
    internal class MathParserTryParseParameters
    {
        public MathParserTryParseParameters(string mathExpression, ICollection<Variable> variables)
        {
            MathExpression = mathExpression;
            Variables = variables;
        }

        public string MathExpression { get; set; }

        public ICollection<Variable> Variables { get; set; }
    }
}