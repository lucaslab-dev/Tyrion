using FluentValidation;
using Tyrion.Validators;

namespace Tyrion.Tests
{
    public sealed class StuffCommandValidator : Validator<StuffCommand>
    {
        public StuffCommandValidator()
        {
            RuleFor(x => x.MyProperty).NotEmpty();
        }
    }
}