using System;
using System.Collections.Generic;
using System.Text;

namespace EgorLucky.MathParser
{
    /// <summary>
    /// Элемент в коллекции класса MathParser
    /// </summary>
    public interface IMathParserEntity
    {
        /// <summary>
        /// Уникальное имя элемента, 
        /// например у одного экземпляра MathParser 
        /// может быть только один элемент с именем "Cos",
        /// то есть одна функция косинуса
        /// </summary>
        string Name { get; }
    }
}
