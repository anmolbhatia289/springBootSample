using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharing
{
    public class ExactSplitView : SplitView
    {
        private float amount { get; set; }
        public ExactSplitView(Guid paidTo) : base(paidTo)
        {
            this.amount = amount;
        }

        public float getAmount() 
        {
            return this.amount;
        }
    }
}
