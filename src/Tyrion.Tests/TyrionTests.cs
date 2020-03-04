using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tyrion.Extensions;
using Tyrion.Tests.Command;
using Tyrion.Tests.Entities;

namespace Tyrion.Tests
{
    [TestClass]
    public class TyrionTests
    {
        private readonly ITyrion _tyrion;

        public TyrionTests()
        {
            var _services = new ServiceCollection();
            _services.AddTyrion(typeof(Stuff));
            _tyrion = new Tyrion(_services.BuildServiceProvider());
        }

        [TestMethod]
        public void TestMethod1()
        {
            var command = new StuffCommand();

            var result = _tyrion.Execute<StuffCommand, Stuff>(command).Result;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var command = new TestCommand();

            var result = _tyrion.Execute<TestCommand, Test>(command).Result;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var command = new Test1Command();

            var result = _tyrion.Execute<Test1Command, Test1>(command).Result;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestMethod4()
        {
            var command = new Test2Command();

            var result = _tyrion.Execute<Test2Command, Test2>(command).Result;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestMethod5()
        {
            var command = new Test3Command();

            var task = _tyrion.Execute(command);

            Assert.IsNotNull(task);
        }
    }
}
