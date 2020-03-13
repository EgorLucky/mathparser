﻿using MathParserClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathParser
{
    public class Variable : IFunction
    {
        public string Name { get; set; }

        public double ComputeValue(ICollection<Parameter> parameters)
        {
            return parameters.Where(p => p.VariableName == this.Name)
                            .Select(p => p.Value)
                            .FirstOrDefault();
        }
    }
}
