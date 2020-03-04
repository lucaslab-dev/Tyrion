using System.Threading.Tasks;
using Tyrion.Handlers;
using Tyrion.Results;
using Tyrion.Tests.Command;
using Tyrion.Tests.Entities;

namespace Tyrion.Tests.Handlers
{
    public sealed class TestCommandHandler : IRequestHandler<TestCommand, Test>,
                                             IRequestHandler<Test1Command, Test1>,
                                             IRequestHandler<Test2Command, Test2>,
                                             IRequestHandler<Test3Command>
    {
        public async Task<IResult<Test>> Execute(TestCommand request)
        {
            return await Task.FromResult(Result<Test>.Successed(new Test()));
        }

        public async Task<IResult<Test1>> Execute(Test1Command command)
        {
            return await Task.FromResult(Result<Test1>.Successed(new Test1()));
        }

        public async Task<IResult<Test2>> Execute(Test2Command request)
        {
            return await Task.FromResult(Result<Test2>.Successed(new Test2()));
        }

        public async Task Execute(Test3Command request)
        {
            await Task.CompletedTask;
        }
    }
}
