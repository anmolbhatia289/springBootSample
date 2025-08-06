using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Jira
{
    public class Sprint
    {
        private string name;
        private DateTime createdAt;
        private long createdBy;
        private bool isActive;

        public Sprint(string name, DateTime createdAt, long userId) 
        { 
            this.name = name;
            this.createdAt = createdAt;
            this.createdBy = userId;
            this.isActive = true;
        }
    }
}
