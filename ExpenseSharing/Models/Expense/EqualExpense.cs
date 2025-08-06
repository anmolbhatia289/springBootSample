using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharing.Models
{
    public class EqualExpense : Expense
    {
        public EqualExpense(string name, string description, Guid paidBy, float amount)
        {
            this.id = Guid.NewGuid();
            this.name = name;
            this.description = description;
            this.types = ExpenseType.EQUAL;
            this.paidBy = paidBy;
            this.amount = amount;
            this.splitStrategy = new EqualSplitStrategy();
        }

        public override void validateExpense(List<SplitView> splits) 
        {
        }
    }
}
