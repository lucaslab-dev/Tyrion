using System.Threading.Tasks;
using Tyrion.Handlers;
using Tyrion.Results;

namespace Tyrion
{
    public interface ITyrion
    {
        /// <summary>
        /// Returns an IResult of TResult where request must be an IRequest.
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IResult<TResult>> Execute<TRequest, TResult>(TRequest request) where TRequest : IRequest;

        /// <summary>
        /// Returns a Task. For use when don't require a return.
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        Task Execute<TRequest>(TRequest request) where TRequest : IRequest;
    }
}
