using System.Threading.Tasks;

namespace Tyrion
{
    public interface IRequestHandler<in TRequest, TResponse> where TRequest : IRequest
    {
        Task<IResult<TResponse>> Execute(TRequest request);
    }

    public interface IRequestHandler<in TRequest> where TRequest : IRequest
    {
        Task<IResult> Execute(TRequest request);
    }
}
