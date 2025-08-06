using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharing
{
    public class PercentageSplitStrategy : SplitStrategy
    {
        public List<Split> getSplits(Expense expense, List<SplitView> splitViews)
        {
            var splits = new List<Split>();
            foreach (SplitView splitView in splitViews)
            {
                var percentageSplitView = splitView as PercentageSplitView;
                splits.Add(new Split(expense.paidBy, splitView.getPaidTo(), (percentageSplitView.getPercentage() * expense.amount ) /100));
            }

            return splits;
        }
    }
}
