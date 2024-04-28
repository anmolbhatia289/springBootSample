using Allen.Models;
using Allen.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Allen.Controllers
{
    public class CustomerController
    {
        public static CustomerController Instance { get; set; }

        private CustomerRepository customerRepository { get; set; }

        private DealController dealController { get; set; }

        private CustomerController() 
        {
            customerRepository = CustomerRepository.getInstance();
            dealController = DealController.getInstance();
        }

        public static CustomerController getInstance()
        {
            if (Instance == null) 
            {
                Instance = new CustomerController();
            }

            return Instance;
        }

        public void AddCustomer(string phoneNumber, string name)
        {
            if (customerRepository.GetCustomer(phoneNumber) == null) 
            {
                var customer = new Customer(phoneNumber, name);
                customerRepository.AddCustomer(customer);
            }
            else
            {
                throw new Exception("Customer already exists");
            }
        }

        public Customer GetCustomer(string phoneNumber)
        {
            if (customerRepository.GetCustomer(phoneNumber) != null)
            {
                return customerRepository.GetCustomer(phoneNumber);
            }
            else
            {
                throw new Exception("Customer does not exists");
            }
        }

        public void ClaimDeal(string phoneNumber, Deal deal)
        {
            if (customerRepository.GetCustomer(phoneNumber) != null)
            {
                var customer = customerRepository.GetCustomer(phoneNumber);
                dealController.ClaimDeal(deal);
            }
        }

        public List<Deal> GetDeals(string phoneNumber, string productName)
        {
            if (customerRepository.GetCustomer(phoneNumber) != null)
            {
                var customer = customerRepository.GetCustomer(phoneNumber);
                return dealController.GetDeals(productName);
            }

            return null;
        }
    }
}
