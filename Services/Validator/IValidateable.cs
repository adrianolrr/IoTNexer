using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTNexerTest.Validator
{
    public interface IValidateable<T> where T : class
    {
        AbstractValidator<T> Validator { get; }
    }
}
