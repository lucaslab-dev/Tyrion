using Tyrion.Handlers;

namespace Tyrion.Tests
{
    public sealed class StuffCommand : IRequest 
    {
        public string MyProperty { get; set; }
    }
}