using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharing
{
    public class Split
    {
        public Guid id{ get; private set; }
        public Guid paidBy { get; private set; }
        public Guid paidTo { get; private set; }
        public float amount { get; private set; }
        public Split(Guid paidBy, Guid paidTo, float amount)
        {
            id = Guid.NewGuid();
            this.paidBy = paidBy;
            this.paidTo = paidTo;
            this.amount = amount;
        }
    }
}
