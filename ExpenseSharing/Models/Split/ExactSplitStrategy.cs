using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharing
{
    public class ExactSplitStrategy : SplitStrategy
    {
        public List<Split> getSplits(Expense expense, List<SplitView> splitViews)
        {
            var splits = new List<Split>();
            foreach (SplitView splitView in splitViews)
            {
                var exactSplitView = splitView as ExactSplitView;
                splits.Add(new Split(expense.paidBy, splitView.getPaidTo(), exactSplitView.getAmount()));
            }

            return splits;
        }
    }
}
