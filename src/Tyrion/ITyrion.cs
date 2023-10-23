using System.Threading.Tasks;

namespace Tyrion
{
    public interface ITyrion
    {
        /// <summary>
        /// Returns an IResult of TResult where request must be an IRequest.
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IResult<TResponse>> Execute<TRequest, TResponse>(TRequest request) where TRequest : IRequest;

        /// <summary>
        /// Returns a Task. For use when don't require a return.
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IResult> Execute<TRequest>(TRequest request) where TRequest : IRequest;

        /// <summary>
        /// Publish a notification for all handlers subscribed.
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="notification"></param>
        /// <returns></returns>
        Task Publish<TRequest>(TRequest notification) where TRequest : INotification;
    }
}
