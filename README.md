# MathParser

MathParser позволяет вычислять математические выражения. Пример использования:

```cs
var parser = new MathParser();

var expression = "2 + 2*x";
var variable = new Variable()
{
    Name = "x"
};
var variables = new List<Variable>();
variables.Add(variable);

var parseResult = parser.TryParse(expression, variables);

if (parseResult.IsSuccessfulCreated)
{
    var parameter = new Parameter
    {
        VariableName = "x",
        Value = 2
    };

    var parameters = new List<Parameter>();
    parameters.Add(parameter);
    var result = parseResult.Expression.ComputeValue(parameters);

    //выведет 6
    Console.WriteLine(result);
}
else
{
    Console.WriteLine(parseResult.ErrorMessage);
}
```
### Поддерживаемые операторы:
- "+"
- "-"
- "/"
- "*"

### Поддерживаемые функции:
- ^ - возведение в степень;
- e - число e;
- pi - число "пи";
- exp(n) - e^n
- cos(x)
- sin(x)
- tg(x)
- ctg(x)
- log(a, b), где a - основание логарифма

### Ппримеры выражений:
- (1+2*cos(pi/2))^2
- (x-y)*(cos(0))
- 2 + 0.5 + 2.5*cos(pi) - log(2, 8) + sin(x) + tg(x)^2

Nuget: https://www.nuget.org/packages/EgorLucky.MathParser

WebApi-сервис, использующий библиотеку, можно посмотреть здесь: https://github.com/EgorLucky/mathparser-rest-service

Задеплоено здесь: https://mathparser.herokuapp.com/swagger

Фронт, в котором можно потыкать сервис, можно посмотреть здесь:https://github.com/EgorLucky/mathparser-front

Задеплоено здесь: http://mathparser-front.herokuapp.com
