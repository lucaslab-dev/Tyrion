using System.Threading.Tasks;

namespace Tyrion.Tests
{
    public sealed class ProductNotificationHandler : INotificationHandler<LastProductInStockEvent>,
                                                     INotificationHandler<ProductGiftEvent>,
                                                     INotificationHandler<ProductDiscountEmailEvent>
    {
        public async Task Publish(LastProductInStockEvent request)
        {
            await Task.CompletedTask.ConfigureAwait(false);
        }

        public async Task Publish(ProductGiftEvent request)
        {
            await Task.CompletedTask.ConfigureAwait(false);
        }

        public async Task Publish(ProductDiscountEmailEvent request)
        {
            await Task.CompletedTask.ConfigureAwait(false);
        }
    }
}
