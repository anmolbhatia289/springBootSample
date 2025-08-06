using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedLocking
{
    public class Node
    {
        public Guid NodeId { get; set; }
        public string NodeName { get; set; }
        public string NodeAddress { get; set; }
        
        public Node(string nodeName, string nodeAddress)
        {
            NodeId = Guid.NewGuid();
            NodeName = nodeName;
            NodeAddress = nodeAddress;
        }
    }
}
