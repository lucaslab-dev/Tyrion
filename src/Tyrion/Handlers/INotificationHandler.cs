using System.Threading.Tasks;

namespace Tyrion
{
    public interface INotificationHandler<in TRequest> where TRequest : INotification
    {
        Task Publish(TRequest request);
    }
}
