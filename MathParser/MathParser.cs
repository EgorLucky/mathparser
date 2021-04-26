using EgorLucky.MathParser;
using EgorLucky.MathParser.Constants;
using EgorLucky.MathParser.ExpressionParsers;
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
        private readonly List<IExpressionParser> _expressionParsers;

        public MathParser()
        {
            _numberFactory = new NumberParser();
            _expressionParsers = new List<IExpressionParser>()
            {
                new PowParser(this),
                new FractionParser(this),
                new SinParser(this),
                new CosParser(this),
                new TgParser(this),
                new CtgParser(this),
                new ExpParser(this),
                new SumParser(this),
                new ProductParser(this),
                new LogParser(this),
                _numberFactory 
            };

            _constants = new List<IConst>();
            _constants.Add(new PI());
            _constants.Add(new E());

            _mathparserEntities = new List<IMathParserEntity>();
            _mathparserEntities.AddRange(_constants);
            _mathparserEntities.AddRange(_expressionParsers);
        }

        /// <summary>
        /// Парсинг математического выражения
        /// </summary>
        /// <param name="mathExpression"></param>
        /// <param name="variables"></param>
        /// <returns>MathTryParseResult</returns>
        public MathTryParseResult TryParse(string mathExpression, ICollection<Variable> variables = null)
        {
            var defaultResult = new MathTryParseResult();
            defaultResult.IsSuccessfulCreated = false;
            defaultResult.InputString = mathExpression;

            var context = new MathParserTryParseParameters(mathExpression, variables);

            var errorMessage = CheckTryParseParameters(context);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                defaultResult.ErrorMessage = errorMessage;
                return defaultResult;
            }

            var tryParseResult = TryParseExpression(context.MathExpression, context.Variables);
            tryParseResult.InputString = defaultResult.InputString;

            return tryParseResult;
        }

        private string CheckTryParseParameters(MathParserTryParseParameters context)
        {
            var errorMessage = "";
            var mathExpression = context.MathExpression;
            var variables = context.Variables;


            if (string.IsNullOrEmpty(mathExpression) || mathExpression.All(ch => char.IsWhiteSpace(ch)))
            {
                errorMessage = $"Empty string in mathExpression";
            }

            //проверка на корректность имен переменных
            if (variables != null && variables.Any(v => string.IsNullOrEmpty(v.Name)))
            {
                errorMessage = $"Empty variable name";
            }

            if (variables != null && variables.Any(v => v.Name.Any(ch => !char.IsLetter(ch))))
            {
                errorMessage = $"Incorrect variable name";
            }

            //проверка имен переменных на совпадение с именами констант
            var matchedName = string.Empty;
            if (variables != null)
                matchedName = variables.Where(v => _mathparserEntities.Exists(c => c.Name.ToString() == v.Name.ToLower()))
                                        .Select(v => v.Name)
                                        .FirstOrDefault();

            if (!string.IsNullOrEmpty(matchedName))
            {
                errorMessage = $"Wrong name for variable {matchedName}. There is already entity with the same name";
            }


            //форматирование строки
            mathExpression = mathExpression.Replace(" ", "");
            mathExpression = mathExpression.ToLower();

            if (!Validate.IsBracketsAreBalanced(mathExpression))
            {
                errorMessage = "brackets are not balanced";
            }

            while (Validate.IsExpressionInBrackets(mathExpression))
                mathExpression = mathExpression
                                    .Remove(mathExpression.Length - 1, 1)
                                    .Remove(0, 1);

            context.MathExpression = mathExpression;

            if (string.IsNullOrEmpty(mathExpression))
            {
                errorMessage = $"Empty string in mathExpression";
            }

            return errorMessage;
        }

        private MathTryParseResult TryParseExpression(string expression, ICollection<Variable> variables)
        {
            foreach(var expressionParser in _expressionParsers
                                        .OrderByDescending(f => f is SumParser)
                                        .ThenByDescending(f => f is ProductParser)
                                        .ThenByDescending(f => f.Name.Length)
                                        .ToList())
            {
                var parseResult = expressionParser.TryParse(expression, variables);
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
                    Expression = _numberFactory.Create(matchedConstant.Value)
                };

            if (variables != null && variables.Any(p => p.Name.ToLower() == expression))
                return new MathTryParseResult
                {
                    IsSuccessfulCreated = true,
                    Expression = ParseVariable(expression, variables)
                };
            
            return new MathTryParseResult
            {
                IsSuccessfulCreated = false,
                ErrorMessage = "Unknown Expression in expression: " + expression
            };
        }

        /// <summary>
        /// Добавление парсера сторонней реализации IFunction 
        /// </summary>
        /// <param name="parser"></param>
        /// <returns></returns>
        public MathParser AddFunctionParser<Function>(FunctionParser<Function> parser) where Function:IFunction, new()
        {
            if (_mathparserEntities.Exists(e => e.Name.ToLower() == parser.Name.ToLower()))
                throw new Exception($"Wrong name for entity {parser.Name}. There is already entity with the same name");
            _expressionParsers.Add(parser);
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

        IExpression ParseVariable(string expression, ICollection<Variable> variables)
        {
            var parameter = variables
                            .Where(p => p.Name.ToLower() == expression)
                            .FirstOrDefault();
            return parameter;
        }
    }
}
