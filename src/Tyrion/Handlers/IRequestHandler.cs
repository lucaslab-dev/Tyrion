using System.Threading.Tasks;
using Tyrion.Results;

namespace Tyrion.Handlers
{
    public interface IRequestHandler<in TRequest, TResult> where TRequest : IRequest
    {
        Task<IResult<TResult>> Execute(TRequest request);
    }

    public interface IRequestHandler<in TRequest> where TRequest : IRequest
    {
        Task Execute(TRequest request);
    }
}
