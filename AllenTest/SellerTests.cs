
using Allen.Controllers;
using Allen.Models;

namespace AllenTest
{
    [TestClass]
    public class SellerTests
    {
        [TestMethod]
        public void TestSellerCreation()
        {
            var sellerController = SellerController.getInstance();
            sellerController.AddSeller("9990591541", "Anmol");
            var SellerFromController = sellerController.GetSeller("9990591541");
            Assert.IsNotNull(SellerFromController);
        }

        [TestMethod]
        public void TestSellerIfNotExists() 
        {
            var sellerController = SellerController.getInstance();
            Seller SellerFromController = null;
            try
            {
                SellerFromController = sellerController.GetSeller("8787514875");
            }
            catch(Exception ex) 
            { 
            }

            Assert.IsNull(SellerFromController);
        }
    }
}