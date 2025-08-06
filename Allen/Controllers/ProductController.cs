using Allen.Models;
using Allen.Repository;

namespace Allen.Controllers
{
    public class ProductController
    {
        public static ProductController Instance { get; set; }

        private ProductRepository ProductRepository { get; set; }

        private ProductController()
        {
            ProductRepository = ProductRepository.getInstance();
        }

        public static ProductController getInstance()
        {
            if (Instance == null)
            {
                Instance = new ProductController();
            }

            return Instance;
        }

        public void AddProduct(string name, string description, float price)
        {
            if (ProductRepository.GetProductByName(name) == null)
            {
                int id = ProductRepository.GetNextProductId();
                var Product = new Product(id, name, description, price);
                ProductRepository.AddProduct(Product);
            }
            else
            {
                throw new Exception("Product already exists");
            }
        }

        public Product GetProduct(string name)
        {
            var productByName = ProductRepository.GetProductByName(name);
            if (productByName == null)
            {
                throw new Exception($"Product by name {name} does not exists");
            }

            return productByName;
        }

    }
}
