using Allen.Models;

namespace Allen.Repository
{
    public class ProductRepository
    {
        public static ProductRepository Instance { get; set; }

        private Dictionary<int, Product> ProductDatabase;

        private ProductRepository()
        {
            ProductDatabase = new Dictionary<int, Product>();
        }

        public static ProductRepository getInstance()
        {
            if (Instance == null)
            {
                Instance = new ProductRepository();
            }

            return Instance;
        }

        public void AddProduct(Product Product)
        {
            ProductDatabase[Product.Id] = Product;
        }

        public Product GetProductByName(string productName)
        {
            foreach (var product in ProductDatabase.Values) 
            {
                if (string.Equals(product.Name, productName))
                {
                    return product;
                }
            }

            return null;
        }

        public Product GetProductById(int productId)
        {
            if (ProductDatabase.ContainsKey(productId))
            {
                return ProductDatabase[productId];
            }
            else
            {
                return null;
            }
        }


        public int GetNextProductId()
        {
            return this.ProductDatabase.Count + 1;
        }
    }
}
