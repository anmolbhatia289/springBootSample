
using Allen.Controllers;
using Allen.Models;

namespace AllenTest
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void TestProductCreation()
        {
            var productController = ProductController.getInstance();
            // inserting product model.
            productController.AddProduct("LG50TV", "LG 50 inch BRAVIA Television", 50000);
            var ProductFromController = productController.GetProduct("LG50TV");
            Assert.IsNotNull(ProductFromController);
        }

        [TestMethod]
        public void TestProductIfNotExists() 
        {
            var productController = ProductController.getInstance();
            Product ProductFromController = null;
            try
            {
                // Search for 32 inch television
                ProductFromController = productController.GetProduct("LG32TV");
            }
            catch(Exception ex) 
            { 
            }

            Assert.IsNull(ProductFromController);
        }
    }
}