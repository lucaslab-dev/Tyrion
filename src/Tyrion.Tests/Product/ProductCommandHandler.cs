using System.Threading.Tasks;

namespace Tyrion.Tests
{
    public sealed class ProductCommandHandler : IRequestHandler<SaveProductCommand, Product>,
                                                IRequestHandler<UpdateProductCommand, Product>,
                                                IRequestHandler<RemoveProductCommand>,
                                                IRequestHandler<InativeProductCommand>
    {
        public async Task<IResult<Product>> Execute(SaveProductCommand request)
        {
            return await Result<Product>.SuccessAsync(new Product());
        }

        public async Task<IResult<Product>> Execute(UpdateProductCommand command)
        {
            return await Result<Product>.SuccessAsync(new Product());
        }

        public async Task<IResult> Execute(RemoveProductCommand request)
        {
            return await Result.SuccessAsync();
        }

        public async Task<IResult> Execute(InativeProductCommand request)
        {
            return await Result.SuccessAsync();
        }
    }
}
