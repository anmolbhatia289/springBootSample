using System;
using System.Collections.Generic;

namespace PaymentGateway
{
    using System;
    using System.Collections.Generic;

    // Payment Strategy interface
    interface IPaymentStrategy
    {
        string ProcessPayment(Dictionary<string, string> details, BankProxy bank);
    }

    // Concrete payment strategy classes
    class UPIPaymentStrategy : IPaymentStrategy
    {
        public string ProcessPayment(Dictionary<string, string> details, BankProxy bank)
        {
            return $"Processing UPI payment with VPA: {details["vpa"]}";
        }
    }

    class CardPaymentStrategy : IPaymentStrategy
    {
        public string ProcessPayment(Dictionary<string, string> details, BankProxy bank)
        {
            return "Processing Card payment";
        }
    }

    class NetbankingPaymentStrategy : IPaymentStrategy
    {
        public string ProcessPayment(Dictionary<string, string> details, BankProxy bank)
        {
            return "Processing Netbanking payment";
        }
    }

    // Payment Method Factory using Strategy Pattern
    class PaymentMethodFactory
    {
        public static IPaymentStrategy CreatePaymentStrategy(string method)
        {
            return method switch
            {
                "UPI" => new UPIPaymentStrategy(),
                "Credit/Debit Card" => new CardPaymentStrategy(),
                "Netbanking" => new NetbankingPaymentStrategy(),
                _ => null,
            };
        }
    }

    // Bank interface
    interface IBank
    {
        string ProcessPayment(double amount, Dictionary<string, string> details);
    }

    // Concrete bank classes
    class HDFCBank : IBank
    {
        public string ProcessPayment(double amount, Dictionary<string, string> details)
        {
            return $"Payment processed by HDFC Bank: {amount}";
        }
    }

    class ICICIBank : IBank
    {
        public string ProcessPayment(double amount, Dictionary<string, string> details)
        {
            return $"Payment processed by ICICI Bank: {amount}";
        }
    }

    // Bank proxy class
    class BankProxy : IBank
    {
        private IBank _realBank;

        public BankProxy(IBank realBank)
        {
            _realBank = realBank;
        }

        public string ProcessPayment(double amount, Dictionary<string, string> details)
        {
            Console.WriteLine("Proxy: Performing pre-processing tasks...");
            return _realBank.ProcessPayment(amount, details);
        }
    }

    class Client
    {
        public string name;

        private Dictionary<IPaymentStrategy, string> paymentTypeToBankMap = new Dictionary<IPaymentStrategy, string>(); // Stores bank proxies
        public void AddBankForPaymentType(IPaymentStrategy paymentType, string bank)
        {
            paymentTypeToBankMap[paymentType] = bank;
        }

        public string GetBankProxyForPaymentType(IPaymentStrategy paymentType)
        {
            return paymentTypeToBankMap[paymentType];
        }
    }
    // Singleton Payment Gateway using Proxy Pattern and Strategy Pattern
    class PaymentGateway
    {
        private static PaymentGateway _instance;
        private Dictionary<string, Client> clients = new Dictionary<string, Client>();
        private Dictionary<string, IPaymentStrategy> paymentStrategies = new Dictionary<string, IPaymentStrategy>();
        private Dictionary<string, BankProxy> banks = new Dictionary<string, BankProxy>(); // Stores real bank objects

        private PaymentGateway() { }

        public static PaymentGateway Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PaymentGateway();
                }
                return _instance;
            }
        }

        public void AddClient(Client client)
        {
            clients[client.name] = client;
        }

        public void RemoveClient(string client)
        {
            clients.Remove(client);
        }

        public bool HasClient(string client)
        {
            return clients.ContainsKey(client);
        }

        public HashSet<string> ListSupportedPaymodes(string client = null)
        {
            return client != null ? clients[client] : new HashSet<string>(paymentStrategies.Keys);
        }

        public void AddSupportForPaymode(string client, string paymode)
        {
            if (!paymentStrategies.ContainsKey(paymode))
            {
                paymentStrategies[paymode] = PaymentMethodFactory.CreatePaymentStrategy(paymode);
            }
            clients[client].Add(paymode);
        }

        public void RemovePaymode(string client, string paymode)
        {
            clients[client].Remove(paymode);
        }

        public void AddBank(string bankName, IBank bank)
        {
            banks[bankName] = new BankProxy(bank); // Wrap the real bank object with a proxy
        }

        public string MakePayment(string client, double amount, string paymode, Dictionary<string, string> details)
        {
            if (!clients.ContainsKey(client))
            {
                return "Client not found";
            }

            if (!clients[client].Contains(paymode))
            {
                return $"{paymode} not supported for {client}";
            }

            if (paymentStrategies.ContainsKey(paymode))
            {
                IPaymentStrategy paymentStrategy = paymentStrategies[paymode];
                return paymentStrategy.ProcessPayment(details, banks["HDFC"]);
            }
            else
            {
                return "Invalid payment method";
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var pg = PaymentGateway.Instance;

            // Add clients
            pg.AddClient("Flipkart");
            pg.AddClient("Amazon");

            // Add supported payment modes for clients
            pg.AddSupportForPaymode("Flipkart", "UPI");
            pg.AddSupportForPaymode("Flipkart", "Netbanking");
            pg.AddSupportForPaymode("Amazon", "Credit/Debit Card");
            pg.AddSupportForPaymode("Amazon", "Netbanking");

            // Add banks
            pg.AddBank("HDFC", new HDFCBank());
            pg.AddBank("ICICI", new ICICIBank());

            // Make payments
            Console.WriteLine(pg.MakePayment("Flipkart", 1000, "Netbanking", new Dictionary<string, string> { { "username", "user123" }, { "password", "pass123" } }));
            Console.WriteLine(pg.MakePayment("Amazon", 500, "Credit/Debit Card", new Dictionary<string, string> { { "card_number", "1234567890123456" }, { "expiry", "12/25" }, { "cvv", "123" } }));
        }
    }

}

