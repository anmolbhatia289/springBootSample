
using Allen.Controllers;
using Allen.Models;

// tooling - c#, .net, service bus (kafka), cosmos db (NoSQL), SQL database
// flask, react, node js

namespace AllenTest
{
    [TestClass]
    public class CustomerTests
    {
        [TestMethod]
        public void TestCustomerCreation()
        {
            var customerController = CustomerController.getInstance();
            customerController.AddCustomer("9990591541", "Anmol");
            var customerFromController = customerController.GetCustomer("9990591541");
            Assert.IsNotNull(customerFromController);
        }

        [TestMethod]
        public void TestCustomerIfNotExists() 
        {
            var customerController = CustomerController.getInstance();
            Customer customerFromController = null;
            try
            {
                customerFromController = customerController.GetCustomer("8787514875");
            }
            catch(Exception ex) 
            { 
            }

            Assert.IsNull(customerFromController);
        }
    }
}