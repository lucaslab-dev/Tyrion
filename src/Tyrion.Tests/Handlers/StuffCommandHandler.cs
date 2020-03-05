using System.Threading.Tasks;
using Tyrion.Handlers;
using Tyrion.Results;
using Tyrion.Tests.Entities;

namespace Tyrion.Tests.Handlers
{
    public sealed class StuffCommandHandler : IRequestHandler<StuffCommand, Stuff>
    {
        public async Task<IResult<Stuff>> Execute(StuffCommand command)
        {
            return await Task.FromResult(Result<Stuff>.Successed(new Stuff())).ConfigureAwait(false);
        }
    }
}
