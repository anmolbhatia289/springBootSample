
using Allen.Controllers;
using Allen.Models;

namespace AllenTest
{
    [TestClass]
    public class DealTests
    {
        [TestMethod]
        public void TestDealCreation()
        {
            var dealController = DealController.getInstance();
            var productController = ProductController.getInstance();
            // inserting product model.
            productController.AddProduct("LG50TV", "LG 50 inch BRAVIA Television", 50000);
            var productFromController = productController.GetProduct("LG50TV");
            dealController.AddDeal(productFromController,120, 2,10);
            var deals = dealController.GetDeals("LG50TV");
            Assert.IsNotNull(deals.FirstOrDefault());
            dealController.ClaimDeal(deals.FirstOrDefault());
        }

        [TestMethod]
        public void TestDealExpiry()
        {
            var dealController = DealController.getInstance();
            var productController = ProductController.getInstance();
            // inserting product model.
            productController.AddProduct("LG52TV", "LG 52 inch BRAVIA Television", 60000);
            var productFromController = productController.GetProduct("LG52TV");
            dealController.AddDeal(productFromController, 1, 2, 10);
            Thread.Sleep(TimeSpan.FromMinutes(2));
            var deals = dealController.GetDeals("LG52TV");
            Assert.IsNotNull(deals.FirstOrDefault());
            try
            {
                dealController.ClaimDeal(deals.FirstOrDefault());
            }
            catch (Exception)
            {

            }
        }

        [TestMethod]
        public void TestDealCreationE2E()
        {
            var dealController = DealController.getInstance();
            var productController = ProductController.getInstance();


            var sellerController = SellerController.getInstance();
            sellerController.AddSeller("9990591541", "Anmol");
            var SellerFromController = sellerController.GetSeller("9990591541");

            sellerController.AddProduct("9990591541", "LG50TV", "LG 50 inch BRAVIA Television", 50000);
            var productFromController = productController.GetProduct("LG50TV");

            Assert.IsNotNull(productFromController);
            sellerController.AddDeal("9990591541", productFromController, 1, 2, 10);

            var deals = dealController.GetDeals("LG50TV");
            Assert.IsNotNull(deals.FirstOrDefault());

            var customerController = CustomerController.getInstance();
        }
    }
}