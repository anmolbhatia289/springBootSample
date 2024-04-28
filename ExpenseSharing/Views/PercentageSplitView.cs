using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharing
{
    public class PercentageSplitView : SplitView
    {
        private Guid paidTo { get; set; }
        private float percentage { get; set; }
        public PercentageSplitView(Guid paidTo, float percentage) : base(paidTo)
        {
            this.percentage = percentage;
        }

        public float getPercentage() 
        {
            return this.percentage;
        }
    }
}
