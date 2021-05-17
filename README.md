# MathParser

MathParser позволяет парсить строки с математическими выражениями и вычислять их.

Простой пример:

```cs
var parser = new MathParser();

var expression = "x+y+2*z";
var parseResult = parser.TryParse(expression, "x", "y", "z");

if (parseResult.IsSuccessfulCreated)
{
    var x = 0;
    var y = 20;
    var z = 1.1;
    
    var result = parseResult.Expression.ComputeValue(x, y, z);
    //выведет 22.2
    Console.WriteLine(result);
}
else
{
    Console.WriteLine(parseResult.ErrorMessage);
}
```
Пример посложнее:
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

### Примеры выражений:
- (1+2*cos(pi/2))^2
- (x-y)*(cos(0))
- 2 + 0.5 + 2.5*cos(pi) - log(2, 8) + sin(x) + tg(x)^2

[Nuget](https://www.nuget.org/packages/EgorLucky.MathParser)

[WebApi-сервис]( https://github.com/EgorLucky/mathparser-rest-service)

[Задеплоено здесь](https://mathparser.herokuapp.com/swagger) (хостится на бесплатном тарифе, поэтому может тормозить)

[Фронт, в котором можно потыкать сервис](https://github.com/EgorLucky/mathparser-front) 

[Задеплоено здесь](http://mathparser-front.herokuapp.com) (тоже хостится на бесплатном тарифе, поэтому может тормозить)

