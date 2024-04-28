using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharing
{
    public class EqualSplitStrategy : SplitStrategy
    {
        public List<Split> getSplits(Expense expense, List<SplitView> splitViews)
        {
            var splits = new List<Split>();
            foreach (SplitView splitView in splitViews) 
            {
                splits.Add(new Split(expense.paidBy, splitView.getPaidTo(), expense.amount / splitViews.Count));
            }

            return splits;
        }
    }
}
