using System.Threading.Tasks;

namespace Tyrion.Tests
{
    public sealed class CategoryCommandHandler : IRequestHandler<CategoryCommand, Category>
    {
        public async Task<IResult<Category>> Execute(CategoryCommand command)
        {
            return await Result<Category>.SuccessAsync(new Category());
        }
    }
}
