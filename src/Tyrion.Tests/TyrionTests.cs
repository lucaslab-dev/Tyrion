using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tyrion.Tests
{
    [TestClass]
    public class Tests
    {
        private readonly ITyrion _tyrion;

        public Tests()
        {
            var services = new ServiceCollection();

            services.AddTyrion(typeof(Category));

            _tyrion = services.BuildServiceProvider().GetRequiredService<ITyrion>();
        }

        [TestMethod]
        public void TestMethodStuff()
        {
            var command = new CategoryCommand
            {
                Name = "stuff"
            };

            var result = _tyrion.Execute<CategoryCommand, Category>(command).Result;

            Assert.IsTrue(result.Successed);
        }

        [TestMethod]
        public void TestMethodSaveProduct()
        {
            var command = new SaveProductCommand();

            var result = _tyrion.Execute<SaveProductCommand, Product>(command).Result;

            Assert.IsTrue(result.Successed);
        }

        [TestMethod]
        public void TestMethodUpdateProduct()
        {
            var command = new UpdateProductCommand();

            var result = _tyrion.Execute<UpdateProductCommand, Product>(command).Result;

            Assert.IsTrue(result.Successed);
        }

        [TestMethod]
        public void TestMethodRemoveProduct()
        {
            var command = new RemoveProductCommand();

            var result = _tyrion.Execute(command).Result;

            Assert.IsTrue(result.Successed);
        }

        [TestMethod]
        public void TestMethodInativeProduct()
        {
            var command = new InativeProductCommand();

            var result = _tyrion.Execute(command).Result;

            Assert.IsTrue(result.Successed);
        }

        [TestMethod]
        public void TestMethodProductNotification()
        {
            _tyrion.Publish(new LastProductInStockEvent()).GetAwaiter();
            _tyrion.Publish(new ProductGiftEvent()).GetAwaiter();
            _tyrion.Publish(new ProductDiscountEmailEvent()).GetAwaiter();
        }
    }
}