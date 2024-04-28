using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allen.Models
{
    public class Deal
    {
        public Deal(int id, DateTime endTime, Product product, int quantity, float discountPercentage)
        {
            Id = id;
            this.endTime = endTime;
            Product = product;
            Quantity = quantity;
            this.discountPercentage = discountPercentage;
            this.UsedQuantity = 0;
        }

        public int Id { get; set; }

        public DateTime endTime { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        public float discountPercentage { get; set; }

        public int UsedQuantity { get; set; }
    }
}
