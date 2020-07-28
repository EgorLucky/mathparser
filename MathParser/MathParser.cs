using EgorLucky.MathParser;
using EgorLucky.MathParser.Constants;
using EgorLucky.MathParser.FunctionParsers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EgorLucky.MathParser
{
    public class MathParser
    {
        private readonly List<IConst> _constants;
        private readonly List<IMathParserEntity> _mathparserEntities;
        private readonly NumberParser _numberFactory;
        private readonly List<IFunctionParser> _functionParsers;

        public MathParser()
        {
            _functionParsers = new List<IFunctionParser>();

            _functionParsers.Add(new PowParser(this));
            _functionParsers.Add(new FractionParser(this));
            _functionParsers.Add(new SinParser(this));
            _functionParsers.Add(new CosParser(this));
            _functionParsers.Add(new TgParser(this));
            _functionParsers.Add(new CtgParser(this));
            _functionParsers.Add(new ExpParser(this));
            _functionParsers.Add(new SumParser(this));
            _functionParsers.Add(new ProductParser(this));
            _functionParsers.Add(new LogParser(this));
            _numberFactory = new NumberParser();
            _functionParsers.Add(_numberFactory);

            _constants = new List<IConst>();
            _constants.Add(new PI());
            _constants.Add(new E());

            _mathparserEntities = new List<IMathParserEntity>();
            _mathparserEntities.AddRange(_constants);
            _mathparserEntities.AddRange(_functionParsers);
        }

        /// <summary>
        /// Парсинг математического выражения
        /// </summary>
        /// <param name="mathExpression"></param>
        /// <param name="variables"></param>
        /// <returns>MathTryParseResult</returns>
        public MathTryParseResult TryParse(string mathExpression, ICollection<Variable> variables = null)
        {
            if(string.IsNullOrEmpty(mathExpression) || mathExpression.All(ch => char.IsWhiteSpace(ch)))
                return new MathTryParseResult()
                {
                    IsSuccessfulCreated = false,
                    ErrorMessage = $"Empty string in mathExpression"
                };

            //форматирование строки
            mathExpression = mathExpression.Replace(" ", "");
            if (!mathExpression.Contains("+-1*"))
            {
                if(mathExpression.StartsWith("-1*"))
                {
                    var subMathExpression = mathExpression.Substring(3);
                    subMathExpression = subMathExpression.Replace("-", "+-1*");
                    mathExpression = "-1*" + subMathExpression;
                }
                else if(mathExpression != "-1")
                        mathExpression = mathExpression.Replace("-", "+-1*");
            }

            var matchedName = string.Empty;
            
            if(variables != null)
                matchedName = variables.Where(v => _mathparserEntities.Exists(c => c.Name.ToString() == v.Name.ToLower()))
                                        .Select(v => v.Name)
                                        .FirstOrDefault();

            if (!string.IsNullOrEmpty(matchedName))
                return new MathTryParseResult()
                {
                    IsSuccessfulCreated = false,
                    ErrorMessage = $"Wrong name for variable {matchedName}. There is already entity with the same name"
                };


            mathExpression = mathExpression.ToLower();

            if (!Validate.IsBracketsAreBalanced(mathExpression))
                return new MathTryParseResult()
                {
                    IsSuccessfulCreated = false,
                    ErrorMessage = "brackets are not balanced"
                };

            while (Validate.IsExpressionInBrackets(mathExpression))
                mathExpression = mathExpression.Remove(mathExpression.Length - 1, 1)
                                        .Remove(0, 1);

            //начало парсинга

            return TryParseFunction(mathExpression, variables);
        }

        private MathTryParseResult TryParseFunction(string expression, ICollection<Variable> variables)
        {
            foreach(var functionParser in _functionParsers
                                        .OrderBy(f => f.GetType() != typeof(SumParser))
                                        .ThenBy(f => f.GetType() != typeof(ProductParser))
                                        .ThenByDescending(f => f.Name.Length)
                                        .ToList())
            {
                var parseResult = functionParser.TryParse(expression, variables);
                if(parseResult.IsSuccessfulCreated)
                {
                    return parseResult;
                }
            }

            var matchedConstant = _constants
                                    .Where(c => c.Name.ToLower() == expression)
                                    .FirstOrDefault();

            if(matchedConstant != null)
                return new MathTryParseResult
                {
                    IsSuccessfulCreated = true,
                    Function = _numberFactory.Create(matchedConstant.Value)
                };

            if (variables.Any(p => p.Name.ToLower() == expression))
                return new MathTryParseResult
                {
                    IsSuccessfulCreated = true,
                    Function = ParseVariable(expression, variables)
                };
            
            return new MathTryParseResult
            {
                IsSuccessfulCreated = false,
                ErrorMessage = "Unknown function in expression: " + expression
            };
        }

        /// <summary>
        /// Добавление парсера сторонней реализации IFunction 
        /// </summary>
        /// <param name="parser"></param>
        /// <returns></returns>
        public MathParser AddFunctionParser(IFunctionParser parser)
        {
            if (_mathparserEntities.Exists(e => e.Name.ToLower() == parser.Name.ToLower()))
                throw new Exception($"Wrong name for entity {parser.Name}. There is already entity with the same name");
            _functionParsers.Add(parser);
            _mathparserEntities.Add(parser);
            return this;
        }

        /// <summary>
        /// Добавление сторонней реализации IConst 
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public MathParser AddConst(IConst constant)
        {
            if (_mathparserEntities.Exists(e => e.Name.ToLower() == constant.Name.ToLower()))
                throw new Exception($"Wrong name for entity {constant.Name}. There is already entity with the same name");
            _constants.Add(constant);
            _mathparserEntities.Add(constant);
            return this;
        }

        IFunction ParseVariable(string expression, ICollection<Variable> variables)
        {
            var parameter = variables
                            .Where(p => p.Name.ToLower() == expression)
                            .FirstOrDefault();
            return parameter;
        }
    }
}
