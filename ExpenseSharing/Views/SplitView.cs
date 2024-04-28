using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharing
{
    public abstract class SplitView
    {
        private Guid paidTo { get; set; }
        public SplitView(Guid paidTo)
        {
            this.paidTo = paidTo;
        }

        public Guid getPaidTo()
        {
            return this.paidTo;
        }
    }
}
