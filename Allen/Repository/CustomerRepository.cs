using Allen.Controllers;
using Allen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allen.Repository
{
    internal class CustomerRepository
    {
        public static CustomerRepository Instance { get; set; }

        private Dictionary<string, Customer> customerDatabase;

        private CustomerRepository()
        {
            customerDatabase = new Dictionary<string, Customer>();
        }

        public static CustomerRepository getInstance()
        {
            if (Instance == null)
            {
                Instance = new CustomerRepository();
            }

            return Instance;
        }

        public void AddCustomer(Customer customer)
        {
            customerDatabase[customer.phoneNumber]=customer;
        }

        public Customer GetCustomer(string phoneNumber) 
        {
            if (customerDatabase.ContainsKey(phoneNumber))
            {
                return customerDatabase[phoneNumber];
            }
            else
            {
                return null;
            }
        }
    }
}
