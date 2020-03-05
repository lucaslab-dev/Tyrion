using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tyrion.Handlers;
using Tyrion.Results;
using Tyrion.Validators;

namespace Tyrion
{
    public sealed class Tyrion : ITyrion
    {
        private static string ValidatorTypeName => typeof(Validator<>).Name;
        private readonly IServiceProvider _serviceProvider;

        public Tyrion(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task<IResult<TResult>> Execute<TRequest, TResult>(TRequest request) where TRequest : IRequest
        {
            try
            {
                var instanceValidator = GetInstanceValidator(request);

                if (instanceValidator.Success)
                {
                    var validator = instanceValidator.Data.Validate<TRequest>(request);

                    if (!validator.IsValid)
                    {
                        return Result<TResult>.Failed(validator.ToString());
                    }
                }

                var service = _serviceProvider.GetRequiredService(typeof(IRequestHandler<TRequest, TResult>)) as IRequestHandler<TRequest, TResult>;

                if (service == null)
                {
                    return Result<TResult>.Failed($"{typeof(TRequest)?.Name} not found or not implemented!");
                }

                return await service.Execute(request);
            }
            catch (Exception ex)
            {
                return Result<TResult>.Failed(ex.Message);
            }
        }

        public async Task Execute<TRequest>(TRequest request) where TRequest : IRequest
        {
            try
            {
                var service = _serviceProvider.GetRequiredService(typeof(IRequestHandler<TRequest>)) as IRequestHandler<TRequest>;

                if (service == null)
                {
                    throw new ArgumentException($"{typeof(TRequest)?.Name} not found or not implemented!");
                }

                await service.Execute(request);
            }
            catch (Exception ex)
            {
                await Task.FromException(ex).ConfigureAwait(false);
            }
        }

        private static IResult<Validator<TRequest>> GetInstanceValidator<TRequest>(TRequest request) where TRequest : IRequest
        {
            var requestType = request.GetType();

            var requestValidatorType = requestType.Assembly
                .GetTypes()
                .FirstOrDefault(type =>
                    type.IsClass &&
                    type.Name.Contains(requestType.Name) &&
                    type.BaseType.Name.Equals(ValidatorTypeName)
                );

            if (requestValidatorType == null)
            {
                return Result<Validator<TRequest>>.Failed($"{requestType.Name} doesn't have rules implemented.");
            }

            var instance = Activator.CreateInstance(requestValidatorType) as Validator<TRequest>;

            return Result<Validator<TRequest>>.Successed(instance);
        }
    }
}
