using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharing
{
    public class User
    {
        public Guid id { get; private set; }
        public string name { get; private set; }
        public string emailAddress { get; private set; }
        public string password { get; private set; }
        public string phoneNumber { get; private set; }

        public User(long id, string name, string emailAddress, string password, string phoneNumber) 
        {
            this.id = Guid.NewGuid();
            this.name = name;
            this.emailAddress = emailAddress;
            this.password = password;
            this.phoneNumber = phoneNumber;
        }

    }
}
