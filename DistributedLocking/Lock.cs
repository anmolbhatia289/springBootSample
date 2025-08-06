using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedLocking
{
    public class Lock
    {
        public Guid lockId { get; set; }
        public string lockName { get; set; }
        public Guid ownerNode { get; set; }
        public DateTime lockTime { get; set; }
        public long lockDuration { get; set; }
        public bool IsAcquired { get; set; }

        public Lock(string lockName, long milliSeconds)
        {
            this.lockId = Guid.NewGuid();
            this.lockName = lockName;
            this.lockDuration = milliSeconds;
            this.IsAcquired = false;
        }
    }
}
