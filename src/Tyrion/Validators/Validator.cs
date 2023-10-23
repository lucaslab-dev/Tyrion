using FluentValidation;
using System.Threading.Tasks;

namespace Tyrion
{
    public abstract class Validator<T> : AbstractValidator<T> where T : IRequest
    {
        private string Message { get; set; }

        public new IResult Validate(T instance)
        {
            if (Equals(instance, default(T)))
            {
                return Result.Fail(Message ?? string.Empty);
            }

            var result = base.Validate(instance);

            return result.IsValid ? Result.Success() : Result.Fail(Message ?? result.ToString());
        }

        public Task<IResult> ValidateAsync(T instance) => Task.FromResult(Validate(instance));

        public void CustomMessage(string message) => Message = message;
    }
}