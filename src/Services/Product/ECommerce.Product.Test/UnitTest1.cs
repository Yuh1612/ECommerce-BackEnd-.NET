using ECommerce.Products.API.Services;
using ECommerce.Products.API.ViewModels.Products.Requests;

namespace ECommerce.Product.Test
{
    [TestClass]
    public class UnitTest1
    {
        private readonly ProductService productService;
        private readonly CategoryService categoryService;

        public UnitTest1()
        {
        }

        public UnitTest1(ProductService productService, CategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }

        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsNull(productService);
            Assert.IsNull(categoryService);
            Assert.IsTrue(1 == 1);
        }
    }
}