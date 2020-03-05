using FluentValidation;
using System.Threading.Tasks;
using Tyrion.Results;

namespace Tyrion.Validators
{
    public abstract class Validator<T> : AbstractValidator<T>
    {
        private string Message { get; set; }

        public new IResult<T> Validate(T instance)
        {
            if (object.Equals(instance, default(T)))
            {
                return Result<T>.Failed(Message ?? string.Empty);
            }

            var result = base.Validate(instance);

            if (result.IsValid)
            {
                return Result<T>.Successed();
            }

            return Result<T>.Failed(Message ?? result.ToString());
        }

        public Task<IResult<T>> ValidateAsync(T instance) => Task.FromResult(Validate(instance));

        public void CustomMessage(string message) => Message = message;
    }
}