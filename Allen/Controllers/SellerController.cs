using Allen.Models;
using Allen.Repository;
using System.Net.Security;

namespace Allen.Controllers
{
    public class SellerController
    {
        public static SellerController Instance { get; set; }

        private SellerRepository sellerRepository { get; set; }

        private DealController dealController { get; set; }

        private ProductController productController { get; set; }

        private SellerController()
        {
            sellerRepository = SellerRepository.getInstance();
            dealController = DealController.getInstance();
            productController = ProductController.getInstance();
        }

        public static SellerController getInstance()
        {
            if (Instance == null)
            {
                Instance = new SellerController();
            }

            return Instance;
        }

        public void AddSeller(string phoneNumber, string name)
        {
            if (sellerRepository.GetSeller(phoneNumber) == null)
            {
                var Seller = new Seller(phoneNumber, name);
                sellerRepository.AddSeller(Seller);
            }
            else
            {
                throw new Exception("Seller already exists");
            }
        }

        public Seller GetSeller(string phoneNumber)
        {
            if (sellerRepository.GetSeller(phoneNumber) != null)
            {
                return sellerRepository.GetSeller(phoneNumber);
            }
            else
            {
                throw new Exception("Seller does not exists");
            }
        }

        public void AddProduct(string phoneNumber, string name, string description, float price)
        {
            if (sellerRepository.GetSeller(phoneNumber) != null)
            {
                var seller = sellerRepository.GetSeller(phoneNumber);
                productController.AddProduct(name, description, price);
            }
        }

        public void AddDeal(string phoneNumber, Product product, int minutesFromNow, int quantity, float discountPercentage)
        {
            if (sellerRepository.GetSeller(phoneNumber) != null)
            {
                var seller = sellerRepository.GetSeller(phoneNumber);
                dealController.AddDeal(product, minutesFromNow, quantity, discountPercentage);
            }
        }
    }
}
