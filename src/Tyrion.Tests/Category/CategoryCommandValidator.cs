using FluentValidation;

namespace Tyrion.Tests
{
    public sealed class CategoryCommandValidator : Validator<CategoryCommand>
    {
        public CategoryCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}