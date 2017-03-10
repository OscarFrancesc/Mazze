using Domain.Operations;
using System;

namespace Infraestructure.Operations
{
    public class GenerateNumber : Random, IGenerateNumber
    {
        public GenerateNumber() : base() { }
    }
}
