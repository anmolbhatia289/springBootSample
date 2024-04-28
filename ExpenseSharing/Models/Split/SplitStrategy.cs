using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharing
{
    public interface SplitStrategy
    {
        public List<Split> getSplits(Expense expense, List<SplitView> splitViews);
    }
}
