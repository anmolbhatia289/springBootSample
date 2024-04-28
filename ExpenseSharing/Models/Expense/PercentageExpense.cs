using ExpenseSharing.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharing.Models
{
    public class PercentageExpense : Expense
    {
        public PercentageExpense(string name, string description, Guid paidBy, float amount)
        {
            this.id = Guid.NewGuid();
            this.name = name;
            this.description = description;
            this.types = ExpenseType.PERCENT;
            this.amount = amount;
            this.paidBy = paidBy;
            this.splitStrategy = new PercentageSplitStrategy();
        }

        public override void validateExpense(List<SplitView> splits)
        {
            float currentPercentage = 0;
            foreach (SplitView splitView in splits)
            {
                var exactSplit = splitView as PercentageSplitView;
                currentPercentage += exactSplit.getPercentage();
            }

            if (currentPercentage!= 100) throw new InvalidSplitException("Percentage do not add up to 100");
        }
    }
}
