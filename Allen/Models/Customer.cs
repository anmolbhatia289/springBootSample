using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allen.Models
{
    public class Customer
    {
        public Customer(string phoneNumber, string name)
        {
            this.phoneNumber = phoneNumber;
            this.Name = name;
        }

        public string phoneNumber { get; set; }
        public string Name { get; set; }
    }
}
