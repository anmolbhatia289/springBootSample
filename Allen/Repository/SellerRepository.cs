using Allen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allen.Repository
{
    public class SellerRepository
    {
        public static SellerRepository Instance { get; set; }

        private Dictionary<string, Seller> SellerDatabase;

        private SellerRepository()
        {
            SellerDatabase = new Dictionary<string, Seller>();
        }

        public static SellerRepository getInstance()
        {
            if (Instance == null)
            {
                Instance = new SellerRepository();
            }

            return Instance;
        }

        public void AddSeller(Seller Seller)
        {
            SellerDatabase[Seller.phoneNumber] = Seller;
        }

        public Seller GetSeller(string phoneNumber)
        {
            if (SellerDatabase.ContainsKey(phoneNumber))
            {
                return SellerDatabase[phoneNumber];
            }
            else
            {
                return null;
            }
        }
    }
}
